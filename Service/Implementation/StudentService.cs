using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Domain;
using Microsoft.AspNetCore.JsonPatch;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class StudentService : IStudentService
    {
        private static IAmazonDynamoDB _client;
        public StudentService(IAmazonDynamoDB client)
        {
            _client = client;
        }
        public async Task AddStudentRecord(Student student)
        {
            //Set a local DB context  
            var context = new DynamoDBContext(_client);

            await CheckAndCreateTable_async("Student");
            AsyncSearch<Student> result = context.ScanAsync<Student>(Enumerable.Empty<ScanCondition>());
            List<Student> usersFound = await result.GetRemainingAsync();
            if (usersFound.LongCount() > 0)
            {
                Student lastRegistered = usersFound.OrderBy(dr => dr.Id).Last();
                if (lastRegistered != null)
                    student.Id = lastRegistered.Id + 1;
                else
                    student.Id++;
            }
            else
                student.Id++;
            //Save an Student object  
            await context.SaveAsync<Student>(student);
        }
        public async Task UpdateStudentRecord(int id,JsonPatchDocument<Student> studentPatch)
        {
            try
            {
                //Set a local DB context  
                var context = new DynamoDBContext(_client);

                // Define item key
                //  Hash-key of the target item is string value "Mark Twain"
                //  Range-key of the target item is string value "The Adventures of Tom Sawyer"
                Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
{
    { "Id", new AttributeValue { S = id.ToString() } }
};

                // Define attribute updates
                Dictionary<string, AttributeValueUpdate> updates = new Dictionary<string, AttributeValueUpdate>();

                PropertyInfo[] properties = typeof(Student).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    updates[property.Name] = new AttributeValueUpdate()
                    {
                        Action = AttributeAction.ADD,
                        Value = new AttributeValue { S = property.GetValue(student).ToString() }
                    };
                }
                // Create UpdateItem request
                UpdateItemRequest request = new UpdateItemRequest
                {
                    TableName = "Student",
                    Key = key,
                    AttributeUpdates = updates
                };

                // Issue request
                await _client.UpdateItemAsync(request);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task DeleteStudentRecord(string id)
        {
            const string tableName = "Student";
            var request = new DeleteItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() {
                        {
                            "id",
                            new AttributeValue {
                                S = id
                            }
                        }
                    }
            };
            var response = await _client.DeleteItemAsync(request);
        }
        public async Task<Student> GetStudentSingleRecord(string id)
        {
            try
            {
                var context = new DynamoDBContext(_client);
                //Getting an Student object  
                List<ScanCondition> conditions = new List<ScanCondition>();
                conditions.Add(new ScanCondition("Id", ScanOperator.Equal, id));
                return await context.LoadAsync<Student>(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<Student>> GetStudentRecords()
        {
            var context = new DynamoDBContext(_client);
            //Getting an Student object  
            List<ScanCondition> conditions = new List<ScanCondition>();
            conditions.Add(new ScanCondition("Id", ScanOperator.IsNotNull));
            var allDocs = await context.ScanAsync<Student>(conditions).GetRemainingAsync();
            return allDocs;
        }
        async Task CheckAndCreateTable_async(string new_table_name)
        {
            Console.WriteLine("  -- Creating a new table named {0}...", new_table_name);
            if (await checkingTableExistence_async(new_table_name))
            {
                Console.WriteLine("     -- No need to create a new table...");
                return;
            }

            Task<CreateTableResponse> newTbl = CreateNewTable_async(new_table_name);
            await newTbl;
        }
        async Task<bool> checkingTableExistence_async(string tblNm)
        {
            DescribeTableResponse descResponse;

            ListTablesResponse tblResponse = await _client.ListTablesAsync();
            if (tblResponse.TableNames.Contains(tblNm))
            {
                Console.WriteLine("     A table named {0} already exists in DynamoDB!", tblNm);

                // If the table exists, get its description
                try
                {
                    descResponse = await _client.DescribeTableAsync(tblNm);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("     However, its description is not available ({0})", ex.Message);
                    return (true);
                }
                return (true);
            }
            return (false);
        }
        async Task<CreateTableResponse> CreateNewTable_async(string tblNm)
        {
            var createTableRequest = new CreateTableRequest
            {
                TableName = tblNm,
                AttributeDefinitions = new List<AttributeDefinition>(),
                KeySchema = new List<KeySchemaElement>(),
                GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>(),
                LocalSecondaryIndexes = new List<LocalSecondaryIndex>(),
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                }
            };
            createTableRequest.KeySchema = new[]
            {
        new KeySchemaElement
        {
            AttributeName = "Id",
            KeyType = KeyType.HASH,
        },
        new KeySchemaElement
        {
            AttributeName = "FirstName",
            KeyType = KeyType.HASH,
        },
        new KeySchemaElement
        {
            AttributeName = "LastName",
            KeyType = KeyType.HASH,
        },
        new KeySchemaElement
        {
            AttributeName = "Phone",
            KeyType = KeyType.HASH,
        },
        new KeySchemaElement
        {
            AttributeName = "Gender",
            KeyType = KeyType.HASH,
        },
        new KeySchemaElement
        {
            AttributeName = "Id",
            KeyType = KeyType.HASH,
        },

    }.ToList();

            createTableRequest.AttributeDefinitions = new[]
            {
        new AttributeDefinition
        {
            AttributeName = "Id",
            AttributeType = ScalarAttributeType.N,
        }
    }.ToList();

            return await _client.CreateTableAsync(createTableRequest);
        }
    }
}


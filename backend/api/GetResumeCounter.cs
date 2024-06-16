// //using System;
// using System.Net.Http;
// using System.IO;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Azure.WebJobs;
// using Microsoft.Azure.WebJobs.Extensions.Http;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Logging;
// using Newtonsoft.Json;
// using Microsoft.Azure.Functions.Worker;
// using Microsoft.Azure.Cosmos;
// //using Newtonsoft.Json;
// using System.Text;
// using System.Configuration;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
     public static class GetResumeCounter
    {
        [FunctionName("GetResumeCounter")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(databaseName: "azureresume", containerName: "Counter", Connection = "AzureResumeAnujConnectionString", Id = "1", PartitionKey = "1")] Counter counter,
            [CosmosDB(databaseName: "azureresume", containerName: "Counter", Connection = "AzureResumeAnujConnectionString")] IAsyncCollector<Counter> outputCollector,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (counter == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Counter not found", Encoding.UTF8, "application/json")
                };
            }

            counter.Count += 1;

            await outputCollector.AddAsync(counter);

            var jsonToReturn = JsonConvert.SerializeObject(counter);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }

    /*public static class GetResumeCounter
    {
        [FunctionName("GetResumeCounter")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            //[CosmosDBInput(databaseName:"azureresumeanuj", containerName: "Counter", Connection = "AzureResumeAnujConnectionString", Id = "1", PartitionKey = "1")] Counter counter,
            [CosmosDBInput(databaseName: "azureresumeanuj", containerName: "Counter", Connection = "AzureResumeAnujConnectionString", Id = "1", PartitionKey = "1")] Counter counter,
            [CosmosDB(databaseName: "azureresumeanuj", containerName: "Counter", Connection = "AzureResumeAnujConnectionString")] IAsyncCollector<Counter> outputCollector,
            //[CosmosDBOutput(databaseName:"azureresumeanuj", containerName: "Counter", Connection = "AzureResumeAnujConnectionString", CreateIfNotExists = true)] out Counter updatedCounter,
            [CosmosDBOutput(databaseName: "azureresumeanuj", containerName: "Counter", Connection = "AzureResumeAnujConnectionString", CreateIfNotExists = true)] Counter updatedCounter,

            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            updatedCounter = counter;
            updatedCounter.Count +=1;

            var jsonToReturn = JsonConvert.SerializeObject(counter);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };

            // string name = req.Query["name"];

            // string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            // dynamic data = JsonConvert.DeserializeObject(requestBody);
            // name = name ?? data?.name;

            // string responseMessage = string.IsNullOrEmpty(name)
            //     ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //     : $"Hello, {name}. This HTTP triggered function executed successfully.";

            //return new OkObjectResult(responseMessage);
        }
    }*/

}

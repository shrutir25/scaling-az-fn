#r "Microsoft.WindowsAzure.Storage"

using System;
using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

public static async Task<HttpResponseMessage> Run(
    HttpRequestMessage req, IAsyncCollector<TableEntity> instanceIds, TraceWriter log)
{
    var instanceId = 
        Environment.GetEnvironmentVariable(
            "WEBSITE_INSTANCE_ID", 
            EnvironmentVariableTarget.Process);
    await instanceIds.AddAsync(new TableEntity(Guid.NewGuid().ToString(), instanceId));
    return req.CreateResponse(HttpStatusCode.OK, instanceId);
}

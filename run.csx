#r "Microsoft.WindowsAzure.Storage"

using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

public static async Task<HttpResponseMessage> Run(
    HttpRequestMessage req, IAsyncCollector<TableEntity> instanceIds, IAsyncCollector<TableEntity> processIds, TraceWriter log)
{
    Process currentProcess = Process.GetCurrentProcess();
    Console.WriteLine("Running process Id = " + currentProcess.Id);
    
    var instanceId = 
        Environment.GetEnvironmentVariable(
            "WEBSITE_INSTANCE_ID", 
            EnvironmentVariableTarget.Process);

    await instanceIds.AddAsync(new TableEntity(Guid.NewGuid().ToString(), instanceId.ToString()));
    await processIds.AddAsync(new TableEntity(Guid.NewGuid().ToString(), currentProcess.Id.ToString()));
    return req.CreateResponse(HttpStatusCode.OK, instanceId);
}

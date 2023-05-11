using Contenda.Sdk;
using Contenda.Sdk.Auth;
using Contenda.Sdk.Models;

var authProvider = new APIKeyAuthProvider("YOUR EMAIL HERE", "YOUR API KEY HERE");
var api = new ContendaAPI(authProvider);

var isApiHealthy = await api.ServiceHealth();

Console.WriteLine($"Is api healthy: {isApiHealthy}");

var authSuccess = await api.Authenticate();

Console.WriteLine($"Successfully authenticated: {authSuccess}");

var usageLimits = await api.GetUsageLimits();

Console.WriteLine(usageLimits!.FriendlyMessage);

var newJob = await api.SubmitVideoToBlogJob("youtube dQw4w9WgXcQ", VideoToBlogJobSubType.Presentation);

var jobId = newJob!.JobId;

Console.WriteLine($"Successfully submitted job: {jobId}");

var jobStatus = await api.GetJobStatus(jobId!);

while (jobStatus!.Status != "succeeded" && jobStatus.Status != "failed")
{
    // poll for job status

    jobStatus = await api.GetJobStatus(jobId!);

    Console.WriteLine("Waiting for job to complete...");

    await Task.Delay(10*1000);
}

Console.WriteLine($"{jobStatus.JobId} {jobStatus.Message}");

if (jobStatus.Status == "failed")
{
    Console.WriteLine("Job failed!");
    return;
}

var blogId = jobStatus.Result!.ResultDocumentId;

Console.WriteLine(await api.GetBlogAsMarkdown(blogId!));

Console.ReadKey();
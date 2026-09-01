using System.Net;

namespace backend.Tests;

public class JobEntriesControllerTests : IClassFixture<JobTrackerWebApplicationFactory>
{
    private readonly JobTrackerWebApplicationFactory _factory;
    private readonly HttpClient _client;
    
    public JobEntriesControllerTests(JobTrackerWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _factory = factory;
    }

    [Fact]
    public async Task GetJobEntries_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/JobEntries");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
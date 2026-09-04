using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using backend.Models.DTOs;

namespace backend.Tests;

public class JobEntriesControllerTests : IClassFixture<JobTrackerWebApplicationFactory>, IAsyncLifetime
{
    private readonly JobTrackerWebApplicationFactory _factory;
    private readonly HttpClient _client;



    public JobEntriesControllerTests(JobTrackerWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _factory = factory;
    }
    
    public async Task InitializeAsync()
    {
        await _factory.ResetDatabaseAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async Task GetJobEntries_ReturnsOk()
    {
        await RegisterAndAuthenticateAsync();
        
        var response = await _client.GetAsync("/api/JobEntries");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostJobEntry_ReturnsCreated()
    {
        await RegisterAndAuthenticateAsync();
        var newJobEntry = new CreateJobEntryDto
        {
            CompanyName = "Test Company",
            JobTitle = "Test Job Title",
        };
        
        var response = await _client.PostAsJsonAsync("/api/JobEntries", newJobEntry);
        var returnedResponse = await response.Content.ReadFromJsonAsync<JobEntryResponseDto>();
        
        Assert.NotEqual(Guid.Empty, returnedResponse!.JobEntryId);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
    
    
    private async Task<AuthResponseDto> RegisterAndAuthenticateAsync()
    {
        var registerUser = new RegisterUserDto
        {
            Email = $"{Guid.NewGuid()}@test.ca",
            Password = "test1234"
        };
        
        var registerResponse = await _client.PostAsJsonAsync("/api/Auth/register", registerUser);
        var response = await registerResponse.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (response is null) throw new InvalidOperationException("Register did not return response body");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.AccessToken);
        
        return response;
    }
}
using BlazorApp1.Models;
using Microsoft.AspNetCore.Components;
namespace BlazorApp1.Components.Pages;

    public partial class Home
{

    [Inject]
    private IHttpClientFactory HttpClientFactory { get; set; } = default!;
    private HttpClient httpClient;
    List<Users> users = new List<Users>();

    protected override async Task OnInitializedAsync()
    {
        httpClient = HttpClientFactory.CreateClient("UserApi");
        await GetUsers();
    }
    private async Task GetUsers()
    {
        try
        {
            var result = await httpClient.GetAsync("/User/GetUsers");
            if (result.IsSuccessStatusCode)
            {
                users = await result.Content.ReadFromJsonAsync<List<Users>>();
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Ocurrio un error: {ex.Message}");
        }
    }
}

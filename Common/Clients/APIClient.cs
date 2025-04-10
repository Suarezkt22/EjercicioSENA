using PruebaTecnicaAmaris.Common.Exceptions;
using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;

namespace PruebaTecnicaAmaris.Common.Clients;

public class APIClient
{
    public static async Task<TResponse?> ExecuteAsync<TResponse>(
        string baseUrl,
        string endpoint,
        Method method
    )
    {
        return await ExecuteAsync<object, TResponse>(baseUrl, endpoint, method, null);
    }

    public static async Task<TResponse?> ExecuteAsync<Tbody, TResponse>(
        string baseUrl,
        string endpoint,
        Method method,
        Tbody? body = default
    )
    {
        try
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(endpoint, method);

            request.AddHeader("Accept", "application/json");

            if (body is not null)
            {
                request.AddBody(body);
            }

            var result = await client.ExecuteAsync(request);

            if (!result.IsSuccessful)
            {
                throw new InternalException();
            }

            if (string.IsNullOrEmpty(result.Content))
            {
                return default;
            }

            return JsonSerializer.Deserialize<TResponse>(result.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw new InternalException();
        }
    }
}

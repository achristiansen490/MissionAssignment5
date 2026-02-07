using System.Text.Json;

namespace Mission5Assignment.Services
{
    // This service is responsible for calling the external Smash Bros API
    // and returning character data to the controller.
    public class SmashBrosApiService
    {
        // HttpClient is used to make HTTP requests to the API
        // It is injected by ASP.NET through dependency injection
        private readonly HttpClient _http;

        // Constructor that receives the HttpClient
        // ASP.NET automatically provides this when the service is registered
        public SmashBrosApiService(HttpClient http)
        {
            _http = http;
        }

        // This method calls the Smash Bros API and returns a list of character names
        public async Task<List<string>> GetUltimateCharactersAsync()
        {
            // API endpoint that returns all Smash Ultimate characters
            var url = "https://smashbrosapi.com/api/v1/ultimate/characters";

            // Send a GET request to the API and receive the response as a stream
            using var stream = await _http.GetStreamAsync(url);

            // Parse the JSON response into a JsonDocument
            using var doc = await JsonDocument.ParseAsync(stream);

            // Create a list to store character names
            var results = new List<string>();

            // We assume the root of the JSON is an array of character objects
            if (doc.RootElement.ValueKind == JsonValueKind.Array)
            {
                // Loop through each item in the array
                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    // Skip the item if it is not a JSON object
                    if (item.ValueKind != JsonValueKind.Object) continue;

                    // Try to read the "name" property from the object
                    if (item.TryGetProperty("name", out var n) && n.ValueKind == JsonValueKind.String)
                        results.Add(n.GetString()!);

                    // Some APIs may use capitalized property names
                    // This checks for "Name" as a fallback
                    else if (item.TryGetProperty("Name", out var N) && N.ValueKind == JsonValueKind.String)
                        results.Add(N.GetString()!);
                }
            }

            // Sort the character names alphabetically
            results.Sort();

            // Return the final list to the controller
            return results;
        }
    }
}

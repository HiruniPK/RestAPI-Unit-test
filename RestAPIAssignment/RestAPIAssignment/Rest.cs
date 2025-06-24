using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestAPIAssignment
{
    public class Rest
    {
        private readonly HttpClient _client = new()
        {
            BaseAddress = new System.Uri("https://api.restful-api.dev/")
        };

        private static string? _createdObjectId;

        [Fact(DisplayName = "1. Get all objects")]
        public async Task GetAllObjects_ReturnsSuccess()
        {
            var response = await _client.GetAsync("objects");
            Assert.True(response.IsSuccessStatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("data", content);
        }
        [Fact(DisplayName = "2. Create object via POST")]
        public async Task CreateObject_ReturnsCreatedObject()
        {
            var payload = new
            {
                name = "Apple MacBook Pro 16",
                data = new
                {
                    year = 2019,
                    price = 1849.99
                }
            };

            var content = new StringContent(
                   JsonSerializer.Serialize(payload),
                   Encoding.UTF8,
                   "application/json"
            );

            var response = await _client.PostAsync("objects", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(responseContent);
            _createdObjectId = json.RootElement.GetProperty("id").GetString();

            Assert.False(string.IsNullOrEmpty(_createdObjectId));
            Assert.Contains("Apple MacBook Pro 16", responseContent);
        }
        [Fact(DisplayName = "3. Get object by ID")]
        public async Task GetObjectById_ReturnsCorrectObject()
        {
            if (string.IsNullOrEmpty(_createdObjectId))
                await CreateObject_ReturnsCreatedObject();

            var response = await _client.GetAsync($"objects/{_createdObjectId}");
            Assert.True(response.IsSuccessStatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Apple MacBook Pro 16", content);
        }
        [Fact(DisplayName = "4. Update object")]
        public async Task UpdateObject_ReturnsUpdatedObject()
        {
            if (string.IsNullOrEmpty(_createdObjectId))
                await CreateObject_ReturnsCreatedObject();

            var updatedPayload = new
            {
                name = "Apple Iphone Pro 16",
                data = new
                {
                    year = 2020,
                    price = 1850.99
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(updatedPayload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PutAsync($"objects/{_createdObjectId}", content);
            Assert.True(response.IsSuccessStatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(responseBody);
            var root = json.RootElement;

            Assert.Equal("Apple Iphone Pro 16", root.GetProperty("name").GetString());

            var data = root.GetProperty("data");
            Assert.Equal(1850.99, data.GetProperty("price").GetDouble());
        }
        [Fact(DisplayName = "5. Delete object")]
        public async Task DeleteObject_ReturnsSuccess()
        {
            if (string.IsNullOrEmpty(_createdObjectId))
                await CreateObject_ReturnsCreatedObject();

            var response = await _client.DeleteAsync($"objects/{_createdObjectId}");
            Assert.True(response.IsSuccessStatusCode);

            var getResponse = await _client.GetAsync($"objects/{_createdObjectId}");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}

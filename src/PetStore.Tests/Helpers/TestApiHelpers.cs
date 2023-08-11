using PetStore.Tests.DTOs;
using System.Net.Http.Json;
using System.Net;
using Xunit;

namespace PetStore.Tests.Helpers
{
    internal class TestApiHelpers
    {

        public void AssumeLoginResponseIsFailed()
        {
            Assert.True(HttpStatusCode.BadRequest == _response?.StatusCode);
        }
        public async Task AssumeLoginResponseIsOk()
        {
            AssumeResponseIsOk();
            string responseMessage = await GetMessageFromResponse();
            Assert.Contains(responseMessage, "logged in user session:");
            string sessionId = responseMessage.Split(":").Last();
            Assert.NotEmpty(sessionId);
        }

        public async Task<List<User>> SubmitNewUsers(int number)
        {
            List<User> users = new();

            for (int i = 0; i < number; i++)
            {
                var user = TestDataHelpers.CreateNewUser();
                await SubmitRequest(Method.POST, "/user", user);
                user.Id = await GetIdFromResponse();
                users.Add(user);
            }

            return users;
        }

        public void AssumeResponseIsNotFound()
        {
            Assert.Equal(_response?.StatusCode, HttpStatusCode.NotFound);
        }

        public async Task<User> SubmitNewUser()
        {
            var user = TestDataHelpers.CreateNewUser();
            await SubmitRequest(Method.POST, "/user", user);
            user.Id = await GetIdFromResponse();
            return user;
        }

        private async Task<string> GetMessageFromResponse()
        {
            string message = string.Empty;
            if (_response != null)
            {
                ApiResponse? response = await _response.Content.ReadFromJsonAsync<ApiResponse>();
                if (response != null)
                {
                    message = response.Message;
                }
                else
                {
                    Assert.Fail("Api Response body is empty");
                }
            }
            else
            {
                Assert.Fail("Response is empty");
            }
            return message;
        }

        private async Task<int> GetIdFromResponse()
        {
            int id = -1;
            if (_response != null)
            {
                ApiResponse? response = await _response.Content.ReadFromJsonAsync<ApiResponse>();
                if (response != null)
                {
                    id = int.Parse(response.Message);
                }
                else
                {
                    Assert.Fail("Api Response body is empty");
                }
            }
            else
            {
                Assert.Fail("Response is empty");
            }
            return id;
        }

        public async Task AssumeResponseIsEqualTo(User expectedUser)
        {
            if (_response != null)
            {
                _response.EnsureSuccessStatusCode();
                var user = await _response.Content.ReadFromJsonAsync<User>();

                Assert.Equal(expectedUser, user);
            }
            else
            {
                Assert.Fail("Response is Empty");
            }

        }

        public void AssumeResponseIsOk()
        {
            Assert.True(_response?.IsSuccessStatusCode);
        }

        public  async Task SubmitRequest<T>(Method requestMethod, string url, T value)
        {
            switch (requestMethod)
            {
                case Method.PUT:
                    _response = await _httpClient.PutAsJsonAsync($"/{_apiVer}{url}", value);
                    break;
                case Method.POST:
                    _response = await _httpClient.PostAsJsonAsync($"/{_apiVer}{url}", value);
                    break;
                default:
                    break;
            }
        }
        public async Task SubmitRequest(Method requestMethod, string url)
        {
            switch (requestMethod)
            {
                case Method.GET:
                    _response = await _httpClient.GetAsync($"/{_apiVer}{url}");
                    break;
                case Method.DELETE:
                    _response = await _httpClient.DeleteAsync($"/{_apiVer}{url}");
                    break;
                default:
                    break;
            }

        }

        private HttpResponseMessage? _response;
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://petstore.swagger.io") };
        private readonly string _apiVer = "v2";
    }
    internal enum Method
    {
        GET,
        PUT,
        POST,
        DELETE
    }
}

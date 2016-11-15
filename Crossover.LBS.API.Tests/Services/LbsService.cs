using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crossover.LBS.API.Tests.Services
{
    public class LbsService
    {
        private const string ApiUrl = "http://localhost:1673/api";

   
        public Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var client = new RestClient(ApiUrl);
            var taskCompletionSource = new TaskCompletionSource<T>();
            client.ExecuteAsync<T>(request, (response) => taskCompletionSource.SetResult(response.Data));
            return taskCompletionSource.Task;
        }


        public Task<string> ExecuteAsyncString(RestRequest request)
        {
            var client = new RestClient(ApiUrl);
            var taskCompletionSource = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, (response) => taskCompletionSource.SetResult(response.Content));
            return taskCompletionSource.Task;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crossover.LBS.API.Contracts.DTO;
using Crossover.LBS.API.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Crossover.LBS.API.Tests
{
    [TestClass]
    public class CredentialTests
    {
        [TestCategory("Integration"), TestMethod]
        public async Task Should_Get_When_Created_And_None_When_Deleted_Test()
        {
            var sampleCredential = new CredentialDto {Username = "test", Password = "pass123"};

            var createRequest = new RestRequest("credentials", Method.POST) { RequestFormat = DataFormat.Json };
            createRequest.AddBody(sampleCredential);

            var createdCredential = await new LbsService().ExecuteAsync<CredentialDto>(createRequest);

            Assert.IsTrue(createdCredential.Id > 0);

            Assert.IsNotNull(await Get(createdCredential.Id));

            await Delete(createdCredential.Id);
            
            Assert.IsNull(await Get(createdCredential.Id));

        }


        private async Task<CredentialDto> Get(int id)
        {
            var getRequest = new RestRequest("credentials/{id}");
            getRequest.AddUrlSegment("id", id.ToString());

            return await new LbsService().ExecuteAsync<CredentialDto>(getRequest);
        }


        private async Task Delete(int id)
        {
            var deletRequest = new RestRequest("credentials/{id}", Method.DELETE);
            deletRequest.AddUrlSegment("id", id.ToString());

            await new LbsService().ExecuteAsyncString(deletRequest);
        }
    }
}

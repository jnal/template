using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crossover.LBS.API.Contracts.DTO;
using Crossover.LBS.API.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crossover.LBS.API.Controllers
{
    [Route("api/[controller]")]
    public class CredentialsController : Controller
    {
        private readonly ICredentialManager _credentialManager;
        public CredentialsController(ICredentialManager credentialManager)
        {
            _credentialManager = credentialManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _credentialManager.GetAllCredentials());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _credentialManager.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CredentialDto credential)
        {
            var createdCredential = await _credentialManager.Create(credential);
            return CreatedAtRoute(new { createdCredential.Id }, createdCredential);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]CredentialDto credential)
        {
            await _credentialManager.Update(credential);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _credentialManager.Delete(id);
            return new NoContentResult();
        }


    }
}

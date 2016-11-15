using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crossover.LBS.API.Contracts.DTO;

namespace Crossover.LBS.API.Contracts.Interfaces
{
    public interface ICredentialManager
    {
        Task<List<CredentialDto>> GetAllCredentials();
        Task<CredentialDto> Get(int id);
        Task<CredentialDto> Create(CredentialDto credentialDto);
        Task Update(CredentialDto credentialDto);
        Task Delete(int id);
    }
}

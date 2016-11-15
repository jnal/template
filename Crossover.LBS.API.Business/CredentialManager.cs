using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crossover.LBS.API.Business.Domain;
using Crossover.LBS.API.Contracts.DTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Crossover.LBS.API.Contracts.Interfaces;
using Z.EntityFramework.Plus;

namespace Crossover.LBS.API.Business
{
    public class CredentialManager : ICredentialManager
    {
        private readonly LbsContext _lbsContext;

        public CredentialManager(LbsContext lbsContext)
        {
            _lbsContext = lbsContext;
        }

        public async Task<List<CredentialDto>> GetAllCredentials()
        {
            var credentials = await _lbsContext.Credentials.ToListAsync();
            return Mapper.Map<List<Credential>, List<CredentialDto>>(credentials);
        }

        public async Task<CredentialDto> Get(int id)
        {
            var credentialEntity = await _lbsContext.Credentials.SingleAsync(x => x.Id == id);
            return Mapper.Map<Domain.Credential, CredentialDto>(credentialEntity);
        }



        public async Task<CredentialDto> Create(CredentialDto credentialDto)
        {
            var credentialEntity = Mapper.Map<CredentialDto, Domain.Credential>(credentialDto);
            _lbsContext.Credentials.Add(credentialEntity);

            await _lbsContext.SaveChangesAsync();
                
            return Mapper.Map<Domain.Credential, CredentialDto>(credentialEntity);
        }


        public async Task Update(CredentialDto credentialDto)
        {
            // saving query to retrieve the record
            // will do update only instead of retrieve -> update
            var credentialToUpdate = new Domain.Credential { Id = credentialDto.Id };
            _lbsContext.Credentials.Attach(credentialToUpdate);
            Mapper.Map(credentialDto, credentialToUpdate);

            await _lbsContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
           await _lbsContext.Credentials.Where(x => x.Id == id).DeleteAsync();

        }
    }
}

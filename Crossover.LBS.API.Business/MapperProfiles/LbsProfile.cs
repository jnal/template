using AutoMapper;
using Crossover.LBS.API.Business.Domain;
using Crossover.LBS.API.Contracts.DTO;

namespace Crossover.LBS.API.Business.MapperProfiles
{
    public class LbsProfile  : Profile
    {
        public LbsProfile()
        {
            CreateMap<Credential, CredentialDto>();
            CreateMap<CredentialDto, Domain.Credential>();

            CreateMap<Machine, MachineDto>();
            CreateMap<MachineDto, Machine>();

            CreateMap<MachineBackup, BackupDto>();
            CreateMap<BackupDto, MachineBackup>();

            CreateMap<BackupLog, BackupLogDto>();
            CreateMap<BackupLogDto, BackupLog>();


        }

    }
}

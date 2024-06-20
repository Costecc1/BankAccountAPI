using AutoMapper;
using BankAccountAPI.Models;
using BankAccountAPI.Models.Dto;

namespace BankAccountAPI
{
    // NOTE - configuration for automapper
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<BankAccountDto, BankAccount>();
                config.CreateMap<BankAccount, BankAccountDto>();
            });
            return mappingConfig;
        }
    }
}

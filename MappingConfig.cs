using ApiLaboratorio.Models;
using ApiLaboratorio.Models.Dto;
using AutoMapper;

namespace ApiLaboratorio
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // Mapeo para User
                config.CreateMap<User, UserDto>();
                config.CreateMap<UserDto, User>();

                // Mapeo para Carrera
                config.CreateMap<Carrera, CarreraDto>();
                config.CreateMap<CarreraDto, Carrera>();

                // Mapeo para Materia
                config.CreateMap<Materia, MateriaDto>();
                config.CreateMap<MateriaDto, Materia>();
            });

            return mappingConfig;
        }
    }
}

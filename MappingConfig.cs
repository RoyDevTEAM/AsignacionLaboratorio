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

                // Mapeo para Role
                config.CreateMap<Role, RoleDto>();
                config.CreateMap<RoleDto, Role>();

                // Mapeo para AsignarMateria
                config.CreateMap<AsignarMateria, AsignarMateriaDto>();
                config.CreateMap<AsignarMateriaDto, AsignarMateria>();

                // Mapeo para Laboratorio
                config.CreateMap<Laboratorio, LaboratorioDto>();
                config.CreateMap<LaboratorioDto, Laboratorio>();

                // Mapeo para Horario
                config.CreateMap<Horario, HorarioDto>();
                config.CreateMap<HorarioDto, Horario>();

                // Mapeo para Facultad
                config.CreateMap<Facultad, FacultadDto>();
                config.CreateMap<FacultadDto, Facultad>();
            });

            return mappingConfig;
        }
    }
}

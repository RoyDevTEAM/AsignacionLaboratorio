using ApiLaboratorio.Data;
using ApiLaboratorio.Models;
using ApiLaboratorio.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLaboratorio.Repositorio
{
    public class CarreraRepositorio : ICarreraRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarreraRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CarreraDto>> GetCarreras()
        {
            var carreras = await _context.Carreras.ToListAsync();
            return _mapper.Map<List<CarreraDto>>(carreras);
        }

        public async Task<CarreraDto> GetCarreraById(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            return _mapper.Map<CarreraDto>(carrera);
        }

        public async Task<CarreraDto> CreateUpdateCarrera(CarreraDto carreraDto)
        {
            Carrera carrera = _mapper.Map<Carrera>(carreraDto);
            if (carreraDto.CarreraId > 0)
            {
                _context.Carreras.Update(carrera);
            }
            else
            {
                await _context.Carreras.AddAsync(carrera);
                await _context.SaveChangesAsync(); // Guardar cambios para que se genere el ID
                carreraDto.CarreraId = carrera.CarreraId; // Asignar el ID generado a carreraDto
            }
            return carreraDto;
        }



        public async Task<bool> DeleteCarrera(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera == null)
                return false;

            _context.Carreras.Remove(carrera);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

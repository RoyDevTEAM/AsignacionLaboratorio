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
    public class FacultadRepositorio : IFacultadRepositorio
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public FacultadRepositorio(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<FacultadDto> CreateUpdateFacultad(FacultadDto facultadDto)
        {
            try
            {
                Facultad facultad = _mapper.Map<FacultadDto, Facultad>(facultadDto);
                if (facultad.FacultadID > 0)
                {
                    _db.Facultades.Update(facultad);
                }
                else
                {
                    await _db.Facultades.AddAsync(facultad);
                }
                await _db.SaveChangesAsync();
                return _mapper.Map<Facultad, FacultadDto>(facultad);
            }
            catch (Exception)
            {
                // Manejo de excepciones si es necesario
                throw;
            }
        }

        public async Task<bool> DeleteFacultad(int id)
        {
            try
            {
                Facultad facultad = await _db.Facultades.FindAsync(id);
                if (facultad == null)
                {
                    return false;
                }
                _db.Facultades.Remove(facultad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejo de excepciones si es necesario
                throw;
            }
        }

        public async Task<FacultadDto> GetFacultadById(int id)
        {
            try
            {
                Facultad facultad = await _db.Facultades.FindAsync(id);
                return _mapper.Map<FacultadDto>(facultad);
            }
            catch (Exception)
            {
                // Manejo de excepciones si es necesario
                throw;
            }
        }

        public async Task<List<FacultadDto>> GetFacultades()
        {
            try
            {
                List<Facultad> facultades = await _db.Facultades.ToListAsync();
                return _mapper.Map<List<FacultadDto>>(facultades);
            }
            catch (Exception)
            {
                // Manejo de excepciones si es necesario
                throw;
            }
        }
    }
}

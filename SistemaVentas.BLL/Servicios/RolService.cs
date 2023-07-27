using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.DTOs;
using SistemaVentas.Models;

namespace SistemaVentas.BLL.Servicios {
  public class RolService : IRolService{

    private readonly IGenericRepository<Rol> rol_repository;
    private readonly IMapper mapper;

    public RolService(IGenericRepository<Rol> rol_repository, IMapper mapper) {
      this.rol_repository = rol_repository;
      this.mapper = mapper;
    }

    public async Task<List<RolDTO>> Lista() {
      try {
        var lista_roles = await rol_repository.Consult();
        return mapper.Map<List<RolDTO>>(lista_roles.ToList());

      } catch (Exception) {
        throw;
      }
    }
  }
}

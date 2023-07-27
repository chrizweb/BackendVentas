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
  public class CategoriaService : ICategoriaService{

    private readonly IGenericRepository<Categoria> categoria_repository;
    private readonly IMapper mapper;

    public CategoriaService(IGenericRepository<Categoria> categoria_repository, IMapper mapper) {
      this.categoria_repository = categoria_repository;
      this.mapper = mapper;
    }

    public async Task<List<CategoriaDTO>> Lista() {
      try {
        var list_categorias = await categoria_repository.Consult();
        return mapper.Map<List<CategoriaDTO>>(list_categorias.ToList());

      } catch (Exception) {

        throw;
      }
    }
  }
}















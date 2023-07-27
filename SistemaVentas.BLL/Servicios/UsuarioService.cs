using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.DTOs;
using SistemaVentas.Models;

namespace SistemaVentas.BLL.Servicios {
  public class UsuarioService : IUsuarioService{

    private readonly IGenericRepository<Usuario> user_repository;
    private readonly IMapper mapper;

    public UsuarioService(IGenericRepository<Usuario> user_repository, IMapper mapper) {
      this.user_repository = user_repository;
      this.mapper = mapper;
    }

    public async Task<List<UsuarioDTO>> Lista() {
      try {
        var query_user = await user_repository.Consult();
        var lista_users = query_user.Include(rol => rol.IdRolNavigation).ToList();

        return mapper.Map<List<UsuarioDTO>>(lista_users);

      } catch (Exception) {

        throw;
      }
    }

    public async Task<SesionDTO> ValidarCredenciales(string correo, string clave) {
      try {
        var query_user = await user_repository.Consult(u =>
        u.Correo == correo && u.Clave == clave
        );

        /*Si el usuario no existe*/
        if (query_user.FirstOrDefault() == null)
          throw new TaskCanceledException("El usuario no existe");
        
        /*Si el usuario existe*/
        Usuario return_user = query_user.Include(rol => rol.IdRolNavigation).First();

        return mapper.Map<SesionDTO>(return_user);

      } catch (Exception) {

        throw;
      }
    }

    public async Task<UsuarioDTO> Crear(UsuarioDTO model) {
      try {
        var created_user = await user_repository.Create(mapper.Map<Usuario>(model));

        /*Si el usuario no es creado*/
        if(created_user.IdUsuario == 0)
          throw new TaskCanceledException("Error al crear usuario");

        /*Si el usuario es creado, se verifica atraves del id*/
        var query = await user_repository.Consult(u => u.IdUsuario == created_user.IdUsuario);

        /*Actualizar usuario creado añadiendo el campo rol*/
        created_user = query.Include(rol => rol.IdRolNavigation).First();
        return mapper.Map<UsuarioDTO>(created_user);

      } catch (Exception) {

        throw;
      }
    }

    public async Task<bool> Editar(UsuarioDTO model) {
      try {
        var user_model = mapper.Map<Usuario>(model);

        var user_found = await user_repository.Get(u =>
        u.IdUsuario == user_model.IdUsuario);

        if (user_found == null)
          throw new TaskCanceledException("El usuario no existe");

        user_found.NombreCompleto = user_model.NombreCompleto;
        user_found.Correo = user_model.Correo;
        user_found.IdRol = user_model.IdRol;
        user_found.Clave = user_model.Clave;
        user_found.EsActivo = user_model.EsActivo;

        bool response = await user_repository.Edit(user_found);

        if(!response)
          throw new TaskCanceledException("No se pudo editar");

        return response;

      } catch (Exception) {

        throw;
      }
    }

    public async Task<bool> Eliminar(int id) {
      try {
        var user_found = await user_repository.Get(u => u.IdUsuario == id);

        if (user_found == null)
          throw new TaskCanceledException("El usuario no existe");

        bool response = await user_repository.Delete(user_found);

        if (user_found == null)
          throw new TaskCanceledException("No se pudo eliminar");

        return response;

      } catch (Exception) {

        throw;
      }
    }


  }
}

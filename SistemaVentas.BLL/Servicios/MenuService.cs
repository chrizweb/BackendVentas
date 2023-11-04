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
  public class MenuService: IMenuService {

    private readonly IGenericRepository<Usuario> user_repository;
    private readonly IGenericRepository<MenuRol> menuRol_repository;
    private readonly IGenericRepository<Menu> menu_repository;
    private readonly IMapper mapper;

    public MenuService(IGenericRepository<Usuario> user_repository, IGenericRepository<MenuRol> menuRol_repository, IGenericRepository<Menu> menu_repository,
     IMapper mapper) {

      this.user_repository = user_repository;
      this.menuRol_repository = menuRol_repository;
      this.menu_repository = menu_repository;
      this.mapper = mapper;
    }

    public async Task<List<MenuDTO>> Lista(int id_usuario) {
      IQueryable<Usuario> tbl_usuario = await user_repository.Consult(u => u.IdUsuario == id_usuario);
      IQueryable<MenuRol> tbl_menuRol = await menuRol_repository.Consult();
      IQueryable<Menu> tbl_menu = await menu_repository.Consult();

      try {
        IQueryable<Menu> tbl_resultado = (
          from u in tbl_usuario
          join mr in tbl_menuRol on u.IdRol equals mr.IdRol
          join m in tbl_menu on mr.IdMenu equals m.IdMenu
          select m
        ).AsQueryable();

        var lista_menus = tbl_resultado.ToList();
        return mapper.Map<List<MenuDTO>>(lista_menus);

      } catch (Exception) {

        throw;
      }
    }
  }
}

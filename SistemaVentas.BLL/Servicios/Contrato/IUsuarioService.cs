﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.DTOs;

namespace SistemaVentas.BLL.Servicios.Contrato {
  public interface IUsuarioService {

    Task<List<UsuarioDTO>> Lista();
    Task<SesionDTO> ValidarCredenciales(string correo, string clave);
    Task<UsuarioDTO> Crear(UsuarioDTO model);
    Task<bool> Editar(UsuarioDTO model);
    Task<bool> Eliminar(int id);
  }
}

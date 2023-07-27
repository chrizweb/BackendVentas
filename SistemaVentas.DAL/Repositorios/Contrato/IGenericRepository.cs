using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace SistemaVentas.DAL.Repositorios.Contrato {
  public interface IGenericRepository<TModel> where TModel:class{

    Task<TModel> Get(Expression<Func<TModel, bool>> filtro);
    Task<TModel> Create(TModel model);
    Task<bool> Edit(TModel model);
    Task<bool> Delete(TModel model);
    Task<IQueryable<TModel>> Consult(Expression<Func<TModel, bool>> filtro = null);

  }
}

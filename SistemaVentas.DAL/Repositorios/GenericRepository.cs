using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SistemaVentas.DAL.Repositorios {
  public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class {

    private readonly DbventasContext db_context;

    public GenericRepository(DbventasContext db_context) {
      this.db_context = db_context;
    }

    public async Task<TModel> Get(Expression<Func<TModel, bool>> filtro) {
      try {
        TModel model = await db_context.Set<TModel>().FirstOrDefaultAsync(filtro);  
        return model;

      } catch (Exception) {

        throw;
      }
    }

    public async Task<TModel> Create(TModel model) {
      try {
        db_context.Set<TModel>().Add(model);
        await db_context.SaveChangesAsync();
        return model;

      } catch (Exception) {

        throw;
      }
    }

    public async Task<bool> Edit(TModel model) {
      try {
        db_context.Set<TModel>().Update(model);
        await db_context.SaveChangesAsync();
        return true;

      } catch (Exception) {

        throw;
      }
    }
    
    public async Task<bool> Delete(TModel model) {
      try {
        db_context.Set<TModel>().Remove(model);
        await db_context.SaveChangesAsync();
        return true;

      } catch (Exception) {

        throw;
      }
    }

    public async Task<IQueryable<TModel>> Consult(Expression<Func<TModel, bool>> filtro = null) {
      try {
        IQueryable<TModel> queryModel = filtro == null ?
          db_context.Set<TModel>() : db_context.Set<TModel>().Where(filtro);
        return queryModel;

      } catch (Exception) {

        throw;
      }
    }




  }
}








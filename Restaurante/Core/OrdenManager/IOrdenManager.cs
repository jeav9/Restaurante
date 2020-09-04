using Restaurante.Helpers;
using Restaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Core.OrdenManager
{
    public interface IOrdenManager
    {
        Task<ResultHelper<IEnumerable<Orden>>> GetOrdenesAsync();
        Task<ResultHelper<Orden>> GetById(int id);
        Task<ResultHelper<Orden>> GetByClientName(string name);
        Task<ResultHelper<Orden>> GetByDate(DateTime date);
        Task<ResultHelper<Orden>> Create(Orden orden);
        Task<ResultHelper<Orden>> Update(Orden orden, int id);
        Task<ResultHelper<string>> Delete(int id);
    }
}

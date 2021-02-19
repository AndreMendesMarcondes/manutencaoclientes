using RefriSilva.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefriSilva.Data.Imp
{
    public interface IClienteRepository
    {
        public Task Create(Cliente entity);
        public Task<Cliente> GetById(string uid);
        Task<IEnumerable<Cliente>> Get();
        Task Update(string uid, Cliente entity);
        Task Delete(string uid);
    }
}

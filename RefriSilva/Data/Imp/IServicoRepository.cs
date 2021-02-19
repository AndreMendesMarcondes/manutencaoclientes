using RefriSilva.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefriSilva.Data.Imp
{
    public interface IServicoRepository
    {
        public Task Create(string clienteUid, Servico entity);
        public Task<Servico> GetById(string clienteUid, string uid);
        Task<IEnumerable<Servico>> Get(string clienteUid);
        Task Update(string clienteUid, string uid, Servico entity);
        Task Delete(string clienteUid, string uid);
    }
}

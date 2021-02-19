using Google.Cloud.Firestore;
using RefriSilva.Data.Imp;
using RefriSilva.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RefriSilva.Data.Interface
{
    public class ServicoRepository : MainContext, IServicoRepository
    {
        CollectionReference _collection;
        private void SetColletion(string clienteUid)
        {
            _collection = _db.Collection("Clientes").Document(clienteUid).Collection("Servicos");
        }

        public async Task Create(string clienteUid, Servico entity)
        {
            SetColletion(clienteUid);
            await _collection.Document(entity.Uid).SetAsync(entity);
        }

        public async Task<Servico> GetById(string clienteUid, string uid)
        {
            SetColletion(clienteUid);
            var snapshot = await _collection.Document(uid).GetSnapshotAsync();

            if (snapshot.Exists)
            {
                var servico = snapshot.ConvertTo<Servico>();
                return servico;
            }
            return null;
        }

        public async Task<IEnumerable<Servico>> Get(string clienteUid)
        {
            SetColletion(clienteUid);
            var myList = new List<Servico>();
            var snapshot = await _collection.GetSnapshotAsync();

            myList.AddRange(from item in snapshot.Documents
                            select item.ConvertTo<Servico>());

            return myList.ToList();
        }

        public async Task Update(string clienteUid, string uid, Servico entity)
        {
            SetColletion(clienteUid);
            var document = _collection.Document(uid);
            await document.SetAsync(entity);
        }

        public async Task Delete(string clienteUid, string uid)
        {
            SetColletion(clienteUid);
            await _collection.Document(uid).DeleteAsync();
        }
    }
}

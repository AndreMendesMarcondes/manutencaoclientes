using Google.Cloud.Firestore;
using RefriSilva.Data.Imp;
using RefriSilva.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RefriSilva.Data.Interface
{
    public class ClienteRepository : MainContext, IClienteRepository
    {
        CollectionReference _collection;

        public ClienteRepository()
        {
            _collection = _db.Collection("Clientes");
        }

        public async Task Create(Cliente entity)
        {
            await _collection.Document(entity.Uid).SetAsync(entity);
        }

        public async Task<Cliente> GetById(string uid)
        {
            var snapshot = await _collection.Document(uid).GetSnapshotAsync();

            if (snapshot.Exists)
            {
                var cliente = snapshot.ConvertTo<Cliente>();
                return cliente;
            }
            return null;
        }

        public async Task<IEnumerable<Cliente>> Get()
        {
            var myList = new List<Cliente>();
            var snapshot = await _collection.GetSnapshotAsync();

            myList.AddRange(from item in snapshot.Documents
                            select item.ConvertTo<Cliente>());

            return myList.ToList();
        }

        public async Task Update(string uid, Cliente entity)
        {
            var document = _collection.Document(uid);
            await document.SetAsync(entity);
        }
        public async Task Delete(string uid)
        {
            await _collection.Document(uid).DeleteAsync();
        }
    }
}

using Google.Cloud.Firestore;
using RefriSilva.Data.Interface;
using RefriSilva.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RefriSilva.Data.Imp
{
    public class UsuarioRepository : MainContext, IUsuarioRepository
    {
        CollectionReference _collection;
        public UsuarioRepository()
        {
            _collection = _db.Collection("Usuarios");
        }
        public async Task<User> Get(string username, string password)
        {
            var snapshot = await _collection.WhereEqualTo("Nome", username).GetSnapshotAsync();
            var usuario = snapshot.Documents.Any() ? snapshot.Documents.FirstOrDefault().ConvertTo<User>() : null;

            if (usuario != null && usuario.Senha == password)
            {
                return usuario;
            }
            else
            {
                return null;
            }
        }
    }
}

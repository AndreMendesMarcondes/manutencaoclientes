using RefriSilva.Models;
using System.Threading.Tasks;

namespace RefriSilva.Data.Interface
{
    public interface IUsuarioRepository
    {
        Task<User> Get(string username, string password);
    }
}

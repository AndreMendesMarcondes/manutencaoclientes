using Google.Cloud.Firestore;
using System;

namespace RefriSilva.Models
{
    [FirestoreData]
    public class Cliente
    {
        public Cliente()
        {
            Uid = Guid.NewGuid().ToString();
            DataCriacao = DateTime.UtcNow;
        }

        [FirestoreDocumentId]
        public string Uid { get; set; }
        [FirestoreProperty]
        public string Nome { get; set; }
        [FirestoreProperty]
        public string Email{ get; set; }
        [FirestoreProperty]
        public string Telefone { get; set; }
        [FirestoreProperty]
        public string Bairro { get; set; }
        [FirestoreProperty]
        public string Endereco { get; set; }
        [FirestoreProperty]
        public string Numero { get; set; }
        [FirestoreProperty]
        public string Complemento { get; set; }
        [FirestoreProperty]
        public DateTime DataCriacao { get; private set; }
    }
}

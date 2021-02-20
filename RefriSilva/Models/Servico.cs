using Google.Cloud.Firestore;
using System;
using System.ComponentModel.DataAnnotations;

namespace RefriSilva.Models
{
    [FirestoreData]
    public class Servico
    {
        public Servico()
        {
            Uid = Guid.NewGuid().ToString();
            DataCriacao = DateTime.UtcNow;
        }

        [FirestoreDocumentId]
        public string Uid { get; set; }
        [FirestoreProperty]
        public string Descricao { get; set; }
        [FirestoreProperty]
        public DateTime DataCriacao { get; private set; }
        [FirestoreProperty]
        public string Valor { get; set; }
        [FirestoreProperty]
        public DateTime DataProximaManutencao { get; set; }
        [FirestoreProperty]
        public DateTime DataProximaLimpeza { get; set; }
        [FirestoreProperty]
        public string ClienteId { get; set; }
    }
}

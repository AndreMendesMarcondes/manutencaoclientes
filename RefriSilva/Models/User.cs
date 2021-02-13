using Google.Cloud.Firestore;

namespace RefriSilva.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreDocumentId]
        public string Uid { get; set; }
        [FirestoreProperty]
        public string Nome { get; set; }
        [FirestoreProperty]
        public string Senha { get; set; }
    }
}

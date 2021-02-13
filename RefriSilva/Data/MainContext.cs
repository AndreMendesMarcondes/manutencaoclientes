using Google.Cloud.Firestore;

namespace RefriSilva.Data
{
    public class MainContext
    {
        protected FirestoreDb _db;

        public MainContext()
        {
            _db = FirestoreDb.Create("refrisilva-2d97d");
        }
    }
}

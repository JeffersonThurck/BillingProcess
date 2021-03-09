using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Client.API.Models
{
    [FirestoreData]
    public class Cliente
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Nome { get; set; }
        [FirestoreProperty]
        public string CPF { get; set; }
        [FirestoreProperty]
        public string Estado { get; set; }

    }
}

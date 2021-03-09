using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Cobrancas.API.Models
{
    [FirestoreData]
    public class Cobranca
    {
        [FirestoreProperty]
        public DateTime DataVencimento { get; set; }
        [FirestoreProperty]
        public Cliente Cliente { get; set; }
        [FirestoreProperty]
        public double Valor { get; set; }

    }
}

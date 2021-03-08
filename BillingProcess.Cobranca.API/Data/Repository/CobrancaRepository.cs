using BillingProcess.Cobranca.API.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Cobranca.API.Data.Repository
{
    public class CobrancaRepository : ICobrancaRepository
    {
        private string collectionName = "cobrancas";
        public FirestoreDb fireStoreDb;
        public CobrancaRepository()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"config.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            fireStoreDb = FirestoreDb.Create("billingprocess-e2aba");
        }

        public async Task<Models.Cobranca> Add(Models.Cobranca cobranca)
        {
            CollectionReference colRef = fireStoreDb.Collection(collectionName);
            await colRef.AddAsync(cobranca);
            return cobranca;
        }

        public async Task<Models.Cobranca> GetByCpf(string cpf)
        {
            CollectionReference docRef = fireStoreDb.Collection(collectionName);
            Query query = docRef.WhereEqualTo("Cliente.CPF", cpf);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            Models.Cobranca cobranca = new Models.Cobranca();
            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                cobranca = documentSnapshot.ConvertTo<Models.Cobranca>();
            }

            return cobranca;
        }

        public async Task<List<Models.Cobranca>> GetAll()
        {
            List<Models.Cobranca> cobrancas = new List<Models.Cobranca>();

            Query query = fireStoreDb.Collection(collectionName);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                Models.Cobranca cobranca = documentSnapshot.ConvertTo<Models.Cobranca>();

                if (documentSnapshot.Exists)
                {
                    cobrancas.Add(new Models.Cobranca { Valor = cobranca.Valor, Cliente = cobranca.Cliente, DataVencimento = cobranca.DataVencimento });
                }

            }
            return cobrancas;
        }
    }
}

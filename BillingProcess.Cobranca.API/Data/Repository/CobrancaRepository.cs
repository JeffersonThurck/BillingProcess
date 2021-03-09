using BillingProcess.Cobrancas.API.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Cobrancas.API.Data.Repository
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

        public async Task<Cobranca> Add(Cobranca cobranca)
        {
            CollectionReference colRef = fireStoreDb.Collection(collectionName);
            await colRef.AddAsync(cobranca);
            return cobranca;
        }

        public async Task<Cobranca> GetByCpf(string cpf)
        {
            CollectionReference docRef = fireStoreDb.Collection(collectionName);
            Query query = docRef.WhereEqualTo("Cliente.CPF", cpf);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            Cobranca cobranca = new Cobranca();
            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                cobranca = documentSnapshot.ConvertTo<Cobranca>();
            }

            return cobranca;
        }

        public async Task<List<Cobranca>> GetAll()
        {
            List<Cobranca> cobrancas = new List<Cobranca>();

            Query query = fireStoreDb.Collection(collectionName);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                Cobranca cobranca = documentSnapshot.ConvertTo<Cobranca>();

                if (documentSnapshot.Exists)
                {
                    cobrancas.Add(new Cobranca { Valor = cobranca.Valor, Cliente = cobranca.Cliente, DataVencimento = cobranca.DataVencimento });
                }

            }
            return cobrancas;
        }
    }
}

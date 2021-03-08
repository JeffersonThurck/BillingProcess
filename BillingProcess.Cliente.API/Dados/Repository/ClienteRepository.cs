using BillingProcess.Client.API.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Client.API.Dados.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private string collectionName = "clientes";
        public FirestoreDb fireStoreDb;
        public ClienteRepository()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"config.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            fireStoreDb = FirestoreDb.Create("billingprocess-e2aba");
        }

        public async Task<Cliente> Add(Cliente cliente)
        {
            CollectionReference colRef = fireStoreDb.Collection(collectionName);
            await colRef.AddAsync(cliente);

            return cliente;
        }

        public async Task<Cliente> GetByCpf(string record)
        {
            CollectionReference docRef = fireStoreDb.Collection(collectionName);
            Query query = docRef.WhereEqualTo("CPF", record);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            Cliente cliente = new Cliente();
            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                if (!documentSnapshot.Exists)
                {
                    throw new Exception();
                }
                cliente = documentSnapshot.ConvertTo<Cliente>();
            }

            return cliente;
        }

        public async Task<List<Cliente>> GetAll()
        {
            List<Cliente> clientes = new List<Cliente>();

            Query query = fireStoreDb.Collection(collectionName);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                Cliente cliente = documentSnapshot.ConvertTo<Cliente>();

                if (documentSnapshot.Exists)
                {
                    clientes.Add(new Cliente { Nome = cliente.Nome, Estado = cliente.Estado, CPF = cliente.CPF });
                }

            }
            return clientes;
        }
    }
}

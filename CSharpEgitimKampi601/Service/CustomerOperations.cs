using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi601.Service
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();

            var document = new BsonDocument
            {
                {"CustomerName", customer.CustomerName },
                {"CustomerSurname", customer.CustomerSurname },
                {"CustomerCity", customer.CustomerCity },
                {"Balance", customer.Balance },
                {"PurchaseAmount", customer.PurchaseAmount },
            };

            customerCollection.InsertOne(document);
        }

        public List<Customer> GetAllCustomer()
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();

            var customers = customerCollection.Find(new BsonDocument()).ToList();

            List<Customer> customerList = new List<Customer>();
            foreach (var c in customers)
            {
                customerList.Add(new Customer
                {
                    CustomerId = c["_id"].ToString(),
                    Balance = decimal.Parse(c["Balance"].ToString()),
                    CustomerName = c["CustomerName"].ToString(),
                    CustomerSurname = c["CustomerSurname"].ToString(),
                    CustomerCity = c["CustomerCity"].ToString(),
                    PurchaseAmount = int.Parse(c["PurchaseAmount"].ToString())
                });
            }

            return customerList;
        }

        public void DeleteCustomer(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            customerCollection.DeleteOne(filter);
        }

        public void UpdateCustomer(Customer customer)
        {
            var connetcion = new MongoDbConnection();
            var customerCollection = connetcion.GetCustomerCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id",ObjectId.Parse(customer.CustomerId));
            var updatedValue = Builders<BsonDocument>.Update
                .Set("CustomerName", customer.CustomerName)
                .Set("CustomerSurame", customer.CustomerSurname)
                .Set("CustomerCity", customer.CustomerCity)
                .Set("Balance", customer.Balance)
                .Set("PurchaseAmount", customer.PurchaseAmount);
            customerCollection.UpdateOne(filter,updatedValue);
        }

        public Customer GetCustomerById(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id",ObjectId.Parse(id));
            var result = customerCollection.Find(filter).FirstOrDefault();
            return new Customer
            {
                Balance = decimal.Parse(result["Balance"].ToString()),
                CustomerCity = result["CustomerCity"].ToString(),
                CustomerId = id,
                CustomerName = result["CustomerName"].ToString(),
                CustomerSurname = result["CustomerSurname"].ToString(),
                PurchaseAmount = int.Parse(result["PurchaseAmount"].ToString()),
            };
        }
    }
}

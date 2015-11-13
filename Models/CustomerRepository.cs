using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using customer_application.EntityDataModel;


namespace customer_application.Models
{
    public class CustomerRepository
    {
        //Customer Data Repository
        private static CustomerDatabaseEntities _customerDb;
        private static CustomerDatabaseEntities CustomerDb
        {
            get { return _customerDb ?? (_customerDb = new CustomerDatabaseEntities()); }
        }

        //Get the Customers
        public static IEnumerable<Customer> GetCustomers()
        {
            var query = from customers in CustomerDb.Customers select customers;
            return query.ToList();
        }

        //Insert the customer to database
        public static void InsertCustomer(Customer customer)
        {
            CustomerDb.Customers.Add(customer);
            CustomerDb.SaveChanges();
        }

        //Delete customer from database
        public static void DeleteCustomer(int customerId)
        {
            var deleteItem = CustomerDb.Customers.FirstOrDefault(c => c.CustomerID == customerId);

            if (deleteItem != null)
            {
                CustomerDb.Customers.Remove(deleteItem);
                CustomerDb.SaveChanges();
            }
        }
    }
}
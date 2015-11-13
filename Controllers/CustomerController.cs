using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using customer_application.EntityDataModel;
using customer_application.Models;

namespace customer_application.Controllers
{
    public class CustomerController : ApiController
    {
        // GET: api/Customer
        public IEnumerable<Customer> Get()
        {
            return CustomerRepository.GetCustomers();
        }
        
        // GET: api/Customer/5
        public Customer Get(int id)
        {
            return CustomerRepository.GetCustomers().FirstOrDefault(s => s.CustomerID == id);
        }

        // POST: api/Customer
        public HttpResponseMessage Post(Customer customer)
        {
            CustomerRepository.InsertCustomer(customer);
            var response = Request.CreateResponse(HttpStatusCode.Created, customer);
            string url = Url.Link("DefaultApi", new { customer.CustomerID});
            response.Headers.Location = new Uri(url);
            return response;
        }
        
        // DELETE: api/Customer/5
        public HttpResponseMessage Delete(int id)
        {
            CustomerRepository.DeleteCustomer(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, id);
            return response;
        }
    }
}

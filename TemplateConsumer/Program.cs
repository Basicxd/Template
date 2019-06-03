using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Newtonsoft.Json;
using Template;

namespace TemplateConsumer
{
    class Program
    {
        public static async Task<IList<Template.Model.Template>> GetCustomersAsync()
        {
            string CustomerUri = "https://restcustomerservicemikail.azurewebsites.net/api/customer";
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(CustomerUri);
                IList<Template.Model.Template> cList = JsonConvert.DeserializeObject<IList<Template.Model.Template>>(content);
                return cList;
            }
        }

        public static async Task<Template.Model.Template> GetCustomersAsyncid(int id)
        {
            string CustomerUri = "https://localhost:44336/api/customer/" + id;
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(CustomerUri);
                Template.Model.Template oneID = JsonConvert.DeserializeObject<Customer>(content);

                return oneID;
            }
        }

        public static async Task<HttpResponseMessage> GetCustomersAsyncDelete(int id)
        {
            string CustomerUri = "https://localhost:44336/api/customer/" + id;
            using (HttpClient client = new HttpClient())
            {
                var reponse = await client.DeleteAsync(CustomerUri);
                return reponse;
            }
        }

        public static async Task<HttpResponseMessage> GetCustomersAsyncAdd(string firstName, string lastName, int year)
        {
            string CustomerUri = "https://localhost:44336/api/customer/";
            using (HttpClient client = new HttpClient())
            {
                int lastId = GetCustomersAsync().Result.Last().ID;

                var jsonString = JsonConvert.SerializeObject(new Customer(firstName, lastName, year, lastId + 1));

                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(CustomerUri, content);
                return response;
            }
        }

        public static async Task<string> UpdateCustomerAsync(Customer newCustomer, int id)
        {
            string CustomerUri = "https://localhost:44336/api/customer/";
            using (HttpClient client = new HttpClient())
            {
                // Laver object om til en JSON string
                var jsonString = JsonConvert.SerializeObject(newCustomer);
                // ?
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                // await gøre at den async skal være færdig før den gå videre
                HttpResponseMessage response = await client.PutAsync(CustomerUri + id, content);
                // 
                string str = await response.Content.ReadAsStringAsync();
                return str;
            }
        }

        //public static async Task<Customer> UpdateCustomerAsync(Customer newCustomer, int id)
        //{
        //    string CustomerUri = "https://localhost:44336/api/customer/";
        //    using (HttpClient client = new HttpClient())
        //    {
        //        var jsonString = JsonConvert.SerializeObject(newCustomer);
        //        Console.WriteLine("JSON: " + jsonString);
        //        StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = await client.PutAsync(CustomerUri, content);
        //        if (response.StatusCode == HttpStatusCode.NotFound)
        //        {
        //            throw new Exception("Customer not found. Try another id");
        //        }
        //        //response.EnsureSuccessStatusCode();
        //        string str = await response.Content.ReadAsStringAsync();
        //        //den laver object om til en customer object
        //        Customer updCustomer = JsonConvert.DeserializeObject<Customer>(str);
        //        return updCustomer;
        //    }
        //}

        static void Main(string[] args)
        {
            //HttpResponseMessage adding = GetCustomersAsyncAdd("Mikol", "F", 1997).Result;

            IList<Customer> result = GetCustomersAsync().Result;
            Console.WriteLine(result.Count);
            foreach (Customer C in result)
            {
                Console.WriteLine(C.ToString());
                //int ID = C.ID;
                //string firstName = C.FirstName;
                //string lastName = C.LastName;
                //int year = C.Year;
                //Console.WriteLine($"Id: {ID} First Name: {firstName} Last Name: {lastName} Year: {year}");
            }



            //Customer result2 = GetCustomersAsyncid(0).Result;
            //Console.WriteLine(result2.ToString());




            Console.ReadLine();
        }
    }
}

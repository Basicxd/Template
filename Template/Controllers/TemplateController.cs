using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Template;

namespace Template.Controllers
{
    //Kan skifte til noget andet hvis den fucker
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {

        // Dette er en Static Liste 
        // Man skal ikke bruge en Static List hvis der er en DataBase
        private static List<Model.Template> templateList = new List<Model.Template>()
        {
            new Model.Template(1, "Mikail"),
            new Model.Template(1, "Fener")
        };
        private static string navn;


        // GET: api/Template
        [HttpGet]
        public List<Model.Template> Get()
        {
            //Static List
            return templateList;
        }
        
        // GET: api/Template/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Template
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Model.Template value)
        {
            if (templateList.Contains(value))
            {
                return new HttpResponseMessage(HttpStatusCode.NotModified);
            }
            else
            {
                Model.Template addingCoin = new Model.Template(value.Id, value.Navn);
                templateList.Add(addingCoin);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
        }

        // PUT: api/Template/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

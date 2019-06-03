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
    public class TempDataBaseController : ControllerBase
    {
        // Hvis man bruger DataBase skal man gøre dette
        // Husk at give navne til GET, POST, PUT, DELETE hvis man har to Controller
        // Tjek noter for GET, POST, PUT, DELETE

        private string ConnectionString =
            "her vil din Connection string være ";

        // GET: api/TempDataBase
        [HttpGet]
        public IEnumerable<Model.Template> GetDatabase()
        {
            const string selectString = "select * from TableNavn";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Model.Template> templateList = new List<Model.Template>();
                        while (reader.Read())
                        {
                            Model.Template book = ReadTemplate(reader);
                            templateList.Add(book);
                        }
                        return templateList;
                    }
                }
            }
        }

        private static Model.Template ReadTemplate(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string navn = reader.IsDBNull(1) ? null : reader.GetString(1);

            Model.Template template = new Model.Template
            {
                Id = id,
                Navn = navn

            };
            return template;
        }

        // GET: api/TempDataBase/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

       
        // POST: api/TempDataBase
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Model.Template value)
        {
                const string insertString = "insert into table navn ændre values ændre";
                using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
                {
                    databaseConnection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertString, databaseConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@dato", value.Id);
                        insertCommand.Parameters.AddWithValue("@tid", value.Navn);
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
        }

        // PUT: api/TempDataBase/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public HttpResponseMessage DeleteDatabase()
        {
            const string insertString = "DELETE FROM TableNavn";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertString, databaseConnection))
                {
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
        }
    }
}

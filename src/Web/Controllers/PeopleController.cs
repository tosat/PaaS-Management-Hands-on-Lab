using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

using Web.Models;

namespace Web.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ILogger<PeopleController> _logger;
        private readonly IConfiguration _configuration;

        private List<int> TransientErrorNumbers = new List<int>{ 4060, 40197, 40501, 40613, 49918, 49919, 49920, 11001 };

        public PeopleController(ILogger<PeopleController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            int totalNumberOfTimesToTry = 4;
            int retryIntervalSeconds = 10;

            var connectionString = _configuration.GetValue<string>("SqlConnectionString");

            List<Person> people = new List<Person>();

            for (int tries = 1; tries < totalNumberOfTimesToTry; tries++)
            {
                try
                {
                    if (tries > 1)
                    {
                        _logger.LogInformation($"Transient error encounterd. Will begin attempt number {tries} of {totalNumberOfTimesToTry}");

                        Thread.Sleep(1000 * retryIntervalSeconds);

                        retryIntervalSeconds = Convert.ToInt32(retryIntervalSeconds * 1.5);
                    }

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "SELECT * FROM people";
                        command.CommandType = CommandType.Text;

                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var person = new Person
                            {
                                Id = reader.GetInt32(0),
                                FName = reader.GetString(1),
                                LName = reader.GetString(2),
                                Dob = reader.GetDateTime(3),
                                Address = reader.GetString(4),
                                Address2 = reader.GetString(5),
                                City = reader.GetString(6),
                                Suburb = reader.GetString(7),
                                Postcode = reader.GetString(8)
                            };

                            people.Add(person);
                        }
                    }

                    ViewBag.People = people;
                    break;
                }
                catch (SqlException sqlEx)
                {
                    if (TransientErrorNumbers.Contains(sqlEx.Number) == true)
                    {
                        continue;
                    }
                    else
                    {
                        _logger.LogInformation($"ERROR({sqlEx.Number}): {sqlEx.Message}");

                        throw new Exception(sqlEx.Message, sqlEx);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"ERROR: {ex.Message}");

                    throw new Exception(ex.Message);
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult GetPerson(int Id)
        {
            var connectionString = _configuration.GetValue<string>("SqlConnectionString");

            Person person = new Person();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM people WHERE Id = @Id";
                command.CommandType = CommandType.Text;

                SqlParameter param = command.CreateParameter();
                param.ParameterName = "@Id";
                param.SqlDbType = SqlDbType.Int;
                param.Direction = ParameterDirection.Input;
                param.Value = Id;
                
                command.Parameters.Add(param);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    person.Id = reader.GetInt32(0);
                    person.FName = reader.GetString(1);
                    person.LName = reader.GetString(2);
                    person.Dob = reader.GetDateTime(3);
                    person.Address = reader.GetString(4);
                    person.Address2 = reader.GetString(5);
                    person.City = reader.GetString(6);
                    person.Suburb = reader.GetString(7);
                    person.Postcode = reader.GetString(8);
                }
            }

            return View(person);
        }
    }
}

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

using Web.Models;

namespace Web.Controllers
{
    public class PolicyController : Controller
    {
        private readonly ILogger<PolicyController> _logger;
        private readonly IConfiguration _configuration;

        public PolicyController(ILogger<PolicyController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public ActionResult Index()
        {
            var connectionString = _configuration.GetValue<string>("SqlConnectionString");

            List<Policy> policies = new List<Policy>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM policies";
                command.CommandType = CommandType.Text;

                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var policy = new Policy
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        DefaultDeductible = reader.GetDecimal(3),
                        DefaultOutOfPocketMax = reader.GetDecimal(4)
                    };

                    policies.Add(policy);
                }
            }

            ViewBag.Policies = policies;
            return View();
        }

        [HttpGet]
        public ActionResult GetPolicy(int Id)
        {
            var connectionString = _configuration.GetValue<string>("SqlConnectionString");

            Policy policy = new Policy();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM policies WHERE Id = @Id";
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
                    policy.Id = reader.GetInt32(0);
                    policy.Name = reader.GetString(1);
                    policy.Description = reader.GetString(2);
                    policy.DefaultDeductible = reader.GetDecimal(3);
                    policy.DefaultOutOfPocketMax = reader.GetDecimal(4);
                }
            }

            return View(policy);
        }
    }
}
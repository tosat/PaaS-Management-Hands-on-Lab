using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

using Web.Models;

namespace Web.Controllers
{
    public class PolicyHolderController : Controller
    {
        private readonly ILogger<PolicyHolderController> _logger;
        private readonly IConfiguration _configuration;

        public PolicyHolderController(ILogger<PolicyHolderController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public ActionResult Index()
        {
            var connectionString = _configuration.GetValue<string>("SqlConnectionString");

            List<PolicyHolder> policyHolders = new List<PolicyHolder>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                var query = "SELECT " + 
                    "ph.Id, " + 
                    "PersonId, " + 
                    "people.[FName] + ' ' + people.[LName] AS PersonName, " + 
                    "Active, " +
                    "StartDate, " +
                    "EndDate, " + 
                    "Username, " +
                    "PolicyNumber, " + 
                    "PolicyId, " +
                    "policies.Name AS PolicyName, " + 
                    "FilePath, " + 
                    "PolicyAmount, " + 
                    "Deductible, " + 
                    "OutOfPocketMax, " + 
                    "EffectiveDate, " + 
                    "ExpirationDate " + 
                    "FROM PolicyHolders ph " + 
                    "INNER JOIN people ON ph.PersonId = people.Id " + 
                    "INNER JOIN policies ON ph.PolicyId = policies.Id";

                command.Connection = conn;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var policyHolder = new PolicyHolder
                    {
                        Id = reader.GetInt32(0),
                        PersonId = reader.GetInt32(1),
                        PersonName = reader.GetString(2),
                        Active = reader.GetBoolean(3),
                        StartDate = reader.GetDateTime(4),
                        EndDate = reader.GetDateTime(5),
                        Username = reader.GetString(6),
                        PolicyNumber = reader.GetString(7),
                        PolicyId = reader.GetInt32(8),
                        PolicyName = reader.GetString(9),
                        FilePath = reader.GetString(10),
                        PolicyAmount = reader.GetDecimal(11),
                        Deductible = reader.GetDecimal(12),
                        OutOfPocketMax = reader.GetDecimal(13),
                        EffectiveDate = reader.GetDateTime(14),
                        ExpirationDate = reader.GetDateTime(15)
                    };

                    policyHolders.Add(policyHolder);
                }
            }

            ViewBag.PolicyHolders = policyHolders;
            return View();
        }

        [HttpGet]
        public ActionResult GetDetails(int Id)
        {
            var connectionString = _configuration.GetValue<string>("SqlConnectionString");
            var documentUrl = $"https://{_configuration.GetValue<string>("StorageAccountName")}.blob.core.windows.net/{_configuration.GetValue<string>("ContainerName")}";

            PolicyHolder policyHolder = new PolicyHolder();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                var query = "SELECT " + 
                    "ph.Id, " + 
                    "PersonId, " + 
                    "people.[FName] + ' ' + people.[LName] AS PersonName, " + 
                    "Active, " +
                    "StartDate, " +
                    "EndDate, " + 
                    "Username, " +
                    "PolicyNumber, " + 
                    "PolicyId, " +
                    "policies.Name AS PolicyName, " + 
                    "FilePath, " + 
                    "PolicyAmount, " + 
                    "Deductible, " + 
                    "OutOfPocketMax, " + 
                    "EffectiveDate, " + 
                    "ExpirationDate " + 
                    "FROM PolicyHolders ph " + 
                    "INNER JOIN people ON ph.PersonId = people.Id " + 
                    "INNER JOIN policies ON ph.PolicyId = policies.Id";

                command.Connection = conn;
                command.CommandText = query;
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
                    policyHolder.Id = reader.GetInt32(0);
                    policyHolder.PersonId = reader.GetInt32(1);
                    policyHolder.PersonName = reader.GetString(2);
                    policyHolder.Active = reader.GetBoolean(3);
                    policyHolder.StartDate = reader.GetDateTime(4);
                    policyHolder.EndDate = reader.GetDateTime(5);
                    policyHolder.Username = reader.GetString(6);
                    policyHolder.PolicyNumber = reader.GetString(7);
                    policyHolder.PolicyId = reader.GetInt32(8);
                    policyHolder.PolicyName = reader.GetString(9);
                    policyHolder.FilePath = reader.GetString(10);
                    policyHolder.PolicyAmount = reader.GetDecimal(11);
                    policyHolder.Deductible = reader.GetDecimal(12);
                    policyHolder.OutOfPocketMax = reader.GetDecimal(13);
                    policyHolder.EffectiveDate = reader.GetDateTime(14);
                    policyHolder.EffectiveDate = reader.GetDateTime(15);
                }

                ViewBag.PolicyDocumentPath = documentUrl + policyHolder.FilePath;
                return View(policyHolder);
            }
        }
    }
}

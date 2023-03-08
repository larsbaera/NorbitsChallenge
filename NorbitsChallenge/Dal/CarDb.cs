using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace NorbitsChallenge.Dal
{
    public class CarDb
    {
        private readonly IConfiguration _config;

        public CarDb(IConfiguration config)
        {
            _config = config;
        }
        // Henter antall dekk basert på licensePlate og companyID
        public int GetTireCount(int companyId, string licensePlate)
        {
            //nullstiller variabelen
            int result = 0;
            // ny variabel connectionString som hentes fra appsettings.json
            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where companyId = {companyId} and licenseplate = '{licensePlate}'";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = (int)reader["tireCount"];
                        }
                    }
                }
            }

            return result;
        }

        public string GetCarMakeModel(int companyId, string licensePlate)
        {
            string MakeModel = "";

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where companyId = {companyId} and licenseplate = '{licensePlate}'";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string manufacturer = (string)reader["Brand"];
                            string model = (string)reader["Model"];
                            MakeModel = $"{manufacturer} {model}";
                        }

                    }
                }
            }
            return MakeModel;
        }
    }
}

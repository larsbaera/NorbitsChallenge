using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Models;

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

        public int UpdateCar (string LP, string model, string brand, string desc, int tireCount, int CompId)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"Update dbo.car Set Model = '{model}', Brand= '{brand}', TireCount= '{tireCount}', Description= '{desc}', CompanyId= '{CompId}' Where LicensePlate = '{LP}'";

                    return command.ExecuteNonQuery();
                }
            }
        }


        public int CreateCar(string LP, string model, string brand, string desc, int tireCount, int CompId)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"INSERT INTO dbo.Car(LicensePlate, Description, Model, Brand, TireCount, CompanyId)" + $"Values ('{LP}', '{desc}', '{brand}','{model}','{tireCount}','{CompId}')";

                    return command.ExecuteNonQuery();
                }
            }
        }

        public int DeleteCar(int companyId, string licensePlate)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"delete from dbo.car where companyId = {companyId} and licenseplate = '{licensePlate}'";

                    return command.ExecuteNonQuery();
                }
            }
        }


        public Car SearchCar(int companyId, string licensePlate)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;
            var car = new Car();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where LicensePlate like '%{licensePlate}%' and companyId = {companyId}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            car.LicensePlate = (string)reader["LicensePlate"];
                            car.Desc = (string)reader["Description"];
                            car.Manufacturer = (string)reader["Model"];
                            car.ProductionModel = (string)reader["Brand"];
                            car.TireCount = (int)reader["TireCount"];
                            car.CompanyId = (int)reader["CompanyId"];

                            return car;
                        }
                    }
                }
            }
            return car;
        }
        public List<Car> GetAllCars (int companyId)
        {
            var Cars  = new List<Car>();
            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where companyId = {companyId}";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tempCar = new Car();
                            tempCar.LicensePlate = (string)reader["LicensePlate"];
                            tempCar.Manufacturer = (string)reader["Brand"];
                            tempCar.ProductionModel = (string)reader["Model"];
                            tempCar.Desc = (string)reader["Description"];
                            tempCar.TireCount = (int)reader["TireCount"];
                            tempCar.CompanyId = (int)reader["CompanyId"];

                            Cars.Add(tempCar);
                        }

                    }
                }
            }

            return Cars;
        }
    }
}

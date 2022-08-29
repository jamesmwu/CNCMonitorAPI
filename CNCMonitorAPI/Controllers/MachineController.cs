using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using MySqlConnector;
using CNCMonitorAPI.Models;

namespace CNCMonitorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public MachineController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet("all")]
        public async Task<JsonResult> Get()
        {
            string query = @"
                        SELECT * FROM `cnc-machine-db`.Machine
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("cncMachine");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                await mycon.OpenAsync();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }

    }
}


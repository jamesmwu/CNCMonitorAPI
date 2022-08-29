using System;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using CNCMonitorAPI.Models;

namespace CNCMonitorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public TimeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //Select entries based on date range
        [HttpGet("Date range")]
        public async Task<JsonResult> Get(DateTime begin, DateTime end, int limit)
        {
            string query = @"
                        SELECT * FROM 
                        `cnc-machine-db`.Time
                        WHERE Time BETWEEN @begin AND @end
                        LIMIT @limit;
            ";

            DataTable table = new DataTable();
            try
            {
                string sqlDataSource = _configuration.GetConnectionString("cncMachine");
                MySqlDataReader myReader;
                using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                {
                    await mycon.OpenAsync();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
       
                        myCommand.Parameters.AddWithValue("@begin", begin);
                        myCommand.Parameters.AddWithValue("@end", end);
                        myCommand.Parameters.AddWithValue("@limit", limit);


                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new JsonResult(table);
        }



    }
}


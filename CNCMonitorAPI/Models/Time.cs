using System;
namespace CNCMonitorAPI.Models
{
    public class Time
    {
        public int idTime { get; set; }
        public string StackLight { get; set; } = string.Empty;
        public int idMachine { get; set; }
        public DateTime timeVal { get; set; }
    }
}


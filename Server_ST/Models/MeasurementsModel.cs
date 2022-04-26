using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class MeasurementsModel
    {
        private int id;
        private string machine;
        private float temperature;
        private float battery;
        private DateTime dateTime;

        public int Id { get => id; set => id = value; }
        
        [Required]
        public string Machine { get => machine; set => machine = value; }
        
        [Required] 
        public float Temperature { get => temperature; set => temperature = value; }
        
        [Required]
        public float Battery { get => battery; set => battery = value; }

        [Required]
        public DateTime DateTime { get => dateTime; set => dateTime = value; }

        internal bool Validate(ref string message)
        {
            DateTime dateRange;
            DateTime.TryParse("2000-01-01T00:00:00.000", out dateRange);

            if (String.IsNullOrEmpty(Machine))
            {
                message = "Machine can't be empty";
                return false;
            }            
            else if (Battery > 100 || Battery < 0)
            {
                message = "Battery is out of range (0 - 100)";
                return false;
            }
            else if ((DateTime.Now.Ticks - dateTime.Ticks) < 10000 || dateTime.Ticks < dateRange.Ticks)
            {
                message = "DateTime is out of range (2000 - Now)";
                return false;
            }

            return true;
        }
    }
}
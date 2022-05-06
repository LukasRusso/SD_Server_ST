using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class MeasurementsModel
    {
        private int _id;
        private string _machine;
        private float _temperature;
        private float _battery;
        private DateTime _dateTime;

        public int id { get => _id; set => _id = value; }
        
        [Required]
        public string machine { get => _machine; set => _machine = value; }
        
        [Required] 
        public float temperature { get => _temperature; set => _temperature = value; }
        
        [Required]
        public float battery { get => _battery; set => _battery = value; }

        [Required]
        public DateTime dateTime { get => _dateTime; set => _dateTime = value; }

        internal bool Validate(ref string message)
        {
            DateTime dateRange;
            DateTime.TryParse("2000-01-01T00:00:00.000", out dateRange);

            if (String.IsNullOrEmpty(machine))
            {
                message = "Machine can't be empty";
                return false;
            }            
            else if (battery > 100 || battery < 0)
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
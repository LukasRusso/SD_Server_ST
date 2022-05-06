using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class MaintenanceModel
    {
        private int _id;
        private string _machine;
        private string _location;       
        private string _worker;
        private DateTime _dateTime;

        public int id { get => _id; set => _id = value; }

        [Required]
        public string machine { get => _machine; set => _machine = value; }

        [Required]
        public string location { get => _location; set => _location = value; }

        [Required]
        public string worker { get => _worker; set => _worker = value; }

        [Required]
        public DateTime dateTime { get => _dateTime; set => _dateTime = value; }

        public bool Validate(ref string message)
        {
            DateTime dateRange;
            DateTime.TryParse("2000-01-01T00:00:00.000", out dateRange);

            if (String.IsNullOrEmpty(machine))
            {
                message = "Machine can't be empty";
                return false;
            }
            else if (String.IsNullOrEmpty(location))
            {
                message = "Location can't be empty";
                return false;
            }
            else if (String.IsNullOrEmpty(worker))
            {
                message = "Worker can't be empty";
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
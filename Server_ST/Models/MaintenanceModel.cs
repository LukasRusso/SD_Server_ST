using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class MaintenanceModel
    {
        private int id;
        private string machine;
        private string location;       
        private string worker;
        private DateTime dateTime;

        public int Id { get => id; set => id = value; }

        [Required]
        public string Machine { get => machine; set => machine = value; }

        [Required]
        public string Location { get => location; set => location = value; }

        [Required]
        public string Worker { get => worker; set => worker = value; }

        [Required]
        public DateTime DateTime { get => dateTime; set => dateTime = value; }

        public bool Validate(ref string message)
        {
            DateTime dateRange;
            DateTime.TryParse("2000-01-01T00:00:00.000", out dateRange);

            if (String.IsNullOrEmpty(Machine))
            {
                message = "Machine can't be empty";
                return false;
            }
            else if (String.IsNullOrEmpty(Location))
            {
                message = "Location can't be empty";
                return false;
            }
            else if (String.IsNullOrEmpty(Worker))
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
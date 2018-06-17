using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace General.Models
{
    public class UserPreferences
    {
        [Key]
        public int UserID { get; set; }

        public string SystemLanguange { get; set; } = "EN";

        public string Currency { get; set; } = "Euro";

        public UserPreferences() { }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace General.Models
{
    public sealed class UserPreferences
    {
        static readonly UserPreferences _instance = new UserPreferences();

        public static UserPreferences Instance
        {
            get
            {
                return _instance;
            }
        }

        [Key]
        public int UserID { get; set; }

        public string SystemLanguange { get; set; } = "EN";

        public string Currency { get; set; } = "Euro";

        private UserPreferences() { }
    }
}

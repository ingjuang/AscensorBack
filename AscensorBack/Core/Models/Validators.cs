using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Validators
    {
        public static bool isShedulingActivate { get; set; } = false;

        public bool getIsShedulingActivate()
        {
            return isShedulingActivate;
        }
    }
}

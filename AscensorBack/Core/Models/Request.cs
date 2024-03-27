using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public int floorToGo {  get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Models
{
    public class Platforms
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ExternalID { get; set; }

        public ICollection<Commands> Commands { get; set; }  = new List<Commands>();

        
    }
}
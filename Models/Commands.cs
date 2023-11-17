using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Models
{
    public class Commands
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string HowTo { get; set; }
        [Required]
        public string CommandLine { get; set; }
        [ForeignKey("Platform")]
        public int PlatformId { get; set; }

        public Platforms Platform { get; set; }
    }
}
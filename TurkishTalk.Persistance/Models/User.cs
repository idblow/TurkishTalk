using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishTalk.Persistance.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public required string FullName { get; set; }
        [Required]
        public required string Login { get; set; }
        [Required]
        public required string HeshedPassword { get; set; }

        public required string Salt { get; set; }

        public byte[]? Image { get; set; }
        public string? ImageContentType { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        public List<ProgresWrite> ProgresWrite { get; set; } = new List<ProgresWrite>();
        public List<ProgresGrammar> ProgresGrammar { get; set; } = new List<ProgresGrammar>();
        public List<ProgresRead> ProgresRead { get; set; } = new List<ProgresRead>();
        public List<ProgressAlfabet> ProgressAlfabet { get; set; } = new List<ProgressAlfabet>();

    }
}

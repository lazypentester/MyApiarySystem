using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Entities
{
    public class EConfirmation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Secret_code { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool Activated { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [ForeignKey("EUser")]
        public int UserId { get; set; }
        public EUser User { get; set; }
    }
}

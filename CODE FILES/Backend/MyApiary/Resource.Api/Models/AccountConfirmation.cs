using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class AccountConfirmation
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        public string Phone { get; set; }
        [Required]
        public bool PhoneConfirmSelected { get; set; } = false;
        [Required]
        public bool MailConfirmSelected { get; set; } = true;
    }
}

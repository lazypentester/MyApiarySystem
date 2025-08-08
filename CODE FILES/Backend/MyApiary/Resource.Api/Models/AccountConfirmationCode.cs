using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class AccountConfirmationCode
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        public string Phone { get; set; }
        [Required]
        public bool PhoneConfirmed { get; set; }
        [Required]
        public bool MailConfirmed { get; set; }
        [Required]
        public string Code { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Entities
{
    public class EUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Surname { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Phone { get; set; }

        [Column(TypeName = "bit")]
        public bool PhoneConfirmed { get; set; } = false;

        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Введите ваш электронный адресс.")]
        [EmailAddress]
        public string Mail { get; set; }

        [Column(TypeName = "bit")]
        public bool MailConfirmed { get; set; } = false;

        [Column(TypeName = "nvarchar(100)")]
        public string Salt { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Pass { get; set; }

        [Column(TypeName = "bit")]
        public bool AccountConfirmed { get; set; } = false;

        [Column(TypeName = "bit")]
        public bool TwoFactorEnabled { get; set; } = false;

        [Column(TypeName = "int")]
        [ForeignKey("ETariff")]
        public int TariffId { get; set; }
        public ETariff Tariff { get; set; }

        [Column(TypeName = "int")]
        [ForeignKey("ERole")]
        public int RoleId { get; set; }
        public ERole Role { get; set; }


        public ICollection<EConfirmation> Confirmations { get; set; }
        //public ICollection<ENotification> Notifications { get; set; }
        public ICollection<EDevice> Devices { get; set; }
        public ICollection<EApiary> Apiaries { get; set; }
    }
}

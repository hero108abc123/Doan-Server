using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA.Shared.Constant.Database;

namespace DA.Auth.Domain
{
    [Table(nameof(AuthCustomer), Schema = DbSchema.Auth)]
    public class AuthCustomer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(128)]
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public int UserId { get; set; }

        public AuthUser User { get; set; }
    }
}

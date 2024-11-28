using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA.Shared.Constant.Database;

namespace DA.Booking.Domain
{
    [Table(nameof(Invoice), Schema = DbSchema.Booking)]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; } 
        public DateTime PaymentDate { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }
    }
}

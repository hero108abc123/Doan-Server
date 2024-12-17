using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA.Shared.Constant.Database;

namespace DA.Vehicle.Domain
{
    [Table(nameof(VehicleSeat), Schema = DbSchema.Vehicle)]
    public class VehicleSeat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Position { get; set; }
        public string Row { get; set; }
        public string Floor { get; set; }
        public int Status { get; set; }
        public int BusId { get; set; }

        [ForeignKey("BusId")]
        public VehicleBusRide BusRide { get; set; }
    }
}

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
    [Table(nameof(VehicleBusRide), Schema = DbSchema.Vehicle)]
    public class VehicleBusRide
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string RideName { get; set; }
        public string LicensePlate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public int BusId { get; set; } // Foreign Key to Bus

        public ICollection<VehicleSeat> Seats { get; set; } = new List<VehicleSeat>();

        [ForeignKey("BusId")]
        public VehicleBus VehicleBus { get; set; } // Navigation Property

        
    }

}

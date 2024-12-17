using DA.Shared.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.Domain
{
    [Table(nameof(VehicleBus), Schema = DbSchema.Vehicle)]
    public class VehicleBus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public decimal Price { get; set; }
        public int TotalSeat { get; set; }
        public ICollection<VehicleBusRide> BusRides { get; set; } = new List<VehicleBusRide>();

    }
}

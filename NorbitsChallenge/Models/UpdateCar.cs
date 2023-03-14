using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NorbitsChallenge.Models
{
    public class UpdateCar
    {
        [DisplayName("License plate")]
        public string LicensePlate { get; set; }
        [DisplayName("Manufacturer")]
        public string Manufacturer { get; set; }
        [DisplayName("Model")]
        public string ProductionModel { get; set; }

        [DisplayName("Description")]
        public string Desc { get; set; }

        [DisplayName("Num Tires")]
        public int TireCount { get; set; }
        [DisplayName("CompanyId")]
        public int CompanyId { get; set; }

    }
}

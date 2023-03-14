using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorbitsChallenge.Models
{
    public class Car
    {
        [Key]
        [StringLength(7, MinimumLength = 7)]
        public string LicensePlate { get; set; }

        public string Manufacturer { get; set; }

        public string ProductionModel { get; set; }

        public string Desc { get; set; }

        public int TireCount { get; set; }

        public int CompanyId { get; set; }
    }
    public class CarUpdate : Car
    {
        [Key]
        [StringLength(7, MinimumLength = 7)]
        [Required]
        [DisplayName("License plate")]
        public new string LicensePlate { get; set; }


    }
}

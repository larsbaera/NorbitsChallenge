namespace NorbitsChallenge.Models;

using System.ComponentModel;
    public class NewCar
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

            public string CarManMod
            {
                get
                {
                    return Manufacturer + " - " + ProductionModel;
                }
            }
    }


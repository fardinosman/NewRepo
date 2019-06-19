using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace KraerApp.Models
{
    public class PropertyModel
    {

        public PropertyModel()
        {
            Address = new Address();
        }
        [JsonProperty("UserId")]
        public string UserId { get; set; } //p1.
        [JsonProperty("BrokerNumber")]

        public string BrokerNumber { get; set; }//9129    
        [JsonProperty("PropertyNumber")]
        public string PropertyNumber { get; set; }  //sagsnr 
        [JsonProperty("Active")]
        public bool Active { get; set; }
        [JsonProperty("PropertyDisplayNumber")]
        public string PropertyDisplayNumber { get; set; } //Sagsnr
        [JsonProperty("PropertyName")]
        public string PropertyName { get; set; } // Egendomsnavn   
        [JsonProperty("Archived")]
        //public CancellationReasonStatus Status { get; set; }

        public short Archived { get; set; } //
        [JsonProperty("ExpireDate")]
        public DateTime? ExpireDate { get; set; } //udløbsdato //user applicatn could be in differnet time zone
        [JsonProperty("Address")]
        public Address Address { get; set; } //
        [JsonProperty("LocalArea")]
        public string LocalArea { get; set; } // bydel
        [JsonProperty("Category")]
        public PropertyCategory Category { get; set; } // patrik
        [JsonProperty("FloorArea")]
        //public PropertyDiscontinuedType DiscontinuedType { get; set; }
        public decimal FloorArea { get; set; } //  etageareal
        [JsonProperty("BaseArea")]
        public decimal BaseArea { get; set; } //grundareal
        [JsonProperty("Sale")]
        public short Sale { get; set; } // til salg
        [JsonProperty("SalesPrice")]
        public decimal SalesPrice { get; set; }//Pris 
        [JsonProperty("Rent")]
        public short Rent { get; set; } //1 eller 0 tilleje
        [JsonProperty("RentalPrice")]
        public decimal RentalPrice { get; set; } // leje pris
        [JsonProperty("OperatingCost")]
        public decimal OperatingCost { get; set; } //drift pris
        [JsonProperty("Description")]
        public string Description { get; set; } //beskrivelse
        [JsonProperty("FlashLine")]
        public string FlashLine { get; set; } //Introtekst
        [JsonProperty("Introductory")]
        public string Introductory { get; set; } //kombeskrivelse
        [JsonProperty("SecondaryBrokerNumber")]
        public string SecondaryBrokerNumber { get; set; }//anden medlems nr
        [JsonProperty("Depriciation")]
        public bool? Depriciation { get; set; } // 
        [JsonProperty("ReturnRate")]
        public decimal? ReturnRate { get; set; } //afkast procent
        [JsonProperty("Url")]
        public string Url { get; set; } //url
        [JsonProperty("Project")]
        public bool? Project { get; set; } //projekt ejendom
        [JsonProperty("SecondaryArea")]
        public decimal? SecondaryArea { get; set; } //seconderareal
        [JsonProperty("EnergyMark")]
        public string[] EnergyMark { get; set; } //engergymærke
        [JsonProperty("EnergyRequired")]
        public bool? EnergyRequired { get; set; } //energy lov
        [JsonProperty("DataMarker")]
        public string DataMarker { get; set; } // ikke nødvendig
        [JsonProperty("Kvhx")]
        public string Kvhx { get; set; } // ikke nødvendig
        [JsonProperty("Alternates")]
        public AlternateInfo[] Alternates { get; set; } // ikke nødvendig

    }
}
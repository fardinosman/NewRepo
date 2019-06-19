using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace KraerApp.Models
{
    public class AlternateInfo
    {
        [JsonProperty("PricePrSpotTo")]
        public decimal PricePrSpotTo { get; set; }
        //        PricePrSpotFrom : Decimal
        //PricePrSpot : Decimal
        //NumberOfSpotsTo : Integer
        //NumberOfSpotsFrom : Integer
        //NumberOfSpots : Integer
        //Facilities : Integer[]
        //Access : Integer[]
        //OperatingCost : Decimal
        //FloorAreaTo : Decimal
        //FloorAreaFrom : Decimal
        //FloorArea : Decimal
        //PropertyCategory : SupplierPropertyCategory
        //PropertyId : Integer
        //YearlyPrice : Decimal
    }
}
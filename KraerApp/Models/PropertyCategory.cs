using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KraerApp.Models
{
    [Flags]
    public enum PropertyCategory
    {
        //[EnumMember]
        None = 0,
        // [EnumMember]
        OwnUseOtherTypes = 0x1,
        //  [EnumMember]
        OwnUseRetailAndStores = 0x2,
        // [EnumMember]
        OwnUseParcel = 0x4,
        // [EnumMember]
        OwnUseHotelsAndRestaurants = 0x8,
        //  [EnumMember]
        OwnUseOffices = 0x10,
        // [EnumMember]
        OwnUseStoresAndProductions = 0x20,
        //[EnumMember]
        InvestmentOtherTypes = 0x40,
        // [EnumMember]
        InvestmentRetailAndStores = 0x80,
        // [EnumMember]
        InvestmentParcel = 0x100,
        //[EnumMember]
        InvestmentHotelsAndRestaurants = 0x200,
        //  [EnumMember]
        InvestmentOffices = 0x400,
        // [EnumMember]
        InvestmentStoresAndProductions = 0x800,
        // [EnumMember]
        InvestmentHousingRental = 0x1000,
        //  [EnumMember]
        BiddingOtherTypes = 0x2000,
        //   [EnumMember]
        BiddingRetailAndStores = 0x4000,
        //[EnumMember]
        BiddingParcel = 0x8000,

        BiddingHotelsAndRestaurants = 0x10000,

        BiddingOffices = 0x20000,

        BiddingStoresAndProductions = 0x40000,

        BiddingHousingRental = 0x80000,

        OwnUseOfficesHotel = 0x100000,

        OfficeHotelGeneral = 0x200000,

        OfficeHotelFlex = 0x400000,

        OfficeHotelPermanent = 0x800000,

        OfficeHotelPrivate = 0x1000000,

    };
}

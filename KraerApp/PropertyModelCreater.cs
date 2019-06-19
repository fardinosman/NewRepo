using KraerApp.Interface;
using KraerApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace KraerApp
{
    public class PropertyModelCreater : IPropertyModelCreater
    {
         PropertyModel IPropertyModelCreater.FillPropertyModel(CSVDataModel item)
        {
            PropertyModel p1 = new PropertyModel();
            p1.BrokerNumber = "9129";
            p1.PropertyNumber = item.Sagsnr;
            p1.Active = item.Aktiv == "J";//
            p1.PropertyDisplayNumber = item.Sagsnr;
            p1.PropertyName = item.Vejnavn;
            // DateTime dt = DateTime.ParseExact(item.Dato, "ddMMyyyy");
            DateTime date;
            DateTime.TryParseExact(item.Dato, "ddMMyyyy",
                                   CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            p1.ExpireDate = date;
            p1.Address = new Address() { StreetName = item.Vejnavn, StreetNumber = item.Husnr, ZipCode = item.Postnr };
            p1.Category = PropertyCategory.None;//Pathrik

            p1.FloorArea = string.IsNullOrEmpty(item.Etageareal) ? 0 : Int32.Parse(item.Etageareal);
            p1.BaseArea = string.IsNullOrEmpty(item.Grundareal) ? 0 : Int32.Parse(item.Grundareal);
            if (string.IsNullOrWhiteSpace(item.Tilsalg))
            {

            }
            else
            {
                var salg = item.Tilsalg;
                if (salg == "N")
                {
                    p1.Sale = 0;
                }
                else if (salg == "J")
                {
                    p1.Sale = 1;
                }

            }
            p1.SalesPrice = string.IsNullOrEmpty(item.Pris) ? 0 : Int32.Parse(item.Pris);
            if (string.IsNullOrWhiteSpace(item.Tilsalg))
            {

            }
            else
            {
                var tilleje = item.tilleje;
                if (tilleje == "N")
                {
                    p1.Rent = 0;
                }
                else if (tilleje == "J")
                {
                    p1.Rent = 1;
                }

            }

            p1.RentalPrice = string.IsNullOrEmpty(item.Lejepris) ? 0 : Int32.Parse(item.Lejepris);

            p1.OperatingCost = p1.RentalPrice = string.IsNullOrEmpty(item.Driftpris) ? 0 : Int32.Parse(item.Driftpris);
            p1.Description = item.KomBeskrivelse;
            p1.FlashLine = item.Overskrift;
            p1.Introductory = item.Introtekst;
            //p1.SecondaryBrokerNumber= 
            p1.ReturnRate = string.IsNullOrEmpty(item.Afkast) ? 0 : decimal.Parse(item.Afkast);
            p1.Url = item.URL;
            // p1.Project= 
            //p1.SecondaryArea=item.se
            p1.EnergyMark = item.EnergiMaerke;
            p1.EnergyRequired = item.EnergiLov == "J";

            return p1;
        }
    }
}
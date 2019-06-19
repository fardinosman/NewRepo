using CsvHelper;
using Kraer.DifferentServices;
using KraerApp.Interface;
using KraerApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace KraerApp.Implemention
{
    public class PostingProperty : IPostToApi
    {
        const string myId = "9"; // SupplierID
        const string mySecret = "kIJUiosdeK870324"; // APiKey;

        List<PropertyModel> propertyModelsList;
        private readonly ICSVimportData _cSVimportData;
        IPropertyModelCreater modelCreater;

        public PostingProperty(ICSVimportData cSVimportData, IPropertyModelCreater modelCreater)
        {
            this.modelCreater = modelCreater;
            _cSVimportData = cSVimportData;
        }

        public List<PropertyModel> Post(IHttpClientWrapper client, DateTime machineUtcDateTime, TimeSpan timeSkewAdjustSeconds)
        {
         //  var client = client;
            string path = @"C:\Users\Fard\Documents\FTP\export.csv";
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();

                using (StreamReader reader = new StreamReader(path, Encoding.Default))
                {


                    //var reader = new StreamReader(path, Encoding.Default);
                    //her henter den Csv filer fra path

                    var csv = new CsvReader(reader);
                    //her placer den data i CSVimportmodel klassen har samme matcherne colums navn som der i CSV filen. 
                    var CSVimportData = csv.GetRecords<CSVDataModel>();

                    csv.Configuration.HeaderValidated = null;
                    csv.Configuration.MissingFieldFound = null;

                    propertyModelsList = new List<PropertyModel>();


                    foreach (var item in CSVimportData)
                    {
                        PropertyModel p1 = new PropertyModel();
                        p1.BrokerNumber = "9129";
                        p1.PropertyNumber = item.Sagsnr;
                        p1.Active = item.Aktiv == "J";
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

                        propertyModelsList.Add(p1);
                        // Console.WriteLine(item.Sagsnr + " " + item.Vejnavn +" " + item.Lejepris);
                        var url = new Uri("https://api.oline.dk/v1/SupplierServices/Brokers/9129/Properties");
                        var requestHeader = Hawk.CreateRequestHeader(machineUtcDateTime, ref timeSkewAdjustSeconds, url, "post");

                        client.Client.DefaultRequestHeaders.Clear();
                        client.Client.DefaultRequestHeaders.Add("Authorization", requestHeader);

                        response = client.Client.PostAsJsonAsync(url, p1).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            Console.Write("Success");


                        }
                    }
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Fail To Post Propertys");


                    }


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.HResult.ToString());
                //Logging.LogAction("PropertyError", "kraerPropertyError", e.HResult.ToString());
            }
            return propertyModelsList;
        }

        public void CsvReadMapPropertyModel(string path, HttpClient client, DateTime machineUtcDateTime, TimeSpan timeSkewAdjustSeconds)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                List<PropertyModel> propertyModelsList = new List<PropertyModel>();
                var CSVimportData = _cSVimportData.DoCSVimportData(path);

                foreach (var item in CSVimportData)
                {
                    PropertyModel p1 = new PropertyModel();
                    p1.BrokerNumber = "9129";
                    p1.PropertyNumber = item.Sagsnr;
                    p1.Active = item.Aktiv == "J";
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

                    propertyModelsList.Add(p1);
                    // Console.WriteLine(item.Sagsnr + " " + item.Vejnavn +" " + item.Lejepris);
                    var url = new Uri("https://api.oline.dk/v1/SupplierServices/Brokers/9129/Properties");
                    var requestHeader = Hawk.CreateRequestHeader(machineUtcDateTime, ref timeSkewAdjustSeconds, url, "post");

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", requestHeader);

                    response = client.PostAsJsonAsync(url, p1).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Console.Write("Success");

                    }
                    if (!response.IsSuccessStatusCode)
                    {
                       // Email.SendMail("osmanfardin@hotmail.dk", "Fard0055", "osmannnfardinnn@gmail.com", "fejl", response.Content.ToString());
                    }//
                }



                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // Logging.LogAction("PropertyError", "kraerPropertyError", e.Message);
                Email.SendMail("osmanfardin@hotmail.dk", "fard0055", "osmannnfardinnn@gmail.com", "fejl", e.Message.ToString());
            }
        }
    }
}
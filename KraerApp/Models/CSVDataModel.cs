using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KraerApp.Models
{
    public class CSVDataModel
    {

        public CSVDataModel()
        {
            //images = new List<Image>();
        }
        public string Afdnr { get; set; }
        public string Sagsnr { get; set; }
        public string SagsnrUDV { get; set; }
        public string Pris { get; set; }
        public string Afkast { get; set; }
        public string Aktiv { get; set; }
        public string EmnekategoriNr { get; set; }
        public string Vejnavn { get; set; }
        public string Husnr { get; set; }
        public string Postnr { get; set; }
        public string Etage { get; set; }
        public string Etageareal { get; set; }
        public string Grundareal { get; set; }
        public string Tilsalg { get; set; }
        public string tilleje { get; set; }
        public string Lejepris { get; set; }
        public string Driftpris { get; set; }
        public string Overskrift { get; set; }
        public string Introtekst { get; set; }
        public string KomBeskrivelse { get; set; }
        public string SagsStatus { get; set; }
        public string Dato { get; set; }
        public string URL { get; set; }
        public string EnergiLov { get; set; }
        public string[] EnergiMaerke { get; set; }

        //  public List<Image> images { get; set; }

    }
}
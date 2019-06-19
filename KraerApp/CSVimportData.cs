using CsvHelper;
using KraerApp.Interface;
using KraerApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KraerApp.Controllers
{
    public class CSVimportData : ICSVimportData
    {
        IReadOnlyList<CSVDataModel> ICSVimportData.DoCSVimportData(string path)
        {
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                //var reader = new StreamReader(path, Encoding.Default);
                //her henter den Csv filer fra path
                var csv = new CsvReader(reader);
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.MissingFieldFound = null;
                csv.Configuration.BadDataFound = null;
                csv.Configuration.Delimiter = ";";
                csv.Configuration.Quote = '"';
                csv.Configuration.HasHeaderRecord = true;
                //her placer den data i CSVimportmodel klassen har samme matcherne colums navn som der i CSV filen. 
                var CSVimportData = csv.GetRecords<CSVDataModel>();
                return CSVimportData.ToList();
            }
        }
    }
}
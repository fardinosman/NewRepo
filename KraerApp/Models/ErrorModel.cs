using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KraerApp.Models
{
    public class ErrorModel
    {

        public ErrorModel()
        {
            Items = new List<Item>();
            FejlCode = string.Empty;
            ErrorMessage = string.Empty;
        }
        [JsonProperty("ErrorCode")]
        public int ErrorCode { get; set; }
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }
        [JsonProperty("Items")]
        public List<Item> Items { get; set; }
        [JsonProperty("FejlCode")]
        public string FejlCode { get; set; }
//
        public string sagsNr { get; set; }
    }

    public class Item
    {

        [JsonProperty("FieldName")]
        public string FieldName { get; set; }
        [JsonProperty("ErrorCode")]
        public int ErrorCode { get; set; }
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }
    }
}
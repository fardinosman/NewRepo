using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KraerApp.Models
{
    public class ImageModel
    {
        public string BrokerNumber { get; set; }
        public string PropertyNumber { get; set; }
        public int ImageNumber { get; set; }
        public byte[] ImageData { get; set; }
        public string Url { get; set; }
        public string DataMarker { get; set; }
    }
}
using CsvHelper;
using Kraer.DifferentServices;
using KraerApp.Interface;
using KraerApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace KraerApp.Implemention
{
    public class PostingImage : IPostingImage
    {
        string path = @"C:\Users\Fard\Documents\FTP\export.csv";
        Image resizeImage;
        string[] exten = { ".PNG", ".JPEG", ".JPG", ".GIF" };
        public void Post(HttpClient client, DateTime machineUtcDateTime, TimeSpan timeSkewAdjustSeconds)
        {
            try
            {

                // Email mailInstance = new Email();

                //------------------------------------------------------------------------------------------
                ImageModel imageModel = new ImageModel();
                PropertyModel propertyModel = new PropertyModel();
                //CSVImage cSVImageModel = new CSVImage();

                MemoryStream m;

                //Path.GetFileName(@"C:\Users\praktikant\Documents\PraktikCode\Kraer\FTPdata\")
                string csvpath = @"C:\Users\Fard\Documents\FTP\export.csv";
                string imagepath =@"C:\Users\Fard\Documents\FTP";



                byte[] bytes;

                PropertyModel p1 = new PropertyModel();
                using (TextReader reader = new StreamReader(imagepath, Encoding.Default))
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


                    List<PropertyModel> propertyModelsList = new List<PropertyModel>();
                    string[] filePaths = Directory.GetFiles(imagepath, "*.jpg");

                    foreach (var item in CSVimportData)
                    {
                        p1.PropertyNumber = item.Sagsnr;
                        foreach (var itemImage in filePaths)
                        {

                            //// var stuffToRemove = Regex.Match(itemImage, @"b\d+").Value;
                            var stuffToRemove = Regex.Match(itemImage, @"([A-Z])-(\d+[^A-Z+])").Value;
                            //slash d means i should have some type of characters lowercase or uppercase
                            // = itemImage.Replace(stuffToRemove, "") ;
                            if (p1.PropertyNumber == stuffToRemove)
                            {
                                //if (p1.PropertyNumber.Equals("D-4064", StringComparison.OrdinalIgnoreCase))
                                //{
                                //    Debugger.Break();
                                //}

                                //var strPath = itemImage;
                                //strPath.Substring(0, strPath.LastIndexOf(''));
                                imageModel.PropertyNumber = stuffToRemove;

                                Image image = Image.FromFile(itemImage);


                                double imageWidth = image.Width;
                                double imageHeight = image.Height;

                                double calculatedResult = imageHeight / imageWidth;
                                double newHeight = 1200 * calculatedResult;

                                //  double newImageHeight = imageWidth * calculatedResult;
                                int NewHeight = (int)Math.Round(newHeight, 0);


                                //Org bredder  / h = x
                                //org højder 

                                //ny højde = ny bredde * x
                                //-------------------------------------------------------------------------------------
                                resizeImage = Resize(image, 1200, NewHeight);

                                m = new MemoryStream();

                                resizeImage.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);

                                //    //to get the bytes we type
                                bytes = m.ToArray();

                                // if bytes > 1200

                                //if (image.Width > size)
                                //{
                                //    using (Bitmap mybitmap = new Bitmap(itemImage))
                                //    {
                                //        ImageCodecInfo jpgEncoder = GetEncoder
                                //    }

                                //}

                                imageModel.ImageData = bytes;


                                // var lastcharacter = stuffToRemove.Substring(stuffToRemove.Length - 1);
                                //imageModel.ImageNumber = Convert.ToInt32(lastcharacter);
                                var lastcharacter = Regex.Match(itemImage, @"\d+\.jpg$").Value;
                                var lastDigit = lastcharacter.Remove(lastcharacter.Length - 4);

                                // fjerner jpg derefter assigner det til imagenumber

                                imageModel.ImageNumber = string.IsNullOrEmpty(lastcharacter) ? 0 : Int32.Parse(lastDigit);
                                imageModel.DataMarker = "";
                                imageModel.BrokerNumber = "9129";


                                var url = new Uri($"https://api.oline.dk/v1/SupplierServices/Brokers/9129/Properties/{imageModel.PropertyNumber}/Images");
                                var requestHeader = Hawk.CreateRequestHeader(machineUtcDateTime, ref timeSkewAdjustSeconds, url, "post");
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Add("Authorization", requestHeader);
                                var response = client.PostAsJsonAsync(url, imageModel).Result;


                                if (response.IsSuccessStatusCode)
                                {
                                    Console.Write("Success");
                                    //Logging.LogAction("ImagePropertyError", "kraerImageError", response.StatusCode.ToString());

                                }
                                else
                                {

                                    Console.WriteLine("Fail To Post Propertys");
                                  //  Logging.LogAction("ImagePropertyError", "kraerImageError", response.StatusCode.ToString());


                                    break;
                                }

                            }
                        }

                    }


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //Logging.LogAction("ImagePropertyError", "kraerImageError", e.Message);
            }

        }



        //private Image ByteToImg(byte[] byteArr)
        //{
        //    var ms = new MemoryStream(byteArr);
        //    return Image.FromStream(ms);
        //}




        // here we have to pass image , and image height and width
        public Image Resize(Image image, int w, int h)
        {
            Bitmap mbp = new Bitmap(w, h);
            Graphics graphics = Graphics.FromImage(mbp);
            graphics.DrawImage(image, 0, 0, w, h);

            return mbp;
        }
    }
}
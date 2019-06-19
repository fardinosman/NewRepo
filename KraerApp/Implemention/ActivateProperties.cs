using CsvHelper;
using Kraer.DifferentServices;
using KraerApp.Interface;
using KraerApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace KraerApp.Implemention
{
    public class ActivateProperties: IActivateProperties
    {

        //  string pass = ConfigurationManager.AppSettings.Keys.ToString();

        public List<ErrorModel> errorModelsList = new List<ErrorModel>();
        HttpClient httpClient;
        ErrorModel ErrorModel;
        CsvReader csvReader;
        IEnumerable<CSVDataModel> CSVimportData;


        /// <summary>
        /// This Method Loops and find all Property number and Activate and then add it to a ErrorModelList and send mail to  Broker with alle the Errors
        /// </summary>
        public void Post(HttpClient client, DateTime machineUtcDateTime, TimeSpan timeSkewAdjustSeconds)
        {
            try
            {
                httpClient = new HttpClient();
                ErrorModel = new ErrorModel();

                var lines = File.ReadAllLines(@"C:\Users\Fard\Documents\FTP\export.csv", Encoding.Default);




                if (lines.Contains("ENDOFDATA"))
                {
                    File.WriteAllLines(@"C:\Users\Fard\Documents\FTP\export.csv", lines.Take(lines.Count() - 2));
                }

                using (TextReader streamReader = new StreamReader(@"C:\Users\Fard\Documents\FTP\export.csv", Encoding.Default))
                {
                    using (csvReader = new CsvReader(streamReader))
                    {
                        if (lines.Contains("ENDOFDATA"))
                        {
                            File.WriteAllLines(@"C:\Users\Fard\Documents\FTP\export.csv", lines.Take(lines.Count() - 2));
                        }


                        csvReader.Configuration.HeaderValidated = null;
                        csvReader.Configuration.MissingFieldFound = null;
                        csvReader.Configuration.BadDataFound = null;
                        csvReader.Configuration.Delimiter = ";";
                        csvReader.Configuration.Quote = '"';
                        CSVimportData = csvReader.GetRecords<CSVDataModel>();


                        StringBuilder stringBuilder = new StringBuilder();

                        foreach (var item in CSVimportData)
                        {

                            var url = new Uri($"https://api.oline.dk/v1/SupplierServices/Brokers/9129/Properties/{item.Sagsnr}/activate");
                            var requestHeader = Hawk.CreateRequestHeader(machineUtcDateTime, ref timeSkewAdjustSeconds, url, "post");
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Add("Authorization", requestHeader);
                            var response = client.PostAsJsonAsync(url, item.Sagsnr).Result;
                            var jsonString = response.Content.ReadAsStringAsync();
                            ErrorModel = JsonConvert.DeserializeObject<ErrorModel>(jsonString.Result);
                            //ErrorModel.sagsNr = item.Sagsnr;
                            errorModelsList.Add(ErrorModel);
                            if (response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Ok");
                            }
                            else
                            {
                                Console.WriteLine("Fail To Activate Properties");
                                //Logging.LogAction("PropertyError", "kraerPropertyError", response.RequestMessage.Content.ToString());
                            }


                        }


                        Console.WriteLine("alert('Email Sent..');");
                        foreach (var errorCode in errorModelsList)
                        {

                            stringBuilder.AppendLine(errorCode.sagsNr);
                            stringBuilder.AppendLine(" ");
                            stringBuilder.AppendLine(errorCode.ErrorCode + " " + errorCode.ErrorMessage + " " + errorCode.FejlCode + " ");


                            foreach (var errorItems in errorCode.Items)
                            {
                                stringBuilder.AppendLine(errorItems.ErrorCode.ToString() + " ");
                                stringBuilder.AppendLine(errorItems.ErrorMessage.ToString() + " ");
                                stringBuilder.AppendLine(errorItems.FieldName.ToString() + " ");


                            }
                            stringBuilder.AppendLine("------------------------------------------------------------------------------------------------------");

                            // SendEmail(stringBuilder.ToString()); SendEmail(CreateBody());


                        }


                        //Logging.LogAction("PropertyActiviationError", "kraerActivationError", stringBuilder.ToString());
                        Email.SendMail("osmanfardin@hotmail.dk", "Fard0055", "osmannnfardinnn@gmail.com", "PropertyError", stringBuilder.ToString());
                        SendEmail(CreateBody());
                        SendEmail(stringBuilder.ToString());
                    }
                }
            }


            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                //Logging.LogAction("PropertyActiviationError", "kraerActivationError", e.Message);
            }

        }

        public void SendEmail(string mailbody)
        {
            try
            {


                using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["SMTPuser"], "osmannnfardinnn@gmail.com"))
                {
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(@"C:/Users/Fard/Desktop/EmailBodyPage.html"))
                    {
                        body = reader.ReadToEnd();

                    }




                    mm.Subject = "List af AktiveringsFejl";
                    mm.Body = CreateBody();
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ConfigurationManager.AppSettings["Host"];
                    smtp.EnableSsl = true;

                    NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["SMTPuser"],
                        ConfigurationManager.AppSettings["SMTPpassword"]);
                    smtp.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
                    smtp.Credentials = NetworkCred;
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                    smtp.Send(mm);
                    Console.WriteLine("alert('Email Sent..');");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public string CreateBody()
        {
            string body = string.Empty;
            try
            {




                using (StreamReader reader = new StreamReader(@"C:/Users/Fard/Desktop/EmailBodyPage.html"))
                {
                    body = reader.ReadToEnd();

                }


                foreach (var errorCode in errorModelsList)
                {

                    body = body.Replace("{sagsNr}", errorCode.sagsNr);




                    foreach (var errorItems in errorCode.Items)
                    {

                        body = body.Replace("{ErrorMessage}", errorItems.ErrorMessage.ToString());
                        body = body.Replace("{FejlCode}", errorItems.ErrorCode.ToString());
                        body = body.Replace("{FieldName}", errorItems.FieldName.ToString());


                    }





                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return body;
        }


    }

}

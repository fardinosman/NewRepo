using KraerApp.Implemention;
using KraerApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
namespace KraerApp.Controllers
{
    public class HawkController : ApiController
    {
        public static UnityContainer container = null;


        // GET: Hawk
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //Post
        public void PostProperties()
        {
            container = new UnityContainer();
            container.RegisterType(typeof(IPostToApi), typeof(PostingProperty));
            container.RegisterType(typeof(IActivateProperties), typeof(ActivateProperties));
            container.RegisterType(typeof(IPostingImage), typeof(PostingImage));
            container.RegisterType(typeof(ICSVimportData), typeof(CSVimportData));
            container.RegisterType(typeof(IHttpClientWrapper), typeof(HttpClientWrapper));
            //container.RegisterType(typeof(IActivateProperties), typeof(ActivateProperties));
           // container.RegisterType(typeof(IMailCreator), typeof(MailCreator));
            container.RegisterType(typeof(IPropertyModelCreater), typeof(PropertyModelCreater));
           // container.RegisterType(typeof(IHelloWork), typeof(PropertyModelCreater));
            // container.RegisterType(typeof(IPostingImage), typeof(PostingImage));

            var lastExsample = container.Resolve<InsertProperty>();
            lastExsample.Execute();

        }
    }
}
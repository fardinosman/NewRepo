using KraerApp.Controllers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KraerApp
{
    public class HelloWork : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            HawkController controller = new HawkController();
            controller.PostProperties();


        }
    }
}
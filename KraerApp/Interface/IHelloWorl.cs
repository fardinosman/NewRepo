using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KraerApp.Interface
{
    interface IHelloWorK
    {
        void Execute(IJobExecutionContext context);
    }
}

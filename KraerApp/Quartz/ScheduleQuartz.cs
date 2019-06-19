using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace KraerApp.Quartz
{
    public class ScheduleQuartz
    {
        public  void RunScheduler()
        {
            Console.WriteLine("Scheduler started");
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = schedulerFactory.GetScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<HelloWork>()
                .WithIdentity("job1", "group1")
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                         .WithIdentity("trigger1", "group1")
                //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(13, 19))
                //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(15,32
                //))
                .ForJob("job1", "group1")
                .Build();
            scheduler.ScheduleJob(job, trigger);
            //Thread.Sleep(TimeSpan.FromMinutes(10));
            //scheduler.Shutdown();
        }
     
    }
}
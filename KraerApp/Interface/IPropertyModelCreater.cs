﻿using KraerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KraerApp.Interface
{
   public interface IPropertyModelCreater
    {
         PropertyModel FillPropertyModel(CSVDataModel item);
    }
}

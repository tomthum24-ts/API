﻿using API.APPLICATION.ViewModels.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.WareHouse
{
    public class WareHouseRequestViewModel : DanhMucRequestViewModel
    {
    }
    public class WareHouseByIdRequestViewModel
    {
        public int Id { get; set; }
    }
}
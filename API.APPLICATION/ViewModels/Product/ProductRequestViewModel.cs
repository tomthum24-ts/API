using API.APPLICATION.ViewModels.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.Product
{
    public class ProductRequestViewModel : DanhMucRequestViewModel
    {
    }
    public class ProductByIdRequestViewModel
    {
        public int Id { get; set; }
    }
}

using Ecomm_BookProject_2_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_BookProject_2_Models.ViewModels
{
  public  class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> listCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}

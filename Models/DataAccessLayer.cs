using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExsitecParonAB.Models
{
    public class DataAccessLayer
    {

        public List<GetProducts_Result> GetProducts()
        {
            using (ParonABEntities1 paronEntities = new ParonABEntities1())
            {
                List<GetProducts_Result> products = paronEntities.GetProducts().ToList();
                return products;
            }
        }

        public List<GetProductWarehouseAmount_Result> GetProductWarehouseAmount(string productName)
        {
            using (ParonABEntities1 paronEntities = new ParonABEntities1())
            {
                List<GetProductWarehouseAmount_Result> result = paronEntities.GetProductWarehouseAmount(productName).ToList();
                return result;
            }
        }
    }
}
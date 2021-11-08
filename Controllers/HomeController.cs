using ExsitecParonAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExsitecParonAB.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();
        ErrorLog log = new ErrorLog();

        public ActionResult Index()
        {
            return View();
        }

        public List<GetProducts_Result> GetAllProducts()
        {
            List<GetProducts_Result> products = new List<GetProducts_Result>();
            try
            {
                products = dal.GetProducts();

            }catch(Exception ex)
            {
                log.ErrorLogging(ex);
            }

            return products;
        }
        public List<GetProductWarehouseAmount_Result> GetProductWarehouseAmount(string productName)
        {
            List<GetProductWarehouseAmount_Result> amount = new List<GetProductWarehouseAmount_Result>();
             try
            {
                amount = dal.GetProductWarehouseAmount(productName);
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }

            return amount;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1102137246.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Default() {
            Models.Service service = new Models.Service();
            List<Models.Employee> getCodeTitle = service.GetCodeTitle();
            List<Models.Employee> getCountry = service.GetCodeTitle();
            List<List<Models.Employee>> list = new List<List<Models.Employee>>();
            list.Add(getCodeTitle);
            list.Add(getCountry);
            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(Models.Employee employee)
        {
            Models.Service service = new Models.Service();
            List<Models.Employee> emp = service.Result(employee);
            return this.Json(emp);
        }

        [HttpPost]
        public JsonResult Delete(int employeeId)
        {
            Models.Service service = new Models.Service();
            service.Delete(employeeId);
            return null;
        }

        [HttpPost]
        public JsonResult Insert(Models.Employee employee)
        {
            Models.Service service = new Models.Service();
            service.Insert(employee);
            return null;
        }

        public ActionResult Insert()
        {
            return View();
        }

    }
}
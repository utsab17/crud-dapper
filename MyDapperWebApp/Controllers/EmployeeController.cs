using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MyDapperWebApp.Models;

namespace MyDapperWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View(Models.DapperORM.ReturnList<EmployeeModel>("EmployeeViewAll" ));
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {

            if (id == 0)
                return View();
            else {
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmployeeID", id);
                return View(DapperORM.ReturnList<EmployeeModel>("EmployeeViewByID", param).FirstOrDefault<EmployeeModel>());
             }
        }

        [HttpPost]
        public ActionResult AddOrEdit(EmployeeModel emp)
        {
            Dapper.DynamicParameters param = new Dapper.DynamicParameters();
            param.Add("@EmployeeID", emp.EmployeeId);
            param.Add("@Name", emp.Name);
            param.Add("@Position", emp.Position);
            param.Add("@Age", emp.Age);
            param.Add("@Salary", emp.Salary);

            DapperORM.ExecuteWithoutReturn("EmployeeAddOrEdit", param);

            return RedirectToAction("Index");
        }
    }
}
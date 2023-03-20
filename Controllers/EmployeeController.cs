using clinic.Models.clinic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private ClinicService clinic;

        public EmployeeController(ClinicService clinicService)
        {
            clinic = clinicService;
        }

        [HttpGet("GetEmployees")]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            //StaffsComponent employeesComponent = new StaffsComponent(clinic);
            try
            {
                var res = clinic.GetStaffs().Result.ToList();
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        [HttpPost("AddEmployee")]
        public bool AddEmployee([FromBody] CreateEmployee createEmployee)
        {
            //AddStaffComponent addEmployeeComponent = new AddStaffComponent(clinic);
            try
            {
                //return addEmployeeComponent.CreateEmployee(createEmployee).Result;
                createEmployee.employeeData.dateAdded = DateTime.Now;
                createEmployee.employeeData.addedBy = createEmployee.userData.Id;
                createEmployee.employeeData.employmentStatus = 1;
                var clinicCreateStaffResult = clinic.CreateStaff(createEmployee.employeeData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}

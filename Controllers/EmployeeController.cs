using clinic.Models.clinic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
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

        [HttpGet("GetDesignations")]
        public ActionResult<IEnumerable<Designation>> GetDesignations()
        {
            //StaffsComponent employeesComponent = new StaffsComponent(clinic);
            try
            {
                var res = clinic.GetDesignations().Result.ToList();
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        [HttpGet("GetEmplyomentTypes")]
        public ActionResult<IEnumerable<EmploymentType>> GetEmplyomentTypes()
        {
            //StaffsComponent employeesComponent = new StaffsComponent(clinic);
            try
            {
                var employeeTypes = new List<EmploymentType>();
                using (var connection = new MySqlConnection("Server=202.182.120.224;Database=clinic;User ID=remote;Password=#3PqKZ$F3G=y9NSD;Connection Timeout=100"))
                {
                    connection.Open();

                    //clear appointment
                    var query = $"SELECT employmentTypeId, employmentType FROM employment_type";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        employeeTypes.Add(new EmploymentType()
                        {
                            employmentTypeId = Convert.ToInt32(reader[0]),
                            employmentType = reader[1].ToString()
                        });
                    }

                    connection.Close();
                }

                return employeeTypes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        [HttpGet("GetNewEmployeeId")]
        public ActionResult<int> GetNewEmployeeId()
        {
            //StaffsComponent employeesComponent = new StaffsComponent(clinic);
            try
            {
                int id = 0;
                using (var connection = new MySqlConnection("Server=202.182.120.224;Database=clinic;User ID=remote;Password=#3PqKZ$F3G=y9NSD;Connection Timeout=100"))
                {
                    connection.Open();

                    //clear appointment
                    var query = $"SELECT max(employeeId) FROM employee";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    id = Convert.ToInt32(cmd.ExecuteScalar());

                    connection.Close();
                }

                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return id;
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

        [HttpPost("UpdateEmployee")]
        public bool UpdateEmployee([FromBody] CreateEmployee createEmployee)
        {
            //AddStaffComponent addEmployeeComponent = new AddStaffComponent(clinic);
            try
            {
                var clinicCreateStaffResult = clinic.UpdateStaff(createEmployee.employeeData.employeeId, createEmployee.employeeData);
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

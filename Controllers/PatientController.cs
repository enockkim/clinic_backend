using clinic.Models.clinic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {       //[HttpPost("AddPatient")]
        private ClinicService clinic;

        public PatientController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        [HttpPost("AddPatient")]
        public async Task<bool> AddPatient([FromBody] CreatePatient createPatient)
        {
            //AddPatientComponent addPatientComponent = new AddPatientComponent(clinic);
            try
            {
                //return addPatientComponent.CreatePatient(createPatient).Result;
                var clinicCreatePatientResult = await clinic.CreatePatient(createPatient.patientData);

                Appointment appointment = new Appointment()
                {
                    appointmentStatus = 0,
                    createdBy = createPatient.userData.Id,
                    appointmentType = 0, //default
                    paymentMethod = 0, //default
                    dateOfCreation = DateTime.Now,
                    patientType = 1,
                    patientId = clinicCreatePatientResult.patientId,
                    employeeId = 0 //default
                };

                var clinicCreateAppointmentResult = await clinic.CreateAppointment(appointment);

                Bill bill = new Bill()
                {
                    appointmentId = clinicCreateAppointmentResult.appointmentId,
                    status = 0
                };
                await clinic.CreateBill(bill);


                //UriHelper.NavigateTo("add-patient", true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        [HttpPost("UpdatePatient")]
        public async Task<bool> UpdatePatient([FromBody] CreatePatient createPatient)
        {
            //AddPatientComponent addPatientComponent = new AddPatientComponent(clinic);
            try
            {
                //return addPatientComponent.CreatePatient(createPatient).Result;
                var clinicCreatePatientResult = await clinic.UpdatePatient(createPatient.patientData.patientId, createPatient.patientData);


                //UriHelper.NavigateTo("add-patient", true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


        [HttpGet("GetPatients")]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            //PatientsComponent patientsComponent = new PatientsComponent(clinic);
            try
            {
                var res = clinic.GetPatients().Result.Where(i => i.status == 1).ToList();
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}

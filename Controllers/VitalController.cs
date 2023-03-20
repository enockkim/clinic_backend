using clinic.Models.clinic;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VitalController : ControllerBase
    {       //[HttpPost("AddVital")]
        private ClinicService clinic;

        public VitalController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        //[HttpPost("")]
        //public bool AddVital([FromBody] Models.clinic.Vital createVital)
        //{
        //    AddVitalComponent addVitalComponent = new AddVitalComponent(clinic);
        //    try
        //    {
        //        return addVitalComponent.CreateVital(createVital).Result;
        //    }catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}


        [HttpGet("GetVitalsByAppointmentId")]
        public ActionResult<IEnumerable<Vital>> GetVitalsByAppointmentId(int appointmentId)
        {
            //VitalsComponent VitalsComponent = new VitalsComponent(clinic);
            List<Vital> Vital = new List<Vital>();
            try
            {
                var res = clinic.GetVitals().Result;
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return res.Where(i => i.appointmentId == appointmentId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}

using clinic.Models.clinic;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacilityController : ControllerBase
    {       //[HttpPost("AddFacility")]
        private ClinicService clinic;

        public FacilityController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        //[HttpPost("")]
        //public bool AddFacility([FromBody] Models.clinic.Facility createFacility)
        //{
        //    AddFacilityComponent addFacilityComponent = new AddFacilityComponent(clinic);
        //    try
        //    {
        //        return addFacilityComponent.CreateFacility(createFacility).Result;
        //    }catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}


        [HttpGet("GetFacilities")]
        public ActionResult<IEnumerable<Facilities>> GetFacilities(int appointmentType)
        {
            //FacilitysComponent FacilitysComponent = new FacilitysComponent(clinic);
            List<Facilities> Facility = new List<Facilities>();
            try
            {
                var res = clinic.GetFacilities().Result;
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return res.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}

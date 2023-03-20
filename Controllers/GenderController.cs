using clinic.Models.clinic;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Route("api/[controller]")]
    public class GenderController : ControllerBase
    {       //[HttpPost("AddGender")]
        private ClinicService clinic;

        public GenderController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        //[HttpPost("")]
        //public bool AddGender([FromBody] Models.clinic.Gender createGender)
        //{
        //    AddGenderComponent addGenderComponent = new AddGenderComponent(clinic);
        //    try
        //    {
        //        return addGenderComponent.CreateGender(createGender).Result;
        //    }catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}


        [HttpGet("GetGenders")]
        public ActionResult<IEnumerable<Gender>> GetGenders()
        {
            //GendersComponent GendersComponent = new GendersComponent(clinic);
            List<Gender> Gender = new List<Gender>();
            try
            {
                var res = clinic.GetGenders().Result;
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

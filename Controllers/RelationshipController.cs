using clinic.Models.clinic;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RelationshipController : ControllerBase
    {       //[HttpPost("AddRelationship")]
        private ClinicService clinic;

        public RelationshipController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        //[HttpPost("")]
        //public bool AddRelationship([FromBody] Models.clinic.Relationship createRelationship)
        //{
        //    AddRelationshipComponent addRelationshipComponent = new AddRelationshipComponent(clinic);
        //    try
        //    {
        //        return addRelationshipComponent.CreateRelationship(createRelationship).Result;
        //    }catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}


        [HttpGet("GetRelationships")]
        public ActionResult<IEnumerable<Relationship>> GetRelationships()
        {
            //RelationshipsComponent RelationshipsComponent = new RelationshipsComponent(clinic);
            List<Relationship> Relationship = new List<Relationship>();
            try
            {
                var res = clinic.GetRelationships().Result;
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

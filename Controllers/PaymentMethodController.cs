using clinic.Models.clinic;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentMethodController : ControllerBase
    {       //[HttpPost("AddPaymentMethod")]
        private ClinicService clinic;

        public PaymentMethodController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        //[HttpPost("")]
        //public bool AddPaymentMethod([FromBody] Models.clinic.PaymentMethod createPaymentMethod)
        //{
        //    AddPaymentMethodComponent addPaymentMethodComponent = new AddPaymentMethodComponent(clinic);
        //    try
        //    {
        //        return addPaymentMethodComponent.CreatePaymentMethod(createPaymentMethod).Result;
        //    }catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}


        [HttpGet("GetPaymentMethods")]
        public ActionResult<IEnumerable<PaymentMethod>> GetPaymentMethods(int appointmentType)
        {
            //PaymentMethodsComponent PaymentMethodsComponent = new PaymentMethodsComponent(clinic);
            List<PaymentMethod> PaymentMethod = new List<PaymentMethod>();
            try
            {
                var res = clinic.GetPaymentMethods().Result;
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

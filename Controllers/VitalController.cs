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
                //var res = clinic.GetVitals().Result;
                return clinic.GetVitals().Result.Where(i => i.appointmentId == appointmentId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        [HttpPost("TransferFromVital")]
        public async Task<bool> TransferFromVital([FromBody] Models.clinic.Vital vitals)
        {
            try
            {
                var appointment = await clinic.GetAppointmentByappointmentId(vitals.appointmentId);
                switch (vitals.status)
                {
                    case 1:
                        var consultationRecord = new Consultations
                        {
                            appointmentId = vitals.appointmentId
                        };
                        var clinicCreateAppointmentResult = await clinic.CreateConsultation(consultationRecord);
                        appointment.appointmentStatus = vitals.status;
                        await clinic.UpdateAppointment(vitals.appointmentId, appointment);
                        vitals.status = 1;
                        break;
                    default:
                        break;
                }

                var clinicUpdateAppointmentResult = await clinic.UpdateVital(vitals.vRecordId, vitals);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error TransferFromVital: "+ex.ToString());
                return false;
            }
        }
    }
}

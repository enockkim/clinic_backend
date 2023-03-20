using clinic.Models.clinic;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LaboratoryController : ControllerBase
    {
        private ClinicService clinic;

        public LaboratoryController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        [HttpPost("CreateLaboratoryRequest")]
        public async Task<Laboratory> CreateLaboratoryRequest([FromBody] Models.clinic.Laboratory laboratoryRequest)
        {
            //AddLaboratoryComponent addLaboratoryComponent = new AddLaboratoryComponent(clinic);
            try
            {
                //return addLaboratoryComponent.CreateLaboratoryRequest(createLaboratory).Result;
                laboratoryRequest.status = 0;
                laboratoryRequest.date = DateTime.Now;
                //laboratoryRequest.appointmentId = appointmentId;
                var clinicCreateLaboratoryResult = await clinic.CreateLaboratory(laboratoryRequest);

                var labTypes = clinic.GetLaboratoryTypes().Result.Where(i => i.labTypeID == laboratoryRequest.labTypeId).Select(i => i.labTypeName).FirstOrDefault();
                var paymentMethod = clinic.GetAppointmentByappointmentId(laboratoryRequest.appointmentId).Result.paymentMethod;
                var billNo = clinic.GetBills().Result.Where(i => i.appointmentId == laboratoryRequest.appointmentId).Select(i => i.billNo).FirstOrDefault();
                var cost = clinic.GetLaboratoryTypes().Result.Where(i => i.labTypeID == laboratoryRequest.labTypeId).Select(i => i.cost).FirstOrDefault();
                BillDetail billDetail = new BillDetail()
                {
                    billNo = billNo,
                    cost = cost,
                    details = labTypes,
                    facility = 3,
                    status = paymentMethod == 1 ? 0 : 2
                };
                await clinic.CreateBillDetail(billDetail);

                //update bill status
                var bill = await clinic.GetBillBybillNo(billNo);
                bill.status = billDetail.status;
                await clinic.UpdateBill(billNo, bill);

                return clinicCreateLaboratoryResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CreateLaboratoryRequest: " + ex.ToString());
                return null;
            }
        }


        [HttpGet("GetLaboratoryRequestsByAppointmentId")]
        public ActionResult<IEnumerable<Laboratory>> GetLaboratoryRequestsByAppointmentId(int appointmentId)
        {
            //LaboratorysComponent LaboratorysComponent = new LaboratorysComponent(clinic);
            List<Laboratory> Laboratory = new List<Laboratory>();
            try
            {
                var res = clinic.GetLaboratories().Result;
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return res.Where(i => i.appointmentId == appointmentId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        [HttpGet("GetLaboratoryTypes")]
        public ActionResult<IEnumerable<LaboratoryType>> GetLaboratoryTypes()
        {
            //LaboratorysComponent LaboratorysComponent = new LaboratorysComponent(clinic);
            List<Laboratory> Laboratory = new List<Laboratory>();
            try
            {
                var res = clinic.GetLaboratoryTypes().Result;
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return res.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }


        [HttpPost("")]
        public async Task<bool> TransferLab([FromBody] Models.clinic.Laboratory createLaboratory)
        {
            //LaboratoriesComponent LaboratoriesComponent = new LaboratoriesComponent(clinic);
            try
            {
                //return LaboratoriesComponent.TransferLab(createLaboratory).Result;
                bool success = false;
                var appointment = clinic.GetAppointmentByappointmentId(createLaboratory.appointmentId).Result;
                var astatus = createLaboratory.status;
                //createLaboratorys.Appointment.appointmentStatus = astatus;

                var clinicGetLaboratoriesResult = await clinic.GetLaboratories();
                var getLaboratoriesRes = clinicGetLaboratoriesResult.ToList();

                switch (appointment.previousFacility)
                {
                    case 1:
                    case 15:
                        createLaboratory.status = 1;
                        await clinic.UpdateLaboratory(createLaboratory.labId, createLaboratory);
                        success = true;
                        break;
                    default:
                        break;
                }

                if (success && getLaboratoriesRes.Where(i => i.appointmentId == createLaboratory.appointmentId && i.status == 0).Count() == 0)
                {
                    appointment.appointmentStatus = appointment.previousFacility;
                    appointment.previousFacility = 4;
                    await clinic.UpdateAppointment(createLaboratory.appointmentId, appointment);
                    //NotificationService.Notify(NotificationSeverity.Success, $"Success", $"Lab results saved. Patient successfully transfered.");
                    //selectedRecord = new List<clinic.Models.clinic.Laboratory>() { };
                }
                else
                {
                    //NotificationService.Notify(NotificationSeverity.Success, $"Success", $"Lab results saved!");
                    //NotificationService.Notify(NotificationSeverity.Info, $"Pending", $"Please clear remaining pending lab requests.");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("TransferLab: " + ex.ToString());
                return false;
            }
        }
    }
}

using clinic.Models.clinic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace clinic.Controllers
{
    [Route("[controller]")]
    public class DiagnosticImagingController : ControllerBase
    {       //[HttpPost("AddDiagnosticImaging")]
        private ClinicService clinic;

        public DiagnosticImagingController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        //[HttpPost("")]
        //public bool AddDiagnosticImaging([FromBody] Models.clinic.DiagnosticImaging createDiagnosticImaging)
        //{
        //    AddDiagnosticImagingComponent addDiagnosticImagingComponent = new AddDiagnosticImagingComponent(clinic);
        //    try
        //    {
        //        return addDiagnosticImagingComponent.CreateDiagnosticImaging(createDiagnosticImaging).Result;
        //    }catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}


        [HttpGet("GetDiagnosticImagingRequestsByAppointmentId")]
        public ActionResult<IEnumerable<DiagnosticImagingRequest>> GetDiagnosticImagingRequestsByAppointmentId(int appointmentId)
        {
            //DiagnosticImagingsComponent DiagnosticImagingsComponent = new DiagnosticImagingsComponent(clinic);
            List<DiagnosticImagingRequest> DiagnosticImaging = new List<DiagnosticImagingRequest>();
            try
            {
                var res = clinic.GetDiagnosticImagingRequests().Result;
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return res.Where(i => i.appointmentId == appointmentId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        [HttpGet("GetDiagnosticImagingTypes")]
        public ActionResult<IEnumerable<ImagingType>> GetDiagnosticImagingTypes()
        {
            List<ImagingType> ImagingTypes = new List<ImagingType>();
            try
            {
                var types = clinic.GetDiagnosticImagingTypes().Result.ToList();
                var subtypes = clinic.GetDiagnositcImagingSubtypes().Result.ToList();

                foreach (var type in types)
                {
                    var imagingType = new ImagingType();
                    imagingType.imagingTypeId = type.imagingTypeId;
                    imagingType.imagingType = type.imagingType;
                    imagingType.subtypes = new List<DiagnositcImagingSubtype>();

                    foreach (var subtype in subtypes)
                    {
                        if (subtype.imagingTypeId == type.imagingTypeId)
                        {
                            imagingType.subtypes.Add(subtype);
                        }
                    }

                    ImagingTypes.Add(imagingType);
                }

                return ImagingTypes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        [HttpPost("CreateImagingRequest")]
        public async Task<bool> CreateImagingRequest([FromBody] DiagnosticImagingRequest diagnosticImagingRequest)
        {
            //AddDiagnosticImagingRequestComponent addDiagImgReqCmp = new AddDiagnosticImagingRequestComponent(clinic);
            try
            {
                //var res = addDiagImgReqCmp.CreateImagingRequest(diagnosticImagingRequest).Result;
                var clinicGetDiagnositcImagingSubtypesResult = await clinic.GetDiagnositcImagingSubtypes();
                var clinicGetDiagnosticImagingTypesResult = await clinic.GetDiagnosticImagingTypes();
                int paymentMethod = (int)clinic.GetAppointmentByappointmentId(diagnosticImagingRequest.appointmentId).Result.paymentMethod;

                diagnosticImagingRequest.status = 0;
                //diagnosticImagingRequest.appointmentId = appointmentId;
                diagnosticImagingRequest.datetimeOfRequest = DateTime.Now;
                var clinicCreateDiagnosticImagingRequestResult = await clinic.CreateDiagnosticImagingRequest(diagnosticImagingRequest);

                var imageTypeId = clinicGetDiagnositcImagingSubtypesResult.Where(i => i.imagingSubTypeId == diagnosticImagingRequest.imagingSubType).Select(i => i.imagingTypeId).FirstOrDefault();
                var imageType = clinicGetDiagnosticImagingTypesResult.Where(i => i.imagingTypeId == imageTypeId).Select(i => i.imagingType).FirstOrDefault();
                var imageSubType = clinicGetDiagnositcImagingSubtypesResult.Where(i => i.imagingSubTypeId == diagnosticImagingRequest.imagingSubType).Select(i => i.imagingSubType).FirstOrDefault();

                var billNo = clinic.GetBills().Result.Where(i => i.appointmentId == diagnosticImagingRequest.appointmentId).Select(i => i.billNo).FirstOrDefault();
                var cost = clinic.GetDiagnositcImagingSubtypes().Result.Where(i => i.imagingSubTypeId == diagnosticImagingRequest.imagingSubType).Select(i => i.cost).FirstOrDefault();
                BillDetail billDetail = new BillDetail()
                {
                    billNo = billNo,
                    cost = cost,
                    details = imageType + " - " + imageSubType,
                    facility = 4,
                    status = paymentMethod == 1 ? 0 : 2
                };
                await clinic.CreateBillDetail(billDetail);

                //update bill status
                var bill = await clinic.GetBillBybillNo(billNo);
                bill.status = billDetail.status;
                await clinic.UpdateBill(billNo, bill);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CreateImagingRequest: " + ex.Message);
                return false;
            }
        }


        [HttpPost("TransferDiagImg")]
        public async Task<bool> TransferDiagImg([FromBody] Models.clinic.DiagnosticImagingRequest diagnosticImagingRequest)
        {
            //DiagnosticImagingRequestsComponent diagnosticImagingRequestsComponent = new DiagnosticImagingRequestsComponent(clinic);
            bool success = false;
            try
            {
                //return diagnosticImagingRequestsComponent.TransferAppointment(diagnosticImagingRequest).Result;
                var clinicGetDiagnosticImagingRequestsResult = await clinic.GetDiagnosticImagingRequests();
                var appointment = clinic.GetAppointmentByappointmentId(diagnosticImagingRequest.appointmentId).Result;
                switch (appointment.previousFacility)
                {
                    case 1:
                    case 15:
                        diagnosticImagingRequest.status = 1;
                        await clinic.UpdateDiagnosticImagingRequest(diagnosticImagingRequest.imagingRequestId, diagnosticImagingRequest);
                        success = true;
                        break;
                    default:
                        break;
                }

                if (success && clinicGetDiagnosticImagingRequestsResult.Where(i => i.appointmentId == diagnosticImagingRequest.appointmentId && i.status == 0).Count() == 0)
                {
                    appointment.appointmentStatus = appointment.previousFacility; //send back to previous facility
                    appointment.previousFacility = 4;
                    await clinic.UpdateAppointment(diagnosticImagingRequest.appointmentId, appointment);
                    return true;
                    //selectedRecord = new List<clinic.Models.clinic.Laboratory>() { };
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TransferDiagImg: " + ex.ToString());
                return false;
            }
        }
    }
}

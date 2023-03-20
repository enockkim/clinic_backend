using clinic.Models.clinic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationController : ControllerBase
    {       //[HttpPost("AddOperation")]
        private ClinicService clinic;

        public OperationController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        //[HttpPost("")]
        //public bool AddOperation([FromBody] Models.clinic.Operation createOperation)
        //{
        //    AddOperationComponent addOperationComponent = new AddOperationComponent(clinic);
        //    try
        //    {
        //        return addOperationComponent.CreateOperation(createOperation).Result;
        //    }catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}


        [HttpGet("GetOperationRequestsByAppointmentId")]
        public ActionResult<IEnumerable<OperationRequest>> GetOperationRequestsByAppointmentId(int appointmentId)
        {
            //OperationsComponent OperationsComponent = new OperationsComponent(clinic);
            List<OperationRequest> Operations = new List<OperationRequest>();
            try
            {
                var res = clinic.GetOperationRequests().Result;
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return res.Where(i => i.appointmentId == appointmentId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        [HttpGet("GetOperationTypes")]
        public ActionResult<IEnumerable<OpType>> GetOperationTypes()
        {
            List<OpType> operationTypes = new List<OpType>();
            try
            {
                var types = clinic.GetOperationTypes().Result.ToList();
                var subtypes = clinic.GetOperationSubtypes().Result.ToList();

                foreach (var type in types)
                {
                    var operationType = new OpType();
                    operationType.operationTypeId = type.operationTypeId;
                    operationType.operationName = type.operationName;
                    operationType.subtypes = new List<OperationSubtype>();

                    foreach (var subtype in subtypes)
                    {
                        if (subtype.operationTypeId == type.operationTypeId)
                        {
                            operationType.subtypes.Add(subtype);
                        }
                    }

                    operationTypes.Add(operationType);
                }

                return operationTypes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        [HttpPost("CreateOperationRequest")]
        public async Task<bool> CreateOperationRequest([FromBody] OperationRequest operationRequest)
        {
            //AddOperationRequestComponent addOperationRequest = new AddOperationRequestComponent(clinic);
            try
            {
                //var res = addOperationRequest.CreateOperationRequest(operationRequest).Result;
                operationRequest.status = 0;
                //operationRequest.appointmentId = appointmentId;
                operationRequest.datetimeOfRequest = DateTime.Now;
                var _clinicCreateDiagnosticImagingRequestResult = await clinic.CreateOperationRequest(operationRequest);
                var clinicGetOperationSubtypesResult = await clinic.GetOperationSubtypes();
                var clinicGetAppointmentsResult = await clinic.GetAppointments();
                var clinicGetOperationTypesResult = await clinic.GetOperationTypes();
                var appointment = clinic.GetAppointmentByappointmentId(operationRequest.appointmentId).Result;

                var operationTypeId = clinicGetOperationSubtypesResult.Where(i => i.operationSubTypeId == operationRequest.operationSubType).Select(i => i.operationTypeId).FirstOrDefault();
                var operationType = clinicGetOperationTypesResult.Where(i => i.operationTypeId == operationTypeId).Select(i => i.operationName).FirstOrDefault();
                var operationSubType = clinicGetOperationSubtypesResult.Where(i => i.operationSubTypeId == operationRequest.operationSubType).Select(i => i.operationSubType1).FirstOrDefault();

                var billNo = clinic.GetBills().Result.Where(i => i.appointmentId == operationRequest.appointmentId).Select(i => i.billNo).FirstOrDefault();
                var cost = clinic.GetDiagnositcImagingSubtypes().Result.Where(i => i.imagingSubTypeId == operationRequest.operationSubType).Select(i => i.cost).FirstOrDefault();
                BillDetail billDetail = new BillDetail()
                {
                    billNo = billNo,
                    cost = cost,
                    details = operationType + " - " + operationSubType,
                    facility = 5,
                    status = appointment.paymentMethod == 1 ? 0 : 2
                };
                await clinic.CreateBillDetail(billDetail);


                //update bill status
                var bill = await clinic.GetBillBybillNo(billNo);
                bill.status = billDetail.status;
                await clinic.UpdateBill(billNo, bill);


                //update appointment
                //appointment.employeeId = Security.GetUsers().Result.Select(u => u.employeeId).FirstOrDefault();

                if (appointment.paymentMethod == 1)
                {
                    appointment.appointmentStatus = 13;
                    appointment.previousFacility = 5;
                    //NotificationService.Notify(NotificationSeverity.Success, $"Accounts", $"Operation request created, patient transfered to cashier to clear bill.");
                }
                else
                {
                    appointment.previousFacility = appointment.appointmentStatus;
                    appointment.appointmentStatus = 5;
                    //NotificationService.Notify(NotificationSeverity.Success, $"Success", $"Operation request created");
                }


                await clinic.UpdateAppointment(appointment.appointmentId, appointment);

                //NotificationService.Notify(NotificationSeverity.Success, $"Success", $"Diagnostic imaging request created.");
                //DialogService.Close(operationrequest);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CreateOperationRequest: " + ex.Message);
                return false;
            }
        }

        [HttpPost("TransferOperationRequest")]
        public async Task<bool> TransferOperationRequest([FromBody] OperationRequest operationRequest)
        {
            try
            {
                var appointment = clinic.GetAppointmentByappointmentId(operationRequest.appointmentId).Result;
                switch (appointment.previousFacility)
                {
                    case 1:
                    case 15:
                        operationRequest.status = 1;
                        await clinic.UpdateOperationRequest(operationRequest.operationRequestId, operationRequest);
                        break;
                    default:
                        break;
                }

                appointment.appointmentStatus = appointment.previousFacility;
                appointment.previousFacility = 5;
                await clinic.UpdateAppointment(appointment.appointmentId, appointment);
                return true;
            }
            catch (Exception clinicCreateAppointmentException)
            {
                Console.WriteLine("TransferOperationRequest: " + clinicCreateAppointmentException.Message);
                return false;
            }
        }

    }
}

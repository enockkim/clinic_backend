using clinic.Models.clinic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math.EC.Endo;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {       //[HttpPost("AddAppointment")]
        private ClinicService clinic;

        public AppointmentController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        [HttpPost("AddAppointment")]
        public async Task<bool> AddAppointment([FromBody] Models.clinic.Appointment appointment)
        {
            //AddAppointmentComponent addAppointmentComponent = new AddAppointmentComponent(clinic);
            try
            {
                //return addAppointmentComponent.CreateAppointment(createAppointment).Result;

                appointment.appointmentStatus = 0;
                //appointment.createdBy = Security.User.Id;
                appointment.dateOfCreation = DateTime.Now;
                appointment.patientType = clinic.GetAppointmentTypes().Result.Where(i => i.typeId == appointment.appointmentType).Select(i => i.patientType).FirstOrDefault();
                var clinicCreateAppointmentResult = await clinic.CreateAppointment(appointment);

                Bill bill = new Bill()
                {
                    appointmentId = clinicCreateAppointmentResult.appointmentId,
                    status = 0
                };
                await clinic.CreateBill(bill);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        [HttpGet("GetAppointmentTypes")]
        public async Task<List<AppointmentType>> GetAppointmentTypes()
        {
            try
            {
                return clinic.GetAppointmentTypes().Result.ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine("GetAppointmentTypes error: ",ex.Message);
                return null;
            }
        }

        [HttpGet("GetAppointments")]
        public async Task<List<AppointmentData>> GetAppointments([FromQuery] int appointmentType, int appointmentStatus)
        {
            //AppointmentsComponent AppointmentsComponent = new AppointmentsComponent(clinic);
            List<AppointmentData> AppointmentData = new List<AppointmentData>();
            try
            {
                //var res = AppointmentsComponent.GetAppointments(appointmentType, appointmentStatus).Result;  
                var appointments = await clinic.GetAppointments();
                var appType = appointmentType;
                var res = appointments.Where(i => i.appointmentStatus == appointmentStatus).ToList();          
                //var res = appointmentType == 0
                //    ?
                //    //all appointment types
                //    appointments.Where(i => i.appointmentStatus == appointmentStatus).ToList()
                //    :
                //    appointments.Where(i => i.appointmentStatus == appointmentStatus && i.appointmentType == appointmentType).ToList();


                foreach (var item in res)
                {
                    AppointmentData.Add(new AppointmentData()
                    {
                        appointmentId = item.appointmentId,
                        patientId = item.patientId,
                        patientName = item.Patient.surname + ", " + item.Patient.otherName,
                        employeeId = item.employeeId,
                        employeeName = item.Employee == null ? null : (item.Employee.surname + ", " + item.Employee.otherName),
                        dateOfAppointment = item.dateOfAppointment,
                        remarks = item.remarks,
                        appointmentStatus = item.appointmentStatus,
                        facilityName = item.Facilities == null ? null : item.Facilities.facilityName,
                        paymentMethod = item.paymentMethod,
                        paymentMethodName = item.PaymentMethod1 == null ? null : item.PaymentMethod1.method,
                        appointmentType = item.appointmentType,
                        appointmentTypeName = item.AppointmentType1 == null ? null : item.AppointmentType1.type,
                        dateOfCreation = item.dateOfCreation,
                        createdBy = item.createdBy,
                        patientType = item.patientType,
                        patientTypeName = item.PatientType.patientType,
                        previousFacility = item.previousFacility,
                        previousFacilityName = item.Facilities == null ? null : item.Facilities.facilityName
                    });
                }
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return AppointmentData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        [HttpPost("TransferPatient")]
        public async Task<int> TransferPatient([FromBody] Models.clinic.AppointmentData appointmentData)
        {
            //// Create a mapping between the AppointmentData and Appointment classes
            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<AppointmentData, Models.clinic.Appointment>();
            //});

            //// Create a mapper instance based on the configuration
            //var mapper = new Mapper(config);

            //var appoointmentUpdate = mapper.Map<Models.clinic.Appointment>(appointmentData);

            int newFacility = 0;

            var appoointmentUpdate = new Models.clinic.Appointment()
            {
                appointmentId = appointmentData.appointmentId,
                patientId = appointmentData.patientId,
                employeeId = appointmentData.employeeId,
                dateOfAppointment = appointmentData.dateOfAppointment,
                remarks = appointmentData.remarks,
                appointmentStatus = appointmentData.appointmentStatus,
                paymentMethod = appointmentData.paymentMethod,
                appointmentType = appointmentData.appointmentType,
                dateOfCreation = appointmentData.dateOfCreation,
                createdBy = appointmentData.createdBy,
                patientType = appointmentData.patientType,
                previousFacility = appointmentData.previousFacility
            };


            //EditAppointmentComponent editAppointmentComponent = new EditAppointmentComponent(clinic);
            try
            {
                //return editAppointmentComponent.TransferPatient(appoointmentUpdate).Result;
                var consultationRecord = new Consultations();
                switch (appoointmentUpdate.appointmentStatus)
                {
                    case 1:
                        consultationRecord = new Consultations
                        {
                            appointmentId = appoointmentUpdate.appointmentId,
                            status = 0
                        };
                        await clinic.CreateConsultation(consultationRecord);
                        appoointmentUpdate.appointmentType = 3;
                        appoointmentUpdate.patientType = 2;
                        newFacility = await ProcessPatientTransfer("consultation fee", appoointmentUpdate, 2);
                        break;
                    case 2:
                        appoointmentUpdate.patientType = 1;
                        newFacility = await ProcessPatientTransfer("maternity fee", appoointmentUpdate, 1);
                        break;
                    case 6:
                        appoointmentUpdate.patientType = 1;
                        newFacility = await ProcessPatientTransfer("emergency and casualty fee", appoointmentUpdate, 1);
                        break;
                    case 7:
                        appoointmentUpdate.patientType = 1;
                        newFacility = await ProcessPatientTransfer("ward admission fee", appoointmentUpdate, 1);
                        break;
                    case 8:
                        appoointmentUpdate.patientType = 1;
                        newFacility = await ProcessPatientTransfer("icu admission fee", appoointmentUpdate, 1);
                        break;
                    case 9:
                        appoointmentUpdate.patientType = 1;
                        newFacility = await ProcessPatientTransfer("moturary fee", appoointmentUpdate, 1);
                        break;
                    case 15:
                        consultationRecord = new Consultations
                        {
                            appointmentId = appoointmentUpdate.appointmentId,
                            status = 0
                        };
                        await clinic.CreateConsultation(consultationRecord);
                        appoointmentUpdate.appointmentType = 1;
                        appoointmentUpdate.patientType = 2;
                        newFacility = await ProcessPatientTransfer("dental consultation fee", appoointmentUpdate, 2);
                        break;
                    case 10:
                        appoointmentUpdate.appointmentType = 3;
                        appoointmentUpdate.patientType = 2;
                        newFacility = await ProcessPatientTransfer("vital fee", appoointmentUpdate, 2);
                        //create vital record
                        var triageRecord = new Vital
                        {
                            appointmentId = appoointmentUpdate.appointmentId,
                            status = 0
                        };
                        await clinic.CreateVital(triageRecord);
                        break;
                    default:
                        //NotificationService.Notify(NotificationSeverity.Warning, $"Incorrect Facility", $"Unable to transfer patient to chosen facility. Kindly choose a differnt facility.", 5000);
                        break;
                }

                return newFacility;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return newFacility;
            }
        }

        [NonAction]
        public async Task<int> ProcessPatientTransfer(string detail, Appointment args, int patientType)
        {
            //Create bill record
            var billed = clinic.GetBillDetails().Result.Where(i => i.Bill.appointmentId == args.appointmentId && i.facility == 1).Any();
            if (!billed)
            {
                var billNo = clinic.GetBills().Result.Where(i => i.appointmentId == args.appointmentId).Select(i => i.billNo).FirstOrDefault();
                int? employeeId = args.employeeId;
                var doctorTypeId = employeeId == null ? 6 : clinic.GetDoctors().Result.Where(i => i.employeeId == employeeId).Select(i => i.doctorType).FirstOrDefault();
                var cost = clinic.GetDoctorTypes().Result.Where(i => i.doctorTypeId == doctorTypeId).Select(c => c.consultationCost).FirstOrDefault();
                BillDetail billDetail = new BillDetail()
                {
                    billNo = billNo,
                    cost = cost,
                    details = detail,
                    facility = args.appointmentStatus,
                    status = args.paymentMethod == 1 ? 0 : 2 //check payment method, if cash send to accounts to clear payment
                };
                await clinic.CreateBillDetail(billDetail);

                //update appoinment status to new facility depending on payment method, ie cash? send to accounts
                if (args.paymentMethod == 1)
                {
                    if((args.appointmentType == 3 || args.appointmentType == 2) && args.previousFacility == 0)
                    {
                        args.previousFacility = 0; //set to pending -> triage

                        //create blank vital record
                        clinic.CreateVital(new Vital()
                        {
                            appointmentId = args.appointmentId,
                            status = 0
                        });
                    }
                    else 
                    {
                        args.previousFacility = args.appointmentStatus;
                    }

                    args.appointmentStatus = 13;
                }
                else
                {
                    args.appointmentStatus = args.appointmentStatus;
                }

                //appointment.appointmentStatus = appointment.paymentMethod == 1 ? 13 : args.appointmentStatus;
                var clinicUpdateAppointmentResult = await clinic.UpdateAppointment(args.appointmentId, args);
            }


            List<int> unavailableFacilities = new List<int> { 0, 3, 4, 5, 12, 13, 14, 16 };
            var clinicGetFacilitiesResult = await clinic.GetFacilities();
            var getFacilityStatusRes = clinicGetFacilitiesResult.Where(i => !unavailableFacilities.Contains(i.facilityId));
            //Show notification
            var facilityName = getFacilityStatusRes.Where(i => i.facilityId == args.appointmentStatus).Select(i => i.facilityName).FirstOrDefault();
            if (args.paymentMethod == 1)
                facilityName = "Cashier";

            return args.appointmentStatus;
        }


        [HttpPost("EditAppointment")]
        public bool EditAppointment([FromBody] Models.clinic.Appointment editAppointmentData)
        {
            //AddAppointmentComponent addAppointmentComponent = new AddAppointmentComponent(clinic);
            try
            {
                //Models.clinic.Appointment editedAppointmentData = new Models.clinic.Appointment()
                //{
                //    appointmentId = editAppointmentData.appointmentId,
                //    patientId = editAppointmentData.patientId,
                //    employeeId = editAppointmentData.employeeId,
                //    dateOfAppointment = editAppointmentData.dateOfAppointment,
                //    remarks = editAppointmentData.remarks,
                //    appointmentStatus = editAppointmentData.appointmentStatus,
                //    paymentMethod = editAppointmentData.paymentMethod,
                //    appointmentType = editAppointmentData.appointmentType,
                //    dateOfCreation = editAppointmentData.dateOfCreation,
                //    createdBy = editAppointmentData.createdBy,
                //    patientType = editAppointmentData.patientType,
                //    previousFacility = editAppointmentData.previousFacility
                //};
                clinic.UpdateAppointment(editAppointmentData.appointmentId, editAppointmentData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        [HttpPost("TransferAppointment")]
        public async Task<bool> TransferAppointment([FromBody] AppointmentData newAppointmentData)
        {
            switch (newAppointmentData.appointmentStatus)
            {
                case 3:
                    //appointmentData.employeeId = Security.GetUsers().Result.Select(u => u.employeeId).FirstOrDefault();
                    if (newAppointmentData.paymentMethod == 1)
                    {
                        newAppointmentData.appointmentStatus = 13;
                        newAppointmentData.previousFacility = 3;
                        //NotificationService.Notify(NotificationSeverity.Success, $"Accounts", $"Laboratory request created, patient transfered to cashier to clear bill.");
                    }
                    else
                    {
                        newAppointmentData.appointmentStatus = 3;
                        //NotificationService.Notify(NotificationSeverity.Success, $"Success", $"Laboratory request created, patient may proceed to laboratory.");
                    }
                    break;
                case 4:
                    if (newAppointmentData.paymentMethod == 1)
                    {
                        newAppointmentData.appointmentStatus = 13;
                        newAppointmentData.previousFacility = 4;
                    }
                    else
                    {
                        newAppointmentData.appointmentStatus = 4;
                    }
                    break;
                case 5:
                    if (newAppointmentData.paymentMethod == 1)
                    {
                        newAppointmentData.appointmentStatus = 13;
                        newAppointmentData.previousFacility = 5;
                    }
                    else
                    {
                        newAppointmentData.appointmentStatus = 5;
                    }
                    break;
                case 12:
                    if (newAppointmentData.paymentMethod == 1)
                    {
                        newAppointmentData.appointmentStatus = 13;
                        newAppointmentData.previousFacility = 12;
                    }
                    else
                    {
                        newAppointmentData.appointmentStatus = 12;
                    }
                    break;
                default:
                    return false;
            }

            Models.clinic.Appointment transferedAppointment = new Models.clinic.Appointment()
            {
                appointmentId = newAppointmentData.appointmentId,
                patientId = newAppointmentData.patientId,
                employeeId = newAppointmentData.employeeId,
                dateOfAppointment = newAppointmentData.dateOfAppointment,
                remarks = newAppointmentData.remarks,
                appointmentStatus = newAppointmentData.appointmentStatus,
                paymentMethod = newAppointmentData.paymentMethod,
                appointmentType = newAppointmentData.appointmentType,
                dateOfCreation = newAppointmentData.dateOfCreation,
                createdBy = newAppointmentData.createdBy,
                patientType = newAppointmentData.patientType,
                previousFacility = newAppointmentData.previousFacility
            };

            try
            {
                await clinic.UpdateAppointment(newAppointmentData.appointmentId, transferedAppointment);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPost("ClearPatient")]
        public async Task<bool> ClearPatient([FromQuery] int appointmentId)
        {
            try
            {
                using (var connection = new MySqlConnection("Server=localhost;Database=clinic;User ID=remote;Password=#3PqKZ$F3G=y9NSD;Connection Timeout=100"))
                {
                    connection.Open();

                    //clear appointment
                    var query = $"UPDATE appointment set appointmentStatus = 14 where appointmentId = {appointmentId}";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    //clear prescription
                    var prescriptionItems = clinic.GetPrescriptions().Result.Where(i => i.appointmentId == appointmentId).ToList();

                    foreach(var prescription in prescriptionItems)
                    {
                        query = $"UPDATE `clinic`.`prescription` SET `status` = '1' WHERE (`prescriptionId` = '{prescription.prescriptionId}');";
                        cmd = new MySqlCommand(query, connection);
                        cmd.ExecuteNonQuery();

                        if(prescription.availability == 1)
                        {
                            var stock = clinic.GetInventoryByitemId(prescription.itemId).Result.stock;
                            var newStock = stock - prescription.dosageNumber;

                            query = $"UPDATE `clinic`.`inventory` SET `stock` = '{newStock}' WHERE (`itemId` = '{prescription.itemId}');";
                            cmd = new MySqlCommand(query, connection);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    connection.Close();
                }

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("ClearPatient: "+ex.Message);
                return false;
            }
        }
    }
}

using clinic.Models.clinic;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Route("api/[controller]")]
    public class FinanceController : ControllerBase
    {       //[HttpPost("AddFinance")]
        private ClinicService clinic;

        public FinanceController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        //[HttpPost("")]
        //public bool AddFinance([FromBody] Models.clinic.Finance createFinance)
        //{
        //    AddFinanceComponent addFinanceComponent = new AddFinanceComponent(clinic);
        //    try
        //    {
        //        return addFinanceComponent.CreateFinance(createFinance).Result;
        //    }catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}


        [HttpGet("GetBills")]
        public ActionResult<IEnumerable<Bill>> GetBills()
        {
            //FinancesComponent FinancesComponent = new FinancesComponent(clinic);
            List<Bill> Finance = new List<Bill>();
            try
            {
                var res = clinic.GetBills().Result;
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return res.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetBills: " + ex.ToString());
                return null;
            }
        }
         
        [HttpGet("GetBillDetailsByBillNo")]
        public ActionResult<IEnumerable<BillDetail>> GetBillDetailsByBillNo(int billNo)  
        {
            //FinancesComponent FinancesComponent = new FinancesComponent(clinic);
            List<BillDetail> Finance = new List<BillDetail>();
            try
            {
                var res = clinic.GetBillDetails().Result;
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return res.Where(i => i.billNo == billNo).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetBills: " + ex.ToString());
                return null;
            }
        }

        [HttpGet("GetCashTypes")]
        public ActionResult<IEnumerable<CashType>> GetCashTypes()
        {
            try
            {
                var res = clinic.GetCashType().Result;
                return res.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetCashTypes: " + ex.Message);
                return null;
            }
        }

        [HttpPost("PayBill")]
        public async Task<bool> PayBill([FromBody] AccountsReceivable paymentDetails)
        {
            //AddAccountsReceivableComponent FinancesComponent = new AddAccountsReceivableComponent(clinic);
            try
            {
                //await FinancesComponent.PayBill(paymentDetails);
                var billNo = paymentDetails.billDetailEntryNo;
                var bill = clinic.GetBillBybillNo(billNo).Result;

                var appointment = clinic.GetAppointmentByappointmentId(clinic.GetBillBybillNo(billNo).Result.appointmentId).Result;
                var details = await clinic.GetBillDetails();

                var cash = bill.Appointment.paymentMethod == 1 || bill.Appointment.paymentMethod == 4;

                paymentDetails.paymentMethod = (int)bill.Appointment.paymentMethod;

                if (paymentDetails.paymentMethod == 1 || paymentDetails.paymentMethod == 4)
                {


                    //await DialogService.OpenAsync<Reports.PrintReceipt>("Print Receipt", new Dictionary<string, object>() { { "billNo", billNo } });
                    //update appointment record status


                    appointment.appointmentStatus = appointment.previousFacility; // (int)clinic.GetBillDetails().Result.OrderByDescending(i => i.entryNo).Where(i => i.billNo == billNo).Select(i => i.facility).FirstOrDefault();
                    await clinic.UpdateAppointment(appointment.appointmentId, appointment);

                    var billDetails = details.Where(i => i.billNo == billNo && (i.status == 0 || i.status == 2));

                    List<BillDetail> updatedBillDetails = new List<BillDetail>();
                    List<AccountsReceivable> newTransaction = new List<AccountsReceivable>();

                    foreach (var item in billDetails)
                    {
                        item.status = 1;
                        updatedBillDetails.Add(item);

                        AccountsReceivable accountsReceivable = new AccountsReceivable()
                        {
                            dateOfTransaction = DateTime.Now,
                            paymentMethod = (int)appointment.paymentMethod,
                            amountDue = (int)item.cost,
                            amountPaid = (int)item.cost,
                            billDetailEntryNo = item.entryNo,
                            transactionRefrence = paymentDetails.transactionRefrence,
                            cashType1 = paymentDetails.cashType1
                        };

                        //Create accounts receivable record

                        newTransaction.Add(accountsReceivable);
                    }

                    foreach (var listItem in updatedBillDetails)
                    {
                        await clinic.UpdateBillDetail(listItem.entryNo, listItem);
                    }

                    foreach (var listItem in newTransaction)
                    {
                        await clinic.CreateAccountsReceivable(listItem);
                    }

                    bill.status = 1;
                    await clinic.UpdateBill(billNo, bill);

                    return true;

                    //NotificationService.Notify(NotificationSeverity.Success, $"Transfered", $"The bill is cleared and the patient has been transfered to " + clinic.GetFacilities().Result.Where(i => i.facilityId == appointment.appointmentStatus).Select(i => i.facilityName).FirstOrDefault(), 5000);
                }
                else if (paymentDetails.paymentMethod == 2)
                {
                    //update appointment record status
                    appointment = clinic.GetAppointmentByappointmentId(clinic.GetBillBybillNo(billNo).Result.appointmentId).Result;
                    appointment.appointmentStatus = 14;
                    await clinic.UpdateAppointment(appointment.appointmentId, appointment);

                    var billDetails = details.Where(i => i.billNo == billNo && i.status == 0);

                    List<BillDetail> updatedBillDetails = new List<BillDetail>();
                    List<AccountsReceivable> newTransaction = new List<AccountsReceivable>();

                    foreach (var item in billDetails)
                    {
                        item.status = 1;
                        updatedBillDetails.Add(item);

                        //Create accounts receivable record
                        paymentDetails.dateOfTransaction = DateTime.Now;
                        paymentDetails.paymentMethod = (int)appointment.paymentMethod;
                        paymentDetails.amountDue = (int)item.cost;
                        paymentDetails.amountPaid = (int)item.cost;
                        paymentDetails.billDetailEntryNo = item.entryNo;

                        newTransaction.Add(paymentDetails);
                    }

                    foreach (var listItem in updatedBillDetails)
                    {
                        await clinic.UpdateBillDetail(listItem.entryNo, listItem);
                    }

                    foreach (var listItem in newTransaction)
                    {
                        await clinic.CreateAccountsReceivable(listItem);
                    }

                    bill.status = 1;
                    await clinic.UpdateBill(billNo, bill);

                    return true;

                    //NotificationService.Notify(NotificationSeverity.Success, $"Cleared", $"The patient bill has been cleared", 5000);

                }
                return false;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Paybill: " + ex.Message);
                return false;
            }
        }
    }
}

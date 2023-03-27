using clinic.Models.clinic;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PharmacyController : ControllerBase
    {       //[HttpPost("AddPharmacy")]
        private ClinicService clinic;

        public PharmacyController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        //[HttpPost("")]
        //public bool AddPharmacy([FromBody] Models.clinic.Pharmacy createPharmacy)
        //{
        //    AddPharmacyComponent addPharmacyComponent = new AddPharmacyComponent(clinic);
        //    try
        //    {
        //        return addPharmacyComponent.CreatePharmacy(createPharmacy).Result;
        //    }catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}


        [HttpGet("GetPrescriptionsByAppointmentId")]
        public ActionResult<IEnumerable<Prescription>> GetPrescriptionsByAppointmentId(int appointmentId)
        {
            //PharmacysComponent PharmacysComponent = new PharmacysComponent(clinic);
            List<Prescription> Pharmacy = new List<Prescription>();
            try
            {
                var res = clinic.GetPrescriptions().Result;
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return res.Where(i => i.appointmentId == appointmentId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        [HttpPost("AddToPrescription")]
        public async Task<bool> AddToPrescription([FromBody] Prescription prescriptionItem)
        {
            try
            {
                //laboratory.status = 0;
                //laboratory.date = DateTime.Now;
                //prescription.appointmentId = appointmentId;
                prescriptionItem.status = 0;

                var inventory = clinic.GetInventoryByitemId(prescriptionItem.itemId).Result;
                var appointment = clinic.GetAppointmentByappointmentId(prescriptionItem.appointmentId).Result;

                if (inventory.stock >= prescriptionItem.dosageNumber)
                {

                    prescriptionItem.availability = 1;
                    var clinicCreatePrescriptionResult = await clinic.CreatePrescription(prescriptionItem);

                    inventory.stock = inventory.stock - prescriptionItem.dosageNumber;
                    await clinic.UpdateInventory(inventory.itemId, inventory);
                    //args.Appointment.appointmentStatus = 12;
                    //await clinic.UpdateAppointment(args.Appointment.appointmentId, args.Appointment);

                    var billNo = clinic.GetBills().Result.Where(i => i.appointmentId == prescriptionItem.appointmentId).Select(i => i.billNo).FirstOrDefault();
                    var unitCost = clinic.GetInventories().Result.Where(i => i.itemId == prescriptionItem.itemId).Select(i => i.unitCost).FirstOrDefault();
                    var cost = unitCost * prescriptionItem.dosageNumber;
                    BillDetail billDetail = new BillDetail()
                    {
                        billNo = billNo,
                        cost = cost,
                        details = inventory.medication,
                        facility = 12,
                        status = appointment.paymentMethod == 1 ? 0 : 2
                    };
                    await clinic.CreateBillDetail(billDetail);

                    //update bill status
                    var bill = await clinic.GetBillBybillNo(billNo);
                    bill.status = billDetail.status;
                    await clinic.UpdateBill(billNo, bill);
                }
                else
                {
                    prescriptionItem.availability = 0;
                    var clinicCreatePrescriptionResult = await clinic.CreatePrescription(prescriptionItem);

                }

                return true;
            }
            catch (Exception clinicCreatePrescriptionException)
            {
                Console.WriteLine("AddToPrescription: " + clinicCreatePrescriptionException.Message);
                return false;
            }
        }

        [HttpGet("GetInventory")]
        public ActionResult<IEnumerable<Inventory>> GetInventory()
        {
            try
            {
                var res = clinic.GetInventories().Result;
                return res.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetInventory: " + ex.Message);
                return null;
            }
        }

        [HttpGet("GetInventoryCategories")]
        public ActionResult<IEnumerable<InventoryCategory>> GetInventoryCategories()
        {
            try
            {
                var res = clinic.GetInventoryCategories().Result;
                return res.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetInventoryCategories: " + ex.Message);
                return null;
            }
        }

        [HttpGet("GetUnitsOfMeasure")]
        public ActionResult<IEnumerable<UnitOfMeasure>> GetUnitsOfMeasure()
        {
            try
            {
                var res = clinic.GetUnitOfMeasures().Result;
                return res.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetUnitOfMeasures: " + ex.Message);
                return null;
            }
        }

        [HttpGet("GetAdministrationTypes")]
        public ActionResult<IEnumerable<AdministrationType>> GetAdministrationTypes()
        {
            try
            {
                var res = clinic.GetAdministrationTypes().Result;
                return res.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAdministrationTypes: " + ex.Message);
                return null;
            }
        }


        [HttpPost("AddCategory")]
        public InventoryCategory AddCategory([FromBody] InventoryCategory newCategory)
        {
            try
            {
                return clinic.CreateInventoryCategory(newCategory).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddCategory error: "+ex.ToString());
                return null;
            }
        }


        [HttpPost("AddMedication")]
        public Inventory AddMedication([FromBody] Inventory newMedication)
        {
            try
            {
                return clinic.CreateInventory(newMedication).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddMedication error: " + ex.ToString());
                return null;
            }
        }


        [HttpPost("AddStock")]
        public async Task<Inventory> AddStock([FromBody] AddStock newStock)
        {
            try
            {
                var currentStock = await clinic.GetInventoryByitemId(newStock.itemId);
                currentStock.stock += newStock.stock;
                await clinic.UpdateInventory(currentStock.itemId, currentStock);

                return currentStock;
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddStock error: " + ex.ToString());
                return null;
            }
        }

    }
}

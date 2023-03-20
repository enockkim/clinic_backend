//using Microsoft.AspNetCore.Mvc;
//using clinic.Data;

//namespace clinic
//{
//    public partial class ExportClinicController : ExportController
//    {
//        private readonly ClinicContext context;

//        public ExportClinicController(ClinicContext context)
//        {
//            this.context = context;
//        }

//        [HttpGet("/export/clinic/appointments/csv")]
//        public FileStreamResult ExportAppointmentsToCSV()
//        {
//            return ToCSV(ApplyQuery(context.Appointments, Request.Query));
//        }

//        [HttpGet("/export/clinic/appointments/excel")]
//        public FileStreamResult ExportAppointmentsToExcel()
//        {
//            return ToExcel(ApplyQuery(context.Appointments, Request.Query));
//        }

//        [HttpGet("/export/clinic/appointmenttypes/csv")]
//        public FileStreamResult ExportAppointmentTypesToCSV()
//        {
//            return ToCSV(ApplyQuery(context.AppointmentTypes, Request.Query));
//        }

//        [HttpGet("/export/clinic/appointmenttypes/excel")]
//        public FileStreamResult ExportAppointmentTypesToExcel()
//        {
//            return ToExcel(ApplyQuery(context.AppointmentTypes, Request.Query));
//        }

//        [HttpGet("/export/clinic/designations/csv")]
//        public FileStreamResult ExportDesignationsToCSV()
//        {
//            return ToCSV(ApplyQuery(context.Designations, Request.Query));
//        }

//        [HttpGet("/export/clinic/designations/excel")]
//        public FileStreamResult ExportDesignationsToExcel()
//        {
//            return ToExcel(ApplyQuery(context.Designations, Request.Query));
//        }

//        [HttpGet("/export/clinic/genders/csv")]
//        public FileStreamResult ExportGendersToCSV()
//        {
//            return ToCSV(ApplyQuery(context.Genders, Request.Query));
//        }

//        [HttpGet("/export/clinic/genders/excel")]
//        public FileStreamResult ExportGendersToExcel()
//        {
//            return ToExcel(ApplyQuery(context.Genders, Request.Query));
//        }

//        [HttpGet("/export/clinic/laboratories/csv")]
//        public FileStreamResult ExportLaboratoriesToCSV()
//        {
//            return ToCSV(ApplyQuery(context.Laboratories, Request.Query));
//        }

//        [HttpGet("/export/clinic/laboratories/excel")]
//        public FileStreamResult ExportLaboratoriesToExcel()
//        {
//            return ToExcel(ApplyQuery(context.Laboratories, Request.Query));
//        }

//        [HttpGet("/export/clinic/laboratorytypes/csv")]
//        public FileStreamResult ExportLaboratoryTypesToCSV()
//        {
//            return ToCSV(ApplyQuery(context.LaboratoryTypes, Request.Query));
//        }

//        [HttpGet("/export/clinic/laboratorytypes/excel")]
//        public FileStreamResult ExportLaboratoryTypesToExcel()
//        {
//            return ToExcel(ApplyQuery(context.LaboratoryTypes, Request.Query));
//        }

//        [HttpGet("/export/clinic/operationrooms/csv")]
//        public FileStreamResult ExportOperationRoomsToCSV()
//        {
//            return ToCSV(ApplyQuery(context.OperationRooms, Request.Query));
//        }

//        [HttpGet("/export/clinic/operationrooms/excel")]
//        public FileStreamResult ExportOperationRoomsToExcel()
//        {
//            return ToExcel(ApplyQuery(context.OperationRooms, Request.Query));
//        }

//        [HttpGet("/export/clinic/operationtypes/csv")]
//        public FileStreamResult ExportOperationTypesToCSV()
//        {
//            return ToCSV(ApplyQuery(context.OperationTypes, Request.Query));
//        }

//        [HttpGet("/export/clinic/operationtypes/excel")]
//        public FileStreamResult ExportOperationTypesToExcel()
//        {
//            return ToExcel(ApplyQuery(context.OperationTypes, Request.Query));
//        }

//        [HttpGet("/export/clinic/patients/csv")]
//        public FileStreamResult ExportPatientsToCSV()
//        {
//            return ToCSV(ApplyQuery(context.Patients, Request.Query));
//        }

//        [HttpGet("/export/clinic/patients/excel")]
//        public FileStreamResult ExportPatientsToExcel()
//        {
//            return ToExcel(ApplyQuery(context.Patients, Request.Query));
//        }

//        [HttpGet("/export/clinic/pharmacies/csv")]
//        public FileStreamResult ExportPharmaciesToCSV()
//        {
//            return ToCSV(ApplyQuery(context.Pharmacies, Request.Query));
//        }

//        [HttpGet("/export/clinic/pharmacies/excel")]
//        public FileStreamResult ExportPharmaciesToExcel()
//        {
//            return ToExcel(ApplyQuery(context.Pharmacies, Request.Query));
//        }

//        [HttpGet("/export/clinic/pharmacyinventories/csv")]
//        public FileStreamResult ExportPharmacyInventoriesToCSV()
//        {
//            return ToCSV(ApplyQuery(context.PharmacyInventories, Request.Query));
//        }

//        [HttpGet("/export/clinic/pharmacyinventories/excel")]
//        public FileStreamResult ExportPharmacyInventoriesToExcel()
//        {
//            return ToExcel(ApplyQuery(context.PharmacyInventories, Request.Query));
//        }

//        [HttpGet("/export/clinic/prescriptions/csv")]
//        public FileStreamResult ExportPrescriptionsToCSV()
//        {
//            return ToCSV(ApplyQuery(context.Prescriptions, Request.Query));
//        }

//        [HttpGet("/export/clinic/prescriptions/excel")]
//        public FileStreamResult ExportPrescriptionsToExcel()
//        {
//            return ToExcel(ApplyQuery(context.Prescriptions, Request.Query));
//        }

//        [HttpGet("/export/clinic/staffs/csv")]
//        public FileStreamResult ExportStaffsToCSV()
//        {
//            return ToCSV(ApplyQuery(context.Staffs, Request.Query));
//        }

//        [HttpGet("/export/clinic/staffs/excel")]
//        public FileStreamResult ExportStaffsToExcel()
//        {
//            return ToExcel(ApplyQuery(context.Staffs, Request.Query));
//        }

//        [HttpGet("/export/clinic/vitals/csv")]
//        public FileStreamResult ExportVitalsToCSV()
//        {
//            return ToCSV(ApplyQuery(context.Vitals, Request.Query));
//        }

//        [HttpGet("/export/clinic/vitals/excel")]
//        public FileStreamResult ExportVitalsToExcel()
//        {
//            return ToExcel(ApplyQuery(context.Vitals, Request.Query));
//        }
//    }
//}

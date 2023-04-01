using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using clinic.Data;
using MySql.Data.MySqlClient;
using static log4net.Appender.RollingFileAppender;

namespace clinic
{
    public partial class ClinicService
    {
        private readonly ClinicContext context;
        //private readonly //navigationManager //navigationManager;

        public ClinicService(ClinicContext context)
        {
            this.context = context;
            //this.//navigationManager = //navigationManager;
        }

        public async Task ExportAppointmentsToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/appointments/excel") : "export/clinic/appointments/excel", true);
        }

        public async Task ExportAppointmentsToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/appointments/csv") : "export/clinic/appointments/csv", true);
        }

        partial void OnAppointmentsRead(ref IQueryable<Models.clinic.Appointment> items);

        public async Task<IQueryable<Models.clinic.Appointment>> GetAppointments(Query query = null)
        {
            var items = context.Appointments.AsQueryable();

            items = items.Include(i => i.Patient);

            //items = items.Include(i => i.Employee);

            items = items.Include(i => i.Facilities);

            //items = items.Include(i => i.PaymentMethod1);

            //items = items.Include(i => i.AppointmentType1);

            items = items.Include(i => i.Facilities);

            items = items.Include(i => i.PatientType);


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAppointmentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAppointmentCreated(Models.clinic.Appointment item);

        public async Task<Models.clinic.Appointment> CreateAppointment(Models.clinic.Appointment appointment)
        {
            OnAppointmentCreated(appointment);

            context.Appointments.Add(appointment);
            context.SaveChanges();

            return appointment;
        }
        //public async Task ExportAppointmentTypesToExcel(Query query = null)
        //{
        //    //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/appointmenttypes/excel") : "export/clinic/appointmenttypes/excel", true);
        //}

        //public async Task ExportAppointmentTypesToCSV(Query query = null)
        //{
        //    //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/appointmenttypes/csv") : "export/clinic/appointmenttypes/csv", true);
        //}

        partial void OnAppointmentTypesRead(ref IQueryable<Models.clinic.AppointmentType> items);

        public async Task<IQueryable<Models.clinic.AppointmentType>> GetAppointmentTypes(Query query = null)
        {
            var items = context.AppointmentTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAppointmentTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAppointmentTypeCreated(Models.clinic.AppointmentType item);

        public async Task<Models.clinic.AppointmentType> CreateAppointmentType(Models.clinic.AppointmentType appointmentType)
        {
            OnAppointmentTypeCreated(appointmentType);

            context.AppointmentTypes.Add(appointmentType);
            context.SaveChanges();

            return appointmentType;
        }


        //public async Task ExportDesignationsToExcel(Query query = null)
        //{
        //    //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/designations/excel") : "export/clinic/designations/excel", true);
        //}

        //public async Task ExportDesignationsToCSV(Query query = null)
        //{
        //    //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/designations/csv") : "export/clinic/designations/csv", true);
        //}

        partial void OnDesignationsRead(ref IQueryable<Models.clinic.Designation> items);

        public async Task<IQueryable<Models.clinic.Designation>> GetDesignations(Query query = null)
        {
            var items = context.Designations.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnDesignationsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDesignationCreated(Models.clinic.Designation item);

        public async Task<Models.clinic.Designation> CreateDesignation(Models.clinic.Designation designation)
        {
            OnDesignationCreated(designation);

            context.Designations.Add(designation);
            context.SaveChanges();

            return designation;
        }
        public async Task ExportGendersToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/genders/excel") : "export/clinic/genders/excel", true);
        }

        public async Task ExportGendersToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/genders/csv") : "export/clinic/genders/csv", true);
        }

        partial void OnGendersRead(ref IQueryable<Models.clinic.Gender> items);

        public async Task<IQueryable<Models.clinic.Gender>> GetGenders(Query query = null)
        {
            var items = context.Genders.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnGendersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnGenderCreated(Models.clinic.Gender item);

        public async Task<Models.clinic.Gender> CreateGender(Models.clinic.Gender gender)
        {
            OnGenderCreated(gender);

            context.Genders.Add(gender);
            context.SaveChanges();

            return gender;
        }
        public async Task ExportLaboratoriesToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/laboratories/excel") : "export/clinic/laboratories/excel", true);
        }

        public async Task ExportLaboratoriesToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/laboratories/csv") : "export/clinic/laboratories/csv", true);
        }

        partial void OnLaboratoriesRead(ref IQueryable<Models.clinic.Laboratory> items);

        public async Task<IQueryable<Models.clinic.Laboratory>> GetLaboratories(Query query = null)
        {
            var items = context.Laboratories.AsQueryable();

            items = items.Include(i => i.LaboratoryType);

            items = items.Include(i => i.Appointment);

            items = items.Include(i => i.Appointment.Bills);

            items = items.Include(i => i.Appointment.Patient);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnLaboratoriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLaboratoryCreated(Models.clinic.Laboratory item);

        public async Task<Models.clinic.Laboratory> CreateLaboratory(Models.clinic.Laboratory laboratory)
        {
            OnLaboratoryCreated(laboratory);

            context.Laboratories.Add(laboratory);
            context.SaveChanges();

            return laboratory;
        }
        public async Task ExportLaboratoryTypesToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/laboratorytypes/excel") : "export/clinic/laboratorytypes/excel", true);
        }

        public async Task ExportLaboratoryTypesToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/laboratorytypes/csv") : "export/clinic/laboratorytypes/csv", true);
        }

        partial void OnLaboratoryTypesRead(ref IQueryable<Models.clinic.LaboratoryType> items);

        public async Task<IQueryable<Models.clinic.LaboratoryType>> GetLaboratoryTypes(Query query = null)
        {
            var items = context.LaboratoryTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnLaboratoryTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLaboratoryTypeCreated(Models.clinic.LaboratoryType item);

        public async Task<Models.clinic.LaboratoryType> CreateLaboratoryType(Models.clinic.LaboratoryType laboratoryType)
        {
            OnLaboratoryTypeCreated(laboratoryType);

            context.LaboratoryTypes.Add(laboratoryType);
            context.SaveChanges();

            return laboratoryType;
        }
        public async Task ExportOperationRoomsToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/operationrooms/excel") : "export/clinic/operationrooms/excel", true);
        }

        public async Task ExportOperationRoomsToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/operationrooms/csv") : "export/clinic/operationrooms/csv", true);
        }

        partial void OnOperationRoomsRead(ref IQueryable<Models.clinic.OperationRoom> items);

        public async Task<IQueryable<Models.clinic.OperationRoom>> GetOperationRooms(Query query = null)
        {
            var items = context.OperationRooms.AsQueryable();

            items = items.Include(i => i.OperationType);

            items = items.Include(i => i.Appointment);


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOperationRoomsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOperationRoomCreated(Models.clinic.OperationRoom item);

        public async Task<Models.clinic.OperationRoom> CreateOperationRoom(Models.clinic.OperationRoom operationRoom)
        {
            OnOperationRoomCreated(operationRoom);

            context.OperationRooms.Add(operationRoom);
            context.SaveChanges();

            return operationRoom;
        }
        public async Task ExportOperationTypesToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/operationtypes/excel") : "export/clinic/operationtypes/excel", true);
        }

        public async Task ExportOperationTypesToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/operationtypes/csv") : "export/clinic/operationtypes/csv", true);
        }

        partial void OnOperationTypesRead(ref IQueryable<Models.clinic.OperationType> items);

        public async Task<IQueryable<Models.clinic.OperationType>> GetOperationTypes(Query query = null)
        {
            var items = context.OperationTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOperationTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOperationTypeCreated(Models.clinic.OperationType item);

        public async Task<Models.clinic.OperationType> CreateOperationType(Models.clinic.OperationType operationType)
        {
            OnOperationTypeCreated(operationType);

            context.OperationTypes.Add(operationType);
            context.SaveChanges();

            return operationType;
        }
        public async Task ExportPatientsToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/patients/excel") : "export/clinic/patients/excel", true);
        }

        public async Task ExportPatientsToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/patients/csv") : "export/clinic/patients/csv", true);
        }

        partial void OnPatientsRead(ref IQueryable<Models.clinic.Patient> items);

        public async Task<IQueryable<Models.clinic.Patient>> GetPatients(Query query = null)
        {
            var items = context.Patients.AsQueryable();

            items = items.Include(i => i.Gender1);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPatientsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPatientCreated(Models.clinic.Patient item);

        public async Task<Models.clinic.Patient> CreatePatient(Models.clinic.Patient patient)
        {
            OnPatientCreated(patient);

            context.Patients.Add(patient);
            context.SaveChanges();

            return patient;
        }
        public async Task ExportPharmaciesToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/pharmacies/excel") : "export/clinic/pharmacies/excel", true);
        }

        public async Task ExportPharmaciesToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/pharmacies/csv") : "export/clinic/pharmacies/csv", true);
        }

        partial void OnPharmaciesRead(ref IQueryable<Models.clinic.Pharmacy> items);

        public async Task<IQueryable<Models.clinic.Pharmacy>> GetPharmacies(Query query = null)
        {
            var items = context.Pharmacies.AsQueryable();

            items = items.Include(i => i.Prescription);


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPharmaciesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPharmacyCreated(Models.clinic.Pharmacy item);

        public async Task<Models.clinic.Pharmacy> CreatePharmacy(Models.clinic.Pharmacy pharmacy)
        {
            OnPharmacyCreated(pharmacy);

            context.Pharmacies.Add(pharmacy);
            context.SaveChanges();

            return pharmacy;
        }
        public async Task ExportPharmacyInventoriesToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/pharmacyinventories/excel") : "export/clinic/pharmacyinventories/excel", true);
        }

        public async Task ExportPharmacyInventoriesToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/pharmacyinventories/csv") : "export/clinic/pharmacyinventories/csv", true);
        }

        partial void OnPharmacyInventoriesRead(ref IQueryable<Models.clinic.Inventory> items);

        public async Task<IQueryable<Models.clinic.Inventory>> GetPharmacyInventories(Query query = null)
        {
            var items = context.PharmacyInventories.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPharmacyInventoriesRead(ref items);

            return await Task.FromResult(items);
        }

        //partial void OnInventoryCreated(Models.clinic.Inventory item);

        //public async Task<Models.clinic.Inventory> CreateInventory(Models.clinic.Inventory Inventory)
        //{
        //    OnInventoryCreated(Inventory);

        //    context.PharmacyInventories.Add(Inventory);
        //    context.SaveChanges();

        //    return Inventory;
        //}
        public async Task ExportPrescriptionsToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/prescriptions/excel") : "export/clinic/prescriptions/excel", true);
        }

        public async Task ExportPrescriptionsToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/prescriptions/csv") : "export/clinic/prescriptions/csv", true);
        }

        partial void OnPrescriptionsRead(ref IQueryable<Models.clinic.Prescription> items);

        public async Task<IQueryable<Models.clinic.Prescription>> GetPrescriptions(Query query = null)
        {
            var items = context.Prescriptions.AsQueryable();

            items = items.Include(i => i.Inventory);

            items = items.Include(i => i.Appointment);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPrescriptionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPrescriptionCreated(Models.clinic.Prescription item);

        public async Task<Models.clinic.Prescription> CreatePrescription(Models.clinic.Prescription prescription)
        {
            OnPrescriptionCreated(prescription);

            context.Prescriptions.Add(prescription);
            context.SaveChanges();

            return prescription;
        }
        public async Task ExportStaffsToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/staffs/excel") : "export/clinic/staffs/excel", true);
        }

        public async Task ExportStaffsToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/staffs/csv") : "export/clinic/staffs/csv", true);
        }

        partial void OnStaffsRead(ref IQueryable<Models.clinic.Employee> items);

        public async Task<IQueryable<Models.clinic.Employee>> GetStaffs(Query query = null)
        {
            var items = context.Staffs.AsQueryable();

            items = items.Include(i => i.Gender1);

            items = items.Include(i => i.Designation);

            items = items.Include(i => i.Doctors);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnStaffsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnStaffCreated(Models.clinic.Employee item);

        public async Task<Models.clinic.Employee> CreateStaff(Models.clinic.Employee employee)
        {

            OnStaffCreated(employee);

            context.Staffs.Add(employee);
            context.SaveChanges();

            return employee;
        }
        public async Task ExportVitalsToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/vitals/excel") : "export/clinic/vitals/excel", true);
        }

        public async Task ExportVitalsToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/vitals/csv") : "export/clinic/vitals/csv", true);
        }

        partial void OnVitalsRead(ref IQueryable<Models.clinic.Vital> items);

        public async Task<IQueryable<Models.clinic.Vital>> GetVitals(Query query = null)
        {
            var items = context.Vitals.AsQueryable();

            //items = items.Include(i => i.Appointment);
            //items = items.Include(i => i.Appointment.Patient);
            //items = items.Include(i => i.Appointment.Bills);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnVitalsRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task<IQueryable<Models.clinic.Vital>> GetUncheckedVitals(Query query = null)
        {
            var items = context.Vitals.AsQueryable();

            items = items.Include(i => i.Appointment);
            items = items.Include(i => i.Appointment.Patient);
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnVitalsRead(ref items);

            return await Task.FromResult(items.Where(i => i.status == 10));
        }

        partial void OnVitalCreated(Models.clinic.Vital item);

        public async Task<Models.clinic.Vital> CreateVital(Models.clinic.Vital vital)
        {
            OnVitalCreated(vital);

            context.Vitals.Add(vital);
            context.SaveChanges();

            return vital;
        }

        partial void OnAppointmentDeleted(Models.clinic.Appointment item);

        public async Task<Models.clinic.Appointment> DeleteAppointment(int? appointmentId)
        {
            var item = context.Appointments
                              .Where(i => i.appointmentId == appointmentId)
                              .Include(i => i.Laboratories)
                              .Include(i => i.OperationRooms)
                              .Include(i => i.Vitals)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnAppointmentDeleted(item);

            context.Appointments.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnAppointmentGet(Models.clinic.Appointment item);

        public async Task<Models.clinic.Appointment> GetAppointmentByappointmentId(int? appointmentId)
        {
            var items = context.Appointments
                              .AsNoTracking()
                              .Where(i => i.appointmentId == appointmentId);

            items = items.Include(i => i.Patient);

            items = items.Include(i => i.Employee);

            var item = items.FirstOrDefault();

            OnAppointmentGet(item);

            return await Task.FromResult(item);
        }

        partial void OnConsultationUpdated(Models.clinic.Consultations item);

        public async Task<Models.clinic.Consultations> UpdateConsultation(int? consultationId, Models.clinic.Consultations consultation)
        {
            OnConsultationUpdated(consultation);

            var item = context.Consultations
                              .Where(i => i.consultationId == consultationId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(consultation);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return consultation;
        }


        public async Task<Models.clinic.Appointment> CancelAppointmentChanges(Models.clinic.Appointment item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnAppointmentUpdated(Models.clinic.Appointment item);

        public async Task<Models.clinic.Appointment> UpdateAppointment(int? appointmentId, Models.clinic.Appointment appointment)
        {
            OnAppointmentUpdated(appointment);

            var item = context.Appointments
                              .Where(i => i.appointmentId == appointmentId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(appointment);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return appointment;
        }

        partial void OnAppointmentTypeDeleted(Models.clinic.AppointmentType item);

        public async Task<Models.clinic.AppointmentType> DeleteAppointmentType(int? typeId)
        {
            var item = context.AppointmentTypes
                              .Where(i => i.typeId == typeId)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnAppointmentTypeDeleted(item);

            context.AppointmentTypes.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnAppointmentTypeGet(Models.clinic.AppointmentType item);

        public async Task<Models.clinic.AppointmentType> GetAppointmentTypeBytypeId(int? typeId)
        {
            var items = context.AppointmentTypes
                              .AsNoTracking()
                              .Where(i => i.typeId == typeId);

            var item = items.FirstOrDefault();

            OnAppointmentTypeGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.AppointmentType> CancelAppointmentTypeChanges(Models.clinic.AppointmentType item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnAppointmentTypeUpdated(Models.clinic.AppointmentType item);

        public async Task<Models.clinic.AppointmentType> UpdateAppointmentType(int? typeId, Models.clinic.AppointmentType appointmentType)
        {
            OnAppointmentTypeUpdated(appointmentType);

            var item = context.AppointmentTypes
                              .Where(i => i.typeId == typeId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(appointmentType);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return appointmentType;
        }




        partial void OnDesignationDeleted(Models.clinic.Designation item);

        public async Task<Models.clinic.Designation> DeleteDesignation(int? designationId)
        {
            var item = context.Designations
                              .Where(i => i.designationId == designationId)
                              .Include(i => i.Staffs)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnDesignationDeleted(item);

            context.Designations.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnDesignationGet(Models.clinic.Designation item);

        public async Task<Models.clinic.Designation> GetDesignationBydesignationId(int? designationId)
        {
            var items = context.Designations
                              .AsNoTracking()
                              .Where(i => i.designationId == designationId);

            var item = items.FirstOrDefault();

            OnDesignationGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.Designation> CancelDesignationChanges(Models.clinic.Designation item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnDesignationUpdated(Models.clinic.Designation item);

        public async Task<Models.clinic.Designation> UpdateDesignation(int? designationId, Models.clinic.Designation designation)
        {
            OnDesignationUpdated(designation);

            var item = context.Designations
                              .Where(i => i.designationId == designationId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(designation);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return designation;
        }

        partial void OnGenderDeleted(Models.clinic.Gender item);

        public async Task<Models.clinic.Gender> DeleteGender(int? id)
        {
            var item = context.Genders
                              .Where(i => i.id == id)
                              .Include(i => i.Patients)
                              .Include(i => i.Staffs)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnGenderDeleted(item);

            context.Genders.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnGenderGet(Models.clinic.Gender item);

        public async Task<Models.clinic.Gender> GetGenderByid(int? id)
        {
            var items = context.Genders
                              .AsNoTracking()
                              .Where(i => i.id == id);

            var item = items.FirstOrDefault();

            OnGenderGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.Gender> CancelGenderChanges(Models.clinic.Gender item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnGenderUpdated(Models.clinic.Gender item);

        public async Task<Models.clinic.Gender> UpdateGender(int? id, Models.clinic.Gender gender)
        {
            OnGenderUpdated(gender);

            var item = context.Genders
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(gender);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return gender;
        }

        partial void OnLaboratoryDeleted(Models.clinic.Laboratory item);

        public async Task<Models.clinic.Laboratory> DeleteLaboratory(int? labId)
        {
            var item = context.Laboratories
                              .Where(i => i.labId == labId)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnLaboratoryDeleted(item);

            context.Laboratories.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnLaboratoryGet(Models.clinic.Laboratory item);

        public async Task<Models.clinic.Laboratory> GetLaboratoryBylabId(int? labId)
        {
            var items = context.Laboratories
                              .AsNoTracking()
                              .Where(i => i.labId == labId);

            items = items.Include(i => i.LaboratoryType);

            items = items.Include(i => i.Appointment);


            var item = items.FirstOrDefault();

            OnLaboratoryGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.Laboratory> CancelLaboratoryChanges(Models.clinic.Laboratory item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnLaboratoryUpdated(Models.clinic.Laboratory item);

        public async Task<Models.clinic.Laboratory> UpdateLaboratory(int? labId, Models.clinic.Laboratory laboratory)
        {
            OnLaboratoryUpdated(laboratory);

            var item = context.Laboratories
                              .Where(i => i.labId == labId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(laboratory);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return laboratory;
        }

        partial void OnLaboratoryTypeDeleted(Models.clinic.LaboratoryType item);

        public async Task<Models.clinic.LaboratoryType> DeleteLaboratoryType(int? labTypeId)
        {
            var item = context.LaboratoryTypes
                              .Where(i => i.labTypeID == labTypeId)
                              .Include(i => i.Laboratories)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnLaboratoryTypeDeleted(item);

            context.LaboratoryTypes.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnLaboratoryTypeGet(Models.clinic.LaboratoryType item);

        public async Task<Models.clinic.LaboratoryType> GetLaboratoryTypeBylabTypeId(int? labTypeId)
        {
            var items = context.LaboratoryTypes
                              .AsNoTracking()
                              .Where(i => i.labTypeID == labTypeId);

            var item = items.FirstOrDefault();

            OnLaboratoryTypeGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.LaboratoryType> CancelLaboratoryTypeChanges(Models.clinic.LaboratoryType item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnLaboratoryTypeUpdated(Models.clinic.LaboratoryType item);

        public async Task<Models.clinic.LaboratoryType> UpdateLaboratoryType(int? labTypeId, Models.clinic.LaboratoryType laboratoryType)
        {
            OnLaboratoryTypeUpdated(laboratoryType);

            var item = context.LaboratoryTypes
                              .Where(i => i.labTypeID == labTypeId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(laboratoryType);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return laboratoryType;
        }

        partial void OnOperationRoomDeleted(Models.clinic.OperationRoom item);

        public async Task<Models.clinic.OperationRoom> DeleteOperationRoom(int? orId)
        {
            var item = context.OperationRooms
                              .Where(i => i.orId == orId)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnOperationRoomDeleted(item);

            context.OperationRooms.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnOperationRoomGet(Models.clinic.OperationRoom item);

        public async Task<Models.clinic.OperationRoom> GetOperationRoomByorId(int? orId)
        {
            var items = context.OperationRooms
                              .AsNoTracking()
                              .Where(i => i.orId == orId);

            items = items.Include(i => i.OperationType);

            items = items.Include(i => i.Appointment);

            var item = items.FirstOrDefault();

            OnOperationRoomGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.OperationRoom> CancelOperationRoomChanges(Models.clinic.OperationRoom item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOperationRoomUpdated(Models.clinic.OperationRoom item);

        public async Task<Models.clinic.OperationRoom> UpdateOperationRoom(int? orId, Models.clinic.OperationRoom operationRoom)
        {
            OnOperationRoomUpdated(operationRoom);

            var item = context.OperationRooms
                              .Where(i => i.orId == orId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(operationRoom);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return operationRoom;
        }

        partial void OnOperationTypeDeleted(Models.clinic.OperationType item);

        public async Task<Models.clinic.OperationType> DeleteOperationType(int? orTypeId)
        {
            var item = context.OperationTypes
                              .Where(i => i.operationTypeId == orTypeId)
                              .Include(i => i.OperationRooms)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnOperationTypeDeleted(item);

            context.OperationTypes.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnOperationTypeGet(Models.clinic.OperationType item);

        public async Task<Models.clinic.OperationType> GetOperationTypeByorTypeId(int? orTypeId)
        {
            var items = context.OperationTypes
                              .AsNoTracking()
                              .Where(i => i.operationTypeId == orTypeId);

            var item = items.FirstOrDefault();

            OnOperationTypeGet(item);

            return await Task.FromResult(item);
        }


        public async Task<Models.clinic.OperationType> GetOperationTypeByoperationTypeId(int? operationTypeId)
        {
            var items = context.OperationTypes
                              .AsNoTracking()
                              .Where(i => i.operationTypeId == operationTypeId);

            var item = items.FirstOrDefault();

            OnOperationTypeGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.OperationType> CancelOperationTypeChanges(Models.clinic.OperationType item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOperationTypeUpdated(Models.clinic.OperationType item);

        public async Task<Models.clinic.OperationType> UpdateOperationType(int? orTypeId, Models.clinic.OperationType operationType)
        {
            OnOperationTypeUpdated(operationType);

            var item = context.OperationTypes
                              .Where(i => i.operationTypeId == orTypeId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(operationType);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return operationType;
        }

        partial void OnPatientDeleted(Models.clinic.Patient item);

        public async Task<Models.clinic.Patient> DeletePatient(int? patientId)
        {
            var item = context.Patients
                              .Where(i => i.patientId == patientId)
                              .Include(i => i.Appointments)
                              .Include(i => i.InsuranceDetails)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnPatientDeleted(item);

            context.Patients.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnPatientGet(Models.clinic.Patient item);

        public async Task<Models.clinic.Patient> GetPatientBypatientId(int? patientId)
        {
            var items = context.Patients
                              .AsNoTracking()
                              .Where(i => i.patientId == patientId);

            items = items.Include(i => i.Gender1);

            var item = items.FirstOrDefault();

            OnPatientGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.Patient> CancelPatientChanges(Models.clinic.Patient item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnPatientUpdated(Models.clinic.Patient item);

        public async Task<Models.clinic.Patient> UpdatePatient(int? patientId, Models.clinic.Patient patient)
        {
            OnPatientUpdated(patient);

            var item = context.Patients
                              .Where(i => i.patientId == patientId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(patient);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return patient;
        }

        partial void OnPharmacyDeleted(Models.clinic.Pharmacy item);

        public async Task<Models.clinic.Pharmacy> DeletePharmacy(int? issueId)
        {
            var item = context.Pharmacies
                              .Where(i => i.issueId == issueId)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnPharmacyDeleted(item);

            context.Pharmacies.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnPharmacyGet(Models.clinic.Pharmacy item);

        public async Task<Models.clinic.Pharmacy> GetPharmacyByissueId(int? issueId)
        {
            var items = context.Pharmacies
                              .AsNoTracking()
                              .Where(i => i.issueId == issueId);

            items = items.Include(i => i.Prescription);


            var item = items.FirstOrDefault();

            OnPharmacyGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.Pharmacy> CancelPharmacyChanges(Models.clinic.Pharmacy item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnPharmacyUpdated(Models.clinic.Pharmacy item);

        public async Task<Models.clinic.Pharmacy> UpdatePharmacy(int? issueId, Models.clinic.Pharmacy pharmacy)
        {
            OnPharmacyUpdated(pharmacy);

            var item = context.Pharmacies
                              .Where(i => i.issueId == issueId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(pharmacy);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return pharmacy;
        }

        partial void OnInventoryDeleted(Models.clinic.Inventory item);

        public async Task<Models.clinic.Inventory> DeleteInventory(int? itemId)
        {
            var item = context.Inventories
                              .Where(i => i.itemId == itemId)
                              .Include(i => i.Prescriptions)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnInventoryDeleted(item);

            context.Inventories.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnInventoryGet(Models.clinic.Inventory item);

        public async Task<Models.clinic.Inventory> GetInventoryByitemId(int itemId)
        {
            var items = context.Inventories
                              .AsNoTracking()
                              .Where(i => i.itemId == itemId);

            var item = items.FirstOrDefault();

            OnInventoryGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.Inventory> CancelInventoryChanges(Models.clinic.Inventory item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnInventoryUpdated(Models.clinic.Inventory item);

        public async Task<Models.clinic.Inventory> UpdateInventory(int? itemId, Models.clinic.Inventory inventory)
        {
            //OnInventoryUpdated(Inventory);

            //var item = context.Inventories
            //                  .Where(i => i.itemId == itemId)
            //                  .FirstOrDefault();
            //if (item == null)
            //{
            //    throw new Exception("Item no longer available");
            //}
            //var entry = context.Entry(item);
            //entry.CurrentValues.SetValues(Inventory);
            //entry.State = EntityState.Modified;
            //context.SaveChanges();


            using (var connection = new MySqlConnection("Server=202.182.120.224;Database=clinic;User ID=remote;Password=#3PqKZ$F3G=y9NSD;Connection Timeout=100"))
            {
                connection.Open();
                var query = $"UPDATE inventory set stock = {inventory.stock} where itemId = {inventory.itemId}";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                connection.Close();
            }


            return inventory;
        }

        partial void OnStaffDeleted(Models.clinic.Employee item);

        public async Task<Models.clinic.Employee> DeleteStaff(int? employeeId)
        {
            var item = context.Staffs
                              .Where(i => i.employeeId == employeeId)
                              .Include(i => i.Appointments)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnStaffDeleted(item);

            context.Staffs.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnStaffGet(Models.clinic.Employee item);

        public async Task<Models.clinic.Employee> GetStaffBystaffId(int? employeeId)
        {
            var items = context.Staffs
                              .AsNoTracking()
                              .Where(i => i.employeeId == employeeId);

            items = items.Include(i => i.Gender1);

            items = items.Include(i => i.Designation);

            var item = items.FirstOrDefault();

            OnStaffGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.Employee> CancelStaffChanges(Models.clinic.Employee item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnStaffUpdated(Models.clinic.Employee item);

        public async Task<Models.clinic.Employee> UpdateStaff(int? employeeId, Models.clinic.Employee employee)
        {
            OnStaffUpdated(employee);

            var item = context.Staffs
                              .Where(i => i.employeeId == employeeId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(employee);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return employee;
        }

        partial void OnVitalDeleted(Models.clinic.Vital item);

        public async Task<Models.clinic.Vital> DeleteVital(int? vRecordId)
        {
            var item = context.Vitals
                              .Where(i => i.vRecordId == vRecordId)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnVitalDeleted(item);

            context.Vitals.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnVitalGet(Models.clinic.Vital item);

        public async Task<Models.clinic.Vital> GetVitalByvRecordId(int? vRecordId)
        {
            var items = context.Vitals
                              .AsNoTracking()
                              .Where(i => i.vRecordId == vRecordId);

            items = items.Include(i => i.Appointment);

            var item = items.FirstOrDefault();

            OnVitalGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.Vital> CancelVitalChanges(Models.clinic.Vital item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnVitalUpdated(Models.clinic.Vital item);

        public async Task<Models.clinic.Vital> UpdateVital(int? vRecordId, Models.clinic.Vital vital)
        {
            OnVitalUpdated(vital);

            var item = context.Vitals
                              .Where(i => i.vRecordId == vRecordId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(vital);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return vital;
        }

        partial void OnFacilitiesRead(ref IQueryable<Models.clinic.Facilities> items);

        public async Task<IQueryable<Models.clinic.Facilities>> GetFacilities(Query query = null)
        {
            var items = context.Facilities.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnFacilitiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnConsltationCreated(Models.clinic.Consultations item);

        public async Task<Models.clinic.Consultations> CreateConsultation(Models.clinic.Consultations consultationRoom)
        {
            OnConsltationCreated(consultationRoom);

            context.Consultations.Add(consultationRoom);
            context.SaveChanges();

            return consultationRoom;
        }

        partial void OnConsultationsRead(ref IQueryable<Models.clinic.Consultations> items);

        public async Task<IQueryable<Models.clinic.Consultations>> GetConsultations(Query query = null)
        {
            var items = context.Consultations.AsQueryable();

            items = items.Include(i => i.Appointment);
            items = items.Include(i => i.Appointment.Laboratories);
            items = items.Include(i => i.Appointment.Patient);
            items = items.Include(i => i.Appointment.Vitals);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnConsultationsRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task ExportDiagnositcImagingSubtypesToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/diagnositcimagingsubtypes/excel") : "export/clinic/diagnositcimagingsubtypes/excel", true);
        }

        public async Task ExportDiagnositcImagingSubtypesToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/diagnositcimagingsubtypes/csv") : "export/clinic/diagnositcimagingsubtypes/csv", true);
        }

        partial void OnDiagnositcImagingSubtypesRead(ref IQueryable<Models.clinic.DiagnositcImagingSubtype> items);

        public async Task<IQueryable<Models.clinic.DiagnositcImagingSubtype>> GetDiagnositcImagingSubtypes(Query query = null)
        {
            var items = context.DiagnositcImagingSubtypes.AsQueryable();

            items = items.Include(i => i.DiagnosticImagingType);


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnDiagnositcImagingSubtypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDiagnositcImagingSubtypeCreated(Models.clinic.DiagnositcImagingSubtype item);

        public async Task<Models.clinic.DiagnositcImagingSubtype> CreateDiagnositcImagingSubtype(Models.clinic.DiagnositcImagingSubtype diagnositcImagingSubtype)
        {
            OnDiagnositcImagingSubtypeCreated(diagnositcImagingSubtype);

            context.DiagnositcImagingSubtypes.Add(diagnositcImagingSubtype);
            context.SaveChanges();

            return diagnositcImagingSubtype;
        }
        public async Task ExportDiagnosticImagingRequestsToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/diagnosticimagingrequests/excel") : "export/clinic/diagnosticimagingrequests/excel", true);
        }

        public async Task ExportDiagnosticImagingRequestsToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/diagnosticimagingrequests/csv") : "export/clinic/diagnosticimagingrequests/csv", true);
        }

        partial void OnDiagnosticImagingRequestsRead(ref IQueryable<Models.clinic.DiagnosticImagingRequest> items);

        public async Task<IQueryable<Models.clinic.DiagnosticImagingRequest>> GetDiagnosticImagingRequests(Query query = null)
        {
            var items = context.DiagnosticImagingRequests.AsQueryable();

            items = items.Include(i => i.Appointment);

            items = items.Include(i => i.DiagnositcImagingSubtype);

            items = items.Include(i => i.DiagnositcImagingSubtype.DiagnosticImagingType);

            items = items.Include(i => i.Appointment.Patient);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnDiagnosticImagingRequestsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDiagnosticImagingRequestCreated(Models.clinic.DiagnosticImagingRequest item);

        public async Task<Models.clinic.DiagnosticImagingRequest> CreateDiagnosticImagingRequest(Models.clinic.DiagnosticImagingRequest diagnosticImagingRequest)
        {
            OnDiagnosticImagingRequestCreated(diagnosticImagingRequest);

            context.DiagnosticImagingRequests.Add(diagnosticImagingRequest);
            context.SaveChanges();

            return diagnosticImagingRequest;
        }
        public async Task ExportDiagnosticImagingTypesToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/diagnosticimagingtypes/excel") : "export/clinic/diagnosticimagingtypes/excel", true);
        }

        public async Task ExportDiagnosticImagingTypesToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/diagnosticimagingtypes/csv") : "export/clinic/diagnosticimagingtypes/csv", true);
        }

        partial void OnDiagnosticImagingTypesRead(ref IQueryable<Models.clinic.DiagnosticImagingType> items);

        public async Task<IQueryable<Models.clinic.DiagnosticImagingType>> GetDiagnosticImagingTypes(Query query = null)
        {
            var items = context.DiagnosticImagingTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnDiagnosticImagingTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDiagnosticImagingTypeCreated(Models.clinic.DiagnosticImagingType item);

        public async Task<Models.clinic.DiagnosticImagingType> CreateDiagnosticImagingType(Models.clinic.DiagnosticImagingType diagnosticImagingType)
        {
            OnDiagnosticImagingTypeCreated(diagnosticImagingType);

            context.DiagnosticImagingTypes.Add(diagnosticImagingType);
            context.SaveChanges();

            return diagnosticImagingType;
        }
        partial void OnDiagnositcImagingSubtypeDeleted(Models.clinic.DiagnositcImagingSubtype item);

        public async Task<Models.clinic.DiagnositcImagingSubtype> DeleteDiagnositcImagingSubtype(int? imagingSubTypeId)
        {
            var item = context.DiagnositcImagingSubtypes
                              .Where(i => i.imagingSubTypeId == imagingSubTypeId)
                              .Include(i => i.DiagnosticImagingRequests)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnDiagnositcImagingSubtypeDeleted(item);

            context.DiagnositcImagingSubtypes.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnDiagnositcImagingSubtypeGet(Models.clinic.DiagnositcImagingSubtype item);

        public async Task<Models.clinic.DiagnositcImagingSubtype> GetDiagnositcImagingSubtypeByimagingSubTypeId(int? imagingSubTypeId)
        {
            var items = context.DiagnositcImagingSubtypes
                              .AsNoTracking()
                              .Where(i => i.imagingSubTypeId == imagingSubTypeId);

            items = items.Include(i => i.DiagnosticImagingType);

            var item = items.FirstOrDefault();

            OnDiagnositcImagingSubtypeGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.DiagnositcImagingSubtype> CancelDiagnositcImagingSubtypeChanges(Models.clinic.DiagnositcImagingSubtype item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnDiagnositcImagingSubtypeUpdated(Models.clinic.DiagnositcImagingSubtype item);

        public async Task<Models.clinic.DiagnositcImagingSubtype> UpdateDiagnositcImagingSubtype(int? imagingSubTypeId, Models.clinic.DiagnositcImagingSubtype diagnositcImagingSubtype)
        {
            OnDiagnositcImagingSubtypeUpdated(diagnositcImagingSubtype);

            var item = context.DiagnositcImagingSubtypes
                              .Where(i => i.imagingSubTypeId == imagingSubTypeId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(diagnositcImagingSubtype);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return diagnositcImagingSubtype;
        }

        partial void OnDiagnosticImagingRequestDeleted(Models.clinic.DiagnosticImagingRequest item);

        public async Task<Models.clinic.DiagnosticImagingRequest> DeleteDiagnosticImagingRequest(int? imagingRequestId)
        {
            var item = context.DiagnosticImagingRequests
                              .Where(i => i.imagingRequestId == imagingRequestId)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnDiagnosticImagingRequestDeleted(item);

            context.DiagnosticImagingRequests.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnDiagnosticImagingRequestGet(Models.clinic.DiagnosticImagingRequest item);

        public async Task<Models.clinic.DiagnosticImagingRequest> GetDiagnosticImagingRequestByimagingRequestId(int? imagingRequestId)
        {
            var items = context.DiagnosticImagingRequests
                              .AsNoTracking()
                              .Where(i => i.imagingRequestId == imagingRequestId);

            items = items.Include(i => i.Appointment);

            items = items.Include(i => i.DiagnositcImagingSubtype);

            var item = items.FirstOrDefault();

            OnDiagnosticImagingRequestGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.DiagnosticImagingRequest> CancelDiagnosticImagingRequestChanges(Models.clinic.DiagnosticImagingRequest item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnDiagnosticImagingRequestUpdated(Models.clinic.DiagnosticImagingRequest item);

        public async Task<Models.clinic.DiagnosticImagingRequest> UpdateDiagnosticImagingRequest(int? imagingRequestId, Models.clinic.DiagnosticImagingRequest diagnosticImagingRequest)
        {
            OnDiagnosticImagingRequestUpdated(diagnosticImagingRequest);

            var item = context.DiagnosticImagingRequests
                              .Where(i => i.imagingRequestId == imagingRequestId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(diagnosticImagingRequest);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return diagnosticImagingRequest;
        }

        partial void OnDiagnosticImagingTypeDeleted(Models.clinic.DiagnosticImagingType item);

        public async Task<Models.clinic.DiagnosticImagingType> DeleteDiagnosticImagingType(int? imagingTypeId)
        {
            var item = context.DiagnosticImagingTypes
                              .Where(i => i.imagingTypeId == imagingTypeId)
                              .Include(i => i.DiagnositcImagingSubtypes)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnDiagnosticImagingTypeDeleted(item);

            context.DiagnosticImagingTypes.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnDiagnosticImagingTypeGet(Models.clinic.DiagnosticImagingType item);

        public async Task<Models.clinic.DiagnosticImagingType> GetDiagnosticImagingTypeByimagingTypeId(int? imagingTypeId)
        {
            var items = context.DiagnosticImagingTypes
                              .AsNoTracking()
                              .Where(i => i.imagingTypeId == imagingTypeId);

            var item = items.FirstOrDefault();

            OnDiagnosticImagingTypeGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.DiagnosticImagingType> CancelDiagnosticImagingTypeChanges(Models.clinic.DiagnosticImagingType item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnDiagnosticImagingTypeUpdated(Models.clinic.DiagnosticImagingType item);

        public async Task<Models.clinic.DiagnosticImagingType> UpdateDiagnosticImagingType(int? imagingTypeId, Models.clinic.DiagnosticImagingType diagnosticImagingType)
        {
            OnDiagnosticImagingTypeUpdated(diagnosticImagingType);

            var item = context.DiagnosticImagingTypes
                              .Where(i => i.imagingTypeId == imagingTypeId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(diagnosticImagingType);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return diagnosticImagingType;
        }

        public async Task ExportAdministrationTypesToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/administrationtypes/excel") : "export/clinic/administrationtypes/excel", true);
        }

        public async Task ExportAdministrationTypesToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/administrationtypes/csv") : "export/clinic/administrationtypes/csv", true);
        }

        partial void OnAdministrationTypesRead(ref IQueryable<Models.clinic.AdministrationType> items);

        public async Task<IQueryable<Models.clinic.AdministrationType>> GetAdministrationTypes(Query query = null)
        {
            var items = context.AdministrationTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAdministrationTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAdministrationTypeCreated(Models.clinic.AdministrationType item);

        public async Task<Models.clinic.AdministrationType> CreateAdministrationType(Models.clinic.AdministrationType administrationType)
        {
            OnAdministrationTypeCreated(administrationType);

            context.AdministrationTypes.Add(administrationType);
            context.SaveChanges();

            return administrationType;
        }

        public async Task ExportInventoriesToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/inventories/excel") : "export/clinic/inventories/excel", true);
        }

        public async Task ExportInventoriesToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/inventories/csv") : "export/clinic/inventories/csv", true);
        }

        partial void OnInventoriesRead(ref IQueryable<Models.clinic.Inventory> items);

        public async Task<IQueryable<Models.clinic.Inventory>> GetInventories(Query query = null)
        {
            var items = context.Inventories.AsQueryable();

            items = items.Include(i => i.InventoryCategory);

            items = items.Include(i => i.AdministrationType1);

            items = items.Include(i => i.UnitOfMeasure1);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnInventoriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnInventoryCreated(Models.clinic.Inventory item);

        public async Task<Models.clinic.Inventory> CreateInventory(Models.clinic.Inventory inventory)
        {
            //OnInventoryCreated(inventory);

            //context.Inventories.Add(inventory);
            //context.SaveChanges();

            using (var connection = new MySqlConnection("Server=202.182.120.224;Database=clinic;User ID=remote;Password=#3PqKZ$F3G=y9NSD;Connection Timeout=100"))
            {
                connection.Open();
                var query = $"INSERT INTO `clinic`.`inventory`(`category`,`brandName`,`medication`,`administrationType`,`unit`,`unitCost`,`stock`,`UnitOfMeasure`)VALUES({inventory.category},'{inventory.brandName}','{inventory.medication}',{inventory.administrationType},{inventory.unit1},{inventory.unitCost},0,{inventory.UnitOfMeasure}); SELECT LAST_INSERT_ID();";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                inventory.itemId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return inventory;
        }
        public async Task ExportInventoryCategoriesToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/Inventorycategories/excel") : "export/clinic/Inventorycategories/excel", true);
        }

        public async Task ExportInventoryCategoriesToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/Inventorycategories/csv") : "export/clinic/Inventorycategories/csv", true);
        }

        partial void OnInventoryCategoriesRead(ref IQueryable<Models.clinic.InventoryCategory> items);

        public async Task<IQueryable<Models.clinic.InventoryCategory>> GetInventoryCategories(Query query = null)
        {
            var items = context.InventoryCategories.AsQueryable();


            items = items.Include(i => i.Inventories);


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnInventoryCategoriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnInventoryCategoryCreated(Models.clinic.InventoryCategory item);
            
        public async Task<Models.clinic.InventoryCategory> CreateInventoryCategory(Models.clinic.InventoryCategory inventoryCategory)
        {
            //OnInventoryCategoryCreated(inventoryCategory);

            //context.InventoryCategories.Add(inventoryCategory);
            //context.SaveChanges();

            using (var connection = new MySqlConnection("Server=202.182.120.224;Database=clinic;User ID=remote;Password=#3PqKZ$F3G=y9NSD;Connection Timeout=100"))
            {
                connection.Open();
                var query = $"INSERT INTO `clinic`.`inventory_category` (`categoryName`) VALUES ('{inventoryCategory.categoryName}'); SELECT LAST_INSERT_ID();";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                inventoryCategory.categoryId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return inventoryCategory;
        }


        partial void OnAdministrationTypeDeleted(Models.clinic.AdministrationType item);

        public async Task<Models.clinic.AdministrationType> DeleteAdministrationType(int? administrationTypeId)
        {
            var item = context.AdministrationTypes
                              .Where(i => i.administrationTypeId == administrationTypeId)
                              .Include(i => i.Inventories)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnAdministrationTypeDeleted(item);

            context.AdministrationTypes.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnAdministrationTypeGet(Models.clinic.AdministrationType item);

        public async Task<Models.clinic.AdministrationType> GetAdministrationTypeByadministrationTypeId(int? administrationTypeId)
        {
            var items = context.AdministrationTypes
                              .AsNoTracking()
                              .Where(i => i.administrationTypeId == administrationTypeId);

            var item = items.FirstOrDefault();

            OnAdministrationTypeGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.AdministrationType> CancelAdministrationTypeChanges(Models.clinic.AdministrationType item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnAdministrationTypeUpdated(Models.clinic.AdministrationType item);

        public async Task<Models.clinic.AdministrationType> UpdateAdministrationType(int? administrationTypeId, Models.clinic.AdministrationType administrationType)
        {
            OnAdministrationTypeUpdated(administrationType);

            var item = context.AdministrationTypes
                              .Where(i => i.administrationTypeId == administrationTypeId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(administrationType);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return administrationType;
        }



        //partial void OnInventoryDeleted(Models.clinic.Inventory item);

        //public async Task<Models.clinic.Inventory> DeleteInventory(int? itemId)
        //{
        //    var item = context.Inventories
        //                      .Where(i => i.itemId == itemId)
        //                      .Include(i => i.Prescriptions)
        //                      .FirstOrDefault();

        //    if (item == null)
        //    {
        //        throw new Exception("Item no longer available");
        //    }

        //    OnInventoryDeleted(item);

        //    context.Inventories.Remove(item);
        //    context.SaveChanges();

        //    return item;
        //}

        //partial void OnInventoryGet(Models.clinic.Inventory item);

        //public async Task<Models.clinic.Inventory> GetInventoryByitemId(int? itemId)
        //{
        //    var items = context.Inventories
        //                      .AsNoTracking()
        //                      .Where(i => i.itemId == itemId);

        //    items = items.Include(i => i.InventoryCategory);

        //    items = items.Include(i => i.AdministrationType1);

        //    var item = items.FirstOrDefault();

        //    OnInventoryGet(item);

        //    return await Task.FromResult(item);
        //}

        //public async Task<Models.clinic.Inventory> CancelInventoryChanges(Models.clinic.Inventory item)
        //{
        //    var entity = context.Entry(item);
        //    entity.CurrentValues.SetValues(entity.OriginalValues);
        //    entity.State = EntityState.Unchanged;

        //    return item;
        //}

        //partial void OnInventoryUpdated(Models.clinic.Inventory item);

        //public async Task<Models.clinic.Inventory> UpdateInventory(int? itemId, Models.clinic.Inventory Inventory)
        //{
        //    OnInventoryUpdated(Inventory);

        //    var item = context.Inventories
        //                      .Where(i => i.itemId == itemId)
        //                      .FirstOrDefault();
        //    if (item == null)
        //    {
        //        throw new Exception("Item no longer available");
        //    }
        //    var entry = context.Entry(item);
        //    entry.CurrentValues.SetValues(Inventory);
        //    entry.State = EntityState.Modified;
        //    context.SaveChanges();

        //    return Inventory;
        //}

        partial void OnInventoryCategoryDeleted(Models.clinic.InventoryCategory item);

        public async Task<Models.clinic.InventoryCategory> DeleteInventoryCategory(int? categoryId)
        {
            var item = context.InventoryCategories
                              .Where(i => i.categoryId == categoryId)
                              .Include(i => i.Inventories)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnInventoryCategoryDeleted(item);

            context.InventoryCategories.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnInventoryCategoryGet(Models.clinic.InventoryCategory item);

        public async Task<Models.clinic.InventoryCategory> GetInventoryCategoryBycategoryId(int? categoryId)
        {
            var items = context.InventoryCategories
                              .AsNoTracking()
                              .Where(i => i.categoryId == categoryId);

            var item = items.FirstOrDefault();

            OnInventoryCategoryGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.InventoryCategory> CancelInventoryCategoryChanges(Models.clinic.InventoryCategory item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnInventoryCategoryUpdated(Models.clinic.InventoryCategory item);

        public async Task<Models.clinic.InventoryCategory> UpdateInventoryCategory(int? categoryId, Models.clinic.InventoryCategory InventoryCategory)
        {
            OnInventoryCategoryUpdated(InventoryCategory);

            var item = context.InventoryCategories
                              .Where(i => i.categoryId == categoryId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(InventoryCategory);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return InventoryCategory;
        }


        partial void OnPrescriptionDeleted(Models.clinic.Prescription item);

        public async Task<Models.clinic.Prescription> DeletePrescription(int? prescriptionId)
        {
            var item = context.Prescriptions
                              .Where(i => i.prescriptionId == prescriptionId)
                              .Include(i => i.Pharmacies)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnPrescriptionDeleted(item);

            context.Prescriptions.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnPrescriptionGet(Models.clinic.Prescription item);

        public async Task<Models.clinic.Prescription> GetPrescriptionByprescriptionId(int? prescriptionId)
        {
            var items = context.Prescriptions
                              .AsNoTracking()
                              .Where(i => i.prescriptionId == prescriptionId);

            items = items.Include(i => i.Inventory);

            var item = items.FirstOrDefault();

            OnPrescriptionGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.Prescription> CancelPrescriptionChanges(Models.clinic.Prescription item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnPrescriptionUpdated(Models.clinic.Prescription item);

        public async Task<Models.clinic.Prescription> UpdatePrescription(int? prescriptionId, Models.clinic.Prescription prescription)
        {
            OnPrescriptionUpdated(prescription);

            var item = context.Prescriptions
                              .Where(i => i.prescriptionId == prescriptionId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(prescription);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return prescription;
        }


        public async Task ExportInsuranceDetailsToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/insurancedetails/excel") : "export/clinic/insurancedetails/excel", true);
        }

        public async Task ExportInsuranceDetailsToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/insurancedetails/csv") : "export/clinic/insurancedetails/csv", true);
        }

        partial void OnInsuranceDetailsRead(ref IQueryable<Models.clinic.InsuranceDetail> items);

        public async Task<IQueryable<Models.clinic.InsuranceDetail>> GetInsuranceDetails(Query query = null)
        {
            var items = context.InsuranceDetails.AsQueryable();

            items = items.Include(i => i.InsuranceProvider1);

            items = items.Include(i => i.Patient);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnInsuranceDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnInsuranceDetailCreated(Models.clinic.InsuranceDetail item);

        public async Task<Models.clinic.InsuranceDetail> CreateInsuranceDetail(Models.clinic.InsuranceDetail insuranceDetail)
        {
            OnInsuranceDetailCreated(insuranceDetail);

            context.InsuranceDetails.Add(insuranceDetail);
            context.SaveChanges();

            return insuranceDetail;
        }
        public async Task ExportInsuranceProvidersToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/insuranceproviders/excel") : "export/clinic/insuranceproviders/excel", true);
        }

        public async Task ExportInsuranceProvidersToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/insuranceproviders/csv") : "export/clinic/insuranceproviders/csv", true);
        }

        partial void OnInsuranceProvidersRead(ref IQueryable<Models.clinic.InsuranceProvider> items);

        public async Task<IQueryable<Models.clinic.InsuranceProvider>> GetInsuranceProviders(Query query = null)
        {
            var items = context.InsuranceProviders.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnInsuranceProvidersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnInsuranceProviderCreated(Models.clinic.InsuranceProvider item);

        public async Task<Models.clinic.InsuranceProvider> CreateInsuranceProvider(Models.clinic.InsuranceProvider insuranceProvider)
        {
            OnInsuranceProviderCreated(insuranceProvider);

            context.InsuranceProviders.Add(insuranceProvider);
            context.SaveChanges();

            return insuranceProvider;
        }

        partial void OnInsuranceDetailDeleted(Models.clinic.InsuranceDetail item);

        public async Task<Models.clinic.InsuranceDetail> DeleteInsuranceDetail(int? insuranceRecordId)
        {
            var item = context.InsuranceDetails
                              .Where(i => i.insuranceRecordId == insuranceRecordId)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnInsuranceDetailDeleted(item);

            context.InsuranceDetails.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnInsuranceDetailGet(Models.clinic.InsuranceDetail item);

        public async Task<Models.clinic.InsuranceDetail> GetInsuranceDetailByinsuranceRecordId(int? insuranceRecordId)
        {
            var items = context.InsuranceDetails
                              .AsNoTracking()
                              .Where(i => i.insuranceRecordId == insuranceRecordId);

            items = items.Include(i => i.InsuranceProvider1);

            items = items.Include(i => i.Patient);

            var item = items.FirstOrDefault();

            OnInsuranceDetailGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.InsuranceDetail> CancelInsuranceDetailChanges(Models.clinic.InsuranceDetail item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnInsuranceDetailUpdated(Models.clinic.InsuranceDetail item);

        public async Task<Models.clinic.InsuranceDetail> UpdateInsuranceDetail(int? insuranceRecordId, Models.clinic.InsuranceDetail insuranceDetail)
        {
            OnInsuranceDetailUpdated(insuranceDetail);

            var item = context.InsuranceDetails
                              .Where(i => i.insuranceRecordId == insuranceRecordId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(insuranceDetail);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return insuranceDetail;
        }

        partial void OnInsuranceProviderDeleted(Models.clinic.InsuranceProvider item);

        public async Task<Models.clinic.InsuranceProvider> DeleteInsuranceProvider(int? providerId)
        {
            var item = context.InsuranceProviders
                              .Where(i => i.providerId == providerId)
                              .Include(i => i.InsuranceDetails)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnInsuranceProviderDeleted(item);

            context.InsuranceProviders.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnInsuranceProviderGet(Models.clinic.InsuranceProvider item);

        public async Task<Models.clinic.InsuranceProvider> GetInsuranceProviderByproviderId(int? providerId)
        {
            var items = context.InsuranceProviders
                              .AsNoTracking()
                              .Where(i => i.providerId == providerId);

            var item = items.FirstOrDefault();

            OnInsuranceProviderGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.InsuranceProvider> CancelInsuranceProviderChanges(Models.clinic.InsuranceProvider item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnInsuranceProviderUpdated(Models.clinic.InsuranceProvider item);

        public async Task<Models.clinic.InsuranceProvider> UpdateInsuranceProvider(int? providerId, Models.clinic.InsuranceProvider insuranceProvider)
        {
            OnInsuranceProviderUpdated(insuranceProvider);

            var item = context.InsuranceProviders
                              .Where(i => i.providerId == providerId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(insuranceProvider);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return insuranceProvider;
        }

        public async Task ExportBillsToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/bills/excel") : "export/clinic/bills/excel", true);
        }

        public async Task ExportBillsToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/bills/csv") : "export/clinic/bills/csv", true);
        }

        partial void OnBillsRead(ref IQueryable<Models.clinic.Bill> items);

        public async Task<IQueryable<Models.clinic.Bill>> GetBills(Query query = null)
        {
            var items = context.Bills.AsQueryable();

            items = items.Include(i => i.Appointment);

            items = items.Include(i => i.Appointment.Patient);

            items = items.Include(i => i.BillDetails);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnBillsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBillCreated(Models.clinic.Bill item);

        public async Task<Models.clinic.Bill> CreateBill(Models.clinic.Bill bill)
        {
            OnBillCreated(bill);

            context.Bills.Add(bill);
            context.SaveChanges();

            return bill;
        }
        public async Task ExportBillDetailsToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/billdetails/excel") : "export/clinic/billdetails/excel", true);
        }

        public async Task ExportBillDetailsToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/billdetails/csv") : "export/clinic/billdetails/csv", true);
        }

        partial void OnBillDetailsRead(ref IQueryable<Models.clinic.BillDetail> items);

        public async Task<IQueryable<Models.clinic.BillDetail>> GetBillDetails(Query query = null)
        {
            var items = context.BillDetails.AsQueryable();

            items = items.Include(i => i.Bill);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnBillDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBillDetailCreated(Models.clinic.BillDetail item);

        public async Task<Models.clinic.BillDetail> CreateBillDetail(Models.clinic.BillDetail billDetail)
        {
            OnBillDetailCreated(billDetail);

            context.BillDetails.Add(billDetail);
            context.SaveChanges();

            return billDetail;
        }

        partial void OnBillDeleted(Models.clinic.Bill item);

        public async Task<Models.clinic.Bill> DeleteBill(int? billNo)
        {
            var item = context.Bills
                              .Where(i => i.billNo == billNo)
                              .Include(i => i.BillDetails)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnBillDeleted(item);

            context.Bills.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnBillGet(Models.clinic.Bill item);

        public async Task<Models.clinic.Bill> GetBillBybillNo(int? billNo)
        {
            var items = context.Bills
                              .AsNoTracking()
                              .Where(i => i.billNo == billNo);

            items = items.Include(i => i.Appointment);

            items = items.Include(i => i.BillDetails);

            var item = items.FirstOrDefault();

            OnBillGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.Bill> CancelBillChanges(Models.clinic.Bill item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnBillUpdated(Models.clinic.Bill item);

        public async Task<Models.clinic.Bill> UpdateBill(int? billNo, Models.clinic.Bill bill)
        {
            OnBillUpdated(bill);

            var item = context.Bills
                              .Where(i => i.billNo == billNo)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(bill);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return bill;
        }

        partial void OnBillDetailDeleted(Models.clinic.BillDetail item);

        public async Task<Models.clinic.BillDetail> DeleteBillDetail(int? entryNo)
        {
            var item = context.BillDetails
                              .Where(i => i.entryNo == entryNo)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnBillDetailDeleted(item);

            context.BillDetails.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnBillDetailGet(Models.clinic.BillDetail item);

        public async Task<Models.clinic.BillDetail> GetBillDetailByentryNo(int? entryNo)
        {
            var items = context.BillDetails
                              .AsNoTracking()
                              .Where(i => i.entryNo == entryNo);

            items = items.Include(i => i.Bill);

            var item = items.FirstOrDefault();

            OnBillDetailGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.BillDetail> CancelBillDetailChanges(Models.clinic.BillDetail item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnBillDetailUpdated(Models.clinic.BillDetail item);

        public async Task<Models.clinic.BillDetail> UpdateBillDetail(int? entryNo, Models.clinic.BillDetail billDetail)
        {
            OnBillDetailUpdated(billDetail);

            var item = context.BillDetails
                              .Where(i => i.entryNo == entryNo)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(billDetail);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return billDetail;
        }

        partial void OnBillDetailUpdated1(Models.clinic.BillDetail item);

        public async Task<Models.clinic.BillDetail> UpdateBillDetail1(int? entryNo, Models.clinic.BillDetail billDetail)
        {
            OnBillDetailUpdated1(billDetail);

            var item = context.BillDetails
                              .Where(i => i.entryNo == entryNo)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            //entry.CurrentValues.SetValues(billDetail);
            //entry.State = EntityState.Modified;
            context.SaveChanges();

            return billDetail;
        }

        partial void OnDoctorDeleted(Models.clinic.Doctor item);

        public async Task<Models.clinic.Doctor> DeleteDoctor(int? employeeId)
        {
            var item = context.Doctors
                              .Where(i => i.employeeId == employeeId)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnDoctorDeleted(item);

            context.Doctors.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnDoctorGet(Models.clinic.Doctor item);

        public async Task<Models.clinic.Doctor> GetDoctorByemployeeId(int? employeeId)
        {
            var items = context.Doctors
                              .AsNoTracking()
                              .Where(i => i.employeeId == employeeId);

            items = items.Include(i => i.DoctorType1);

            items = items.Include(i => i.Employee);

            var item = items.FirstOrDefault();

            OnDoctorGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.Doctor> CancelDoctorChanges(Models.clinic.Doctor item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnDoctorUpdated(Models.clinic.Doctor item);

        public async Task<Models.clinic.Doctor> UpdateDoctor(int? employeeId, Models.clinic.Doctor doctor)
        {
            OnDoctorUpdated(doctor);

            var item = context.Doctors
                              .Where(i => i.employeeId == employeeId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(doctor);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return doctor;
        }

        partial void OnDoctorTypeDeleted(Models.clinic.DoctorType item);

        public async Task<Models.clinic.DoctorType> DeleteDoctorType(int? doctorTypeId)
        {
            var item = context.DoctorTypes
                              .Where(i => i.doctorTypeId == doctorTypeId)
                              .Include(i => i.Doctors)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnDoctorTypeDeleted(item);

            context.DoctorTypes.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnDoctorTypeGet(Models.clinic.DoctorType item);

        public async Task<Models.clinic.DoctorType> GetDoctorTypeBydoctorTypeId(int? doctorTypeId)
        {
            var items = context.DoctorTypes
                              .AsNoTracking()
                              .Where(i => i.doctorTypeId == doctorTypeId);

            var item = items.FirstOrDefault();

            OnDoctorTypeGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.DoctorType> CancelDoctorTypeChanges(Models.clinic.DoctorType item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnDoctorTypeUpdated(Models.clinic.DoctorType item);

        public async Task<Models.clinic.DoctorType> UpdateDoctorType(int? doctorTypeId, Models.clinic.DoctorType doctorType)
        {
            OnDoctorTypeUpdated(doctorType);

            var item = context.DoctorTypes
                              .Where(i => i.doctorTypeId == doctorTypeId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(doctorType);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return doctorType;
        }

        public async Task ExportDoctorsToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/doctors/excel") : "export/clinic/doctors/excel", true);
        }

        public async Task ExportDoctorsToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/doctors/csv") : "export/clinic/doctors/csv", true);
        }

        partial void OnDoctorsRead(ref IQueryable<Models.clinic.Doctor> items);

        public async Task<IQueryable<Models.clinic.Doctor>> GetDoctors(Query query = null)
        {
            var items = context.Doctors.AsQueryable();

            items = items.Include(i => i.DoctorType1);

            items = items.Include(i => i.Employee);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnDoctorsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDoctorCreated(Models.clinic.Doctor item);

        public async Task<Models.clinic.Doctor> CreateDoctor(Models.clinic.Doctor doctor)
        {
            OnDoctorCreated(doctor);

            context.Doctors.Add(doctor);
            context.SaveChanges();

            return doctor;
        }
        public async Task ExportDoctorTypesToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/doctortypes/excel") : "export/clinic/doctortypes/excel", true);
        }

        public async Task ExportDoctorTypesToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/doctortypes/csv") : "export/clinic/doctortypes/csv", true);
        }

        partial void OnDoctorTypesRead(ref IQueryable<Models.clinic.DoctorType> items);

        public async Task<IQueryable<Models.clinic.DoctorType>> GetDoctorTypes(Query query = null)
        {
            var items = context.DoctorTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnDoctorTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDoctorTypeCreated(Models.clinic.DoctorType item);

        public async Task<Models.clinic.DoctorType> CreateDoctorType(Models.clinic.DoctorType doctorType)
        {
            OnDoctorTypeCreated(doctorType);

            context.DoctorTypes.Add(doctorType);
            context.SaveChanges();

            return doctorType;
        }

        public async Task ExportPaymentMethodsToExcel(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/paymentmethods/excel") : "export/clinic/paymentmethods/excel", true);
        }

        public async Task ExportPaymentMethodsToCSV(Query query = null)
        {
            //navigationManager.NavigateTo(query != null ? query.ToUrl("export/clinic/paymentmethods/csv") : "export/clinic/paymentmethods/csv", true);
        }

        partial void OnPaymentMethodsRead(ref IQueryable<Models.clinic.PaymentMethod> items);

        public async Task<IQueryable<Models.clinic.PaymentMethod>> GetPaymentMethods(Query query = null)
        {
            var items = context.PaymentMethods.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPaymentMethodsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPaymentMethodCreated(Models.clinic.PaymentMethod item);

        public async Task<Models.clinic.PaymentMethod> CreatePaymentMethod(Models.clinic.PaymentMethod paymentMethod)
        {
            OnPaymentMethodCreated(paymentMethod);

            context.PaymentMethods.Add(paymentMethod);
            context.SaveChanges();

            return paymentMethod;
        }

        partial void OnPaymentMethodDeleted(Models.clinic.PaymentMethod item);

        public async Task<Models.clinic.PaymentMethod> DeletePaymentMethod(int? methodId)
        {
            var item = context.PaymentMethods
                              .Where(i => i.methodId == methodId)
                              .Include(i => i.Appointments)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnPaymentMethodDeleted(item);

            context.PaymentMethods.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnPaymentMethodGet(Models.clinic.PaymentMethod item);

        public async Task<Models.clinic.PaymentMethod> GetPaymentMethodBymethodId(int? methodId)
        {
            var items = context.PaymentMethods
                              .AsNoTracking()
                              .Where(i => i.methodId == methodId);

            var item = items.FirstOrDefault();

            OnPaymentMethodGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.PaymentMethod> CancelPaymentMethodChanges(Models.clinic.PaymentMethod item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnPaymentMethodUpdated(Models.clinic.PaymentMethod item);

        public async Task<Models.clinic.PaymentMethod> UpdatePaymentMethod(int? methodId, Models.clinic.PaymentMethod paymentMethod)
        {
            OnPaymentMethodUpdated(paymentMethod);

            var item = context.PaymentMethods
                              .Where(i => i.methodId == methodId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(paymentMethod);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return paymentMethod;
        }


        partial void OnOperationSubtypeDeleted(Models.clinic.OperationSubtype item);

        public async Task<Models.clinic.OperationSubtype> DeleteOperationSubtype(int? operationSubTypeId)
        {
            var item = context.OperationSubtypes
                              .Where(i => i.operationSubTypeId == operationSubTypeId)
                              .Include(i => i.OperationRequests)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnOperationSubtypeDeleted(item);

            context.OperationSubtypes.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnOperationSubtypeGet(Models.clinic.OperationSubtype item);

        public async Task<Models.clinic.OperationSubtype> GetOperationSubtypeByoperationSubTypeId(int? operationSubTypeId)
        {
            var items = context.OperationSubtypes
                              .AsNoTracking()
                              .Where(i => i.operationSubTypeId == operationSubTypeId);

            items = items.Include(i => i.OperationType);

            var item = items.FirstOrDefault();

            OnOperationSubtypeGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.OperationSubtype> CancelOperationSubtypeChanges(Models.clinic.OperationSubtype item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOperationSubtypeUpdated(Models.clinic.OperationSubtype item);

        public async Task<Models.clinic.OperationSubtype> UpdateOperationSubtype(int? operationSubTypeId, Models.clinic.OperationSubtype operationSubtype)
        {
            OnOperationSubtypeUpdated(operationSubtype);

            var item = context.OperationSubtypes
                              .Where(i => i.operationSubTypeId == operationSubTypeId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(operationSubtype);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return operationSubtype;
        }








        partial void OnOperationRequestDeleted(Models.clinic.OperationRequest item);

        public async Task<Models.clinic.OperationRequest> DeleteOperationRequest(int? operationRequestId)
        {
            var item = context.OperationRequests
                              .Where(i => i.operationRequestId == operationRequestId)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnOperationRequestDeleted(item);

            context.OperationRequests.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnOperationRequestGet(Models.clinic.OperationRequest item);

        public async Task<Models.clinic.OperationRequest> GetOperationRequestByoperationRequestId(int? operationRequestId)
        {
            var items = context.OperationRequests
                              .AsNoTracking()
                              .Where(i => i.operationRequestId == operationRequestId);

            items = items.Include(i => i.OperationSubtype1);

            items = items.Include(i => i.Appointment);

            var item = items.FirstOrDefault();

            OnOperationRequestGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.clinic.OperationRequest> CancelOperationRequestChanges(Models.clinic.OperationRequest item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOperationRequestUpdated(Models.clinic.OperationRequest item);

        public async Task<Models.clinic.OperationRequest> UpdateOperationRequest(int? operationRequestId, Models.clinic.OperationRequest operationRequest)
        {
            OnOperationRequestUpdated(operationRequest);

            var item = context.OperationRequests
                              .Where(i => i.operationRequestId == operationRequestId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(operationRequest);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return operationRequest;
        }



        partial void OnOperationSubtypeCreated(Models.clinic.OperationSubtype item);

        public async Task<Models.clinic.OperationSubtype> CreateOperationSubtype(Models.clinic.OperationSubtype operationSubtype)
        {
            OnOperationSubtypeCreated(operationSubtype);

            context.OperationSubtypes.Add(operationSubtype);
            context.SaveChanges();

            return operationSubtype;
        }
        partial void OnOperationSubtypesRead(ref IQueryable<Models.clinic.OperationSubtype> items);

        public async Task<IQueryable<Models.clinic.OperationSubtype>> GetOperationSubtypes(Query query = null)
        {
            var items = context.OperationSubtypes.AsQueryable();

            items = items.Include(i => i.OperationType);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOperationSubtypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOperationRequestsRead(ref IQueryable<Models.clinic.OperationRequest> items);

        public async Task<IQueryable<Models.clinic.OperationRequest>> GetOperationRequests(Query query = null)
        {
            var items = context.OperationRequests.AsQueryable();

            items = items.Include(i => i.OperationSubtype1);

            items = items.Include(i => i.Appointment);

            items = items.Include(i => i.OperationSubtype1.OperationType);

            items = items.Include(i => i.Appointment.Patient);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOperationRequestsRead(ref items);

            return await Task.FromResult(items);
        }




        partial void OnOperationRequestCreated(Models.clinic.OperationRequest item);

        public async Task<Models.clinic.OperationRequest> CreateOperationRequest(Models.clinic.OperationRequest operationRequest)
        {
            OnOperationRequestCreated(operationRequest);

            context.OperationRequests.Add(operationRequest);
            context.SaveChanges();

            return operationRequest;
        }


        partial void OnCashTypeRead(ref IQueryable<Models.clinic.CashType> items);

        public async Task<IQueryable<Models.clinic.CashType>> GetCashType(Query query = null)
        {
            var items = context.CashTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCashTypeRead(ref items);

            return await Task.FromResult(items);
        }


        partial void OnAccountsReceivableCreated(Models.clinic.AccountsReceivable item);

        public async Task<Models.clinic.AccountsReceivable> CreateAccountsReceivable(Models.clinic.AccountsReceivable accountsReceivableRecord)
        {
            //OnAccountsReceivableCreated(accountsReceivableRecord);

            //context.AccountsReceivables.Add(accountsReceivableRecord);
            //context.SaveChanges();

            using (var connection = new MySqlConnection("Server=202.182.120.224;Database=clinic;User ID=remote;Password=#3PqKZ$F3G=y9NSD;Connection Timeout=100"))
            {
                connection.Open();
                var query = $"INSERT INTO `clinic`.`accounts_receivable`(`amountDue`,`amountPaid`,`dateOfTransaction`,`paymentMethod`,`cashType`,`billDetailEntryNo`,`transactionRefrence`)VALUES({accountsReceivableRecord.AmountDue},{accountsReceivableRecord.AmountPaid},'{accountsReceivableRecord.FormattedDateTime}',{accountsReceivableRecord.PaymentMethod},{accountsReceivableRecord.CashType},{accountsReceivableRecord.BillDetailEntryNo},'{accountsReceivableRecord.TransactionRefrence}');";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                connection.Close();
            }            

            return accountsReceivableRecord;
        }



        partial void OnCountiesRead(ref IQueryable<Models.clinic.County> items);

        public async Task<IQueryable<Models.clinic.County>> GetCounties(Query query = null)
        {
            var items = context.Counties.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCountiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSubcountiesRead(ref IQueryable<Models.clinic.Subcounty> items);

        public async Task<IQueryable<Models.clinic.Subcounty>> GetSubcounties(Query query = null)
        {
            var items = context.SubCounties.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnSubcountiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnVitalsRead(ref IQueryable<Models.clinic.Ward> items);

        public async Task<IQueryable<Models.clinic.Ward>> GetWards(Query query = null)
        {
            var items = context.Wards.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnVitalsRead(ref items);

            return await Task.FromResult(items);
        }


        partial void OnRelationshipsRead(ref IQueryable<Models.clinic.Relationship> items);

        public async Task<IQueryable<Models.clinic.Relationship>> GetRelationships(Query query = null)
        {
            var items = context.Relationships.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnRelationshipsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRelationshipCreated(Models.clinic.Relationship item);

        public async Task<Models.clinic.Relationship> CreateRelationship(Models.clinic.Relationship relationship)
        {
            OnRelationshipCreated(relationship);

            context.Relationships.Add(relationship);
            context.SaveChanges();

            return relationship;
        }


        public async Task<Models.clinic.Relationship> CancelRelationshipChanges(Models.clinic.Relationship item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }


        partial void OnRelationshipUpdated(Models.clinic.Relationship item);

        public async Task<Models.clinic.Relationship> UpdateRelationship(int? relationshipId, Models.clinic.Relationship relationship)
        {
            OnRelationshipUpdated(relationship);

            var item = context.Relationships
                              .Where(i => i.relationshipId == relationshipId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(relationship);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return relationship;
        }

        partial void OnRelationshipGet(Models.clinic.Relationship item);

        public async Task<Models.clinic.Relationship> GetRelationshipByrelationshipId(int? relationshipId)
        {
            var items = context.Relationships
                              .AsNoTracking()
                              .Where(i => i.relationshipId == relationshipId);

            var item = items.FirstOrDefault();

            OnRelationshipGet(item);

            return await Task.FromResult(item);
        }

        partial void OnRelationshipDeleted(Models.clinic.Relationship item);

        public async Task<Models.clinic.Relationship> DeleteRelationship(int? relationshipId)
        {
            var item = context.Relationships
                              .Where(i => i.relationshipId == relationshipId)
                              .Include(i => i.Patients)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnRelationshipDeleted(item);

            context.Relationships.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnAccountsReceivablesRead(ref IQueryable<Models.clinic.AccountsReceivable> items);

        public async Task<IQueryable<Models.clinic.AccountsReceivable>> GetAccountsReceivables(Query query = null)
        {
            var items = context.AccountsReceivables.AsQueryable();

            //items = items.Include(i => i.PaymentMethod1);

            //items = items.Include(i => i.CashType1);

            //items = items.Include(i => i.BillDetail1);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAccountsReceivablesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSupplierCreated(Models.clinic.Supplier item);

        public async Task<Models.clinic.Supplier> CreateSupplier(Models.clinic.Supplier supplier)
        {
            OnSupplierCreated(supplier);

            context.Supplier.Add(supplier);
            context.SaveChanges();

            return supplier;
        }

        partial void OnSuppliersRead(ref IQueryable<Models.clinic.Supplier> items);

        public async Task<IQueryable<Models.clinic.Supplier>> GetSuppliers(Query query = null)
        {
            var items = context.Supplier.AsQueryable();
            items = items.AsNoTracking();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnSuppliersRead(ref items);

            return await Task.FromResult(items);
        }


        partial void OnSupplierUpdated(Models.clinic.Supplier item);

        public async Task<Models.clinic.Supplier> UpdateSupplier(int? supplierId, Models.clinic.Supplier supplier)
        {
            OnSupplierUpdated(supplier);

            var item = context.Supplier
                              .Where(i => i.suppliersID == supplierId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(supplier);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return supplier;
        }

        partial void OnSupplierGet(Models.clinic.Supplier item);

        public async Task<Models.clinic.Supplier> GetSuppliersById(int? id)
        {
            var items = context.Supplier
                              .AsNoTracking()
                              .Where(i => i.suppliersID == id);

            var item = items.FirstOrDefault();

            OnSupplierGet(item);

            return await Task.FromResult(item);
        }


        partial void OnAccountsPayableCreated(Models.clinic.AccountsPayable item);

        public async Task<Models.clinic.AccountsPayable> CreateAccountsPayable(Models.clinic.AccountsPayable accountsPayableRecord)
        {
            OnAccountsPayableCreated(accountsPayableRecord);

            context.AccountsPayables.Add(accountsPayableRecord);
            context.SaveChanges();

            return accountsPayableRecord;
        }

        partial void OnAccountsPayableRead(ref IQueryable<Models.clinic.AccountsPayable> items);

        public async Task<IQueryable<Models.clinic.AccountsPayable>> GetAccountsPayables(Query query = null)
        {
            var items = context.AccountsPayables.AsQueryable();

            items = items.Include(i => i.PaymentMethod1);

            items = items.Include(i => i.Supplier);

            items = items.Include(i => i.Inventory);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAccountsPayableRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnClinicSetupUpdate(Models.clinic.ClinicSetup item);

        public async Task<Models.clinic.ClinicSetup> UpdateClinicSetup(int? id, Models.clinic.ClinicSetup clinicSetup)
        {
            OnClinicSetupUpdate(clinicSetup);
            var item = context.ClinicSetup
                  .Where(i => i.id == id)
                  .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(clinicSetup);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return clinicSetup;
        }


        partial void OnGetClinicSetup(ref IQueryable<Models.clinic.ClinicSetup> items);

        public async Task<IQueryable<Models.clinic.ClinicSetup>> GetClinicSetup(Query query = null)
        {
            var items = context.ClinicSetup.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnGetClinicSetup(ref items);

            return await Task.FromResult(items);
        }


        partial void OnUnitOfMeasureRead(ref IQueryable<Models.clinic.UnitOfMeasure> items);

        public async Task<IQueryable<Models.clinic.UnitOfMeasure>> GetUnitOfMeasures(Query query = null)
        {
            var items = context.UnitOfMeasure.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnUnitOfMeasureRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUnitOfMeasureCreated(Models.clinic.UnitOfMeasure item);

        public async Task<Models.clinic.UnitOfMeasure> CreateUnitOfMeasure(Models.clinic.UnitOfMeasure uom)
        {
            OnUnitOfMeasureCreated(uom);

            context.UnitOfMeasure.Add(uom);
            context.SaveChanges();

            return uom;
        }

        partial void OnUnitOfMeasureUpdated(Models.clinic.UnitOfMeasure item);

        public async Task<Models.clinic.UnitOfMeasure> UpdateUnitOfMeasure(int? uomId, Models.clinic.UnitOfMeasure uom)
        {
            OnUnitOfMeasureUpdated(uom);

            var item = context.UnitOfMeasure
                              .Where(i => i.uomId == uomId)
                              .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(uom);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return uom;
        }

        partial void OnUnitOfMesureGet(Models.clinic.UnitOfMeasure item);

        public async Task<Models.clinic.UnitOfMeasure> GetUnitOfMeasureByid(int? uomId)
        {
            var items = context.UnitOfMeasure
                              .AsNoTracking()
                              .Where(i => i.uomId == uomId);

            var item = items.FirstOrDefault();

            OnUnitOfMesureGet(item);

            return await Task.FromResult(item);
        }

        partial void OnUnitOfMeasureDelete(Models.clinic.UnitOfMeasure item);

        public async Task<Models.clinic.UnitOfMeasure> DeleteUnitOfMeasure(int? uomId)
        {
            var item = context.UnitOfMeasure
                              .Where(i => i.uomId == uomId)
                              .FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item no longer available");
            }

            OnUnitOfMeasureDelete(item);

            context.UnitOfMeasure.Remove(item);
            context.SaveChanges();

            return item;
        }

    }
}

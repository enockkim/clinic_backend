using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using clinic.Models.clinic;

namespace clinic.Data
{
    public partial class ClinicContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {
        }

        public ClinicContext()
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<clinic.Models.clinic.Appointment>()
                  .HasOne(i => i.Patient)
                  .WithMany(i => i.Appointments)
                  .HasForeignKey(i => i.patientId)
                  .HasPrincipalKey(i => i.patientId);
            builder.Entity<clinic.Models.clinic.Appointment>()
                  .HasOne(i => i.Employee)
                  .WithMany(i => i.Appointments)
                  .HasForeignKey(i => i.employeeId)
                  .HasPrincipalKey(i => i.employeeId);
            builder.Entity<clinic.Models.clinic.Appointment>()
                  .HasOne(i => i.PaymentMethod1)
                  .WithMany(i => i.Appointments)
                  .HasForeignKey(i => i.paymentMethod)
                  .HasPrincipalKey(i => i.methodId);
            builder.Entity<clinic.Models.clinic.Appointment>()
                  .HasOne(i => i.Facilities)
                  .WithMany(i => i.Appointments)
                  .HasForeignKey(i => i.appointmentStatus)
                  .HasPrincipalKey(i => i.facilityId);
            builder.Entity<clinic.Models.clinic.Appointment>()
                  .HasOne(i => i.AppointmentType1)
                  .WithMany(i => i.Appointments)
                  .HasForeignKey(i => i.appointmentType)
                  .HasPrincipalKey(i => i.typeId);
            builder.Entity<clinic.Models.clinic.Appointment>()
                  .HasOne(i => i.PatientType)
                  .WithMany(i => i.Appointments)
                  .HasForeignKey(i => i.patientType)
                  .HasPrincipalKey(i => i.patientTypeId);
            builder.Entity<clinic.Models.clinic.Laboratory>()
                  .HasOne(i => i.LaboratoryType)
                  .WithMany(i => i.Laboratories)
                  .HasForeignKey(i => i.labTypeId)
                  .HasPrincipalKey(i => i.labTypeID);
            builder.Entity<clinic.Models.clinic.Laboratory>()
                  .HasOne(i => i.Appointment)
                  .WithMany(i => i.Laboratories)
                  .HasForeignKey(i => i.appointmentId)
                  .HasPrincipalKey(i => i.appointmentId);
            builder.Entity<clinic.Models.clinic.Patient>()
                  .HasOne(i => i.Gender1)
                  .WithMany(i => i.Patients)
                  .HasForeignKey(i => i.gender)
                  .HasPrincipalKey(i => i.id);
            builder.Entity<clinic.Models.clinic.Patient>()
                  .HasOne(i => i.Relationship)
                  .WithMany(i => i.Patients)
                  .HasForeignKey(i => i.nokRelationship)
                  .HasPrincipalKey(i => i.relationshipId);
            builder.Entity<clinic.Models.clinic.Pharmacy>()
                  .HasOne(i => i.Prescription)
                  .WithMany(i => i.Pharmacies)
                  .HasForeignKey(i => i.prescriptionId)
                  .HasPrincipalKey(i => i.prescriptionId);
            builder.Entity<clinic.Models.clinic.Prescription>()
                  .HasOne(i => i.Inventory)
                  .WithMany(i => i.Prescriptions)
                  .HasForeignKey(i => i.itemId)
                  .HasPrincipalKey(i => i.itemId);
            builder.Entity<clinic.Models.clinic.Prescription>()
                  .HasOne(i => i.Appointment)
                  .WithMany(i => i.Prescriptions)
                  .HasForeignKey(i => i.appointmentId)
                  .HasPrincipalKey(i => i.appointmentId);
            builder.Entity<clinic.Models.clinic.Employee>()
                  .HasOne(i => i.Gender1)
                  .WithMany(i => i.Staffs)
                  .HasForeignKey(i => i.gender)
                  .HasPrincipalKey(i => i.id);
            builder.Entity<clinic.Models.clinic.Employee>()
                  .HasOne(i => i.Designation)
                  .WithMany(i => i.Staffs)
                  .HasForeignKey(i => i.designationId)
                  .HasPrincipalKey(i => i.designationId);
            builder.Entity<clinic.Models.clinic.Vital>()
                  .HasOne(i => i.Appointment)
                  .WithMany(i => i.Vitals)
                  .HasForeignKey(i => i.appointmentId)
                  .HasPrincipalKey(i => i.appointmentId);
            builder.Entity<clinic.Models.clinic.Consultations>()
                  .HasOne(i => i.Appointment)
                  .WithMany(i => i.Consultations)
                  .HasForeignKey(i => i.appointmentId)
                  .HasPrincipalKey(i => i.appointmentId);
            builder.Entity<clinic.Models.clinic.DiagnositcImagingSubtype>()
                  .HasOne(i => i.DiagnosticImagingType)
                  .WithMany(i => i.DiagnositcImagingSubtypes)
                  .HasForeignKey(i => i.imagingTypeId)
                  .HasPrincipalKey(i => i.imagingTypeId);
            builder.Entity<clinic.Models.clinic.DiagnosticImagingRequest>()
                  .HasOne(i => i.Appointment)
                  .WithMany(i => i.DiagnosticImagingRequests)
                  .HasForeignKey(i => i.appointmentId)
                  .HasPrincipalKey(i => i.appointmentId);
            builder.Entity<clinic.Models.clinic.DiagnosticImagingRequest>()
                  .HasOne(i => i.DiagnositcImagingSubtype)
                  .WithMany(i => i.DiagnosticImagingRequests)
                  .HasForeignKey(i => i.imagingSubType)
                  .HasPrincipalKey(i => i.imagingSubTypeId);
            builder.Entity<clinic.Models.clinic.Inventory>()
                  .HasOne(i => i.InventoryCategory)
                  .WithMany(i => i.Inventories)
                  .HasForeignKey(i => i.category)
                  .HasPrincipalKey(i => i.categoryId);
            builder.Entity<clinic.Models.clinic.Inventory>()
                  .HasOne(i => i.AdministrationType1)
                  .WithMany(i => i.Inventories)
                  .HasForeignKey(i => i.administrationType)
                  .HasPrincipalKey(i => i.administrationTypeId);
            builder.Entity<clinic.Models.clinic.Inventory>()
                  .HasOne(i => i.UnitOfMeasure1)
                  .WithMany(i => i.Inventories)
                  .HasForeignKey(i => i.UnitOfMeasure)
                  .HasPrincipalKey(i => i.uomId);
            builder.Entity<clinic.Models.clinic.InsuranceDetail>()
                  .HasOne(i => i.InsuranceProvider1)
                  .WithMany(i => i.InsuranceDetails)
                  .HasForeignKey(i => i.insuranceProvider)
                  .HasPrincipalKey(i => i.providerId);
            builder.Entity<clinic.Models.clinic.InsuranceDetail>()
                  .HasOne(i => i.Patient)
                  .WithMany(i => i.InsuranceDetails)
                  .HasForeignKey(i => i.patientId)
                  .HasPrincipalKey(i => i.patientId);
            builder.Entity<clinic.Models.clinic.Doctor>()
                  .HasOne(i => i.DoctorType1)
                  .WithMany(i => i.Doctors)
                  .HasForeignKey(i => i.doctorType)
                  .HasPrincipalKey(i => i.doctorTypeId);
            builder.Entity<clinic.Models.clinic.Doctor>()
                  .HasOne(i => i.Employee)
                  .WithMany(i => i.Doctors)
                  .HasForeignKey(i => i.employeeId)
                  .HasPrincipalKey(i => i.employeeId);
            builder.Entity<clinic.Models.clinic.Bill>()
                  .HasOne(i => i.Appointment)
                  .WithMany(i => i.Bills)
                  .HasForeignKey(i => i.appointmentId)
                  .HasPrincipalKey(i => i.appointmentId);
            builder.Entity<clinic.Models.clinic.BillDetail>()
                  .HasOne(i => i.Bill)
                  .WithMany(i => i.BillDetails)
                  .HasForeignKey(i => i.billNo)
                  .HasPrincipalKey(i => i.billNo);
            builder.Entity<clinic.Models.clinic.OperationRequest>()
                  .HasOne(i => i.OperationSubtype1)
                  .WithMany(i => i.OperationRequests)
                  .HasForeignKey(i => i.operationSubType)
                  .HasPrincipalKey(i => i.operationSubTypeId);
            builder.Entity<clinic.Models.clinic.OperationRequest>()
                  .HasOne(i => i.Appointment)
                  .WithMany(i => i.OperationRequests)
                  .HasForeignKey(i => i.appointmentId)
                  .HasPrincipalKey(i => i.appointmentId);
            builder.Entity<clinic.Models.clinic.OperationRoom>()
                  .HasOne(i => i.OperationType)
                  .WithMany(i => i.OperationRooms)
                  .HasForeignKey(i => i.orTypeId)
                  .HasPrincipalKey(i => i.operationTypeId);
            builder.Entity<clinic.Models.clinic.OperationRoom>()
                  .HasOne(i => i.Appointment)
                  .WithMany(i => i.OperationRooms)
                  .HasForeignKey(i => i.appointmentId)
                  .HasPrincipalKey(i => i.appointmentId);
            builder.Entity<clinic.Models.clinic.OperationRoom>()
                  .HasOne(i => i.Appointment)
                  .WithMany(i => i.OperationRooms)
                  .HasForeignKey(i => i.appointmentId)
                  .HasPrincipalKey(i => i.appointmentId);
            builder.Entity<clinic.Models.clinic.OperationSubtype>()
                  .HasOne(i => i.OperationType)
                  .WithMany(i => i.OperationSubtypes)
                  .HasForeignKey(i => i.operationTypeId)
                  .HasPrincipalKey(i => i.operationTypeId);
            builder.Entity<clinic.Models.clinic.AccountsReceivable>()
                  .HasOne(i => i.PaymentMethod1)
                  .WithMany(i => i.AccountsReceivables)
                  .HasForeignKey(i => i.paymentMethod)
                  .HasPrincipalKey(i => i.methodId);
            builder.Entity<clinic.Models.clinic.AccountsReceivable>()
                  .HasOne(i => i.CashType1)
                  .WithMany(i => i.AccountsReceivables)
                  .HasForeignKey(i => i.cashType1)
                  .HasPrincipalKey(i => i.cashTypeId);
            builder.Entity<clinic.Models.clinic.AccountsReceivable>()
                  .HasOne(i => i.BillDetail1)
                  .WithMany(i => i.AccountsReceivables)
                  .HasForeignKey(i => i.billDetailEntryNo)
                  .HasPrincipalKey(i => i.entryNo);
            builder.Entity<clinic.Models.clinic.AppointmentType>()
                  .HasOne(i => i.PatientType)
                  .WithMany(i => i.AppointmentType)
                  .HasForeignKey(i => i.patientType)
                  .HasPrincipalKey(i => i.patientTypeId);
            builder.Entity<clinic.Models.clinic.Subcounty>()
                  .HasOne(i => i.County)
                  .WithMany(i => i.Subcounties)
                  .HasForeignKey(i => i.countyId)
                  .HasPrincipalKey(i => i.id);
            builder.Entity<clinic.Models.clinic.Ward>()
                  .HasOne(i => i.Subcounty)
                  .WithMany(i => i.Ward)
                  .HasForeignKey(i => i.subcountyId)
                  .HasPrincipalKey(i => i.subcountyId);
            builder.Entity<clinic.Models.clinic.AccountsPayable>()
                  .HasOne(i => i.PaymentMethod1)
                  .WithMany(i => i.AccountsPayable)
                  .HasForeignKey(i => i.paymentMethod)
                  .HasPrincipalKey(i => i.methodId);
            builder.Entity<clinic.Models.clinic.AccountsPayable>()
                  .HasOne(i => i.Supplier)
                  .WithMany(i => i.AccountsPayable)
                  .HasForeignKey(i => i.supplierId)
                  .HasPrincipalKey(i => i.suppliersID);
            builder.Entity<clinic.Models.clinic.AccountsPayable>()
                  .HasOne(i => i.Inventory)
                  .WithMany(i => i.AccountsPayable)
                  .HasForeignKey(i => i.itemId)
                  .HasPrincipalKey(i => i.itemId);

            this.OnModelBuilding(builder);
        }


        public DbSet<clinic.Models.clinic.Appointment> Appointments
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.AppointmentType> AppointmentTypes
        {
            get;
            set;
        }


        public DbSet<clinic.Models.clinic.Designation> Designations
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.Gender> Genders
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.Laboratory> Laboratories
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.LaboratoryType> LaboratoryTypes
        {
            get;
            set;
        }


        public DbSet<clinic.Models.clinic.OperationRequest> OperationRequests
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.OperationRoom> OperationRooms
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.OperationSubtype> OperationSubtypes
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.OperationType> OperationTypes
        {
            get;
            set;
        }


        public DbSet<clinic.Models.clinic.Patient> Patients
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.Pharmacy> Pharmacies
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.Inventory> PharmacyInventories
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.Prescription> Prescriptions
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.Employee> Staffs
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.Vital> Vitals
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.Facilities> Facilities
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.Consultations> Consultations { get; set; }

        public DbSet<clinic.Models.clinic.DiagnositcImagingSubtype> DiagnositcImagingSubtypes
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.DiagnosticImagingRequest> DiagnosticImagingRequests
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.DiagnosticImagingType> DiagnosticImagingTypes
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.AdministrationType> AdministrationTypes
        {
            get;
            set;
        }
        public DbSet<clinic.Models.clinic.Inventory> Inventories
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.InventoryCategory> InventoryCategories
        {
            get;
            set;
        }


        public DbSet<clinic.Models.clinic.InsuranceDetail> InsuranceDetails
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.InsuranceProvider> InsuranceProviders
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.Doctor> Doctors
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.DoctorType> DoctorTypes
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.Bill> Bills
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.BillDetail> BillDetails
        {
            get;
            set;
        }
        public DbSet<clinic.Models.clinic.PaymentMethod> PaymentMethods
        {
            get;
            set;
        }

        public DbSet<clinic.Models.clinic.CashType> CashTypes { get; set; }
        public DbSet<clinic.Models.clinic.AccountsReceivable> AccountsReceivables { get; set; }
        public DbSet<clinic.Models.clinic.PatientType> PatientTypes { get; set; }
        public DbSet<clinic.Models.clinic.Relationship> Relationships { get; set; }
        public DbSet<clinic.Models.clinic.County> Counties { get; set; }
        public DbSet<clinic.Models.clinic.Subcounty> SubCounties { get; set; }
        public DbSet<clinic.Models.clinic.Ward> Wards { get; set; }
        public DbSet<clinic.Models.clinic.Supplier> Supplier { get; set; }
        public DbSet<clinic.Models.clinic.AccountsPayable> AccountsPayables { get; set; }
        public DbSet<clinic.Models.clinic.ClinicSetup> ClinicSetup { get; set; }
        public DbSet<clinic.Models.clinic.UnitOfMeasure> UnitOfMeasure { get; set; }
    }

}

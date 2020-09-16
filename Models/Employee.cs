using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class Employee
    {
        public Employee()
        {
            CheckFace = new HashSet<CheckFace>();
            EmployeeFace = new HashSet<EmployeeFace>();
            EmployeeInStore = new HashSet<EmployeeInStore>();
            EmployeeRoleList = new HashSet<EmployeeRoleList>();
            TokenUser = new HashSet<TokenUser>();
            WorkingShift = new HashSet<WorkingShift>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Role { get; set; }
        public string EmpEnrollNumber { get; set; }
        public int MainStoreId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public int BrandId { get; set; }
        public decimal? Salary { get; set; }
        public int Status { get; set; }
        public DateTime? DateStartWork { get; set; }
        public int? EmployeeGroupId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeRegency { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? EmployeeSex { get; set; }
        public string PersonalCardId { get; set; }
        public DateTime? DatePersonalCard { get; set; }
        public string PlaceOfPersonalCard { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string MainAddress { get; set; }
        public string EmployeeHometown { get; set; }
        public string EmployeePlaceBorn { get; set; }
        public DateTime? DateQuitWork { get; set; }
        public string ReasonQuit { get; set; }
        public string PersonalIncomTax { get; set; }
        public int? MaritalStatus { get; set; }
        public string SocialInsuranceNumber { get; set; }
        public string BankNumber { get; set; }
        public int? Bank { get; set; }
        public int? EducationType { get; set; }
        public string EducationStatus { get; set; }
        public int? EducationQualify { get; set; }
        public string Specialized { get; set; }
        public string SchoolName { get; set; }
        public string CourseYear { get; set; }
        public int? FormOfTraining { get; set; }
        public string ContactOne { get; set; }
        public string ContactTwo { get; set; }
        public string Image { get; set; }

        public virtual EmployeeGroup EmployeeGroup { get; set; }
        public virtual ICollection<CheckFace> CheckFace { get; set; }
        public virtual ICollection<EmployeeFace> EmployeeFace { get; set; }
        public virtual ICollection<EmployeeInStore> EmployeeInStore { get; set; }
        public virtual ICollection<EmployeeRoleList> EmployeeRoleList { get; set; }
        public virtual ICollection<TokenUser> TokenUser { get; set; }
        public virtual ICollection<WorkingShift> WorkingShift { get; set; }
    }
}

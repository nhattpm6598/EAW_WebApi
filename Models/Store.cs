using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class Store
    {
        public Store()
        {
            AttendanceDate = new HashSet<AttendanceDate>();
            EmployeeInStore = new HashSet<EmployeeInStore>();
            FaceScanMachine = new HashSet<FaceScanMachine>();
            StoreGroupMapping = new HashSet<StoreGroupMapping>();
            WorkingShift = new HashSet<WorkingShift>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public bool? IsAvailable { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Type { get; set; }
        public int? RoomRentMode { get; set; }
        public DateTime? ReportDate { get; set; }
        public string ShortName { get; set; }
        public int? GroupId { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public string DefaultAdminPassword { get; set; }
        public bool? HasProducts { get; set; }
        public bool? HasNews { get; set; }
        public bool? HasImageCollections { get; set; }
        public bool? HasMultipleLanguage { get; set; }
        public bool? HasWebPages { get; set; }
        public bool? HasCustomerFeedbacks { get; set; }
        public int? BrandId { get; set; }
        public bool? HasOrder { get; set; }
        public bool? HasBlogEditCollections { get; set; }
        public string LogoUrl { get; set; }
        public string FbAccessToken { get; set; }
        public string StoreFeatureFilter { get; set; }
        public bool? RunReport { get; set; }
        public int? AttendanceStoreFilter { get; set; }
        public string StoreCode { get; set; }
        public int? PosId { get; set; }
        public string StoreConfig { get; set; }
        public string DefaultDashBoard { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public bool? Active { get; set; }
        public int? PaymentTypeApply { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<AttendanceDate> AttendanceDate { get; set; }
        public virtual ICollection<EmployeeInStore> EmployeeInStore { get; set; }
        public virtual ICollection<FaceScanMachine> FaceScanMachine { get; set; }
        public virtual ICollection<StoreGroupMapping> StoreGroupMapping { get; set; }
        public virtual ICollection<WorkingShift> WorkingShift { get; set; }
    }
}

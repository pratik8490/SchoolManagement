using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Core.Models
{
    public class StudentAdmissionDetailModel
    {
        public System.DateTime AdmissionDate { get; set; }
        public int GRNo { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string EmailId { get; set; }
        public string AddressPermanent { get; set; }
        public Nullable<int> StateIdPermanent { get; set; }
        public Nullable<int> CityIdPermanent { get; set; }
        public Nullable<int> DistrictIdPermanent { get; set; }
        public Nullable<int> TalukaIdPermanent { get; set; }
        public string PincodePermanent { get; set; }
        public string Address { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int TalukaId { get; set; }
        public string Pincode { get; set; }
        public string FatherOccupation { get; set; }
        public string MotherOccupation { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public int Religion { get; set; }
        public int Category { get; set; }
        public int Cast { get; set; }
        public int BloodGroup { get; set; }
        public int Gender { get; set; }
        public Nullable<int> IsPhysicallyHandicap { get; set; }
        public string NativePlace { get; set; }
        public Nullable<int> Nationality { get; set; }
        public string StudentPhotoLink { get; set; }
        public string FatherPhotoLink { get; set; }
        public string MotherPhotoLink { get; set; }
        public string StudentPhotoName { get; set; }
        public string FatherPhotoName { get; set; }
        public string MotherPhotoName { get; set; }
        public string PhoneOffice { get; set; }
        public string PhoneResidence { get; set; }
        public string MobileNumber { get; set; }
        public int Status { get; set; }
        public string LastSchoolName { get; set; }
        public Nullable<int> PreviousPresent { get; set; }
        public Nullable<int> PreviousWorkingDays { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int AdmissionStandardId { get; set; }
        public int DepartmentId { get; set; }
        public int AcademicYearId { get; set; }
        public int AdmissionClassTypeId { get; set; }
        public string UniqueNumber { get; set; }
        public string TrusteePhotoName { get; set; }
        public string TrusteePhotoLink { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string TalukaName { get; set; }
        public string AdmissionStandardName { get; set; }
        public string AdmissionClassTypeName { get; set; }
        public string CurrentStandardName { get; set; }
        public string CurrentClassTypeName { get; set; }
        public int CurrentStandardId { get; set; }
        public int CurrentClassTypeId { get; set; }
        public int RollNumber { get; set; }
        public string DepartmentName { get; set; }
        public string StudentName { get; set; }
        public int LeaveStudentId { get; set; }
        public DateTime? LeaveDate { get; set; }
        public string ReasonOfLeave { get; set; }
        public int LCNo { get; set; }
        public int LeaveStandardId { get; set; }
        public int LeaveClassTypeId { get; set; }
        public string LeaveStandardName { get; set; }
        public string LeaveClassTypeName { get; set; }
        public int Check { get; set; }
        public string CastName { get; set; }
        public string CategoryName { get; set; }

        public bool IsPastStudent
        {
            get
            {
                if (this.Status == 2)
                    return true;
                else
                    return false;
            }
            set
            {
            }
        }
    }
}
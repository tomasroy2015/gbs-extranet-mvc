using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources.Abstract;
using Resources.Concrete;
    
namespace Resources {
    public class Resources
    {
        private static IResourceProvider resourceProvider = new DbResourceProvider();


        /// <summary>Hide Menu</summary>
        public static string HideMenu
        {
            get
            {
                return (string)resourceProvider.GetResource("HideMenu", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Show Menu</summary>
        public static string ShowMenu
        {
            get
            {
                return (string)resourceProvider.GetResource("ShowMenu", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>This field is required!</summary>
        public static string RequiredFieldWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("RequiredFieldWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>This field must be numeric!</summary>
        public static string NumericFieldWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("NumericFieldWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>This field must contain a currency value!</summary>
        public static string CurrencyFieldWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("CurrencyFieldWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>This field must contain a percentage value</summary>
        public static string PercentageFieldWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("PercentageFieldWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please enter a valid email address!</summary>
        public static string EmailFieldWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("EmailFieldWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please enter a valid phone number as shown in this example: (123) 456-78-90</summary>
        public static string PhoneFieldWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("PhoneFieldWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The maximum character limit for this field. :</summary>
        public static string MaxCharWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("MaxCharWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The minimum character limit for this field :</summary>
        public static string MinCharWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("MinCharWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The value given is out of range.Condition :</summary>
        public static string RangeWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("RangeWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Required field</summary>
        public static string RequiredFieldDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("RequiredFieldDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Select...</summary>
        public static string ListSelectExpression
        {
            get
            {
                return (string)resourceProvider.GetResource("ListSelectExpression", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Invalid date!</summary>
        public static string DateFieldWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("DateFieldWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>DD.MM.YYYY</summary>
        public static string DateFieldDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("DateFieldDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please click to select the date</summary>
        public static string DateSelector
        {
            get
            {
                return (string)resourceProvider.GetResource("DateSelector", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please select from list!</summary>
        public static string RequiredFieldListWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("RequiredFieldListWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please make a choice!</summary>
        public static string RequiredFieldRadioWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("RequiredFieldRadioWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>All...</summary>
        public static string ListAllExpression
        {
            get
            {
                return (string)resourceProvider.GetResource("ListAllExpression", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Sorry, you are not authorized!</summary>
        public static string NotAuthorized
        {
            get
            {
                return (string)resourceProvider.GetResource("NotAuthorized", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>First Page</summary>
        public static string PagingFirstPage
        {
            get
            {
                return (string)resourceProvider.GetResource("PagingFirstPage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Previous Page</summary>
        public static string PagingPreviousPage
        {
            get
            {
                return (string)resourceProvider.GetResource("PagingPreviousPage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Next Page</summary>
        public static string PagingNextPage
        {
            get
            {
                return (string)resourceProvider.GetResource("PagingNextPage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Last Page</summary>
        public static string PagingLastPage
        {
            get
            {
                return (string)resourceProvider.GetResource("PagingLastPage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Record Count</summary>
        public static string PagingRecordCount
        {
            get
            {
                return (string)resourceProvider.GetResource("PagingRecordCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Page</summary>
        public static string PagingPage
        {
            get
            {
                return (string)resourceProvider.GetResource("PagingPage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The operation has been completed successfully</summary>
        public static string OperationSuccess
        {
            get
            {
                return (string)resourceProvider.GetResource("OperationSuccess", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>An error has occured while processing</summary>
        public static string OperationFail
        {
            get
            {
                return (string)resourceProvider.GetResource("OperationFail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The record will be deleted. Do you want to continue?</summary>
        public static string DeleteConfirmWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("DeleteConfirmWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please select a file</summary>
        public static string AttachmentSelectionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("AttachmentSelectionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>File type is not appropriate</summary>
        public static string AttachmentTypeWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("AttachmentTypeWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>File size can be at most 10 mb</summary>
        public static string MaxAttachmentSizeWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("MaxAttachmentSizeWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Logout</summary>
        public static string Logout
        {
            get
            {
                return (string)resourceProvider.GetResource("Logout", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>At least one record must be constituted in the list</summary>
        public static string ListItemExistenceWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("ListItemExistenceWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>There are empty or improper fields on the page. Please check it out! (Fields marked with * sign are mandatory)</summary>
        public static string ValidationPopupErrorMessage
        {
            get
            {
                return (string)resourceProvider.GetResource("ValidationPopupErrorMessage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please wait..</summary>
        public static string ProgressMessage
        {
            get
            {
                return (string)resourceProvider.GetResource("ProgressMessage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>No record has been found</summary>
        public static string NoRecordFoundMessage
        {
            get
            {
                return (string)resourceProvider.GetResource("NoRecordFoundMessage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Would you like to continue the operation?</summary>
        public static string ProcessConfirmWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("ProcessConfirmWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Different value</summary>
        public static string FilterNotEqualDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("FilterNotEqualDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Upper value</summary>
        public static string FilterUpperLimitDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("FilterUpperLimitDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Lower value (subvalue)</summary>
        public static string FilterLowerLimitDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("FilterLowerLimitDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Lower value (subvalue)</summary>
        public static string MinimumValue
        {
            get
            {
                return (string)resourceProvider.GetResource("MinimumValue", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Upper value</summary>
        public static string MaximumValue
        {
            get
            {
                return (string)resourceProvider.GetResource("MaximumValue", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Go to page</summary>
        public static string PagingGoToPage
        {
            get
            {
                return (string)resourceProvider.GetResource("PagingGoToPage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Select at least one record</summary>
        public static string RecordSelectWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("RecordSelectWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>At least one criteria must be supplied!</summary>
        public static string CriteriaSelectWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("CriteriaSelectWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Page (paginate) again</summary>
        public static string PagingSetPaging
        {
            get
            {
                return (string)resourceProvider.GetResource("PagingSetPaging", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Show all records</summary>
        public static string PagingShowAllRecord
        {
            get
            {
                return (string)resourceProvider.GetResource("PagingShowAllRecord", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your reservation request has been taken. We will contact you as soon as possible..</summary>
        public static string ReservationTaken
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationTaken", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Photo Gallery</summary>
        public static string PhotoGalleryLink
        {
            get
            {
                return (string)resourceProvider.GetResource("PhotoGalleryLink", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Rate</summary>
        public static string RoomRate
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomRate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Address</summary>
        public static string Address
        {
            get
            {
                return (string)resourceProvider.GetResource("Address", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Phone</summary>
        public static string Phone
        {
            get
            {
                return (string)resourceProvider.GetResource("Phone", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Fax</summary>
        public static string Fax
        {
            get
            {
                return (string)resourceProvider.GetResource("Fax", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>E-Mail</summary>
        public static string Email
        {
            get
            {
                return (string)resourceProvider.GetResource("Email", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation</summary>
        public static string Reservation
        {
            get
            {
                return (string)resourceProvider.GetResource("Reservation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Home</summary>
        public static string HomePage
        {
            get
            {
                return (string)resourceProvider.GetResource("HomePage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Promotions</summary>
        public static string Promotions
        {
            get
            {
                return (string)resourceProvider.GetResource("Promotions", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary> Copy Right Symbol: Global Booking System-  All rights reserved.</summary>
        public static string CopyRight
        {
            get
            {
                return (string)resourceProvider.GetResource("CopyRight", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Show</summary>
        public static string Show
        {
            get
            {
                return (string)resourceProvider.GetResource("Show", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Secure Online Reservation System</summary>
        public static string SecureTransaction
        {
            get
            {
                return (string)resourceProvider.GetResource("SecureTransaction", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation Info</summary>
        public static string ReservationInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Location</summary>
        public static string Location
        {
            get
            {
                return (string)resourceProvider.GetResource("Location", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hotel</summary>
        public static string Hotel
        {
            get
            {
                return (string)resourceProvider.GetResource("Hotel", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservations</summary>
        public static string Reservations
        {
            get
            {
                return (string)resourceProvider.GetResource("Reservations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Information</summary>
        public static string HotelInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Agency</summary>
        public static string Agency
        {
            get
            {
                return (string)resourceProvider.GetResource("Agency", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfers</summary>
        public static string Transfers
        {
            get
            {
                return (string)resourceProvider.GetResource("Transfers", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tours</summary>
        public static string Tours
        {
            get
            {
                return (string)resourceProvider.GetResource("Tours", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner</summary>
        public static string BusinessPartner
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartner", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deals</summary>
        public static string Deals
        {
            get
            {
                return (string)resourceProvider.GetResource("Deals", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm</summary>
        public static string Firm
        {
            get
            {
                return (string)resourceProvider.GetResource("Firm", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm Information</summary>
        public static string FirmInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Invoices</summary>
        public static string Invoices
        {
            get
            {
                return (string)resourceProvider.GetResource("Invoices", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>ReservationStatements</summary>
        public static string ReservationStatements
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationStatements", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>FinancialOverview</summary>
        public static string FinancialOverview
        {
            get
            {
                return (string)resourceProvider.GetResource("FinancialOverview", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>SEPADirectDebittRegistration </summary>
        public static string SEPADirectDebittRegistration 
        {
            get
            {
                return (string)resourceProvider.GetResource("SEPADirectDebittRegistration ", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Contact Us</summary>
        public static string ContactUs
        {
            get
            {
                return (string)resourceProvider.GetResource("ContactUs", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Management</summary>
        public static string Management
        {
            get
            {
                return (string)resourceProvider.GetResource("Management", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm Operations</summary>
        public static string FirmOperations
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmOperations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>User Operations</summary>
        public static string UserOperations
        {
            get
            {
                return (string)resourceProvider.GetResource("UserOperations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Messages</summary>
        public static string Messages
        {
            get
            {
                return (string)resourceProvider.GetResource("Messages", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Operations</summary>
        public static string HotelOperations
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelOperations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Name</summary>
        public static string HotelName
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Address</summary>
        public static string HotelAddress
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelAddress", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Phone</summary>
        public static string HotelPhone
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelPhone", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Fax</summary>
        public static string HotelFax
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelFax", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Postcode</summary>
        public static string HotelPostCode
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelPostCode", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Country</summary>
        public static string HotelCountry
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelCountry", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property City</summary>
        public static string HotelCity
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelCity", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Type</summary>
        public static string HotelType
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelType", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Class</summary>
        public static string HotelClass
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelClass", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hotel Chain</summary>
        public static string HotelChain
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelChain", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Type of Accommodation</summary>
        public static string AccommodationType
        {
            get
            {
                return (string)resourceProvider.GetResource("AccommodationType", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room Count</summary>
        public static string RoomCount
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Website</summary>
        public static string HotelWebAddress
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelWebAddress", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property E-Mail</summary>
        public static string HotelEmail
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelEmail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Description</summary>
        public static string HotelDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm and Executive Information</summary>
        public static string FirmAndUserInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmAndUserInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm Name</summary>
        public static string FirmName
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm Address</summary>
        public static string FirmAddress
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmAddress", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm Phone</summary>
        public static string FirmPhone
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmPhone", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm Fax</summary>
        public static string FirmFax
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmFax", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm Postcode</summary>
        public static string FirmPostCode
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmPostCode", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm Country</summary>
        public static string FirmCountry
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmCountry", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm City</summary>
        public static string FirmCity
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmCity", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm E-Mail</summary>
        public static string FirmEmail
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmEmail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm Tax Department</summary>
        public static string FirmTaxDepartment
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmTaxDepartment", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm Tax Number</summary>
        public static string FirmTaxNo
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmTaxNo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Executive Salutation</summary>
        public static string ContactPersonSalutation
        {
            get
            {
                return (string)resourceProvider.GetResource("ContactPersonSalutation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Executive Name</summary>
        public static string ContactPersonName
        {
            get
            {
                return (string)resourceProvider.GetResource("ContactPersonName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Executive Surname</summary>
        public static string ContactPersonSurname
        {
            get
            {
                return (string)resourceProvider.GetResource("ContactPersonSurname", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Executive Phone</summary>
        public static string ContactPersonPhone
        {
            get
            {
                return (string)resourceProvider.GetResource("ContactPersonPhone", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Executive E-Mail</summary>
        public static string ContactPersonEmail
        {
            get
            {
                return (string)resourceProvider.GetResource("ContactPersonEmail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Executive Title</summary>
        public static string ContactPersonPosition
        {
            get
            {
                return (string)resourceProvider.GetResource("ContactPersonPosition", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Send</summary>
        public static string Send
        {
            get
            {
                return (string)resourceProvider.GetResource("Send", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Authorization</summary>
        public static string Authorization
        {
            get
            {
                return (string)resourceProvider.GetResource("Authorization", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Name</summary>
        public static string Name
        {
            get
            {
                return (string)resourceProvider.GetResource("Name", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Surname</summary>
        public static string Surname
        {
            get
            {
                return (string)resourceProvider.GetResource("Surname", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>User Name</summary>
        public static string UserName
        {
            get
            {
                return (string)resourceProvider.GetResource("UserName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Country</summary>
        public static string Country
        {
            get
            {
                return (string)resourceProvider.GetResource("Country", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>City</summary>
        public static string City
        {
            get
            {
                return (string)resourceProvider.GetResource("City", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Authority</summary>
        public static string SecurityGroup
        {
            get
            {
                return (string)resourceProvider.GetResource("SecurityGroup", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Status</summary>
        public static string Status
        {
            get
            {
                return (string)resourceProvider.GetResource("Status", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Activity Status</summary>
        public static string ActiveStatus
        {
            get
            {
                return (string)resourceProvider.GetResource("ActiveStatus", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Locked</summary>
        public static string Locked
        {
            get
            {
                return (string)resourceProvider.GetResource("Locked", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Filter</summary>
        public static string Filter
        {
            get
            {
                return (string)resourceProvider.GetResource("Filter", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Remove Filter</summary>
        public static string FilterRemove
        {
            get
            {
                return (string)resourceProvider.GetResource("FilterRemove", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Active</summary>
        public static string Active
        {
            get
            {
                return (string)resourceProvider.GetResource("Active", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Passive</summary>
        public static string Passive
        {
            get
            {
                return (string)resourceProvider.GetResource("Passive", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>New Record</summary>
        public static string NewRecord
        {
            get
            {
                return (string)resourceProvider.GetResource("NewRecord", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>User Code</summary>
        public static string UserID
        {
            get
            {
                return (string)resourceProvider.GetResource("UserID", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Title</summary>
        public static string Salutation
        {
            get
            {
                return (string)resourceProvider.GetResource("Salutation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Postal Code</summary>
        public static string Postcode
        {
            get
            {
                return (string)resourceProvider.GetResource("Postcode", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>IP Address</summary>
        public static string IPAddress
        {
            get
            {
                return (string)resourceProvider.GetResource("IPAddress", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Password</summary>
        public static string Password
        {
            get
            {
                return (string)resourceProvider.GetResource("Password", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Save</summary>
        public static string Save
        {
            get
            {
                return (string)resourceProvider.GetResource("Save", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Close</summary>
        public static string Close
        {
            get
            {
                return (string)resourceProvider.GetResource("Close", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm Code</summary>
        public static string FirmID
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmID", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tax Office</summary>
        public static string TaxDepartment
        {
            get
            {
                return (string)resourceProvider.GetResource("TaxDepartment", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tax ID No.</summary>
        public static string TaxNo
        {
            get
            {
                return (string)resourceProvider.GetResource("TaxNo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Region</summary>
        public static string Region
        {
            get
            {
                return (string)resourceProvider.GetResource("Region", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Number of Floors</summary>
        public static string FloorCount
        {
            get
            {
                return (string)resourceProvider.GetResource("FloorCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Construction Date</summary>
        public static string BuiltYear
        {
            get
            {
                return (string)resourceProvider.GetResource("BuiltYear", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Last Restoration Date</summary>
        public static string RenovationYear
        {
            get
            {
                return (string)resourceProvider.GetResource("RenovationYear", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Check-in</summary>
        public static string Checkin
        {
            get
            {
                return (string)resourceProvider.GetResource("Checkin", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Check-out</summary>
        public static string Checkout
        {
            get
            {
                return (string)resourceProvider.GetResource("Checkout", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Code</summary>
        public static string HotelID
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelID", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Sort By</summary>
        public static string Sort
        {
            get
            {
                return (string)resourceProvider.GetResource("Sort", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner Information</summary>
        public static string BusinessPartnerInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner Name</summary>
        public static string BusinessPartnerName
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Activity Area</summary>
        public static string BusinessPartnerType
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerType", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner Country</summary>
        public static string BusinessPartnerCountry
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerCountry", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner City</summary>
        public static string BusinessPartnerCity
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerCity", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner Address</summary>
        public static string BusinessPartnerAddress
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerAddress", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner Phone</summary>
        public static string BusinessPartnerPhone
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerPhone", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner Fax</summary>
        public static string BusinessPartnerFax
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerFax", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner Website</summary>
        public static string BusinessPartnerWebAddress
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerWebAddress", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner E-Mail</summary>
        public static string BusinessPartnerEmail
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerEmail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Brief Information About Business Partner</summary>
        public static string BusinessPartnerDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner Postal Code</summary>
        public static string BusinessPartnerPostCode
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerPostCode", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner Operations</summary>
        public static string BusinessPartnerOperations
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerOperations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner Code</summary>
        public static string BusinessPartnerID
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerID", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Parameters</summary>
        public static string Parameters
        {
            get
            {
                return (string)resourceProvider.GetResource("Parameters", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Rate & Availability</summary>
        public static string RateAndAvailability
        {
            get
            {
                return (string)resourceProvider.GetResource("RateAndAvailability", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reports</summary>
        public static string Reports
        {
            get
            {
                return (string)resourceProvider.GetResource("Reports", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Statistics</summary>
        public static string Statistics
        {
            get
            {
                return (string)resourceProvider.GetResource("Statistics", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reviews</summary>
        public static string Reviews
        {
            get
            {
                return (string)resourceProvider.GetResource("Reviews", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>General Information</summary>
        public static string GeneralInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("GeneralInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Facilities</summary>
        public static string HotelFacilities
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelFacilities", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Cancellation Policy</summary>
        public static string CancelPolicy
        {
            get
            {
                return (string)resourceProvider.GetResource("CancelPolicy", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Rooms</summary>
        public static string HotelRooms
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRooms", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Photos</summary>
        public static string Photos
        {
            get
            {
                return (string)resourceProvider.GetResource("Photos", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Ranking</summary>
        public static string HotelRank
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRank", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Credit Cards Accepted by the Property</summary>
        public static string HotelCreditCard
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelCreditCard", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Paid</summary>
        public static string Charged
        {
            get
            {
                return (string)resourceProvider.GetResource("Charged", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Conditions</summary>
        public static string HotelConditions
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelConditions", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Price</summary>
        public static string Charge
        {
            get
            {
                return (string)resourceProvider.GetResource("Charge", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>If there are fields with paid entrances, pricing unit and currency should be submitted.</summary>
        public static string UnitAndCurrencyRequired
        {
            get
            {
                return (string)resourceProvider.GetResource("UnitAndCurrencyRequired", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation can be cancelled within the following days before the check-in date:</summary>
        public static string HotelRefundableDayCount
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRefundableDayCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The following amount will be charged in the event that the reservation is not used or not cancelled on time:</summary>
        public static string ReservationPenaltyRate
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationPenaltyRate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room Type</summary>
        public static string RoomType
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomType", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room Space (m2)</summary>
        public static string RoomSize
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomSize", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Maximum number of persons</summary>
        public static string RoomMaxPeopleCount
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomMaxPeopleCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Maximum number of children that can stay in the room</summary>
        public static string RoomMaxChildrenCount
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomMaxChildrenCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Number of baby cots that can be put in the room</summary>
        public static string RoomBabyCotCount
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomBabyCotCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Number of extra beds that can be put in the room</summary>
        public static string RoomExtraBedCount
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomExtraBedCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Smoking Status</summary>
        public static string Smoking
        {
            get
            {
                return (string)resourceProvider.GetResource("Smoking", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room View</summary>
        public static string RoomView
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomView", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room Bed Information</summary>
        public static string RoomBedInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomBedInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Number of Beds</summary>
        public static string BedCount
        {
            get
            {
                return (string)resourceProvider.GetResource("BedCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room Facilities</summary>
        public static string RoomFacilities
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomFacilities", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>This cannot be deleted since there are active reservations for the room</summary>
        public static string HotelRoomHasActiveReservationWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRoomHasActiveReservationWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Back</summary>
        public static string Back
        {
            get
            {
                return (string)resourceProvider.GetResource("Back", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>General Photos of the Property</summary>
        public static string HotelGeneralPhotos
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelGeneralPhotos", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Daily Statistics</summary>
        public static string DailyHitCounts
        {
            get
            {
                return (string)resourceProvider.GetResource("DailyHitCounts", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Monthly Statistics</summary>
        public static string MonthlyHitCounts
        {
            get
            {
                return (string)resourceProvider.GetResource("MonthlyHitCounts", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Annual Statistics</summary>
        public static string YearlyHitCounts
        {
            get
            {
                return (string)resourceProvider.GetResource("YearlyHitCounts", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Date Range</summary>
        public static string DateInterval
        {
            get
            {
                return (string)resourceProvider.GetResource("DateInterval", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Number of Views / Reservations</summary>
        public static string HitCountOverReservationcount
        {
            get
            {
                return (string)resourceProvider.GetResource("HitCountOverReservationcount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Year</summary>
        public static string Year
        {
            get
            {
                return (string)resourceProvider.GetResource("Year", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Month</summary>
        public static string Month
        {
            get
            {
                return (string)resourceProvider.GetResource("Month", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Day</summary>
        public static string Day
        {
            get
            {
                return (string)resourceProvider.GetResource("Day", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Commission (%)</summary>
        public static string ComissionInPercent
        {
            get
            {
                return (string)resourceProvider.GetResource("ComissionInPercent", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The commission you specified for your property and the ranking formed based on other factors is provided below. You can adjust your status in the list at your discretion by adjusting the commission proportion; then, you can update the commission of your property based on this proportion.</summary>
        public static string HotelRankingDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRankingDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Date</summary>
        public static string Date
        {
            get
            {
                return (string)resourceProvider.GetResource("Date", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Propert Commission</summary>
        public static string HotelComission
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelComission", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Start Date</summary>
        public static string StartDate
        {
            get
            {
                return (string)resourceProvider.GetResource("StartDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>End Date</summary>
        public static string EndDate
        {
            get
            {
                return (string)resourceProvider.GetResource("EndDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Commission Start Date</summary>
        public static string ComissionStartDate
        {
            get
            {
                return (string)resourceProvider.GetResource("ComissionStartDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Commission End Date</summary>
        public static string ComissionEndDate
        {
            get
            {
                return (string)resourceProvider.GetResource("ComissionEndDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The end date must be same or later than the start date</summary>
        public static string DateComparisionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("DateComparisionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>It should be equal or later than the day date</summary>
        public static string TodayComparisionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("TodayComparisionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Average</summary>
        public static string Average
        {
            get
            {
                return (string)resourceProvider.GetResource("Average", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Average Property Point</summary>
        public static string HotelAveragePoint
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelAveragePoint", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Review</summary>
        public static string Review
        {
            get
            {
                return (string)resourceProvider.GetResource("Review", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Availability Closing</summary>
        public static string CloseAvailability
        {
            get
            {
                return (string)resourceProvider.GetResource("CloseAvailability", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Daily Availability Opening/Closing</summary>
        public static string CloseOpenDailyAvailability
        {
            get
            {
                return (string)resourceProvider.GetResource("CloseOpenDailyAvailability", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Closed</summary>
        public static string Closed
        {
            get
            {
                return (string)resourceProvider.GetResource("Closed", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Available</summary>
        public static string Available
        {
            get
            {
                return (string)resourceProvider.GetResource("Available", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>No prices submitted</summary>
        public static string RateMissing
        {
            get
            {
                return (string)resourceProvider.GetResource("RateMissing", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>All reserved</summary>
        public static string Full
        {
            get
            {
                return (string)resourceProvider.GetResource("Full", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Availability</summary>
        public static string RoomAvailability
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomAvailability", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Daily Room Availability</summary>
        public static string DailyRoomAvailability
        {
            get
            {
                return (string)resourceProvider.GetResource("DailyRoomAvailability", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Long Term Room Availability</summary>
        public static string LongPeriodRoomAvailability
        {
            get
            {
                return (string)resourceProvider.GetResource("LongPeriodRoomAvailability", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Availability Opening /Closing</summary>
        public static string CloseOpenAvailability
        {
            get
            {
                return (string)resourceProvider.GetResource("CloseOpenAvailability", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Open</summary>
        public static string Open
        {
            get
            {
                return (string)resourceProvider.GetResource("Open", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Currency</summary>
        public static string Currency
        {
            get
            {
                return (string)resourceProvider.GetResource("Currency", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Single Price</summary>
        public static string SinglePrice
        {
            get
            {
                return (string)resourceProvider.GetResource("SinglePrice", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room Price</summary>
        public static string RoomPrice
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomPrice", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Price Policy</summary>
        public static string PricePolicy
        {
            get
            {
                return (string)resourceProvider.GetResource("PricePolicy", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The day range cannot be selected more than a month</summary>
        public static string DateIntervalWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("DateIntervalWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room Price for Single Day</summary>
        public static string DailyRoomRate
        {
            get
            {
                return (string)resourceProvider.GetResource("DailyRoomRate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Long Term Room Price</summary>
        public static string LongPeriodRoomRate
        {
            get
            {
                return (string)resourceProvider.GetResource("LongPeriodRoomRate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation ID</summary>
        public static string ReservationID
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationID", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation Date</summary>
        public static string ReservationDate
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Check-in Date</summary>
        public static string CheckInDate
        {
            get
            {
                return (string)resourceProvider.GetResource("CheckInDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Check-out Date</summary>
        public static string CheckOutDate
        {
            get
            {
                return (string)resourceProvider.GetResource("CheckOutDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Guest Name</summary>
        public static string GuestName
        {
            get
            {
                return (string)resourceProvider.GetResource("GuestName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation Status</summary>
        public static string ReservationStatus
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationStatus", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please limit the number of returning reservation by using the search criteria</summary>
        public static string MaxReservationListCountWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("MaxReservationListCountWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation Owner</summary>
        public static string Reserver
        {
            get
            {
                return (string)resourceProvider.GetResource("Reserver", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Number of Nights</summary>
        public static string NightCount
        {
            get
            {
                return (string)resourceProvider.GetResource("NightCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Amount</summary>
        public static string Amount
        {
            get
            {
                return (string)resourceProvider.GetResource("Amount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Commission</summary>
        public static string Comission
        {
            get
            {
                return (string)resourceProvider.GetResource("Comission", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Guest Note</summary>
        public static string GuestNote
        {
            get
            {
                return (string)resourceProvider.GetResource("GuestNote", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Number of Persons</summary>
        public static string PeopleCount
        {
            get
            {
                return (string)resourceProvider.GetResource("PeopleCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Price for Single Night</summary>
        public static string NightPrice
        {
            get
            {
                return (string)resourceProvider.GetResource("NightPrice", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Traveler Profile</summary>
        public static string TravellerType
        {
            get
            {
                return (string)resourceProvider.GetResource("TravellerType", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Estimated Arrival Time</summary>
        public static string EstimatedArrivalTime
        {
            get
            {
                return (string)resourceProvider.GetResource("EstimatedArrivalTime", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>New check-in date cannot be before the reservation check-in date</summary>
        public static string CheckInDateComparisionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("CheckInDateComparisionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>New check-out date cannot be after the reservation check-out date</summary>
        public static string CheckOutDateComparisionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("CheckOutDateComparisionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Credit Card Provider </summary>
        public static string CCType
        {
            get
            {
                return (string)resourceProvider.GetResource("CCType", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Name on the Credit Card</summary>
        public static string CCFullName
        {
            get
            {
                return (string)resourceProvider.GetResource("CCFullName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Credit Card Number</summary>
        public static string CCNo
        {
            get
            {
                return (string)resourceProvider.GetResource("CCNo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Expiration Date</summary>
        public static string CCExpiration
        {
            get
            {
                return (string)resourceProvider.GetResource("CCExpiration", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>CVC Code</summary>
        public static string CCCVC
        {
            get
            {
                return (string)resourceProvider.GetResource("CCCVC", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hotels</summary>
        public static string HotelList
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelList", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer Reservations</summary>
        public static string TransferReservations
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferReservations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Reservations</summary>
        public static string TourReservations
        {
            get
            {
                return (string)resourceProvider.GetResource("TourReservations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal Reservations</summary>
        public static string DealReservations
        {
            get
            {
                return (string)resourceProvider.GetResource("DealReservations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Name</summary>
        public static string FullName
        {
            get
            {
                return (string)resourceProvider.GetResource("FullName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer Currency (Amount & Cost)</summary>
        public static string TransferCurrency
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferCurrency", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Passenger Groups</summary>
        public static string Pax
        {
            get
            {
                return (string)resourceProvider.GetResource("Pax", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Minimum Number of Passengers</summary>
        public static string MinPeopleCount
        {
            get
            {
                return (string)resourceProvider.GetResource("MinPeopleCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Maximum Number of Passengers</summary>
        public static string MaxPeopleCount
        {
            get
            {
                return (string)resourceProvider.GetResource("MaxPeopleCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The submitted value must be equal or higher than the start value!</summary>
        public static string ValueComparisionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("ValueComparisionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer Date</summary>
        public static string TransferDate
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Departure Point</summary>
        public static string DeparturePoint
        {
            get
            {
                return (string)resourceProvider.GetResource("DeparturePoint", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Destination Point</summary>
        public static string DestinationPoint
        {
            get
            {
                return (string)resourceProvider.GetResource("DestinationPoint", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Flight Code</summary>
        public static string FlightNumber
        {
            get
            {
                return (string)resourceProvider.GetResource("FlightNumber", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Return Transfer Date</summary>
        public static string ReturnDate
        {
            get
            {
                return (string)resourceProvider.GetResource("ReturnDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Return Transfer Time</summary>
        public static string ReturnTime
        {
            get
            {
                return (string)resourceProvider.GetResource("ReturnTime", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer Time</summary>
        public static string TransferTime
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferTime", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer Address</summary>
        public static string TransferAddress
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferAddress", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Number of Passengers</summary>
        public static string PassangerCount
        {
            get
            {
                return (string)resourceProvider.GetResource("PassangerCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Return Flight Code</summary>
        public static string ReturnFlightNumber
        {
            get
            {
                return (string)resourceProvider.GetResource("ReturnFlightNumber", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal No</summary>
        public static string DealID
        {
            get
            {
                return (string)resourceProvider.GetResource("DealID", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal</summary>
        public static string DealName
        {
            get
            {
                return (string)resourceProvider.GetResource("DealName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Quota</summary>
        public static string Quota
        {
            get
            {
                return (string)resourceProvider.GetResource("Quota", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Cost</summary>
        public static string Cost
        {
            get
            {
                return (string)resourceProvider.GetResource("Cost", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Description</summary>
        public static string DealDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("DealDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Special Note</summary>
        public static string SpecialNote
        {
            get
            {
                return (string)resourceProvider.GetResource("SpecialNote", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Language</summary>
        public static string Language
        {
            get
            {
                return (string)resourceProvider.GetResource("Language", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Promotion Code</summary>
        public static string PromotionCode
        {
            get
            {
                return (string)resourceProvider.GetResource("PromotionCode", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal Start Date</summary>
        public static string DealStartDate
        {
            get
            {
                return (string)resourceProvider.GetResource("DealStartDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal End Date</summary>
        public static string DealEndDate
        {
            get
            {
                return (string)resourceProvider.GetResource("DealEndDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour No</summary>
        public static string TourID
        {
            get
            {
                return (string)resourceProvider.GetResource("TourID", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour</summary>
        public static string TourName
        {
            get
            {
                return (string)resourceProvider.GetResource("TourName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Period Start Date</summary>
        public static string TourPeriodStartDate
        {
            get
            {
                return (string)resourceProvider.GetResource("TourPeriodStartDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Period End Date</summary>
        public static string TourPeriodEndDate
        {
            get
            {
                return (string)resourceProvider.GetResource("TourPeriodEndDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Arrangement Frequency</summary>
        public static string TourFrequency
        {
            get
            {
                return (string)resourceProvider.GetResource("TourFrequency", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Duration</summary>
        public static string TourDuration
        {
            get
            {
                return (string)resourceProvider.GetResource("TourDuration", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Start Time</summary>
        public static string TourStartDateTime
        {
            get
            {
                return (string)resourceProvider.GetResource("TourStartDateTime", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Limitation for the Age of Uncharged Children</summary>
        public static string ChildAge
        {
            get
            {
                return (string)resourceProvider.GetResource("ChildAge", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Start City</summary>
        public static string TourStartCity
        {
            get
            {
                return (string)resourceProvider.GetResource("TourStartCity", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Start Region</summary>
        public static string TourStartRegion
        {
            get
            {
                return (string)resourceProvider.GetResource("TourStartRegion", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Includes</summary>
        public static string TourInclude
        {
            get
            {
                return (string)resourceProvider.GetResource("TourInclude", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Features</summary>
        public static string TourFacility
        {
            get
            {
                return (string)resourceProvider.GetResource("TourFacility", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Start City</summary>
        public static string StartCity
        {
            get
            {
                return (string)resourceProvider.GetResource("StartCity", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Start Region</summary>
        public static string StartRegion
        {
            get
            {
                return (string)resourceProvider.GetResource("StartRegion", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Possible Participation Date</summary>
        public static string TourPreferredDateTime
        {
            get
            {
                return (string)resourceProvider.GetResource("TourPreferredDateTime", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Number of Uncharged Children</summary>
        public static string FreeChildCount
        {
            get
            {
                return (string)resourceProvider.GetResource("FreeChildCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Turkey Address</summary>
        public static string TurkeyAddress
        {
            get
            {
                return (string)resourceProvider.GetResource("TurkeyAddress", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Description</summary>
        public static string TourDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("TourDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Delete</summary>
        public static string Delete
        {
            get
            {
                return (string)resourceProvider.GetResource("Delete", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Make Main Photo</summary>
        public static string SetAsMainPhoto
        {
            get
            {
                return (string)resourceProvider.GetResource("SetAsMainPhoto", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>New Upload</summary>
        public static string NewPhotoUploadText
        {
            get
            {
                return (string)resourceProvider.GetResource("NewPhotoUploadText", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Longitude</summary>
        public static string Longitude
        {
            get
            {
                return (string)resourceProvider.GetResource("Longitude", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Latitude</summary>
        public static string Latitude
        {
            get
            {
                return (string)resourceProvider.GetResource("Latitude", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Map Zoom Index</summary>
        public static string MapZoomIndex
        {
            get
            {
                return (string)resourceProvider.GetResource("MapZoomIndex", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Map</summary>
        public static string HotelMap
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelMap", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Lowest Room Price</summary>
        public static string HotelLowestPrice
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelLowestPrice", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>System Settings</summary>
        public static string SystemConfiguration
        {
            get
            {
                return (string)resourceProvider.GetResource("SystemConfiguration", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Close the property for chat</summary>
        public static string HotelShowOffline
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelShowOffline", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Find the Best Deals </summary>
        public static string HotelSearch
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelSearch", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>City, region, district or specific hotel</summary>
        public static string HotelDestination
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelDestination", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Dates are not set</summary>
        public static string DatesNotSet
        {
            get
            {
                return (string)resourceProvider.GetResource("DatesNotSet", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Search</summary>
        public static string Search
        {
            get
            {
                return (string)resourceProvider.GetResource("Search", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Adult Count</summary>
        public static string AdultCount
        {
            get
            {
                return (string)resourceProvider.GetResource("AdultCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Children Count</summary>
        public static string ChildCount
        {
            get
            {
                return (string)resourceProvider.GetResource("ChildCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room</summary>
        public static string Room
        {
            get
            {
                return (string)resourceProvider.GetResource("Room", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The check-out date must be later than the check-in date</summary>
        public static string HotelSearchDateComparisionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelSearchDateComparisionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservations more than 30 days are not accepted</summary>
        public static string HotelSearchDateBookDaysWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelSearchDateBookDaysWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Main Region</summary>
        public static string MainRegion
        {
            get
            {
                return (string)resourceProvider.GetResource("MainRegion", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation</summary>
        public static string Book
        {
            get
            {
                return (string)resourceProvider.GetResource("Book", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Details</summary>
        public static string HotelDetail
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelDetail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Show Map</summary>
        public static string ShowMap
        {
            get
            {
                return (string)resourceProvider.GetResource("ShowMap", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hide Map</summary>
        public static string HideMap
        {
            get
            {
                return (string)resourceProvider.GetResource("HideMap", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Entry was not found. Please try again..</summary>
        public static string NoDestinationFound
        {
            get
            {
                return (string)resourceProvider.GetResource("NoDestinationFound", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Region</summary>
        public static string HotelRegion
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRegion", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Price (Per night)</summary>
        public static string PriceRange
        {
            get
            {
                return (string)resourceProvider.GetResource("PriceRange", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Remove Filter</summary>
        public static string RemoveFilter
        {
            get
            {
                return (string)resourceProvider.GetResource("RemoveFilter", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>(Paid)</summary>
        public static string ChargedWithParenthesis
        {
            get
            {
                return (string)resourceProvider.GetResource("ChargedWithParenthesis", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Check Availability</summary>
        public static string CheckAvailability
        {
            get
            {
                return (string)resourceProvider.GetResource("CheckAvailability", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Book Now</summary>
        public static string BookNow
        {
            get
            {
                return (string)resourceProvider.GetResource("BookNow", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Submit your check-in and check-out dates to see the room prices</summary>
        public static string RoomPriceDisplayWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomPriceDisplayWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Conditions</summary>
        public static string Conditions
        {
            get
            {
                return (string)resourceProvider.GetResource("Conditions", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room Details</summary>
        public static string RoomDetail
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomDetail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Baby cots can be put into the room</summary>
        public static string BabyCotCountAvailable
        {
            get
            {
                return (string)resourceProvider.GetResource("BabyCotCountAvailable", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Extra beds can be put into the room</summary>
        public static string ExtraBedAvailable
        {
            get
            {
                return (string)resourceProvider.GetResource("ExtraBedAvailable", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Ask the bed types submitted above as multiple choice</summary>
        public static string BedPreferenceWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("BedPreferenceWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>or</summary>
        public static string Or
        {
            get
            {
                return (string)resourceProvider.GetResource("Or", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Bed Selection</summary>
        public static string BedPreference
        {
            get
            {
                return (string)resourceProvider.GetResource("BedPreference", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The reservation can be cancelled #Days# days before check-in date. In case of no use or no cancellation, "#Penalty#" will be deducted.</summary>
        public static string RefundableInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("RefundableInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The complete reservation price can be taken in all circumstances.</summary>
        public static string NonRefundableInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("NonRefundableInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Non Refundable</summary>
        public static string NonRefundable
        {
            get
            {
                return (string)resourceProvider.GetResource("NonRefundable", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>All taxes and fees are included in the room price.</summary>
        public static string TaxIncludedWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("TaxIncludedWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You can see the cancellation policy by mousing over the icon in the “Conditions” section of related room.</summary>
        public static string CancelPolicyWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("CancelPolicyWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>No available rooms were found for the submitted dates.</summary>
        public static string RoomNotAvailableWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomNotAvailableWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Select room for reservation</summary>
        public static string NoRoomSelectedWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("NoRoomSelectedWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Option</summary>
        public static string Option
        {
            get
            {
                return (string)resourceProvider.GetResource("Option", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>All taxes and fees are included in the total price. Price is calculated from the daily exchange rate, the payment will made over the currency of the property.</summary>
        public static string lblBookingPriceWarninglblBookingPriceWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("lblBookingPriceWarninglblBookingPriceWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Calculations are based on the daily exchange rate. Please note that you will pay the price over the currency of the property.</summary>
        public static string BookingTotalPriceWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("BookingTotalPriceWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Total Price</summary>
        public static string TotalPrice
        {
            get
            {
                return (string)resourceProvider.GetResource("TotalPrice", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>E-Mail Verification</summary>
        public static string EmailVerification
        {
            get
            {
                return (string)resourceProvider.GetResource("EmailVerification", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The submitted e-mail addresses do not match. Please check.</summary>
        public static string EmailVerificationWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("EmailVerificationWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Double Price</summary>
        public static string DoublePrice
        {
            get
            {
                return (string)resourceProvider.GetResource("DoublePrice", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>No credit cards are required for reservation</summary>
        public static string CreditCardNotRequiredForReservation
        {
            get
            {
                return (string)resourceProvider.GetResource("CreditCardNotRequiredForReservation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Invalid CVC code</summary>
        public static string InvalidCVC
        {
            get
            {
                return (string)resourceProvider.GetResource("InvalidCVC", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Invalid credit card number</summary>
        public static string InvalidCreditCardNo
        {
            get
            {
                return (string)resourceProvider.GetResource("InvalidCreditCardNo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>No booking fees! Your credit card is needed because it will guarantee your booking.</summary>
        public static string CreditCardWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("CreditCardWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>I agree with the above reservation conditions and <a href='TermsAndConditions.aspx' target='_blank'>Gbshotels.com terms and conditions</a>.</summary>
        public static string ReservationBookAgreement
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationBookAgreement", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You must agree with the conditions of reservation, property and Gbshotels.com</summary>
        public static string ReservationBookAgreementWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationBookAgreementWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Make me a member and notify me of the promotions by e-mail</summary>
        public static string SendPromotionalEmail
        {
            get
            {
                return (string)resourceProvider.GetResource("SendPromotionalEmail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The submitted passwords do not match. Please check.</summary>
        public static string PasswordVerificationWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("PasswordVerificationWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You must agree with  Gbshotels.com terms and conditions</summary>
        public static string UserInfoAgreementWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("UserInfoAgreementWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>I agree with <a href='TermsAndConditions.aspx' target='_blank'>Gbshotels.com terms and conditions</a>.</summary>
        public static string UserInfoAgreement
        {
            get
            {
                return (string)resourceProvider.GetResource("UserInfoAgreement", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Register</summary>
        public static string Register
        {
            get
            {
                return (string)resourceProvider.GetResource("Register", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Login</summary>
        public static string Login
        {
            get
            {
                return (string)resourceProvider.GetResource("Login", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Manage Reservations</summary>
        public static string DisplayReservations
        {
            get
            {
                return (string)resourceProvider.GetResource("DisplayReservations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your registration is complete. In order for your account to be active, please go to the link in the e-mail, we sent you.</summary>
        public static string UserRegistrationEmailVerificationWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("UserRegistrationEmailVerificationWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Password Verification</summary>
        public static string PasswordVerification
        {
            get
            {
                return (string)resourceProvider.GetResource("PasswordVerification", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please check your e-mail and password</summary>
        public static string CheckYourEmailAndPasswordWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("CheckYourEmailAndPasswordWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Invalid verification code</summary>
        public static string InvalidVerificationCode
        {
            get
            {
                return (string)resourceProvider.GetResource("InvalidVerificationCode", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Forgot my Password - Locked Account</summary>
        public static string ForgottenPasswordOrLockedAccount
        {
            get
            {
                return (string)resourceProvider.GetResource("ForgottenPasswordOrLockedAccount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Obsolete Code Link</summary>
        public static string ObsoleteCodeLink
        {
            get
            {
                return (string)resourceProvider.GetResource("ObsoleteCodeLink", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Related e-mail has been sent to the email address you supplied. Please check your e-mail and click the link in the e-mail.</summary>
        public static string RemindPasswordEmailWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("RemindPasswordEmailWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>No user account related to this e-mail address</summary>
        public static string NoRegisteredUserWithThisEmail
        {
            get
            {
                return (string)resourceProvider.GetResource("NoRegisteredUserWithThisEmail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Forgot my Password</summary>
        public static string ForgottenPassword
        {
            get
            {
                return (string)resourceProvider.GetResource("ForgottenPassword", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Account has been locked</summary>
        public static string AccountLocked
        {
            get
            {
                return (string)resourceProvider.GetResource("AccountLocked", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>For a faster reservation, please login to your account</summary>
        public static string UserLoginWhileBooking
        {
            get
            {
                return (string)resourceProvider.GetResource("UserLoginWhileBooking", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Unfortunately, the rooms you selected has been already reserved.</summary>
        public static string HotelRoomAlreadyBooked
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRoomAlreadyBooked", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hotel Reservations</summary>
        public static string HotelReservations
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelReservations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>User Account</summary>
        public static string UserAccount
        {
            get
            {
                return (string)resourceProvider.GetResource("UserAccount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>PIN Code</summary>
        public static string PinCode
        {
            get
            {
                return (string)resourceProvider.GetResource("PinCode", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your reservation is successful. The reservation details were sent to your e-mail address. Please note down your reservation number and PIN code:</summary>
        public static string BookingConfirmationWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("BookingConfirmationWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Print</summary>
        public static string Print
        {
            get
            {
                return (string)resourceProvider.GetResource("Print", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Contact Info</summary>
        public static string HotelContact
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelContact", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Cancel Reservation</summary>
        public static string CancelReservation
        {
            get
            {
                return (string)resourceProvider.GetResource("CancelReservation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Cancel Selected Room Reservations</summary>
        public static string CancelSelectedRoomReservations
        {
            get
            {
                return (string)resourceProvider.GetResource("CancelSelectedRoomReservations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Firm Requests</summary>
        public static string FirmRequests
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmRequests", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Request</summary>
        public static string Request
        {
            get
            {
                return (string)resourceProvider.GetResource("Request", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You can contact us via the following ways for your requests, suggestions and questions:</summary>
        public static string AdminContactInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("AdminContactInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Period</summary>
        public static string Period
        {
            get
            {
                return (string)resourceProvider.GetResource("Period", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Invoice No</summary>
        public static string InvoiceID
        {
            get
            {
                return (string)resourceProvider.GetResource("InvoiceID", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please deposit total sum to the following bank account before the expiry date. Do not forget to write the invoice number to the “Note” section of your payment orders.</summary>
        public static string InvoicePaymentWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("InvoicePaymentWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Invoice Date</summary>
        public static string InvoiceDate
        {
            get
            {
                return (string)resourceProvider.GetResource("InvoiceDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Expiry Date</summary>
        public static string DueDate
        {
            get
            {
                return (string)resourceProvider.GetResource("DueDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Invoice</summary>
        public static string Invoice
        {
            get
            {
                return (string)resourceProvider.GetResource("Invoice", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Total Amount</summary>
        public static string TotalAmount
        {
            get
            {
                return (string)resourceProvider.GetResource("TotalAmount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Section</summary>
        public static string Part
        {
            get
            {
                return (string)resourceProvider.GetResource("Part", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Anonymous</summary>
        public static string Anonymous
        {
            get
            {
                return (string)resourceProvider.GetResource("Anonymous", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Online</summary>
        public static string Online
        {
            get
            {
                return (string)resourceProvider.GetResource("Online", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Live Support</summary>
        public static string ChatNow
        {
            get
            {
                return (string)resourceProvider.GetResource("ChatNow", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Contanct to the property</summary>
        public static string ContactToHotel
        {
            get
            {
                return (string)resourceProvider.GetResource("ContactToHotel", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Configuration</summary>
        public static string Configuration
        {
            get
            {
                return (string)resourceProvider.GetResource("Configuration", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please check your username and password</summary>
        public static string CheckYourUserNameAndPasswordWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("CheckYourUserNameAndPasswordWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your session has been abondoned</summary>
        public static string LogoutWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("LogoutWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>People to Check-in the Property  in the Near Future</summary>
        public static string HotelRecentArrive
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRecentArrive", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Today</summary>
        public static string Today
        {
            get
            {
                return (string)resourceProvider.GetResource("Today", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tomorrow</summary>
        public static string Tomorrow
        {
            get
            {
                return (string)resourceProvider.GetResource("Tomorrow", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>People to Check-out the Property in the Near Future</summary>
        public static string HotelRecentLeave
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRecentLeave", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Recent Reservations for the Property</summary>
        public static string HotelRecentReservation
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRecentReservation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Recent Reservation Cancellations for the Property</summary>
        public static string HotelRecentReservationCancel
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRecentReservationCancel", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Yesterday</summary>
        public static string Yesterday
        {
            get
            {
                return (string)resourceProvider.GetResource("Yesterday", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Partial Reservation Cancel</summary>
        public static string PartialHotelReservationCancelWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("PartialHotelReservationCancelWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Invoices to be Paid</summary>
        public static string FirmInvoiceToBePaid
        {
            get
            {
                return (string)resourceProvider.GetResource("FirmInvoiceToBePaid", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Page</summary>
        public static string HotelPage
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelPage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room Reservation was Cancelled</summary>
        public static string HotelRoomReservationCancelled
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRoomReservationCancelled", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Update</summary>
        public static string Update
        {
            get
            {
                return (string)resourceProvider.GetResource("Update", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Feedback</summary>
        public static string Feedback
        {
            get
            {
                return (string)resourceProvider.GetResource("Feedback", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Notification Culture</summary>
        public static string NotificationCulture
        {
            get
            {
                return (string)resourceProvider.GetResource("NotificationCulture", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>User Login</summary>
        public static string UserLogin
        {
            get
            {
                return (string)resourceProvider.GetResource("UserLogin", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Display Reservation</summary>
        public static string DisplayReservation
        {
            get
            {
                return (string)resourceProvider.GetResource("DisplayReservation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Old Value</summary>
        public static string OldValue
        {
            get
            {
                return (string)resourceProvider.GetResource("OldValue", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Total Room Price</summary>
        public static string TotalRoomPrice
        {
            get
            {
                return (string)resourceProvider.GetResource("TotalRoomPrice", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please subcribe to know about promotions and the news</summary>
        public static string EmailList
        {
            get
            {
                return (string)resourceProvider.GetResource("EmailList", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Subscribe</summary>
        public static string Subscribe
        {
            get
            {
                return (string)resourceProvider.GetResource("Subscribe", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your e-mail address will be kept safe and you can unsubscribe whenever you would like</summary>
        public static string EmailListWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("EmailListWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You have been successfuly unsubscribed</summary>
        public static string EmailListUnsubscribtionSuccessful
        {
            get
            {
                return (string)resourceProvider.GetResource("EmailListUnsubscribtionSuccessful", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>E-mail address has not been found</summary>
        public static string EmailNotFound
        {
            get
            {
                return (string)resourceProvider.GetResource("EmailNotFound", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You can send your questions, suggestions or feedbacks through below form</summary>
        public static string UserMessageText
        {
            get
            {
                return (string)resourceProvider.GetResource("UserMessageText", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your message has been received successfully. After being reviewed, you will be replied as soon as possible.</summary>
        public static string MessageReceivedSuccessfully
        {
            get
            {
                return (string)resourceProvider.GetResource("MessageReceivedSuccessfully", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Subject</summary>
        public static string Subject
        {
            get
            {
                return (string)resourceProvider.GetResource("Subject", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Message</summary>
        public static string Message
        {
            get
            {
                return (string)resourceProvider.GetResource("Message", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>User Messages</summary>
        public static string UserMessages
        {
            get
            {
                return (string)resourceProvider.GetResource("UserMessages", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Description</summary>
        public static string Description
        {
            get
            {
                return (string)resourceProvider.GetResource("Description", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>About Gbshotels.com</summary>
        public static string AboutGbshotels_com
        {
            get
            {
                return (string)resourceProvider.GetResource("AboutGbshotels.com", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Frequently Asked Questions</summary>
        public static string FAQ
        {
            get
            {
                return (string)resourceProvider.GetResource("FAQ", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Terms and Conditions</summary>
        public static string TermsAndConditions
        {
            get
            {
                return (string)resourceProvider.GetResource("TermsAndConditions", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Gbshotels.com is a global platform which allows you to make travel bookings(especially accommodation bookings) for popular cities around the world in an easy and hassle-free way.Our mission is to contribute positively to our users’ travel plans in the destinations they are going. We are doing our best to fulfill this mission with our young and dynamic team and our quality service understanding.We include the most outstanding properties and most attractive deals within their categories for our content. Meanwhile, we always seek to make a difference and add values with our free, safe and easily followed-up reservation system and our prices for the budget.We wish you a good time for your trip,Gbshotels.com Staff</summary>
        public static string AboutGbshotelsText
        {
            get
            {
                return (string)resourceProvider.GetResource("AboutGbshotelsText", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation owners, properties and business partners making transactions in Gbshotels.com website, benefiting from all of our services including the services presented with e-mail or phone are deemed to have read, understood and accepted the following terms and conditions stated below. You are not entitled to use this website if you do not accept these provisions, conditions and notifications.<b>* Introduction</b>On our Gbshotels.com website, we provide services for making reservations for staying in the accommodation properties (hotels, hostels, apartments, etc.) within our content; and other travel reservations. An informational e-mail will be sent to you after your reservation and the related property and business partners will be informed of your reservation. The information within in our website is the information provided by the properties and business partners included in our system. Despite the fact that we overview the content provided to us, it is not possible for us to be held responsible from an incoherency, inadequacy or a mistake. Thus, all properties within our website are always responsible of the information and of the accuracy of prices, quota and all other information. It is not possible for us to be held responsible of an error that might derive from any maintenance, repair or interruption in our website. We do not charge extra for the reservation transactions done on our Gbshotels.com website.<b>* Price Policy</b>Gbshotels.com makes the necessary efforts to provide best prices as a global source to its users. All prices on our website are VAT included. Some changes in the prices might be observed in some occasions (non-refundable reservation, promotions). The reservation owner is informed of all those details. Thus, we advise the reservation owners to carefully examine what is included in the total sum. Contradicting and obvious errors (typing errors, etc.) are not binding. Currency converter should only be used for informative purposes since the prices may vary due to the fluctuations in the currencies. Gbshotels.com cannot be held responsible from the calculations and the problems that might arise in this regard.<b>* Credit Card</b>Your credit card information is generally received to guarantee your reservation and your card can be charged in accordance with the reservation terms and conditions (non-refundable reservation, failing to use the reservation, deposit, etc.). Thus, check the details, terms and conditions of the reservations carefully before making a reservation. Your credit card information is sent to the property or business partners after being taken via using SSL with encryption method. In the event of credit card fraud or the usage of your credit card by the third parties most banks and credit card companies take the risk and cover the losses arising out of fraud or abuse. Either way, please inform us during those occasions. Gbshotels.com will use its best efforts to provide you the necessary support. However, Gbshotels.com cannot be held responsible from occasions as stated. Your credit card is subjected to pre-provision.<b>* Cancellation</b>Based on the terms and conditions of your reservation and the related property or business partner, you might be entitled to cancel your reservation. You can clearly see this status in the e-mail dispatched to you and the summary page before making the reservation. Thus, please make your reservations after checking whether you are entitled to cancel your reservations and the deadline of cancelling it. Please note that the relevant property or business partner may charge from your credit card in the event of a failure to use the reservation or to cancel it. <b>* Properties and Business Partners</b>Accommodation properties (hotel, hostel, apartment, etc.) and our business partners included on our web-site offer their service and products via Gbshotels.com. They aim at providing optimum, high quality and proper service for our users by collaborating Gbshotels.com. However, these properties and business partners are not entitled to represent Gbshotels.com. Gbshotels.com shall not be accountable for undesired and legal circumstances that might arise out of these properties and business partners. Also, Gbshotels.com is authorized to stop and cancel; displaying service and products of the included properties and business partners. Gbshotels.com shall not be accountable for problems and legal circumstances that might arise out of incomplete and wrong information provided by reservation owners. Additionally, the properties and our business partners shall be responsible for the security of sensitive information (password, etc.) they used for entering the system. Gbshotels.com shall not be responsible for any damages of the properties and business partners that result from usage of this information out of its purpose. To solve conflicts that may arise between facilities, business partners and reservation owners, Gbshotels.com records will be taken as baseline and reference. However, Gbshotels.com cannot be held responsible for such conflicts. Gbshotels.com has the right to publish its content on its partner web-sites. <b>* Copyright</b>Gbshotels.com web-site is for personal and non-commercial use. You are not entitled to use, copy, reproduce, tracking with spider, scraping, etc. methods and download the content of this web-site and the information, software, products and services included, in line with any commercial and competitive activity.<b>* Miscellaneous</b>Our web-site cares for your privacy. It shall take necessary precautions for storage and protection of your information. But, also the users shall be responsible for the security of sensitive information (password, etc.) they used for entering the system. In any case of acquisition of this information by other parties or institutions because of user error, the user shall be liable. Gbshotels.com reserves the right to adjust, refuse or remove user reviews written referring to provided service within its content. Main presentation languages of Gbshotels.com is English. Any misunderstanding and incoherency resulted from translations into other languages shall be non-binding. Occasionally, changes in these terms and conditions might be possible.</summary>
        public static string TermsAndConditionsText
        {
            get
            {
                return (string)resourceProvider.GetResource("TermsAndConditionsText", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Popular Destinations</summary>
        public static string PopularDestinations
        {
            get
            {
                return (string)resourceProvider.GetResource("PopularDestinations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Why Gbshotels.com</summary>
        public static string WhyGbshotels
        {
            get
            {
                return (string)resourceProvider.GetResource("WhyGbshotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary><figure><div class="thumb"><img src="../Images/tick.png"  width="32"></div><div style="padding-top:10px">Free reservation and cancellation</div></figure><figure><div class="thumb"><img src="../Images/tick.png"  width="32"></div><div style="padding-top:2px">Exclusive properties and facilities in their own categories, quality service and budget-friendly prices</div></figure><figure><div class="thumb"><img src="../Images/tick.png"  width="32"></div><div style="padding-top:5px">Easy and user-friendly online reservation management</div></figure><figure><div class="thumb"><img src="../Images/tick.png"  width="32"></div><div style="padding-top:10px">Secure transactions with SSL encryption</div></figure><figure><div class="thumb"><img src="../Images/tick.png"  width="32"></div><div style="padding-top:10px">Last minute deals, charming promotions</div></figure></summary>
        public static string WhyGbshotelsText
        {
            get
            {
                return (string)resourceProvider.GetResource("WhyGbshotelsText", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Add Property</summary>
        public static string HotelApplication
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelApplication", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner Membership</summary>
        public static string BusinessPartnerApplication
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerApplication", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Partner Login</summary>
        public static string HotelAndBusinessPartnerLogin
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelAndBusinessPartnerLogin", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Click to see the hotels of this desination..</summary>
        public static string GoToDestinationHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("GoToDestinationHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Promotion</summary>
        public static string Promotion
        {
            get
            {
                return (string)resourceProvider.GetResource("Promotion", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Discount (%)</summary>
        public static string DiscountPercentage
        {
            get
            {
                return (string)resourceProvider.GetResource("DiscountPercentage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Minimum Accommodation Day Count</summary>
        public static string MinumumDayCount
        {
            get
            {
                return (string)resourceProvider.GetResource("MinumumDayCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Airport</summary>
        public static string Airport
        {
            get
            {
                return (string)resourceProvider.GetResource("Airport", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your accommodation date needs to be between these dates :</summary>
        public static string HotelPromotionPeriodInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelPromotionPeriodInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Gbshotels.com</summary>
        public static string GbshotelsDiscount
        {
            get
            {
                return (string)resourceProvider.GetResource("GbshotelsDiscount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Discount</summary>
        public static string Discount
        {
            get
            {
                return (string)resourceProvider.GetResource("Discount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You are taking advantage of this promotion</summary>
        public static string PromotionGrantedWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("PromotionGrantedWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You will see the total price that you will pay (with Gbshotels.com discounts and property discounts) and the promotions you’ll take on the next reservation page.</summary>
        public static string RoomRateDiscountWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomRateDiscountWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Payable Amount</summary>
        public static string PayableAmount
        {
            get
            {
                return (string)resourceProvider.GetResource("PayableAmount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Discount</summary>
        public static string HotelDiscount
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelDiscount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Web Routing Name</summary>
        public static string RoutingName
        {
            get
            {
                return (string)resourceProvider.GetResource("RoutingName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>View</summary>
        public static string HotelRoomView
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRoomView", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property</summary>
        public static string Property
        {
            get
            {
                return (string)resourceProvider.GetResource("Property", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Accommodation Start Date</summary>
        public static string AccommodationStartDate
        {
            get
            {
                return (string)resourceProvider.GetResource("AccommodationStartDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Accommodation End Date</summary>
        public static string AccommodationEndDate
        {
            get
            {
                return (string)resourceProvider.GetResource("AccommodationEndDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>General</summary>
        public static string General
        {
            get
            {
                return (string)resourceProvider.GetResource("General", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hotel Name, Address, Region, etc.</summary>
        public static string HotelKeywordText
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelKeywordText", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Detailed Search</summary>
        public static string DetailedSearch
        {
            get
            {
                return (string)resourceProvider.GetResource("DetailedSearch", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Closest Airport</summary>
        public static string ClosestAirport
        {
            get
            {
                return (string)resourceProvider.GetResource("ClosestAirport", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Closest Airport Distance (km)</summary>
        public static string ClosestAirportDistance
        {
            get
            {
                return (string)resourceProvider.GetResource("ClosestAirportDistance", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Approximate Distances from the Property</summary>
        public static string ApproximateDistancesFromTheHotel
        {
            get
            {
                return (string)resourceProvider.GetResource("ApproximateDistancesFromTheHotel", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Listed Hotels</summary>
        public static string ListedHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("ListedHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Other Hotels</summary>
        public static string OtherHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("OtherHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Landmark</summary>
        public static string Landmark
        {
            get
            {
                return (string)resourceProvider.GetResource("Landmark", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer Search</summary>
        public static string TransferSearch
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferSearch", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Destination/hotel name: </summary>
        public static string Destination
        {
            get
            {
                return (string)resourceProvider.GetResource("Destination", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deposit</summary>
        public static string Deposit
        {
            get
            {
                return (string)resourceProvider.GetResource("Deposit", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer Cancellation Policy</summary>
        public static string TransferCancelPolicy
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferCancelPolicy", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Cancellation Policy</summary>
        public static string TourCancelPolicy
        {
            get
            {
                return (string)resourceProvider.GetResource("TourCancelPolicy", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal Cancellation Policy</summary>
        public static string DealCancelPolicy
        {
            get
            {
                return (string)resourceProvider.GetResource("DealCancelPolicy", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation can be cancelled within the following days before the transfer date:</summary>
        public static string TransferRefundableDayCount
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferRefundableDayCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Currency (Amount & Cost)</summary>
        public static string TourCurrency
        {
            get
            {
                return (string)resourceProvider.GetResource("TourCurrency", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal Deposit Information</summary>
        public static string DealDeposit
        {
            get
            {
                return (string)resourceProvider.GetResource("DealDeposit", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Deposit Information</summary>
        public static string TourDeposit
        {
            get
            {
                return (string)resourceProvider.GetResource("TourDeposit", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer Deposit Information</summary>
        public static string TransferDeposit
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferDeposit", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal Currency (Amount & Cost)</summary>
        public static string DealCurrency
        {
            get
            {
                return (string)resourceProvider.GetResource("DealCurrency", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>I ask for return transfer as well</summary>
        public static string ReturnTransfer
        {
            get
            {
                return (string)resourceProvider.GetResource("ReturnTransfer", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Same type promotion exists for the submitted period</summary>
        public static string PromotionPeriodConflictWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("PromotionPeriodConflictWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>All Room Types</summary>
        public static string AllRoomTypes
        {
            get
            {
                return (string)resourceProvider.GetResource("AllRoomTypes", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Valid for all room types</summary>
        public static string ValidForAllRooms
        {
            get
            {
                return (string)resourceProvider.GetResource("ValidForAllRooms", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Valid for specific room types</summary>
        public static string ValidForSpecificRooms
        {
            get
            {
                return (string)resourceProvider.GetResource("ValidForSpecificRooms", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You can take advantage of the promotion according to the room type you select</summary>
        public static string HotelPromotionMayBeGrantedWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelPromotionMayBeGrantedWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room types you accommodate needs to be these types :</summary>
        public static string HotelPromotionRoomTypeInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelPromotionRoomTypeInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You take advantage of this promotion if this room type is selected</summary>
        public static string PromotionGrantedWhenTheRoomBookedWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("PromotionGrantedWhenTheRoomBookedWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Condition to get this promotion :</summary>
        public static string PromotionConditionInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("PromotionConditionInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You need to accommodate at least these amount of days :</summary>
        public static string HotelPromotionMinumumDayInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelPromotionMinumumDayInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You can see the conditions to get the promotion by getting over the information icon</summary>
        public static string PromotionConditionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("PromotionConditionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>#DiscountPercentage# % Discount</summary>
        public static string Discount_Percent
        {
            get
            {
                return (string)resourceProvider.GetResource("Discount%", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The transfer should be between the following dates:</summary>
        public static string TransferPromotionPeriodInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferPromotionPeriodInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer Price</summary>
        public static string TransferAmount
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferAmount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>My Basket</summary>
        public static string TBBasket
        {
            get
            {
                return (string)resourceProvider.GetResource("TBBasket", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your selection has been successfully added to the basket. You can search and add other transfers, tours and deals altogether in your basket and you can make reservation by clicking on “My Basket” link in the upper section of the page at any time.</summary>
        public static string ItemAddedToTBBasket
        {
            get
            {
                return (string)resourceProvider.GetResource("ItemAddedToTBBasket", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Add</summary>
        public static string Add
        {
            get
            {
                return (string)resourceProvider.GetResource("Add", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Special transfer experience with comfortable and large vehicles for budget-friendly prices</summary>
        public static string TransferMarketingWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferMarketingWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You can see the transfer conditions by mousing over the information icon</summary>
        public static string TransferConditionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferConditionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Remove</summary>
        public static string Remove
        {
            get
            {
                return (string)resourceProvider.GetResource("Remove", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Return date should be at the same date or later than the transfer date</summary>
        public static string TransferDateComparisionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferDateComparisionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Return date cannot be so later than the transfer date</summary>
        public static string TransferDateOutOfRangeWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferDateOutOfRangeWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Total Transfer Reservation Sum</summary>
        public static string TotalTransferAmount
        {
            get
            {
                return (string)resourceProvider.GetResource("TotalTransferAmount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>This transfer reservation can be cancelled #Days# days before transfer date. In case of no use or no cancellation, "#Penalty#" will be deducted. No refund for the deposit amount.</summary>
        public static string TransferRefundableInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferRefundableInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You will pay the remaining sum #Amount# in cash, to the driver carrying out the transfer.</summary>
        public static string TransferCostWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferCostWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Search</summary>
        public static string TourSearch
        {
            get
            {
                return (string)resourceProvider.GetResource("TourSearch", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Region</summary>
        public static string TourRegion
        {
            get
            {
                return (string)resourceProvider.GetResource("TourRegion", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Click to view the tour..</summary>
        public static string GoToTour
        {
            get
            {
                return (string)resourceProvider.GetResource("GoToTour", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Popular</summary>
        public static string Popular
        {
            get
            {
                return (string)resourceProvider.GetResource("Popular", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Code/Name/Description</summary>
        public static string TourKeyword
        {
            get
            {
                return (string)resourceProvider.GetResource("TourKeyword", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Detail</summary>
        public static string TourDetail
        {
            get
            {
                return (string)resourceProvider.GetResource("TourDetail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Code</summary>
        public static string Code
        {
            get
            {
                return (string)resourceProvider.GetResource("Code", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Start Point</summary>
        public static string StartPoint
        {
            get
            {
                return (string)resourceProvider.GetResource("StartPoint", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Regions</summary>
        public static string TourRegions
        {
            get
            {
                return (string)resourceProvider.GetResource("TourRegions", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Added to basket</summary>
        public static string AddedToBasket
        {
            get
            {
                return (string)resourceProvider.GetResource("AddedToBasket", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Children Policy</summary>
        public static string ChildAgePolicy
        {
            get
            {
                return (string)resourceProvider.GetResource("ChildAgePolicy", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Maximum age for the children that might join for free:</summary>
        public static string ChildAgePolicyWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("ChildAgePolicyWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>This tour reservation can be cancelled until the end of tour period - Unless exact date of participation to the tour has been specified -. In case of no use or no cancellation, "#Penalty#" will be deducted. No refund for the deposit amount.</summary>
        public static string TourRefundableInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("TourRefundableInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Conditions</summary>
        public static string TourCondition
        {
            get
            {
                return (string)resourceProvider.GetResource("TourCondition", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Miscellaneous and premium tours that will color up your stay in Turkey</summary>
        public static string TourMarketingWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("TourMarketingWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Possible Participation Date</summary>
        public static string PreferredTourDateTime
        {
            get
            {
                return (string)resourceProvider.GetResource("PreferredTourDateTime", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You can view the tour conditions by mousing over the information icon to the side</summary>
        public static string TourConditionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("TourConditionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Total Tour Reservation Sum</summary>
        public static string TotalTourAmount
        {
            get
            {
                return (string)resourceProvider.GetResource("TotalTourAmount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You will pay the remaining amount #Amount# in cash, to the tour guide. </summary>
        public static string TourCostWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("TourCostWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Private transfer with comfortable vehicles</summary>
        public static string MainPageTransferDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("MainPageTransferDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Keyword</summary>
        public static string Keyword
        {
            get
            {
                return (string)resourceProvider.GetResource("Keyword", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Click to view the deal..</summary>
        public static string GoToDeal
        {
            get
            {
                return (string)resourceProvider.GetResource("GoToDeal", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal Search</summary>
        public static string DealSearch
        {
            get
            {
                return (string)resourceProvider.GetResource("DealSearch", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal Region</summary>
        public static string DealRegion
        {
            get
            {
                return (string)resourceProvider.GetResource("DealRegion", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal Name/Description</summary>
        public static string DealKeyword
        {
            get
            {
                return (string)resourceProvider.GetResource("DealKeyword", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal Conditions</summary>
        public static string DealCondition
        {
            get
            {
                return (string)resourceProvider.GetResource("DealCondition", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>This deal reservation can be cancelled until the end of deal period. In case of no use or no cancellation, "#Penalty#" will be deducted. No refund for the deposit amount.</summary>
        public static string DealRefundableInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("DealRefundableInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You can view the deal conditions by mousing over the information icon to the side</summary>
        public static string DealConditionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("DealConditionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Total Deal Reservation Sum</summary>
        public static string TotalDealAmount
        {
            get
            {
                return (string)resourceProvider.GetResource("TotalDealAmount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You will pay the remaining amount #Amount#, to the facility where you will benefit from the deal. You will see the address and the contact information of the facility in the reservation details.</summary>
        public static string DealCostWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("DealCostWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer & Tour & Deal Reservations</summary>
        public static string TransferTourDealReservations
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferTourDealReservations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer</summary>
        public static string Transfer
        {
            get
            {
                return (string)resourceProvider.GetResource("Transfer", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour</summary>
        public static string Tour
        {
            get
            {
                return (string)resourceProvider.GetResource("Tour", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal</summary>
        public static string Deal
        {
            get
            {
                return (string)resourceProvider.GetResource("Deal", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Cancel Selected Reservations</summary>
        public static string CancelSelectedReservation
        {
            get
            {
                return (string)resourceProvider.GetResource("CancelSelectedReservation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer Code</summary>
        public static string TransferID
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferID", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation Operation</summary>
        public static string ReservationOperation
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationOperation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Charged Amount</summary>
        public static string ChargedAmount
        {
            get
            {
                return (string)resourceProvider.GetResource("ChargedAmount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer Reservation ID</summary>
        public static string TransferReservationID
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferReservationID", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Reservation ID</summary>
        public static string TourReservationID
        {
            get
            {
                return (string)resourceProvider.GetResource("TourReservationID", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal Reservation ID</summary>
        public static string DealReservationID
        {
            get
            {
                return (string)resourceProvider.GetResource("DealReservationID", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Used Deal Reservations</summary>
        public static string DealUsedReservations
        {
            get
            {
                return (string)resourceProvider.GetResource("DealUsedReservations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>No reservations with the promotion code were found</summary>
        public static string NoReservationForPromotionCode
        {
            get
            {
                return (string)resourceProvider.GetResource("NoReservationForPromotionCode", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Mark Reservation As Used</summary>
        public static string MarkAsUsed
        {
            get
            {
                return (string)resourceProvider.GetResource("MarkAsUsed", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>DestinationHotels</summary>
        public static string DestinationHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("DestinationHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Destination Tours</summary>
        public static string DestinationTours
        {
            get
            {
                return (string)resourceProvider.GetResource("DestinationTours", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Destination Deals</summary>
        public static string DestinationDeals
        {
            get
            {
                return (string)resourceProvider.GetResource("DestinationDeals", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Destination Info</summary>
        public static string DestinationInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("DestinationInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You can view the region hotels, tours and deals  or get information about the region by mousing over the icons within the map above or clicking the links below..</summary>
        public static string PopularDestinationDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("PopularDestinationDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You must agree with Gbshotels.com terms and conditions</summary>
        public static string GbshotelsAgreementWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("GbshotelsAgreementWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>I agree with <a href='TermsAndConditions.aspx' target='_blank'>Gbshotels.com terms and conditions</a>.</summary>
        public static string GbshotelsAgreement
        {
            get
            {
                return (string)resourceProvider.GetResource("GbshotelsAgreement", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your password must be minumum 8 characters and contain at least one digit and one letter</summary>
        public static string PasswordStrengthWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("PasswordStrengthWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Settings</summary>
        public static string Settings
        {
            get
            {
                return (string)resourceProvider.GetResource("Settings", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Change Password</summary>
        public static string ChangePassword
        {
            get
            {
                return (string)resourceProvider.GetResource("ChangePassword", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Current Password</summary>
        public static string CurrentPassword
        {
            get
            {
                return (string)resourceProvider.GetResource("CurrentPassword", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>New Password</summary>
        public static string NewPassword
        {
            get
            {
                return (string)resourceProvider.GetResource("NewPassword", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your current password doesn't match the supplied password</summary>
        public static string CurrentPasswordMatchWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("CurrentPasswordMatchWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>There exists hotel with this routing name</summary>
        public static string RoutingNameWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("RoutingNameWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Authorized Properties</summary>
        public static string AuthorizedHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("AuthorizedHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Authorized Business Partners</summary>
        public static string AuthorizedBusinessPartners
        {
            get
            {
                return (string)resourceProvider.GetResource("AuthorizedBusinessPartners", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your application is successfully received. We will return as soon as we review your application. Thank you for your interest.</summary>
        public static string ApplicationReceivedWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("ApplicationReceivedWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Maximum number of adults who can stay in the room</summary>
        public static string RoomMaxAdultCount
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomMaxAdultCount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>My name isn't displayed for my review</summary>
        public static string AnonymousReview
        {
            get
            {
                return (string)resourceProvider.GetResource("AnonymousReview", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Page</summary>
        public static string PropertyPage
        {
            get
            {
                return (string)resourceProvider.GetResource("PropertyPage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Reservations</summary>
        public static string PropertyReservations
        {
            get
            {
                return (string)resourceProvider.GetResource("PropertyReservations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Information</summary>
        public static string PropertyInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("PropertyInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Ranking</summary>
        public static string PropertyRank
        {
            get
            {
                return (string)resourceProvider.GetResource("PropertyRank", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>View Reservation</summary>
        public static string ViewReservation
        {
            get
            {
                return (string)resourceProvider.GetResource("ViewReservation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Credit Card Info</summary>
        public static string CreditCardInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("CreditCardInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Notify as Invalid Credit Card</summary>
        public static string ReportAsInvalidCreditCard
        {
            get
            {
                return (string)resourceProvider.GetResource("ReportAsInvalidCreditCard", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation Cancelled – Invalid Credit Card</summary>
        public static string CancelReservationInvalidCreditCardInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("CancelReservationInvalidCreditCardInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation Not Used</summary>
        public static string MarkAsNoUse
        {
            get
            {
                return (string)resourceProvider.GetResource("MarkAsNoUse", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Change Check-in / Check-out Dates</summary>
        public static string ChangeHotelReservationDates
        {
            get
            {
                return (string)resourceProvider.GetResource("ChangeHotelReservationDates", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation History</summary>
        public static string ReservationHistory
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationHistory", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Go to Business Partner</summary>
        public static string GoToBusinessPartner
        {
            get
            {
                return (string)resourceProvider.GetResource("GoToBusinessPartner", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Business Partner Details</summary>
        public static string BusinessPartnerDetails
        {
            get
            {
                return (string)resourceProvider.GetResource("BusinessPartnerDetails", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Go to Reservation</summary>
        public static string GoToReservation
        {
            get
            {
                return (string)resourceProvider.GetResource("GoToReservation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Update Reservation Operation</summary>
        public static string ReservationOperationUpdate
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationOperationUpdate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Upload Photo</summary>
        public static string UploadPhoto
        {
            get
            {
                return (string)resourceProvider.GetResource("UploadPhoto", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Approve</summary>
        public static string Approve
        {
            get
            {
                return (string)resourceProvider.GetResource("Approve", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reject</summary>
        public static string Reject
        {
            get
            {
                return (string)resourceProvider.GetResource("Reject", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Go To Hotel</summary>
        public static string GoToHotel
        {
            get
            {
                return (string)resourceProvider.GetResource("GoToHotel", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>View Invoice Details</summary>
        public static string ViewInvoiceDetails
        {
            get
            {
                return (string)resourceProvider.GetResource("ViewInvoiceDetails", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Invoice Paid</summary>
        public static string InvoicePaid
        {
            get
            {
                return (string)resourceProvider.GetResource("InvoicePaid", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Cancel</summary>
        public static string Cancel
        {
            get
            {
                return (string)resourceProvider.GetResource("Cancel", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Modify Reservation</summary>
        public static string ModifyReservation
        {
            get
            {
                return (string)resourceProvider.GetResource("ModifyReservation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Update Transfer Date</summary>
        public static string UpdateTransferDate
        {
            get
            {
                return (string)resourceProvider.GetResource("UpdateTransferDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Mark as Responded</summary>
        public static string MarkAsReplied
        {
            get
            {
                return (string)resourceProvider.GetResource("MarkAsReplied", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Mark as Deleted</summary>
        public static string MarkAsDeleted
        {
            get
            {
                return (string)resourceProvider.GetResource("MarkAsDeleted", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>New Credit Card Entry</summary>
        public static string NewCreditCard
        {
            get
            {
                return (string)resourceProvider.GetResource("NewCreditCard", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Write Review for Accommodation Experience</summary>
        public static string AccommodationReview
        {
            get
            {
                return (string)resourceProvider.GetResource("AccommodationReview", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Baby cot or extra beds might be necessary considering the capacity of the room you choose and the ages of the children. Please review the children, baby cot and extra bed policies of the hotel before you make your booking.</summary>
        public static string ChildrenReservationWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("ChildrenReservationWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Person</summary>
        public static string Person
        {
            get
            {
                return (string)resourceProvider.GetResource("Person", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Contact Info</summary>
        public static string ContactInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("ContactInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Quota is full</summary>
        public static string QuotaFullWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("QuotaFullWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Exclusive, charming deals only by Gbshotels.com</summary>
        public static string DealMarketingWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("DealMarketingWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary> Copy Right Symbol: Gbshotels.com - All rights reserved.</summary>
        public static string GbshotelsCopyRight
        {
            get
            {
                return (string)resourceProvider.GetResource("GbshotelsCopyRight", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Subscription</summary>
        public static string Membership
        {
            get
            {
                return (string)resourceProvider.GetResource("Membership", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Destination, landmark, hotel name</summary>
        public static string HotelSearchText
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelSearchText", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tables</summary>
        public static string Tables
        {
            get
            {
                return (string)resourceProvider.GetResource("Tables", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Due to the features of this deal, the total price will be taken as deposit and this amount will be non-refundable. You will see the address and the contact information of the facility in the reservation details.</summary>
        public static string DealCostWarningWithoutBPPayment
        {
            get
            {
                return (string)resourceProvider.GetResource("DealCostWarningWithoutBPPayment", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Due to the features of this tour, the total price will be taken as deposit and this amount will be non-refundable. Please contact us after setting your tour date in accordance with your staying date, tour period and tour arrangement frequency.</summary>
        public static string TourCostWarningWithoutBPPayment
        {
            get
            {
                return (string)resourceProvider.GetResource("TourCostWarningWithoutBPPayment", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Due to the features of this transfer, the total price will be taken as deposit and this amount will be non-refundable.</summary>
        public static string TransferCostWarningWithoutBPPayment
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferCostWarningWithoutBPPayment", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Quality, exclusive tours</summary>
        public static string MainPageTourDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("MainPageTourDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Unmissable, charming deals</summary>
        public static string MainPageDealDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("MainPageDealDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Book now, pay when you stay</summary>
        public static string MainPageHotelDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("MainPageHotelDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>I agree with the above reservation conditions, property conditions and <a href='TermsAndConditions.aspx' target='_blank'>Gbshotels.com terms and conditions</a>.</summary>
        public static string HotelReservationBookAgreement
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelReservationBookAgreement", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Code</summary>
        public static string TourCode
        {
            get
            {
                return (string)resourceProvider.GetResource("TourCode", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>After you reset your password and verify it, you can login to your account</summary>
        public static string PasswordReset
        {
            get
            {
                return (string)resourceProvider.GetResource("PasswordReset", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>To login to the extranet, please click this link and specify a password</summary>
        public static string NewAdminPassword
        {
            get
            {
                return (string)resourceProvider.GetResource("NewAdminPassword", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>There already exists a user account for this e-mail!</summary>
        public static string UserAccountExistWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("UserAccountExistWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>The property accepts minimum #DayCount# days reservations</summary>
        public static string MinumumAccommodationWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("MinumumAccommodationWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal Detail</summary>
        public static string DealDetail
        {
            get
            {
                return (string)resourceProvider.GetResource("DealDetail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Translation</summary>
        public static string Translation
        {
            get
            {
                return (string)resourceProvider.GetResource("Translation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>GBS Hotels</summary>
        public static string GBSHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("GBSHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Excel</summary>
        public static string Excel
        {
            get
            {
                return (string)resourceProvider.GetResource("Excel", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Transfer Contact</summary>
        public static string TransferContact
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferContact", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tour Contact</summary>
        public static string TourContact
        {
            get
            {
                return (string)resourceProvider.GetResource("TourContact", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Deal Contact</summary>
        public static string DealContact
        {
            get
            {
                return (string)resourceProvider.GetResource("DealContact", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Exact Participation Date</summary>
        public static string ExactTourDate
        {
            get
            {
                return (string)resourceProvider.GetResource("ExactTourDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Notify business partner</summary>
        public static string NotifyBusinessPartner
        {
            get
            {
                return (string)resourceProvider.GetResource("NotifyBusinessPartner", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hotels</summary>
        public static string Hotels
        {
            get
            {
                return (string)resourceProvider.GetResource("Hotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Total Property Reservation Sum</summary>
        public static string TotalHotelAmount
        {
            get
            {
                return (string)resourceProvider.GetResource("TotalHotelAmount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Sorry, the page couldn't be found.</summary>
        public static string PageNotFound
        {
            get
            {
                return (string)resourceProvider.GetResource("PageNotFound", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Last Minute Hotel Deals</summary>
        public static string LastMinuteHotelDeal
        {
            get
            {
                return (string)resourceProvider.GetResource("LastMinuteHotelDeal", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reservation Contact Info</summary>
        public static string ReservationContactInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationContactInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You will be notified about the address after you make a reservation for the deal.</summary>
        public static string DealAddressWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("DealAddressWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>E-Mails</summary>
        public static string Emails
        {
            get
            {
                return (string)resourceProvider.GetResource("Emails", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Countries</summary>
        public static string Countries
        {
            get
            {
                return (string)resourceProvider.GetResource("Countries", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your account has been activated successfully</summary>
        public static string AccountVerifiedWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("AccountVerifiedWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>and</summary>
        public static string And
        {
            get
            {
                return (string)resourceProvider.GetResource("And", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please fill in the fields below (Fields marked with * sign are mandatory)</summary>
        public static string FillFieldsWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("FillFieldsWarning", CultureInfo.CurrentUICulture.Name);
            }
        }
        /// <summary>Please fill in the fields above (Fields marked with * sign are mandatory)</summary>
        public static string FillFieldsWarningAbove
        {
            get
            {
                return (string)resourceProvider.GetResource("FillFieldsWarningAbove", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>We are Turkey travel experts. Thus, we are confident in our prices and quality service. No credit card information is needed for reservation; Book now, Pay while you benefit from the service..</summary>
        public static string MainPageHeaderText
        {
            get
            {
                return (string)resourceProvider.GetResource("MainPageHeaderText", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>All Regions</summary>
        public static string AllRegions
        {
            get
            {
                return (string)resourceProvider.GetResource("AllRegions", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Secret Hotel</summary>
        public static string SecretHotel
        {
            get
            {
                return (string)resourceProvider.GetResource("SecretHotel", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Secret Hotel</summary>
        public static string SecretProperty
        {
            get
            {
                return (string)resourceProvider.GetResource("SecretProperty", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>More photos</summary>
        public static string MorePhotos
        {
            get
            {
                return (string)resourceProvider.GetResource("MorePhotos", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Promotion Start Date</summary>
        public static string PromotionStartDate
        {
            get
            {
                return (string)resourceProvider.GetResource("PromotionStartDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Promotion End Date</summary>
        public static string PromotionEndDate
        {
            get
            {
                return (string)resourceProvider.GetResource("PromotionEndDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Click to view and modify your property information, features, conditions, rooms and photos</summary>
        public static string HotelMainPageUpdateWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelMainPageUpdateWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Promote with a Quick Deal </summary>
        public static string QuickRoomDeal
        {
            get
            {
                return (string)resourceProvider.GetResource("QuickRoomDeal", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Property Reservation Currency</summary>
        public static string HotelReservationCurrency
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelReservationCurrency", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Since we will publish secret deals often, we would like to hide our property name (You can change this option later in your administration panel)</summary>
        public static string HotelIsSecret
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelIsSecret", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room Information (Room information for one of the rooms you plan to sell with our system)</summary>
        public static string HotelRoomInfo
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRoomInfo", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Starting from those many hours in advance</summary>
        public static string LastMinuteMargin
        {
            get
            {
                return (string)resourceProvider.GetResource("LastMinuteMargin", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please update your password</summary>
        public static string UpdatePasswordWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("UpdatePasswordWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Place of Interest</summary>
        public static string POI
        {
            get
            {
                return (string)resourceProvider.GetResource("POI", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please provide bed information</summary>
        public static string BedOptionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("BedOptionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Until #Time#</summary>
        public static string UntilTime
        {
            get
            {
                return (string)resourceProvider.GetResource("UntilTime", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>From #Time#</summary>
        public static string FromTime
        {
            get
            {
                return (string)resourceProvider.GetResource("FromTime", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>This property only accepts cash payments.</summary>
        public static string OnlyCashPaymentAccepted
        {
            get
            {
                return (string)resourceProvider.GetResource("OnlyCashPaymentAccepted", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You have already seen the credit card information.</summary>
        public static string CreditCardAlreadyDisplayedWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("CreditCardAlreadyDisplayedWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Because of security concerns, this credit card information is displayed <b>only once</b>. Please, plan your operations accordingly.</summary>
        public static string CreditCardDisplayWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("CreditCardDisplayWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>#Region# Hotels</summary>
        public static string RegionHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("RegionHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Top Deal</summary>
        public static string ValueDeal
        {
            get
            {
                return (string)resourceProvider.GetResource("ValueDeal", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Click to update room availability daily or long term</summary>
        public static string HotelMainPageRoomAvailabilityUpdateWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelMainPageRoomAvailabilityUpdateWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Click to update room rates daily or long term</summary>
        public static string HotelMainPageRoomRateUpdateWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelMainPageRoomRateUpdateWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Days</summary>
        public static string Days
        {
            get
            {
                return (string)resourceProvider.GetResource("Days", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hour</summary>
        public static string Hour
        {
            get
            {
                return (string)resourceProvider.GetResource("Hour", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hours</summary>
        public static string Hours
        {
            get
            {
                return (string)resourceProvider.GetResource("Hours", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Latest booking : #Count# #Unit# ago</summary>
        public static string LatestBookingDate
        {
            get
            {
                return (string)resourceProvider.GetResource("LatestBookingDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>We are doing our best for you. Click to see the reviews about us..</summary>
        public static string ReviewCentre
        {
            get
            {
                return (string)resourceProvider.GetResource("ReviewCentre", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>All Tours</summary>
        public static string AllTours
        {
            get
            {
                return (string)resourceProvider.GetResource("AllTours", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Price for #NightCount# nights</summary>
        public static string RoomPriceOverOneNight
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomPriceOverOneNight", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Instant confirmation</summary>
        public static string InstantConfirmation
        {
            get
            {
                return (string)resourceProvider.GetResource("InstantConfirmation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Not Certain</summary>
        public static string NotCertain
        {
            get
            {
                return (string)resourceProvider.GetResource("NotCertain", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your Details</summary>
        public static string YourDetails
        {
            get
            {
                return (string)resourceProvider.GetResource("YourDetails", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary></summary>
        public static string PageDescriptionContent
        {
            get
            {
                return (string)resourceProvider.GetResource("PageDescriptionContent", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Click the adjacent button to book this transfer</summary>
        public static string ClickButtonToBook
        {
            get
            {
                return (string)resourceProvider.GetResource("ClickButtonToBook", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>"Hotel name, address" or "Specific Address" or "Airport"</summary>
        public static string TransferAddressDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("TransferAddressDescription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>We care about customer satisfaction. Click to read reviews about our service..</summary>
        public static string CustomerSatisfaction
        {
            get
            {
                return (string)resourceProvider.GetResource("CustomerSatisfaction", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Cross update transfer prices</summary>
        public static string UpdateReverseTransfer
        {
            get
            {
                return (string)resourceProvider.GetResource("UpdateReverseTransfer", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>SWIFT</summary>
        public static string SWIFT
        {
            get
            {
                return (string)resourceProvider.GetResource("SWIFT", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>IBAN</summary>
        public static string IBAN
        {
            get
            {
                return (string)resourceProvider.GetResource("IBAN", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Bank Account Number</summary>
        public static string BankAccountNumber
        {
            get
            {
                return (string)resourceProvider.GetResource("BankAccountNumber", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Bank Branch</summary>
        public static string BankBranchName
        {
            get
            {
                return (string)resourceProvider.GetResource("BankBranchName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Bank</summary>
        public static string BankName
        {
            get
            {
                return (string)resourceProvider.GetResource("BankName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Secret Deal</summary>
        public static string SecretDeal
        {
            get
            {
                return (string)resourceProvider.GetResource("SecretDeal", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Booking Date</summary>
        public static string BookingDate
        {
            get
            {
                return (string)resourceProvider.GetResource("BookingDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Genius</summary>
        public static string Genius
        {
            get
            {
                return (string)resourceProvider.GetResource("Genius", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Accommodation Date</summary>
        public static string AccommodationDate
        {
            get
            {
                return (string)resourceProvider.GetResource("AccommodationDate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>How far in advance can the deal be booked?</summary>
        public static string DealMarginPeriod
        {
            get
            {
                return (string)resourceProvider.GetResource("DealMarginPeriod", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Starting from those many days in advance</summary>
        public static string EarlyBookerMargin
        {
            get
            {
                return (string)resourceProvider.GetResource("EarlyBookerMargin", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>This is a secret deal</summary>
        public static string SecretDealText
        {
            get
            {
                return (string)resourceProvider.GetResource("SecretDealText", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Activate Deal</summary>
        public static string ActivateDeal
        {
            get
            {
                return (string)resourceProvider.GetResource("ActivateDeal", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>New Promotion</summary>
        public static string NewPromotion
        {
            get
            {
                return (string)resourceProvider.GetResource("NewPromotion", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please select at least one day!</summary>
        public static string DaySelectionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("DaySelectionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please select at least one price policy!</summary>
        public static string PricePolicySelectionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("PricePolicySelectionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please select at least one room!</summary>
        public static string RoomSelectionWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomSelectionWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>This property has exclusive promotions for Genius bookers</summary>
        public static string GeniusPromotionAvailableWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("GeniusPromotionAvailableWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>This property has exclusive promotions for the member users(users logged to the system)</summary>
        public static string SecretPromotionPromotionAvailableWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("SecretPromotionPromotionAvailableWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Genius Booker</summary>
        public static string GeniusBooker
        {
            get
            {
                return (string)resourceProvider.GetResource("GeniusBooker", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Preferred Hotel</summary>
        public static string PreferredHotel
        {
            get
            {
                return (string)resourceProvider.GetResource("PreferredHotel", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Prices are per room</summary>
        public static string RoomPriceWarning
        {
            get
            {
                return (string)resourceProvider.GetResource("RoomPriceWarning", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Included</summary>
        public static string Included
        {
            get
            {
                return (string)resourceProvider.GetResource("Included", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Excluded</summary>
        public static string Excluded
        {
            get
            {
                return (string)resourceProvider.GetResource("Excluded", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>VAT</summary>
        public static string VAT
        {
            get
            {
                return (string)resourceProvider.GetResource("VAT", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>City Tax(per night per person)</summary>
        public static string CityTaxPerPersonPerNight
        {
            get
            {
                return (string)resourceProvider.GetResource("CityTaxPerPersonPerNight", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>per night per person</summary>
        public static string PerPersonPerNight
        {
            get
            {
                return (string)resourceProvider.GetResource("PerPersonPerNight", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Channel Manager</summary>
        public static string ChannelManager
        {
            get
            {
                return (string)resourceProvider.GetResource("ChannelManager", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Availability & Rate Update</summary>
        public static string AvailabilityRateUpdate
        {
            get
            {
                return (string)resourceProvider.GetResource("AvailabilityRateUpdate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>CTA</summary>
        public static string CloseToArrival
        {
            get
            {
                return (string)resourceProvider.GetResource("CloseToArrival", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>CTD</summary>
        public static string CloseToDeparture
        {
            get
            {
                return (string)resourceProvider.GetResource("CloseToDeparture", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Unit</summary>
        public static string Unit
        {
            get
            {
                return (string)resourceProvider.GetResource("Unit", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>City Tax</summary>
        public static string CityTax
        {
            get
            {
                return (string)resourceProvider.GetResource("CityTax", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Minimum Stay</summary>
        public static string MinimumStay
        {
            get
            {
                return (string)resourceProvider.GetResource("MinimumStay", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Finance</summary>
        public static string Finance
        {
            get
            {
                return (string)resourceProvider.GetResource("Finance", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Recently Seen</summary>
        public static string RecentlySeen
        {
            get
            {
                return (string)resourceProvider.GetResource("Recently Seen", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>My lists</summary>
        public static string Mylists
        {
            get
            {
                return (string)resourceProvider.GetResource("My lists", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Customer Service</summary>
        public static string CustomerService
        {
            get
            {
                return (string)resourceProvider.GetResource("Customer Service", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Become an affiliate</summary>
        public static string Becomeanaffiliate
        {
            get
            {
                return (string)resourceProvider.GetResource("Become an affiliate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>For Business</summary>
        public static string ForBusiness
        {
            get
            {
                return (string)resourceProvider.GetResource("For Business", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Districts</summary>
        public static string Districts
        {
            get
            {
                return (string)resourceProvider.GetResource("Districts", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Apartments</summary>
        public static string Apartments
        {
            get
            {
                return (string)resourceProvider.GetResource("Apartments", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Resorts</summary>
        public static string Resorts
        {
            get
            {
                return (string)resourceProvider.GetResource("Resorts", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Villas</summary>
        public static string Villas
        {
            get
            {
                return (string)resourceProvider.GetResource("Villas", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hostels</summary>
        public static string Hostels
        {
            get
            {
                return (string)resourceProvider.GetResource("Hostels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>B&Bs</summary>
        public static string BnBs
        {
            get
            {
                return (string)resourceProvider.GetResource("B&Bs", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Guesthouses</summary>
        public static string Guesthouses
        {
            get
            {
                return (string)resourceProvider.GetResource("Guesthouses", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>All Property Types</summary>
        public static string AllPropertyTypes
        {
            get
            {
                return (string)resourceProvider.GetResource("All Property Types", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>All Themes</summary>
        public static string AllThemes
        {
            get
            {
                return (string)resourceProvider.GetResource("All Themes", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>About</summary>
        public static string About
        {
            get
            {
                return (string)resourceProvider.GetResource("About", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>All Destinations</summary>
        public static string AllDestinations
        {
            get
            {
                return (string)resourceProvider.GetResource("All Destinations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Destination finder</summary>
        public static string Destinationfinder
        {
            get
            {
                return (string)resourceProvider.GetResource("Destination finder", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Careers</summary>
        public static string Careers
        {
            get
            {
                return (string)resourceProvider.GetResource("Careers", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Privacy and Cookies</summary>
        public static string PrivacyandCookies
        {
            get
            {
                return (string)resourceProvider.GetResource("Privacy and Cookies", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Extranet Login</summary>
        public static string ExtranetLogin
        {
            get
            {
                return (string)resourceProvider.GetResource("Extranet Login", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>TOP DESTINATIONS</summary>
        public static string TOPDESTINATIONS
        {
            get
            {
                return (string)resourceProvider.GetResource("TOPDESTINATIONS", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>WHERE YOU WANT TO STAY TONIGHT?</summary>
        public static string WHEREYOUWANTTOSTAY
        {
            get
            {
                return (string)resourceProvider.GetResource("WHEREYOUWANTTOSTAY", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Popular Cities</summary>
        public static string PopularCities
        {
            get
            {
                return (string)resourceProvider.GetResource("PopularCities", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Popular Hotels</summary>
        public static string PopularHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("PopularHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>More Hotels</summary>
        public static string MoreHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("MoreHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Featured Hotels</summary>
        public static string FeaturedHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("FeaturedHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Recently Added Hotels</summary>
        public static string RecentlyAddedHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("RecentlyAddedHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Latest News</summary>
        public static string LatestNews
        {
            get
            {
                return (string)resourceProvider.GetResource("LatestNews", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>OUR PARTNERS</summary>
        public static string OURPARTNERS
        {
            get
            {
                return (string)resourceProvider.GetResource("OURPARTNERS", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>FOLLOW US</summary>
        public static string FOLLOWUS
        {
            get
            {
                return (string)resourceProvider.GetResource("FOLLOWUS", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>I don't have specific dates yet</summary>
        public static string havespecificdatesyet
        {
            get
            {
                return (string)resourceProvider.GetResource("havespecificdatesyet", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Travelling for</summary>
        public static string Travellingfor
        {
            get
            {
                return (string)resourceProvider.GetResource("Travellingfor", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Work</summary>
        public static string Work
        {
            get
            {
                return (string)resourceProvider.GetResource("Work", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Leisure</summary>
        public static string Leisure
        {
            get
            {
                return (string)resourceProvider.GetResource("Leisure", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Not only Hotels, It's more than just Hotels!</summary>
        public static string NotOnlyHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("NotOnlyHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Motel</summary>
        public static string Motel
        {
            get
            {
                return (string)resourceProvider.GetResource("Motel", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Residence</summary>
        public static string Residence
        {
            get
            {
                return (string)resourceProvider.GetResource("Residence", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Convention Center</summary>
        public static string ConventionCenter
        {
            get
            {
                return (string)resourceProvider.GetResource("Convention Center", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Email Address</summary>
        public static string EmailAddress
        {
            get
            {
                return (string)resourceProvider.GetResource("Email Address", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Subscription</summary>
        public static string Subscription
        {
            get
            {
                return (string)resourceProvider.GetResource("Subscription", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Join us and share this web page to all of your friends and family members</summary>
        public static string JoinUs
        {
            get
            {
                return (string)resourceProvider.GetResource("JoinUs", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Sign up to receive our promotions and special offers that best suite your business needs.</summary>
        public static string Signuptoreceive
        {
            get
            {
                return (string)resourceProvider.GetResource("Signuptoreceive", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>SUBSCRIBE NOW & SAVE UPTO 50% WITH OUR HOTTEST OFFERS</summary>
        public static string SubscribeNow
        {
            get
            {
                return (string)resourceProvider.GetResource("SubscribeNow", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Rates & Reviews</summary>
        public static string RatesandReviews
        {
            get
            {
                return (string)resourceProvider.GetResource("RatesandReviews", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Add Your Hotel</summary>
        public static string AddYourHotels
        {
            get
            {
                return (string)resourceProvider.GetResource("AddYourHotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>PROFILE</summary>
        public static string Profile
        {
            get
            {
                return (string)resourceProvider.GetResource("Profile", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>My wish lists</summary>
        public static string Mywishlists
        {
            get
            {
                return (string)resourceProvider.GetResource("Mywishlists", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>USEFUL LINKS</summary>
        public static string UsefulLinks
        {
            get
            {
                return (string)resourceProvider.GetResource("UsefulLinks", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Get the free app</summary>
        public static string Getthefreeapp
        {
            get
            {
                return (string)resourceProvider.GetResource("Getthefreeapp", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Press</summary>
        public static string Press
        {
            get
            {
                return (string)resourceProvider.GetResource("Press", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Book with confidence</summary>
        public static string GbshotelsBook
        {
            get
            {
                return (string)resourceProvider.GetResource("GbshotelsBook", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Very Good</summary>
        public static string VeryGood
        {
            get
            {
                return (string)resourceProvider.GetResource("VeryGood", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Score From</summary>
        public static string ScoreFrom
        {
            get
            {
                return (string)resourceProvider.GetResource("ScoreFrom", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Superb</summary>
        public static string Superb
        {
            get
            {
                return (string)resourceProvider.GetResource("Superb", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Guests</summary>
        public static string Guests
        {
            get
            {
                return (string)resourceProvider.GetResource("Guests", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Join us and share this web page to all of your friends and family members</summary>
        public static string SocialMessage
        {
            get
            {
                return (string)resourceProvider.GetResource("SocialMessage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>New</summary>
        public static string New
        {
            get
            {
                return (string)resourceProvider.GetResource("New", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Near By</summary>
        public static string Nearby
        {
            get
            {
                return (string)resourceProvider.GetResource("Near by", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Street View</summary>
        public static string StreetView
        {
            get
            {
                return (string)resourceProvider.GetResource("Street View", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Find somewhere to stay in</summary>
        public static string Findsomewheretostayin
        {
            get
            {
                return (string)resourceProvider.GetResource("Find somewhere to stay in", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Properties</summary>
        public static string Properties
        {
            get
            {
                return (string)resourceProvider.GetResource("Properties", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Destinations In</summary>
        public static string DestinationsIn
        {
            get
            {
                return (string)resourceProvider.GetResource("Destinations In", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Recommended for you in</summary>
        public static string Recommended
        {
            get
            {
                return (string)resourceProvider.GetResource("Recommended", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>has a</summary>
        public static string hasa
        {
            get
            {
                return (string)resourceProvider.GetResource("has a", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>average review score of</summary>
        public static string averagereview
        {
            get
            {
                return (string)resourceProvider.GetResource("averagereview", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Welcome to </summary>
        public static string Welcometo
        {
            get
            {
                return (string)resourceProvider.GetResource("Welcometo ", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>London </summary>
        public static string London
        {
            get
            {
                return (string)resourceProvider.GetResource("London ", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Enter your dates and choose out of 2315 properties!</summary>
        public static string Enteryourdates
        {
            get
            {
                return (string)resourceProvider.GetResource("Enteryourdates", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>FREE cancellation on most rooms! Instant confirmation when you reserve</summary>
        public static string FREEcancellation
        {
            get
            {
                return (string)resourceProvider.GetResource("FREEcancellation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Enter your details and select</summary>
        public static string Enteryourdates1
        {
            get
            {
                return (string)resourceProvider.GetResource("Enteryourdates1", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>No Hotels To display</summary>
        public static string NoHotelsTodisplay
        {
            get
            {
                return (string)resourceProvider.GetResource("NoHotelsTodisplay", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Destinations</summary>
        public static string Destinations
        {
            get
            {
                return (string)resourceProvider.GetResource("Destinations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>More Destinations</summary>
        public static string MoreDestinations
        {
            get
            {
                return (string)resourceProvider.GetResource("More Destinations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>No</summary>
        public static string No
        {
            get
            {
                return (string)resourceProvider.GetResource("No", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>average review score</summary>
        public static string Averagereviewscore
        {
            get
            {
                return (string)resourceProvider.GetResource("Averagereviewscore", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Average price per night</summary>
        public static string Averagepricepernight
        {
            get
            {
                return (string)resourceProvider.GetResource("Averagepricepernight", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Km From</summary>
        public static string KmFrom
        {
            get
            {
                return (string)resourceProvider.GetResource("KmFrom", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Quantity</summary>
        public static string Quantity
        {
            get
            {
                return (string)resourceProvider.GetResource("Quantity", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Information</summary>
        public static string Information
        {
            get
            {
                return (string)resourceProvider.GetResource("Information", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>See All Reviews</summary>
        public static string SeeAllReviews
        {
            get
            {
                return (string)resourceProvider.GetResource("SeeAllReviews", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Write a review</summary>
        public static string Writeareview
        {
            get
            {
                return (string)resourceProvider.GetResource("Writeareview", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Facilities of</summary>
        public static string Facilitiesof
        {
            get
            {
                return (string)resourceProvider.GetResource("Facilitiesof", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Policies of</summary>
        public static string Policiesof
        {
            get
            {
                return (string)resourceProvider.GetResource("Policiesof", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Specification	</summary>
        public static string Specification
        {
            get
            {
                return (string)resourceProvider.GetResource("Specification", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Discover	</summary>
        public static string Discover
        {
            get
            {
                return (string)resourceProvider.GetResource("Discover", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>My Viewed Hotel	</summary>
        public static string MyViewedHotel
        {
            get
            {
                return (string)resourceProvider.GetResource("MyViewedHotel", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary></summary>
        public static string Hotelvirt
        {
            get
            {
                return (string)resourceProvider.GetResource("Hotelvirt", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Email Friend</summary>
        public static string EmailFriend
        {
            get
            {
                return (string)resourceProvider.GetResource("EmailFriend", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Copy this hotel link</summary>
        public static string Copythishotellink
        {
            get
            {
                return (string)resourceProvider.GetResource("Copythishotellink", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>sendemail</summary>
        public static string sendemail
        {
            get
            {
                return (string)resourceProvider.GetResource("sendemail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Contact</summary>
        public static string Contact
        {
            get
            {
                return (string)resourceProvider.GetResource("Contact", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>2 Queen anne street</summary>
        public static string Queenannestreet
        {
            get
            {
                return (string)resourceProvider.GetResource("2 Queen anne street", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Stephen J</summary>
        public static string StephenJ
        {
            get
            {
                return (string)resourceProvider.GetResource("Stephen J", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Jessen</summary>
        public static string Jessen
        {
            get
            {
                return (string)resourceProvider.GetResource("Jessen", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>W1G 9LQ</summary>
        public static string W1G9LQ
        {
            get
            {
                return (string)resourceProvider.GetResource("W1G 9LQ", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>REGISTERED USER SIGN UP</summary>
        public static string REGISTEREDUSERSIGNUP
        {
            get
            {
                return (string)resourceProvider.GetResource("REGISTEREDUSERSIGNUP", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Member Information</summary>
        public static string MemberInformation
        {
            get
            {
                return (string)resourceProvider.GetResource("MemberInformation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Login Information</summary>
        public static string LoginInformation
        {
            get
            {
                return (string)resourceProvider.GetResource("LoginInformation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Contact Information</summary>
        public static string ContactInformation
        {
            get
            {
                return (string)resourceProvider.GetResource("ContactInformation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Captcha Security</summary>
        public static string CaptchaSecurity
        {
            get
            {
                return (string)resourceProvider.GetResource("CaptchaSecurity", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>First Name</summary>
        public static string FirstName
        {
            get
            {
                return (string)resourceProvider.GetResource("FirstName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Last Name</summary>
        public static string LastName
        {
            get
            {
                return (string)resourceProvider.GetResource("LastName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reset</summary>
        public static string Reset
        {
            get
            {
                return (string)resourceProvider.GetResource("Reset", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Submit</summary>
        public static string Submit
        {
            get
            {
                return (string)resourceProvider.GetResource("Submit", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Remember me</summary>
        public static string Rememberme
        {
            get
            {
                return (string)resourceProvider.GetResource("Rememberme", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>New Account</summary>
        public static string NewAccount
        {
            get
            {
                return (string)resourceProvider.GetResource("NewAccount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Create Account</summary>
        public static string CreateAccount
        {
            get
            {
                return (string)resourceProvider.GetResource("CreateAccount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hide</summary>
        public static string Hide
        {
            get
            {
                return (string)resourceProvider.GetResource("Hide", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Select</summary>
        public static string Select
        {
            get
            {
                return (string)resourceProvider.GetResource("Select", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Manager</summary>
        public static string Manager
        {
            get
            {
                return (string)resourceProvider.GetResource("Manager", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Sales</summary>
        public static string Accounts
        {
            get
            {
                return (string)resourceProvider.GetResource("Accounts", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Tick here if same contact details for Manager, Sales and Accounts</summary>
        public static string TickHere
        {
            get
            {
                return (string)resourceProvider.GetResource("TickHere", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Next</summary>
        public static string Next
        {
            get
            {
                return (string)resourceProvider.GetResource("Next", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Activities</summary>
        public static string Activities
        {
            get
            {
                return (string)resourceProvider.GetResource("Activities", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Services</summary>
        public static string Services
        {
            get
            {
                return (string)resourceProvider.GetResource("Services", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Enter Field</summary>
        public static string EnterField
        {
            get
            {
                return (string)resourceProvider.GetResource("EnterField", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Total</summary>
        public static string Total
        {
            get
            {
                return (string)resourceProvider.GetResource("Total", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>from</summary>
        public static string from
        {
            get
            {
                return (string)resourceProvider.GetResource("from", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Home</summary>
        public static string Home
        {
            get
            {
                return (string)resourceProvider.GetResource("Home", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Inserted SuccessFully</summary>
        public static string InsertedSuccessFully
        {
            get
            {
                return (string)resourceProvider.GetResource("InsertedSuccessFully", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>UserName UnAvailable</summary>
        public static string UserNameUnAvailable
        {
            get
            {
                return (string)resourceProvider.GetResource("UserNameUnAvailable", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Invalid EmailId</summary>
        public static string InvalidEmailId
        {
            get
            {
                return (string)resourceProvider.GetResource("InvalidEmailId", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>UserName Available</summary>
        public static string UserNameAvailable
        {
            get
            {
                return (string)resourceProvider.GetResource("UserNameAvailable", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Use filters Below to Refine your search</summary>
        public static string FilterSearch
        {
            get
            {
                return (string)resourceProvider.GetResource("FilterSearch", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Number of extra baby cots that can be put in the room</summary>
        public static string ExtraBabyCots
        {
            get
            {
                return (string)resourceProvider.GetResource("ExtraBabyCots", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Option 1</summary>
        public static string Option1
        {
            get
            {
                return (string)resourceProvider.GetResource("Option1", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Option 2</summary>
        public static string Option2
        {
            get
            {
                return (string)resourceProvider.GetResource("Option2", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Option 3</summary>
        public static string Option3
        {
            get
            {
                return (string)resourceProvider.GetResource("Option3", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Single</summary>
        public static string Single
        {
            get
            {
                return (string)resourceProvider.GetResource("Single", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Double</summary>
        public static string Double
        {
            get
            {
                return (string)resourceProvider.GetResource("Double", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Queen Size</summary>
        public static string QueenSize
        {
            get
            {
                return (string)resourceProvider.GetResource("QueenSize", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>King Size</summary>
        public static string KingSize
        {
            get
            {
                return (string)resourceProvider.GetResource("KingSize", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Bunk</summary>
        public static string Bunk
        {
            get
            {
                return (string)resourceProvider.GetResource("Bunk", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Sofa</summary>
        public static string Sofa
        {
            get
            {
                return (string)resourceProvider.GetResource("Sofa", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Refundable</summary>
        public static string Refundable
        {
            get
            {
                return (string)resourceProvider.GetResource("Refundable", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Baby Cot</summary>
        public static string BabyCot
        {
            get
            {
                return (string)resourceProvider.GetResource("BabyCot", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Extra Bed</summary>
        public static string ExtraBed
        {
            get
            {
                return (string)resourceProvider.GetResource("ExtraBed", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Children Policy</summary>
        public static string ChildrenPolicy
        {
            get
            {
                return (string)resourceProvider.GetResource("ChildrenPolicy", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Internet</summary>
        public static string Internet
        {
            get
            {
                return (string)resourceProvider.GetResource("Internet", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Parking</summary>
        public static string Parking
        {
            get
            {
                return (string)resourceProvider.GetResource("Parking", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Domestic Animals</summary>
        public static string DomesticAnimals
        {
            get
            {
                return (string)resourceProvider.GetResource("DomesticAnimals", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Agreement</summary>
        public static string Agreement
        {
            get
            {
                return (string)resourceProvider.GetResource("Agreement", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Emad Profile</summary>
        public static string EmadProfile
        {
            get
            {
                return (string)resourceProvider.GetResource("EmadProfile", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Notations</summary>
        public static string Notations
        {
            get
            {
                return (string)resourceProvider.GetResource("Notations", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your Profile</summary>
        public static string YourProfile
        {
            get
            {
                return (string)resourceProvider.GetResource("YourProfile", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Service</summary>
        public static string Service
        {
            get
            {
                return (string)resourceProvider.GetResource("Service", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Bookings</summary>
        public static string Bookings
        {
            get
            {
                return (string)resourceProvider.GetResource("Bookings", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Newsletters</summary>
        public static string Newsletters
        {
            get
            {
                return (string)resourceProvider.GetResource("Newsletters", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Notes</summary>
        public static string Notes
        {
            get
            {
                return (string)resourceProvider.GetResource("Notes", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Agenda</summary>
        public static string Agenda
        {
            get
            {
                return (string)resourceProvider.GetResource("Agenda", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>GBS Gold</summary>
        public static string GBSGold
        {
            get
            {
                return (string)resourceProvider.GetResource("GBSGold", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Prepaid GBS Card</summary>
        public static string PrepaidGBSCard
        {
            get
            {
                return (string)resourceProvider.GetResource("PrepaidGBSCard", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Change your settings</summary>
        public static string Changeyoursettings
        {
            get
            {
                return (string)resourceProvider.GetResource("Changeyoursettings", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hi!</summary>
        public static string Hi
        {
            get
            {
                return (string)resourceProvider.GetResource("Hi!", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>See all of your bookings by verifying email address </summary>
        public static string Seeallofyourbookings
        {
            get
            {
                return (string)resourceProvider.GetResource("Seeallofyourbookings", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Resend the verification email</summary>
        public static string Resendtheverificationemail
        {
            get
            {
                return (string)resourceProvider.GetResource("Resendtheverificationemail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Trips</summary>
        public static string Trips
        {
            get
            {
                return (string)resourceProvider.GetResource("Trips", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Places</summary>
        public static string Places
        {
            get
            {
                return (string)resourceProvider.GetResource("Places", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Days away</summary>
        public static string Daysaway
        {
            get
            {
                return (string)resourceProvider.GetResource("Daysaway", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Half-off-hotels</summary>
        public static string Half_off_hotels
        {
            get
            {
                return (string)resourceProvider.GetResource("Half-off-hotels", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Subscribe to Secret Deals</summary>
        public static string SubscribetoSecretDeals
        {
            get
            {
                return (string)resourceProvider.GetResource("SubscribetoSecretDeals", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your Email</summary>
        public static string YourEmail
        {
            get
            {
                return (string)resourceProvider.GetResource("YourEmail", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Recently Viewed</summary>
        public static string RecentlyViewed
        {
            get
            {
                return (string)resourceProvider.GetResource("RecentlyViewed", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Delete all</summary>
        public static string Deleteall
        {
            get
            {
                return (string)resourceProvider.GetResource("Deleteall", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Share this results with friends</summary>
        public static string Sharethisresultswithfriends
        {
            get
            {
                return (string)resourceProvider.GetResource("Sharethisresultswithfriends", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Check another list</summary>
        public static string Checkanotherlist
        {
            get
            {
                return (string)resourceProvider.GetResource("Checkanotherlist", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Create</summary>
        public static string Create
        {
            get
            {
                return (string)resourceProvider.GetResource("Create", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Incredible places</summary>
        public static string Incredibleplaces
        {
            get
            {
                return (string)resourceProvider.GetResource("Incredibleplaces", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>My next trip</summary>
        public static string Mynexttrip
        {
            get
            {
                return (string)resourceProvider.GetResource("Mynexttrip", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your past bookings</summary>
        public static string Yourpastbookings
        {
            get
            {
                return (string)resourceProvider.GetResource("Yourpastbookings", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>See all</summary>
        public static string Seeall
        {
            get
            {
                return (string)resourceProvider.GetResource("Seeall", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary> Traveler Type</summary>
        public static string TravelerType1
        {
            get
            {
                return (string)resourceProvider.GetResource("TravelerType1", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Score</summary>
        public static string Score
        {
            get
            {
                return (string)resourceProvider.GetResource("Score", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Traveler Date</summary>
        public static string TravelerDate
        {
            get
            {
                return (string)resourceProvider.GetResource("Traveler Date", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Positive</summary>
        public static string Positive
        {
            get
            {
                return (string)resourceProvider.GetResource("Positive", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Negative</summary>
        public static string Negative
        {
            get
            {
                return (string)resourceProvider.GetResource("Negative", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Personal information</summary>
        public static string Personalinformation
        {
            get
            {
                return (string)resourceProvider.GetResource("Personal information", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Ex.Trendy,Nice beds,Good amenities,View from the room</summary>
        public static string Explus
        {
            get
            {
                return (string)resourceProvider.GetResource("Explus", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Ex.A little overpriced,Breakfast,Small room</summary>
        public static string Exdrowdack
        {
            get
            {
                return (string)resourceProvider.GetResource("Exdrowdack", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your email will be kept privacy.it will not be shown any party</summary>
        public static string Youremailwillbekeptprivacy_itwillnotbeshownanyparty
        {
            get
            {
                return (string)resourceProvider.GetResource("Your email will be kept privacy.it will not be shown any party", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Ex:London,UK</summary>
        public static string exlondon
        {
            get
            {
                return (string)resourceProvider.GetResource("exlondon", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your name</summary>
        public static string Yourname
        {
            get
            {
                return (string)resourceProvider.GetResource("Your name", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Send Review</summary>
        public static string SendReview
        {
            get
            {
                return (string)resourceProvider.GetResource("Send Review", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Free</summary>
        public static string Free
        {
            get
            {
                return (string)resourceProvider.GetResource("Free", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Surcharge</summary>
        public static string Surcharge
        {
            get
            {
                return (string)resourceProvider.GetResource("Surcharge", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Dashboard</summary>
        public static string Dashboard
        {
            get
            {
                return (string)resourceProvider.GetResource("Dashboard", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Ready to</summary>
        public static string Readyto
        {
            get
            {
                return (string)resourceProvider.GetResource("Readyto", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Welcome to GbsHotels.com!</summary>
        public static string WelcometoGbsHotels_com
        {
            get
            {
                return (string)resourceProvider.GetResource("WelcometoGbsHotels.com!", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Travel guides</summary>
        public static string Travelguides
        {
            get
            {
                return (string)resourceProvider.GetResource("Travelguides", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Make booking your next trip easier</summary>
        public static string Makebooking
        {
            get
            {
                return (string)resourceProvider.GetResource("Makebooking", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Save the details of your travel group</summary>
        public static string Savethedetails
        {
            get
            {
                return (string)resourceProvider.GetResource("Savethedetails", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Save a credit card</summary>
        public static string Saveacreditcard
        {
            get
            {
                return (string)resourceProvider.GetResource("Saveacreditcard", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your recent searches</summary>
        public static string Yourrecentsearches
        {
            get
            {
                return (string)resourceProvider.GetResource("Yourrecentsearches", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Resume search</summary>
        public static string Resumesearch
        {
            get
            {
                return (string)resourceProvider.GetResource("Resumesearch", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Edit your settings</summary>
        public static string Edityoursettings
        {
            get
            {
                return (string)resourceProvider.GetResource("Edityoursettings", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>By having an account you are agreeing with our</summary>
        public static string Byhavinganaccount
        {
            get
            {
                return (string)resourceProvider.GetResource("Byhavinganaccount", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Privacy Statement</summary>
        public static string PrivacyStatement
        {
            get
            {
                return (string)resourceProvider.GetResource("PrivacyStatement", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Linked to reviews</summary>
        public static string Linkedtoreviews
        {
            get
            {
                return (string)resourceProvider.GetResource("Linked to reviews", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You are viewing the private version of your reviews timeline</summary>
        public static string Youreviewtimeline
        {
            get
            {
                return (string)resourceProvider.GetResource("You review timeline", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary> See the public version</summary>
        public static string seepublic
        {
            get
            {
                return (string)resourceProvider.GetResource("see public", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You joined </summary>
        public static string Youjoined
        {
            get
            {
                return (string)resourceProvider.GetResource("You joined ", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Off </summary>
        public static string Off
        {
            get
            {
                return (string)resourceProvider.GetResource("Off ", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Welcome to your review timeline. </summary>
        public static string Welcomereviewtimeline
        {
            get
            {
                return (string)resourceProvider.GetResource("Welcome review timeline", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>After you stay at a property you'll get the chance to write a review and share what you liked best about the city. Your contributions will appear here. </summary>
        public static string aftergetreview
        {
            get
            {
                return (string)resourceProvider.GetResource("aftergetreview", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Read tips for writing a great review </summary>
        public static string Readtips
        {
            get
            {
                return (string)resourceProvider.GetResource("Read tips", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Try the Destination Finder, powered by travellers like you </summary>
        public static string trydestinationfind
        {
            get
            {
                return (string)resourceProvider.GetResource("trydestinationfind", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Find out how we verify reviews </summary>
        public static string Findoutreviews
        {
            get
            {
                return (string)resourceProvider.GetResource("Find outreviews", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>pending reviews </summary>
        public static string pendingreviews
        {
            get
            {
                return (string)resourceProvider.GetResource("pendingreviews", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Q&A </summary>
        public static string QA
        {
            get
            {
                return (string)resourceProvider.GetResource("QA", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Everything</summary>
        public static string Everything
        {
            get
            {
                return (string)resourceProvider.GetResource("Everything", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Endoresments</summary>
        public static string Endoresments
        {
            get
            {
                return (string)resourceProvider.GetResource("Endoresments", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Switch "On" to link this timeline to all your reviews.</summary>
        public static string Switchontolink
        {
            get
            {
                return (string)resourceProvider.GetResource("Switchontolink", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>We’ll show the display name you chose to protect your real identity.</summary>
        public static string weshowname
        {
            get
            {
                return (string)resourceProvider.GetResource("weshowname", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your real name, personal details, and upcoming bookings will always remain private.</summary>
        public static string yourrealname
        {
            get
            {
                return (string)resourceProvider.GetResource("yourrealname", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Find out more</summary>
        public static string Findoutmore
        {
            get
            {
                return (string)resourceProvider.GetResource("Findoutmore", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your Display Name</summary>
        public static string YourDisplayName
        {
            get
            {
                return (string)resourceProvider.GetResource("YourDisplayName", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>To protect your privacy, please create a display name that will appear next to your reviews and other contributions.</summary>
        public static string toprotectyourprivacy
        {
            get
            {
                return (string)resourceProvider.GetResource("toprotectyourprivacy", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Ok! Make my page public</summary>
        public static string Makemypagepublic
        {
            get
            {
                return (string)resourceProvider.GetResource("Makemypagepublic", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Edit</summary>
        public static string Edit
        {
            get
            {
                return (string)resourceProvider.GetResource("Edit", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>I agree with <a href='Home/TermsAndConditions'target='_blank'>Gbshotels.com terms and conditions</a>.</summary>
        public static string UserInfoAgreementNew1
        {
            get
            {
                return (string)resourceProvider.GetResource("UserInfoAgreementNew1", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Select Hotel</summary>
        public static string SelectHotel
        {
            get
            {
                return (string)resourceProvider.GetResource("SelectHotel", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Booking Details</summary>
        public static string BookingDetails
        {
            get
            {
                return (string)resourceProvider.GetResource("BookingDetails", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Booking Success</summary>
        public static string BookingSuccess
        {
            get
            {
                return (string)resourceProvider.GetResource("BookingSuccess", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please Confirm Your Booking Details</summary>
        public static string ConfirmBooking
        {
            get
            {
                return (string)resourceProvider.GetResource("ConfirmBooking", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your reservation is successful. The reservation details were sent to your e-mail address.Please note down your Reservation ID and PIN code:</summary>
        public static string ReservationSucesssMessage
        {
            get
            {
                return (string)resourceProvider.GetResource("ReservationSucesssMessage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>I agree with</summary>
        public static string Iagreewith
        {
            get
            {
                return (string)resourceProvider.GetResource("Iagreewith", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>reviews from real guests</summary>
        public static string reviewsfrom
        {
            get
            {
                return (string)resourceProvider.GetResource("reviewsfrom", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>hotels in</summary>
        public static string hotelsin
        {
            get
            {
                return (string)resourceProvider.GetResource("hotelsin", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>countries world wide</summary>
        public static string countriesworldwide
        {
            get
            {
                return (string)resourceProvider.GetResource("countriesworldwide", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Available Rooms</summary>
        public static string AvailableRooms
        {
            get
            {
                return (string)resourceProvider.GetResource("AvailableRooms", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Facilities</summary>
        public static string Facilities
        {
            get
            {
                return (string)resourceProvider.GetResource("Facilities", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Added to your Wishlist</summary>
        public static string AddtoWishlist
        {
            get
            {
                return (string)resourceProvider.GetResource("AddtoWishlist", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Removed from your Wishlist</summary>
        public static string RemoveWishlist
        {
            get
            {
                return (string)resourceProvider.GetResource("RemoveWishlist", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>No reviews from real guests</summary>
        public static string Noreviewsfromrealguests
        {
            get
            {
                return (string)resourceProvider.GetResource("Noreviewsfromrealguests", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>please Check facilities</summary>
        public static string pleaseCheckfacilities
        {
            get
            {
                return (string)resourceProvider.GetResource("pleaseCheckfacilities", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please Check Corresponding Radio button</summary>
        public static string PleaseCheckCorrespondingRadiobutton
        {
            get
            {
                return (string)resourceProvider.GetResource("PleaseCheckCorrespondingRadiobutton", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Add Another Room/Save</summary>
        public static string AddAnotherRoomSave
        {
            get
            {
                return (string)resourceProvider.GetResource("AddAnotherRoom/Save", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Book now to get this fantastic Price.</summary>
        public static string Fantasticprice
        {
            get
            {
                return (string)resourceProvider.GetResource("Fantasticprice", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>If you book later, there's a chance the price will go up or the hotel will be sold out.</summary>
        public static string BookLaterWarningMeaasge
        {
            get
            {
                return (string)resourceProvider.GetResource("BookLaterWarningMeaasge", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Any Budget</summary>
        public static string AnyBudget
        {
            get
            {
                return (string)resourceProvider.GetResource("AnyBudget", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Room / night</summary>
        public static string Roompernight
        {
            get
            {
                return (string)resourceProvider.GetResource("Roompernight", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>more than</summary>
        public static string MoreThan
        {
            get
            {
                return (string)resourceProvider.GetResource("MoreThan", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Hotels found in</summary>
        public static string HotelsFoundIn
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelsFoundIn", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Change password</summary>
        public static string Changepassword
        {
            get
            {
                return (string)resourceProvider.GetResource("Change password", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Repeat password</summary>
        public static string Repeatpassword
        {
            get
            {
                return (string)resourceProvider.GetResource("Repeat password", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Save Change</summary>
        public static string SaveChange
        {
            get
            {
                return (string)resourceProvider.GetResource("Save Change", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Add a card</summary>
        public static string Addacard
        {
            get
            {
                return (string)resourceProvider.GetResource("Add a card", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Use for business</summary>
        public static string Useforbusiness
        {
            get
            {
                return (string)resourceProvider.GetResource("Use for business", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your payment details are stored securely</summary>
        public static string yourpayment
        {
            get
            {
                return (string)resourceProvider.GetResource("yourpayment", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>You can store a credit card for business-related travel; that way, it'll be easier to invoice these bookings.</summary>
        public static string youstorecredit
        {
            get
            {
                return (string)resourceProvider.GetResource("youstorecredit", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>MyProfile</summary>
        public static string MyProfile
        {
            get
            {
                return (string)resourceProvider.GetResource("MyProfile", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Check Your Email and click the below link</summary>
        public static string CheckYourEmailandclickthebelowlink
        {
            get
            {
                return (string)resourceProvider.GetResource("CheckYourEmailandclickthebelowlink", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Reset Password</summary>
        public static string ResetPassword
        {
            get
            {
                return (string)resourceProvider.GetResource("ResetPassword", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Confirm password</summary>
        public static string Confirmpassword
        {
            get
            {
                return (string)resourceProvider.GetResource("Confirmpassword", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your Password will be reset Successfully</summary>
        public static string YourPasswordwillberesetSuccessfully
        {
            get
            {
                return (string)resourceProvider.GetResource("YourPasswordwillberesetSuccessfully", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Password does not match</summary>
        public static string Passworddoesnotmatch
        {
            get
            {
                return (string)resourceProvider.GetResource("Passworddoesnotmatch", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please Check Room Facilities</summary>
        public static string PleaseCheckRoomFacilities
        {
            get
            {
                return (string)resourceProvider.GetResource("PleaseCheckRoomFacilities", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please Check Agreement</summary>
        public static string PleaseCheckAgreement
        {
            get
            {
                return (string)resourceProvider.GetResource("PleaseCheckAgreement", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Updated Successfully</summary>
        public static string UpdatedSuccess
        {
            get
            {
                return (string)resourceProvider.GetResource("UpdatedSuccess", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Edit picture</summary>
        public static string Editpicture
        {
            get
            {
                return (string)resourceProvider.GetResource("Editpicture", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Expiry date</summary>
        public static string Expirydate
        {
            get
            {
                return (string)resourceProvider.GetResource("Expirydate", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>CreditCard Already Exists</summary>
        public static string CreditCardAlreadyExists
        {
            get
            {
                return (string)resourceProvider.GetResource("CreditCardAlreadyExists", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Choose Your Language</summary>
        public static string Chooselanguage
        {
            get
            {
                return (string)resourceProvider.GetResource("Chooselanguage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Please Verify Your Account for Edit Your Page</summary>
        public static string pleaseverify
        {
            get
            {
                return (string)resourceProvider.GetResource("pleaseverify", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Currencies</summary>
        public static string Currencies
        {
            get
            {
                return (string)resourceProvider.GetResource("Currencies", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Night</summary>
        public static string Night
        {
            get
            {
                return (string)resourceProvider.GetResource("Night", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string Maintenance
        {
            get
            {
                return (string)resourceProvider.GetResource("Maintenance", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string UserHistory
        {
            get
            {
                return (string)resourceProvider.GetResource("UserHistory", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Communications
        {
            get
            {
                return (string)resourceProvider.GetResource("Communications", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string HotelRateAvailability
        {
            get
            {
                return (string)resourceProvider.GetResource("HotelRateAvailability", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string UpperAgelimit
        {
            get
            {
                return (string)resourceProvider.GetResource("UpperAgelimit", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string Operation
        {
            get
            {
                return (string)resourceProvider.GetResource("Operation", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string Noroomsavailable
        {
            get
            {
                return (string)resourceProvider.GetResource("NoroomsAvilable", CultureInfo.CurrentUICulture.Name);
            }
      }
        public static string Typeusername
        {
            get
            {
                return (string)resourceProvider.GetResource("Typeusername", CultureInfo.CurrentUICulture.Name);
            }
      }
        public static string TypePassword
        {
            get
            {
                return (string)resourceProvider.GetResource("TypePassword", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Your browser doesn't have Flash, Silverlight, Gears, BrowserPlus or HTML5 support.</summary>
        public static string YourBrowserDoesnotHaveFlash
        {
            get
            {
                return (string)resourceProvider.GetResource("YourBrowserDoesnotHaveFlash", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Save SuccessFully</summary>
        public static string SaveSuccessFully
        {
            get
            {
                return (string)resourceProvider.GetResource("SaveSuccessFully", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>All...</summary>
        public static string All
        {
            get
            {
                return (string)resourceProvider.GetResource("All", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Select Country...</summary>
        public static string SelectCountry
        {
            get
            {
                return (string)resourceProvider.GetResource("SelectCountry", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Select Salutation...</summary>
        public static string SelectSalutation
        {
            get
            {
                return (string)resourceProvider.GetResource("SelectSalutation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>SelectStatus...</summary>
        public static string SelectStatus
        {
            get
            {
                return (string)resourceProvider.GetResource("SelectStatus", CultureInfo.CurrentUICulture.Name);
            }
        }


        /// <summary>Export to Excel</summary>
        public static string ExporttoExcel
        {
            get
            {
                return (string)resourceProvider.GetResource("ExporttoExcel", CultureInfo.CurrentUICulture.Name);
            }
        }

    }          
}

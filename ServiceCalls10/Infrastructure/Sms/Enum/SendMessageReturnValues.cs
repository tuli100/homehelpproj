using System;
using System.Xml.Serialization;

namespace HomeHelpCallsWebSite.Infrastructure.Sms.Enum
{
 
        [Serializable()]
        [XmlTypeAttribute(Namespace = "http://www.smscenter.co.il/")]
        public enum SendMessageReturnValues
        {
            OK,
            InvalidUserNameOrPassword,
            CompanyNotAllowedToSendToSpecifiedProvider,
            SomeMessagesSent,
            NoMessagesSent,
            InternalError,
            InvalidPhoneNumber,
            ThePhoneNumberListMustContainAtLeastOneNumber,
            ThePhoneNumberListAndTheCCListMustHaveTheSameNumberOfItems,
            CompanyIsDisabled,
            UserIsDisabledOrDeleted,
            CompanyIsNotAllowedToPushAnymoreMessages,
            InvalidIPAddress,
            CompanyIsNotAllowedToSendReverseBillingMessages,
            PriceForReverseBillingCantBeZero,
            MessageCanNotBeEmpty,
        }

}
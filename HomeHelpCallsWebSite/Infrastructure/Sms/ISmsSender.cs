using System;
using HomeHelpCallsWebSite.Infrastructure.Sms.Enum;
using System.ServiceModel;

namespace HomeHelpCallsWebSite.Infrastructure.Sms
{
    [ServiceContract(Namespace = "http://www.smscenter.co.il/")]
    public interface ISmsSender
    {
        [OperationContract(Action = "http://www.smscenter.co.il/SendMessagesV2", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        [ServiceKnownType(typeof(object[]))]
        SendMessageReturnValues SendMessagesV2(string UserName, string Password, string SenderName, string SendToPhoneNumbers, string CCToEmail, string Message, SMSOperation SMSOperation, long DeliveryDelayInMinutes, long ExpirationDelayInMinutes, MessageOptions MessageOption, double Price);
    }
}
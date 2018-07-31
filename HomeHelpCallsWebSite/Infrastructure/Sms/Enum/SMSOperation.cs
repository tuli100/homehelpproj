using System;
using System.Xml.Serialization;

namespace HomeHelpCallsWebSite.Infrastructure.Sms.Enum
{
    [Serializable]
    [XmlType(Namespace = "http://www.smscenter.co.il/")]
    public enum SMSOperation
    {
        Push,
        Pull,
        ReverseBilling,
        WAPPush,
        BinaryDCS_F7,
        WapQuestion_OC,
        WapMessage_OC,
        IVR,
        MMS,
        Flash,
        IVRConference,
        GCM
    }
}
using System;
using System.Xml.Serialization;

namespace HomeHelpCallsWebSite.Infrastructure.Sms.Enum
{
    [Serializable()]
    [XmlType(Namespace = "http://www.smscenter.co.il/")]
    public enum MessageOptions
    {
        Regular,
        Concatenated,
        Reply
    }
}
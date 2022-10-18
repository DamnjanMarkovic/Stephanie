using System;
using System.Xml;

using static Stephanie.RequestCommands;

namespace Stephanie.Helpers
{
    public static class XmlHelper
    {

        private const string parameters_path = "DeviceInfo/Paramaters/Settings[@ID='29']";

        private const string attribute_id = "ID";
        private const string attribute_divider = "Divider";
        private const string attribute_description = "Description";

        private const string value_id = "29";
        private const string value_divider = "1";
        private const string value_description = "HFO amplitude in %";

        private static string ProtocolVersion { get; set; }

        public static void Init(string protocolVersion)
        {
            ProtocolVersion = protocolVersion;
        }


        public static XmlDocument MatchXmlDocumentToProtocol(XmlDocument currentDocument)
        {
            switch (ProtocolVersion)
            {
                case "1.3":
                    return UpdateXmlDoc(currentDocument);
                default:
                    return currentDocument;
            }
        }

        private static XmlDocument UpdateXmlDoc(XmlDocument document)
        {
            try
            {
                XmlNode hfoKey = document.SelectNodes(parameters_path)[0];
                hfoKey.Attributes[attribute_divider].Value = value_divider;
                hfoKey.Attributes[attribute_description].Value = value_description;
            }
            catch (Exception)
            {
                throw new InvalidXMLResourceForRequestCommandsException();
            }

            return document;
        }

    }

    


}

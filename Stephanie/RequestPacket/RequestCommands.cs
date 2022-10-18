using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Stephanie.Helpers;
using static Stephanie.RequestCommands;

namespace Stephanie
{
    internal partial class RequestCommands
    {

        #region Constants

        protected const string REQUESTCOMMANDS_XML_XPATH = "DeviceInfo/RequestCommands/*";
        protected const string REQUESTCOMMANDS_XML_TYPE = "RequestPacketType";
        protected const string REQUESTCOMMANDS_XML_TEXT = "RequestCommandText";

        #endregion

        protected static Dictionary<Type, string> m_oRequestCommandsList = null;
        public static Dictionary<Type, string> RequestCommandsList
        {
            get
            {
                if (m_oRequestCommandsList == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlNodeList xmlNodes;
                    Type curType;
                    string curCommandText;

                    m_oRequestCommandsList = new Dictionary<Type, string>();

                    xmlDoc.LoadXml(Properties.Resources.DeviceInfo);
                    xmlDoc = XmlHelper.MatchXmlDocumentToProtocol(xmlDoc);

                    xmlNodes = xmlDoc.SelectNodes(REQUESTCOMMANDS_XML_XPATH);

                    foreach (XmlNode xmlNode in xmlNodes)
                    {
                        if (xmlNode.Attributes[REQUESTCOMMANDS_XML_TYPE] == null ||
                            xmlNode.Attributes[REQUESTCOMMANDS_XML_TEXT] == null)
                        {
                            throw new InvalidXMLResourceForRequestCommandsException();
                        }

                        curType = Type.GetType(xmlNode.Attributes[REQUESTCOMMANDS_XML_TYPE].Value, false, true);
                        curCommandText = xmlNode.Attributes[REQUESTCOMMANDS_XML_TEXT].Value;

                        if (curType == null)
                        {
                            throw new RequestPacketTypeNotFoundException(xmlNode.Attributes[REQUESTCOMMANDS_XML_TYPE].Value);
                        }

                        m_oRequestCommandsList.Add(curType, curCommandText);
                    }
                }

                return m_oRequestCommandsList;
            }
        }

        // Diabling the option to create an instance of this class.
        private RequestCommands() { }

        public static string GetRequestCommand(Type requestPacketType)
        {
            if (!RequestCommandsList.ContainsKey(requestPacketType))
            {
                throw new UnrecognizedPacketTypeException(requestPacketType.FullName);
            }

            return RequestCommandsList[requestPacketType];
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Stephanie.Helpers;

namespace Stephanie
{
    public abstract partial class Parameter : ParameterInfo
    {
        #region Contsants

        protected const string REQUESTCOMMANDS_XML_XPATH = "DeviceInfo/Paramaters/*";
        protected const string REQUESTCOMMANDS_XML_ID = "ID";
        protected const string REQUESTCOMMANDS_XML_NAME = "Name";
        protected const string REQUESTCOMMANDS_XML_DESCRIPTION = "Description";
        protected const string REQUESTCOMMANDS_XML_DEVIDER = "Divider";

        #endregion

        #region Properties

        protected string m_Value;
        public string Value
        {
            get { return m_Value; }
        }

        protected bool m_IsValid;
	    public bool IsValid
	    {
	      get { return m_IsValid;}
	    }

        // ParamInfoList
        //  Description: Reads all parameters names, descriptions and dividers from the resource xml and creates a static list from it.
        protected static Dictionary<byte, ParameterInfo> m_ParamInfoList = null;
        public static Dictionary<byte, ParameterInfo> ParamInfoList
        {
            get
            {
                if (m_ParamInfoList == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlNodeList xmlNodes;
                    byte curID;
                    string curName;
                    string curDescription;
                    Int16 curDivider;

                    m_ParamInfoList = new Dictionary<byte, ParameterInfo>();

                    xmlDoc.LoadXml(Properties.Resources.DeviceInfo);
                    xmlDoc = XmlHelper.MatchXmlDocumentToProtocol(xmlDoc);

                    xmlNodes = xmlDoc.SelectNodes(REQUESTCOMMANDS_XML_XPATH);

                    foreach (XmlNode xmlNode in xmlNodes)
                    {
                        if (xmlNode.Attributes[REQUESTCOMMANDS_XML_ID] == null ||
                            xmlNode.Attributes[REQUESTCOMMANDS_XML_NAME] == null)
                        {
                            throw new InvalidXMLResourceForParamInfoListException();
                        }

                        curID = byte.Parse(xmlNode.Attributes[REQUESTCOMMANDS_XML_ID].Value);
                        curName = xmlNode.Attributes[REQUESTCOMMANDS_XML_NAME].Value;

                        // Getting Description if available
                        curDescription = xmlNode.Attributes[REQUESTCOMMANDS_XML_DESCRIPTION] == null
                                            ? null : xmlNode.Attributes[REQUESTCOMMANDS_XML_DESCRIPTION].Value;

                        curDivider = xmlNode.Attributes[REQUESTCOMMANDS_XML_DEVIDER] == null
                                            ? (Int16)1 : Int16.Parse(xmlNode.Attributes[REQUESTCOMMANDS_XML_DEVIDER].Value);

                        m_ParamInfoList.Add(curID, new ParameterInfo(curName, curDescription, curDivider));
                    }
                }

                return m_ParamInfoList;
            }
        }

        #endregion

        #region Constructors

        public Parameter(byte[] byData, int startIndex)
            : this(byData[startIndex], BitConverter.ToInt16(byData, startIndex + 1))
        { }

        public Parameter(byte id, Int16 value)
        {
            ParameterInfo oParamInfo;

            // Making sure the id is recognized
            if (!ParamInfoList.ContainsKey(id))
            {
                throw new InvalidParameterIDException(id);
            }

            oParamInfo = ParamInfoList[id];
            m_Name = oParamInfo.Name;
            m_Description = oParamInfo.Description;
            m_Divider = oParamInfo.Divider;
            m_IsValid = AssignAndValidateValue(value);
        }

        #endregion

        #region Functions
            
            abstract protected bool AssignAndValidateValue(Int16 value);

        #endregion
    }
}

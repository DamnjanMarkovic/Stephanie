using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Stephanie.Helpers;

namespace Stephanie
{
    internal partial class AlarmParameter : Parameter
    {
        #region Constants
        protected const Int16 INVALID_VALUE = 255;
        protected const string ALARM_XPATH = "DeviceInfo/Alarms/*";
        protected const string ALARM_NO = "AlarmNo";
        protected const string ALARM_TEXT = "Text";
        protected const string ALARM_PRIORITY_XPATH = "DeviceInfo/AlarmPriorities/*";
        protected const string ALARM_PRIORITY_ID = "ID";
        protected const string ALARM_PRIORITY_TEXT = "Text";

        protected const string ALARM_DESC_FORMAT = "Alarm Text Message: {0}, Alarm Priority: {1}";
        #endregion

        #region Properties

        // AlarmList
        //  Description: Reads all alarms names from the resource xml and creates a static list from it.
        protected static Dictionary<byte, string> m_AlarmList = null;
        protected static Dictionary<byte, string> AlarmList
        {
            get
            {
                if (m_AlarmList == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlNodeList xmlNodes;
                    byte curAlarmNo;
                    string curAlarmText;

                    m_AlarmList = new Dictionary<byte, string>();

                    xmlDoc.LoadXml(Properties.Resources.DeviceInfo);
                    xmlNodes = xmlDoc.SelectNodes(ALARM_XPATH);

                    foreach (XmlNode xmlNode in xmlNodes)
                    {
                        if (xmlNode.Attributes[ALARM_NO] == null ||
                            xmlNode.Attributes[ALARM_TEXT] == null)
                        {
                            throw new InvalidXMLResourceForAlarmsException();
                        }

                        curAlarmNo = byte.Parse(xmlNode.Attributes[ALARM_NO].Value);
                        curAlarmText = xmlNode.Attributes[ALARM_TEXT].Value;

                        m_AlarmList.Add(curAlarmNo, curAlarmText);
                    }
                }

                return m_AlarmList;
            }
        }

        // AlarmPrioritiesList
        //  Description: Reads all alarms priorities names from the resource xml and creates a static list from it.
        protected static Dictionary<byte, string> m_AlarmPrioritiesList = null;
        protected static Dictionary<byte, string> AlarmPrioritiesList
        {
            get
            {
                if (m_AlarmPrioritiesList == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlNodeList xmlNodes;
                    byte curAlarmPriorityID;
                    string curAlarmPriorityText;

                    m_AlarmPrioritiesList = new Dictionary<byte, string>();

                    xmlDoc.LoadXml(Properties.Resources.DeviceInfo);
                    xmlDoc = XmlHelper.MatchXmlDocumentToProtocol(xmlDoc);

                    xmlNodes = xmlDoc.SelectNodes(ALARM_PRIORITY_XPATH);

                    foreach (XmlNode xmlNode in xmlNodes)
                    {
                        if (xmlNode.Attributes[ALARM_PRIORITY_ID] == null ||
                            xmlNode.Attributes[ALARM_PRIORITY_TEXT] == null)
                        {
                            throw new InvalidXMLResourceForAlarmPrioritiesException();
                        }

                        curAlarmPriorityID = byte.Parse(xmlNode.Attributes[ALARM_PRIORITY_ID].Value);
                        curAlarmPriorityText = xmlNode.Attributes[ALARM_PRIORITY_TEXT].Value;

                        m_AlarmPrioritiesList.Add(curAlarmPriorityID, curAlarmPriorityText);
                    }
                }

                return m_AlarmPrioritiesList;
            }
        }

        #endregion

        #region Constructors

        public AlarmParameter(byte[] byData, int startIndex)
            : base(byData, startIndex)
        { }

        public AlarmParameter(byte id, Int16 value)
            : base(id, value)
        { }
        
        #endregion

        #region Functions

        protected override bool AssignAndValidateValue(short value)
        {
            bool retVal = false;

            if (value != INVALID_VALUE)
            {
                AssignAlarmValue(value);
                retVal = true;
            }

            return retVal;
        }

        protected void AssignAlarmValue(short value)
        {
            string alarmDesc, alarmPriority;

            // using the lower byte to identify the alarm description
            alarmDesc = GetAlarmDescription((byte)(value & 0xff));
            // using the lower byte to identify the alarm priority
            alarmPriority = GetAlarmPriority((byte)(value >> 8));

            m_Value = string.Format(ALARM_DESC_FORMAT, alarmDesc, alarmPriority);
        }

        protected string GetAlarmDescription(byte alarmNo)
        {
            if (!AlarmList.ContainsKey(alarmNo))
            {
                throw new InvalidAlarmNumberException(alarmNo);
            }

            return AlarmList[alarmNo];
        }

        protected string GetAlarmPriority(byte alarmPriorityID)
        {
            if (!AlarmPrioritiesList.ContainsKey(alarmPriorityID))
            {
                throw new InvalidAlarmPriorityIDException(alarmPriorityID);
            }

            return AlarmPrioritiesList[alarmPriorityID];
        }

        #endregion
    }
}

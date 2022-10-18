using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    internal partial class AlarmParameter
    {
        internal class InvalidXMLResourceForAlarmsException : Exception
        {
            public InvalidXMLResourceForAlarmsException()
                : base(Properties.Resources.ERR_ALARM_XML)
            { }
        }

        internal class InvalidXMLResourceForAlarmPrioritiesException : Exception
        {
            public InvalidXMLResourceForAlarmPrioritiesException()
                : base(Properties.Resources.ERR_ALARM_PRIORITY)
            { }
        }

        internal class InvalidAlarmNumberException : Exception
        {
            public InvalidAlarmNumberException(byte alarmNo)
                : base(string.Format(Properties.Resources.ERR_ALARM_NO,alarmNo))
            { }
        }

        internal class InvalidAlarmPriorityIDException : Exception
        {
            public InvalidAlarmPriorityIDException(byte alarmPriorityID)
                : base(string.Format(Properties.Resources.ERR_ALARM_PRIORITY, alarmPriorityID))
            { }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    internal partial class DataParameter : Parameter
    {

        #region Constants
        protected const Int16 INVALID_VALUE = -1;
        #endregion

        #region Constructors

        public DataParameter(byte[] byData, int startIndex)
            : base(byData, startIndex)
        { }
        
        public DataParameter(byte id, Int16 value)
            : base(id, value)
        { }
      
        #endregion

        #region Functions

        protected override bool AssignAndValidateValue(short value)
        {
            bool retVal = false;

            if (value != INVALID_VALUE)
            {
                // Calculating the real value for this parameter
                m_Value = ((float)value / m_Divider).ToString();
                retVal = true;
            }

            return retVal;
        }

        #endregion
    }
}

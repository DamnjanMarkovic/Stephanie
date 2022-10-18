using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Stephanie
{
    // class ParameterInfo
    //  Description: this class is used to hold general information regarding a single parameter
    public class ParameterInfo
    {

        #region Properties

        protected string m_Name;
        public string Name
        {
            get { return m_Name; }
        }

        protected string m_Description;
	    public string Description
	    {
	      get { return m_Description;}
    	}

        protected Int16 m_Divider;
        public Int16 Divider
        {
            get { return m_Divider; }
        }

        #endregion

        #region Constructor

        public ParameterInfo()
        { }

        public ParameterInfo(string name, string description, Int16 divider)
        {
            m_Name = name;
            m_Divider = divider;

            if (description != null)
            {
                m_Description = description;
            }
        }

        #endregion
    }
}

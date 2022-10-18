using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

using Microsoft.Win32;

using MVBSLib;

using Stephanie.Helpers;
using Stephanie.RequestPacket;

// ReSharper disable InconsistentNaming

namespace Stephanie
{

    public partial class Driver : IIObjectExecution,
                            IIObjectExecutionEx,
                            IISupportLazyWriter,
                            IISupportOutComm
    {

        #region Constants

        protected const int DEFAULT_TIMEOUT = 5000;
        protected const string DUMP_FILENAME = "StephanieDump_{0} {1}.bin";
        protected const string INDALID_PARAM_MSG = "Parameter {0} was reported invalid by the device";

        protected const string SET_MODESWITCH = "SET_ModeSwitch";

        #endregion

        #region Dump Configuration

        private string m_DumpFileFullPath;

        private string DumpFileFullPath
        {
            get
            {
                if (m_DumpFileFullPath != null) return m_DumpFileFullPath;

                string sIP, sPort;

                OutComm.GetRemoteAddress(out sIP, out sPort);

                var sDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var sFilename = string.Format(DUMP_FILENAME, sIP, sPort);

                if (sDirectory != null) m_DumpFileFullPath = Path.Combine(sDirectory, sFilename);
                return m_DumpFileFullPath;
            }
        }
        #endregion

        #region Back Server Objects

        public ConfigSettings ConfSettings { get; set; }
        public PoolSettings PoolSettings { get; set; }



        protected ILazyWriter m_oLazyWriter;
        public ILazyWriter LazyWriter
        {
            get { return m_oLazyWriter; }
        }

        protected IOutComm m_oOutComm;
        public IOutComm OutComm
        {
            get { return m_oOutComm; }
        }

        protected IHelperObject m_oHelperObject;
        public IHelperObject HelperObject
        {
            get { return m_oHelperObject; }
        }

        #endregion

        #region IIObjectExecution Members

        public void ChangeDriverConfiguration()
        {
        }

        public object Execute(object Param)
        {
            try
            {
                ProcessMessages();
            }
            catch (Exception ex)
            {
                HelperObject.DebugTrace($"Exception thrown : {ex}");
                throw;
            }

            return null;
        }

        public void Initialize(IHelperObject pHelperObject)
        {
            m_oHelperObject = pHelperObject;
            InitPoolSettings();
        }

        public virtual void InitPoolSettings ()
        {
            try
            {
                PoolSettings = new PoolSettings(m_oHelperObject);
                PoolSettings.LoadSettings();
                XmlHelper.Init(PoolSettings.ProtocolVersion);
            }
            catch (Exception e)
            {
                m_oHelperObject.WriteToLog(e.ToString());
                throw;
            }
        }

        public void Terminate()
        {
           
        }

        #endregion

        #region IIObjectExecutionEx Members

        public void InitializeConfiguration()
        {

            try
            {
                ConfSettings = new ConfigSettings(HelperObject);
                ConfSettings.LoadSettings();
            }
            catch (Exception ex)
            {
                HelperObject.WriteToLog(ex.ToString());
                throw;
            }
            LazyWriter.SetConfigDefaultBed(ConfSettings.BedID);
    
            
        }

        #endregion

        #region IISupportLazyWriter Members

        public void SetLazyWriterObject(ILazyWriter pLazyWriter)
        {
            m_oLazyWriter = pLazyWriter;
        }

        #endregion

        #region IISupportOutComm Members

        public void SetOutCommObject(IOutComm pOutComm)
        {
            m_oOutComm = pOutComm;
        }

        #endregion

        #region Functions

        public bool IsDebugMode()
        {
            bool retVal;
            int iDebugMode = 0;
            try
            {
                iDebugMode = (int)Registry.GetValue("HKEY_LOCAL_MACHINE\\" + HelperObject.GetPoolRegKey(), "DebugState", iDebugMode);
                retVal = iDebugMode > 0;
            }
            catch (Exception ex)
            {
                HelperObject.WriteToLog($"DebugState key is missing - {ex}");
                retVal = false;
            }
         
            return retVal;
        }

        protected int GetBedID()
        {
            string sEntry = "BedID";
            string sDefault = "-1";

            var sValue = m_oHelperObject.GetConfigCustomValue(ref sEntry, ref sDefault);

            if (sValue == sDefault)
            {
                throw new InvalidBedIDException();
            }

            return int.Parse(sValue);
        }

        // ProcessMessages
        //  Description:    Responsible for getting all data (data values, alarms and settings) from the device,
        //                  parse them and write them to the lazy writer
        protected void ProcessMessages()
        {
            // Creating a list to store all parsed data (as parameters)
            List<Parameter> oParamList = new List<Parameter>();

            // add data values retrieved from the device
            oParamList.AddRange(ProcessData());
            // +++ currently not using alarm cause of documentation-reality mismatch!!!
            // add alarms data retrieved from the device
            //oParamList.AddRange(ProcessAlarms());
            // add settings values retrieved from the device
            oParamList.AddRange(ProcessSettings());

            // add device settings values retrieved from the device
            oParamList.AddRange(ProcessDeviceSettings());

            // write all data to the lazy writer
            WriteToLazyWriter(oParamList);
        }

        // ProcessData
        //  Description:    Communicates with the device to get data values and then parses it.
        //
        //  Output:         List of parameters which represents the parsed data extracted from the data received from the device.
        protected List<Parameter> ProcessData()
        {
            // Create a request packet and send it to the device
            var byData = GetRawDataFromDevice(new DataRequestPacket());

            // Create a response packet from the data arrived from the device
            var oDataResponsePacket = new DataResponsePacket(byData);

            // Returning the data as a list of parameters (parsed)
            return oDataResponsePacket.GetParsedData();
        }

        // ProcessAlarms
        //  Description:    Communicates with the device to get alarms data and then parses it.
        //
        //  Output:         List of parameters which represents the parsed data extracted from the data received from the device.
        protected List<Parameter> ProcessAlarms()
        {
            // Create a request packet and send it to the device
            var byData = GetRawDataFromDevice(new AlarmRequestPacket());

            // Create a response packet from the data arrived from the device
            var oAlarmResponsePacket = new AlarmResponsePacket(byData);

            // Returning the data as a list of parameters (parsed)
            return oAlarmResponsePacket.GetParsedData();
        }

        // ProcessSettings
        //  Description:    Communicates with the device to get settings data and then parses it.
        //
        //  Output:         List of parameters which represents the parsed data extracted from the data received from the device.
        protected List<Parameter> ProcessSettings()
        {
            // Create a request packet and send it to the device
            var byData = GetRawDataFromDevice(new SettingsRequestPacket());

            // Create a response packet from the data arrived from the device
            var oSettingResponsePacket = new SettingResponsePacket(byData);

            // Returning the data as a list of parameters (parsed)
            return oSettingResponsePacket.GetParsedData();
        }

        // ProcessDeviceSettings
        //  Description:    Communicates with the device to get device settings data and then parses it.
        //
        //  Output:         List of parameters which represents the parsed data extracted from the data received from the device settings.
        protected List<Parameter> ProcessDeviceSettings()
        {
            // Create a request packet and send it to the device
            var byData = GetRawDataFromDevice(new DeviceSettingsRequestPacket());

            // Create a response packet from the data arrived from the device
            var oSettingResponsePacket = new DeviceSettingResponsePacket(byData);

            // Returning the data as a list of parameters (parsed)
            return oSettingResponsePacket.GetParsedData();
        }



        // WriteToLazyWriter
        //  Description:    Responsible to write all data extracted from packets received from the device to the lazy writer.
        //                  If the parameter is invalid it will be written to the log.
        //
        //  Input:          List of parameters to be written to the lazy writer
        protected void WriteToLazyWriter(List<Parameter> oParamList)
        {

            // issue 107035.2 => Data enters in stand-by mode
            if (IsInStandByMode(oParamList))
            {
                LazyWriter.WriteSignalEx(SET_MODESWITCH, "0");
                HelperObject.WriteToLog("The Device is in StandBy mode.");
                return;
            }


            foreach (Parameter oParam in oParamList)
            {
                if (oParam.IsValid)
                {
                    // If the parameter is valid - write to lazy writer
                    LazyWriter.WriteSignalEx(oParam.Name, oParam.Value);
                    if (oParam.Description != null)
                    {
                        // If a description is available write it as well
                        LazyWriter.WriteDescription(oParam.Name, oParam.Description);
                    }
                }
                else
                {
                    // Write to log to indicate that this parameter was reported invalid by the device.
                    HelperObject.DebugTrace(string.Format(INDALID_PARAM_MSG, oParam.Name));
                }
            }
        }

        /// <summary>
        /// Check the value of the SET_ModeSwitch.
        /// </summary>
        /// <param name="oParamList"></param>
        /// <returns>return true is in standby mode</returns>
        private bool IsInStandByMode(IEnumerable<Parameter> oParamList)
        {
            foreach (Parameter parameter in oParamList)
            {
                if (parameter.Name == SET_MODESWITCH && parameter.Value == "0") return true;
            }
            return false;
        }

        // GetRawDataFromDevice
        //  Description:    Getting raw data from device by request
        //
        //  Input:          oRequestPacket - the request packet to be sent to the device.
        //  Output:         the byte array received from the device upon sending the request.
        protected byte[] GetRawDataFromDevice(RequestPacket.RequestPacket oRequestPacket)
        {
            object o;
            byte[] byBuffer;
            // Creating a byte array to be sent as the request
            byBuffer = CommunicationPacket.AddCommunicationBytes(oRequestPacket.GetPacketAsBytes());

            HelperObject.DebugTrace("Sending: " + Encoding.ASCII.GetString(byBuffer));

             o = byBuffer;

            OutComm.ClearBuffer();
            if (!OutComm.SendByteArray(ref o))
            {
                if (!OutComm.Connect())
                {
                    throw new DriverConnectionFailedException();
                }

                if (!OutComm.SendByteArray(ref o))
                {
                    throw new DriverCommErrorException();
                }
            }

            var eRetCode = OutComm.ReceiveByteArray(out o, DEFAULT_TIMEOUT, false);

            switch (eRetCode)
            {
                case OutCommCodes.CommError:
                    throw new DriverCommErrorException();
                case OutCommCodes.Timeout:
                    throw new DriverTimeoutException();
            }

            // Converting the received byte array from one based to zero based
            byBuffer = new byte[((Array)o).Length];
            Array.Copy((Array)o, 1, byBuffer, 0, ((Array)o).Length-1);

            // Striping the communication bytes from the received byte array
            byBuffer = CommunicationPacket.StripCommunicationBytes(byBuffer);

            if (IsDebugMode())
            {
                // Writing the data received to a dump file
                DumpResponse(byBuffer);
            }

            return byBuffer;
        }

        private void DumpResponse(byte[] byData)
        {
            // Writing the packet content to the dump file.
            File.WriteAllBytes(DumpFileFullPath, byData);
        }


        #endregion
    }
}

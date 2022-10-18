using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MVBSLib;

using NSubstitute;

namespace StephanieUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private const int default_timeout = 5000;

        private static bool DataSent { get; set; }


        [TestMethod]
        public void TestValuesStandByMode()
        {
            byte[] dataResponse =
                    {
                        0x00, 0x00, 0x25, 0x31, 0x01, 0xC5, 0x00, 0x02, 0xFF, 0xFF, 0x03, 0x5D, 0x00, 0x04,
                        0x4C, 0x00, 0x05, 0x02, 0x00, 0x06, 0x46, 0x00, 0x07, 0x35, 0x01, 0x08, 0x96,
                        0x00, 0x09, 0xA4, 0x01, 0x0A, 0xF2, 0x00, 0x0B, 0x4D, 0x08, 0x0C, 0x06, 0x00,
                        0x6C, 0x9B
                    };

            //null, null, %, 1

            //byte[] settingsResponse =
            //       {
            //            0x00, 0x00, 0x2B, 0x33, 0x12, 0x00, 0x00, 0x13, 0xE8, 0x03, 0x14, 0x13, 0x00, 0x15,
            //            0x5D, 0x00, 0x16, 0x4F, 0x00, 0x17, 0xC6, 0x00, 0x18, 0x05, 0x00, 0x19, 0x18,
            //            0x01, 0x1A, 0x03, 0x00, 0x1B, 0x14, 0x00, 0x1C, 0xB2, 0x01, 0x1D, 0x19, 0x00,
            //            0x40, 0xA5, 0x00, 0x43, 0x00, 0x00, 0x3D, 0xA4};

            byte[] settingsResponse =
                    {
                            0x00, 0x00, 0x0A, 0x34,
                            0x20, 0x01,
                            0x21, 0x02, 0x00,
                            0x22, 0x01,
                            0x23, 0x01,
                            0xE9, 0x11
                    };

            List<Tuple<string, string>> sequenceList = TestFullProcess(dataResponse, settingsResponse);

            Assert.AreEqual(sequenceList.Count, 1);
            // sequenceList contains only the debug mode => no data entering MV


        }

        [TestMethod]
        public void TestValuesRunningNoProtocolVersion()
        {
            byte[] dataResponse =
               {
                    0x00, 0x02, 0x25, 0x31, 0x01, 0xc7, 0x00, 0x02,
                    0x3f, 0x01, 0x03, 0x5a, 0x00, 0x04, 0x27, 0x00,
                    0x05, 0x02, 0x00, 0x06, 0x00, 0x00, 0x07, 0x58,
                    0x02, 0x08, 0x90, 0x01, 0x09, 0xd2, 0x00, 0x0a,
                    0xd5, 0x00, 0x0b, 0x00, 0x00, 0x0c, 0x00, 0x00,
                    0x78, 0xd5

                };

            //byte[] settingsResponse =
            //        {
            //        0x00, 0x02, 0x2b, 0x33, 0x12, 0x7c, 0x01, 0x13,
            //        0xff, 0xff, 0x14, 0xff, 0xff, 0x15, 0x64, 0x00,
            //        0x16, 0x28, 0x00, 0x17, 0xc8, 0x00, 0x18, 0x05,
            //        0x00, 0x19, 0x00, 0x00, 0x1a, 0x03, 0x00, 0x1b,
            //        0x0c, 0x00, 0x1c, 0xd2, 0x00, 0x1d, 0x14, 0x00,
            //        0x40, 0x43, 0x00, 0x43, 0x01, 0x00, 0x56, 0xa0
            //        };

            byte[] settingsResponse =
                    {
                            0x00, 0x00, 0x0A, 0x34,
                            0x20, 0x01,
                            0x21, 0x02, 0x00,
                            0x22, 0x01,
                            0x23, 0x01,
                            0xE9, 0x11
                    };

            List<Tuple<string, string>> sequenceList = TestFullProcess(dataResponse, settingsResponse);

            ValidateSequences(sequenceList, "PEEP", "3.9");
            ValidateSequences(sequenceList, "Freq", "60");
            ValidateSequences(sequenceList, "R", "0");
            ValidateSequences(sequenceList, "SET_Resist.", "-0.1");
            ValidateSequences(sequenceList, "SET_FiO2", "21");
            ValidateSequences(sequenceList, "SET_ModeSwitch", "1");
            ValidateSequences(sequenceList, "SET_Posz", "2");


        }

        [TestMethod]
        public void TestValuesRunningProtocol1_3()
        {
            byte[] dataResponse =
               {
                    0x00, 0x02, 0x25, 0x31, 0x01, 0xc7, 0x00, 0x02,
                    0x3f, 0x01, 0x03, 0x5a, 0x00, 0x04, 0x27, 0x00,
                    0x05, 0x02, 0x00, 0x06, 0x00, 0x00, 0x07, 0x58,
                    0x02, 0x08, 0x90, 0x01, 0x09, 0xd2, 0x00, 0x0a,
                    0xd5, 0x00, 0x0b, 0x00, 0x00, 0x0c, 0x00, 0x00,
                    0x78, 0xd5
                    };

            //byte[] settingsResponse =
            //        {
            //        0x00, 0x02, 0x2b, 0x33, 0x12, 0x7c, 0x01, 0x13,
            //        0xff, 0xff, 0x14, 0xff, 0xff, 0x15, 0x64, 0x00,
            //        0x16, 0x28, 0x00, 0x17, 0xc8, 0x00, 0x18, 0x05,
            //        0x00, 0x19, 0x00, 0x00, 0x1a, 0x03, 0x00, 0x1b,
            //        0x0c, 0x00, 0x1c, 0xd2, 0x00, 0x1d, 0x14, 0x00,
            //        0x40, 0x43, 0x00, 0x43, 0x01, 0x00, 0x56, 0xa0
            //        };

            byte[] settingsResponse =
                    {
                            0x00, 0x00, 0x0A, 0x34,
                            0x20, 0x01,
                            0x21, 0x02, 0x00,
                            0x22, 0x01,
                            0x23, 0x01,
                            0xE9, 0x11
                    };

            List<Tuple<string, string>> sequenceList = TestFullProcess(dataResponse, settingsResponse, "1.3");

            ValidateSequences(sequenceList, "PEEP", "3.9");
            ValidateSequences(sequenceList, "Freq", "60");
            ValidateSequences(sequenceList, "R", "0");
            ValidateSequences(sequenceList, "SET_Resist.", "-0.1");
            ValidateSequences(sequenceList, "SET_FiO2", "21");
            ValidateSequences(sequenceList, "SET_ModeSwitch", "1");
            ValidateSequences(sequenceList, "SET_Posz", "20");


        }

        private List<Tuple<string, string>> TestFullProcess(byte[] dataBytes, byte[] settingsBytes, string protocolVersion = "")
        {
            DataSent = false;

            StringBuilder logBuilder = new StringBuilder();

            List<Tuple<string, string>> sequenceList;


            IHelperObject helperObjectMock = CreateHelperObjectMock(logBuilder);

            IOutComm outCommMock = CreateOutCommMock(dataBytes, settingsBytes);

            ILazyWriter lazyWriterMock = CreateLazyWriterMock(logBuilder, out sequenceList);


            ExecuteDriverRun(helperObjectMock, lazyWriterMock, outCommMock, protocolVersion);

            return sequenceList;




        }



        private void ValidateSequences(List<Tuple<string, string>> sequenceList, string sequence, string expectedResult)
        {
            string foundResult = string.Empty;

            foreach (var tuple in sequenceList)
            {
                if (tuple.Item1.Equals(sequence))
                {
                    foundResult = tuple.Item2;
                }

            }


            Assert.AreEqual(foundResult, expectedResult);
        }

        private void ExecuteDriverRun(IHelperObject helperObjectMock, ILazyWriter lazyWriterMock, IOutComm outCommMock, string protocolVersion)
        {
            TestableDriver driver = new TestableDriver(protocolVersion, helperObjectMock);

            driver.Initialize(helperObjectMock);
            driver.SetLazyWriterObject(lazyWriterMock);
            driver.SetOutCommObject(outCommMock);
            driver.InitializeConfiguration();
            driver.Execute(null);
        }

        private static IHelperObject CreateHelperObjectMock(StringBuilder logBuilder)
        {
            IHelperObject helperObject = Substitute.For<IHelperObject>();

            helperObject.WhenForAnyArgs(h => h.WriteToLog("")).Do(ci =>
            {
                logBuilder.AppendLine(ci.Arg<string>());
            });

            helperObject.WhenForAnyArgs(h => h.DebugTrace("")).Do(ci =>
            {
                logBuilder.AppendLine(ci.Arg<string>());
            });

            string entryString = string.Empty;
            string defaultString = string.Empty;

            helperObject.GetConfigCustomValue(ref entryString, ref defaultString).ReturnsForAnyArgs(ci => "10");

            helperObject.GetPoolRegKey().Returns(c => @"SOFTWARE\WOW6432Node\iMD Soft\Metavision Back Server\Servers\Mv Communication Server\Drivers Pools\Stephanie\");

            return helperObject;
        }

        private static IOutComm CreateOutCommMock(byte[] dataBytes, byte[] settingsBytes)
        {
            IOutComm outCommMock = Substitute.For<IOutComm>();

            object refObj = null;

            outCommMock.Connect().ReturnsForAnyArgs(info => true);
            outCommMock.SendByteArray(ref refObj).ReturnsForAnyArgs(ci => true);
            object o;

            outCommMock.ReceiveByteArray(out o, default_timeout).ReturnsForAnyArgs(ci =>
            {

                if (DataSent == false)
                {
                    DataSent = true;
                    ci[0] = dataBytes;
                }
                else
                {
                    ci[0] = settingsBytes;
                    DataSent = false;
                }




                return OutCommCodes.NoError;
            });

            outCommMock.ClearBuffer();

            return outCommMock;
        }

        private static ILazyWriter CreateLazyWriterMock(StringBuilder logBuilder, out List<Tuple<string, string>> sequenceList)
        {
            ILazyWriter lazyWriterMock = Substitute.For<ILazyWriter>();

            List<Tuple<string, string>> sequences = new List<Tuple<string, string>>();
            lazyWriterMock.WhenForAnyArgs(l => l.WriteSignalEx("", "")).Do(ci =>
            {
                sequences.Add(new Tuple<string, string>(ci.ArgAt<string>(0), ci.ArgAt<string>(1)));
                logBuilder.AppendLine($"[{ci.ArgAt<string>(0)}]:{ci.ArgAt<string>(1)}");
            });

            lazyWriterMock.SetConfigDefaultBed(10);



            lazyWriterMock.WriteDescription("", "");

            sequenceList = sequences;
            return lazyWriterMock;
        }
    }
}

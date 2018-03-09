using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetGUI;
using SS;
using SSGui;

namespace ControllerTester
{
    [TestClass]
    public class ControllerTester
    {

        //Event Tests
        [TestMethod]
        public void TestRetrieveEditBoxValueEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireRetrieveEditBoxValueEvent(new TextBox());
            Assert.IsTrue(stub.CalledRetrieveEditBoxValueEvent);
        }

        [TestMethod]
        public void TestChangeCurrentEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireChangeCurrentEvent(new SpreadsheetPanel());
            Assert.IsTrue(stub.CalledChangeCurrent);

        }

        [TestMethod]
        public void TestMoveLeftEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireMoveLeftEvent(new SpreadsheetPanel());
            Assert.IsTrue(stub.CalledMoveLeftEvent);

        }

        [TestMethod]
        public void TestMoveRightEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireMoveRightEvent(new SpreadsheetPanel());
            Assert.IsTrue(stub.CalledMoveRightEvent);

        }

        [TestMethod]
        public void TestMoveUpEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireMoveUpEvent(new SpreadsheetPanel());
            Assert.IsTrue(stub.CalledMoveUpEvent);

        }

        [TestMethod]
        public void TestMoveDownEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireMoveDownEvent(new SpreadsheetPanel());
            Assert.IsTrue(stub.CalledMoveDownEvent);
        }

        [TestMethod]
        public void TestChangeCellContentEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireChangeCellContentEvent("");
            Assert.IsTrue(stub.CalledChangeCellContentEvent);
        }

        [TestMethod]
        public void TestUpdateRelevantEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            HashSet<string> tester = new HashSet<string>();
            tester.Add("A1");
            tester.Add("B1");
            tester.Add("C1");
            tester.Add("D1");

            Spreadsheet testee = new Spreadsheet();
            testee.SetContentsOfCell("A1", "z");
            testee.SetContentsOfCell("B1", "=2");
            testee.SetContentsOfCell("C1", "2=2");
            controller.setModel(testee);

            controller.setCellsToChange(tester);
            

            stub.FireUpdateRelevantEvent();
            Assert.IsTrue(stub.CalledUpdateRelevantEvent);
        }

        [TestMethod]
        public void TestUpdateRelevantEvent2()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            HashSet<string> tester = new HashSet<string>();
            tester.Add("A1");
            tester.Add("B1");
            tester.Add("C1");
            tester.Add("D1");

            Spreadsheet testee = new Spreadsheet();
            testee.SetContentsOfCell("A1", "z");
            testee.SetContentsOfCell("B1", "=2");
            testee.SetContentsOfCell("C1", "2=2");
            controller.setModel(testee);

            controller.setCellsToChange(tester);
            stub.FireUpdateRelevantEvent();
            Assert.IsTrue(stub.CalledUpdateRelevantEvent);
        }

        [TestMethod]
        public void TestSaveSpreadsheetEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            
            stub.FireSaveSpreadsheetEvent("");

        }

        [TestMethod]
        public void TestUpdateTitleTextEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);
            stub.FireUpdateTitleTextEvent("Title", new SSWindow()); ;
            Assert.IsTrue(stub.CalledUpdateTitleTextEvent);
        }

        [TestMethod]
        public void TestNewSpreadsheetEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);
            stub.FireNewSpreadsheetEvent();
            Assert.IsTrue(stub.CalledNewSpreadsheetEvent);
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void TestOpenSpreadsheetEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);
            stub.FireOpenSpreadsheetEvent("/tst.txt");
        }

        [TestMethod]
        public void TestUpdateAllNonEmptyEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);
            stub.FireUpdateAllNonEmptyEvent();
            Assert.IsTrue(stub.CalledUpdateAllNonEmptyEvent);
        }


        [TestMethod]
        public void OpenSSWithPathTest()
        {
            SSWindowStub stub = new SSWindowStub();
            string path = System.IO.Directory.GetCurrentDirectory();
            Controller controller = new Controller(stub, path
                .Remove(path.Length-10)
                +"/Test_Files/OpenTest.ss");
        }

        [TestMethod]
        public void SaveSSWithPathTest()
        {
            SSWindowStub stub = new SSWindowStub();
            string path = System.IO.Directory.GetCurrentDirectory();
            Controller controller = new Controller(stub, path
                .Remove(path.Length - 10)
                + "/Test_Files/OpenTest.ss");
            stub.FireSaveSpreadsheetEvent(path
                .Remove(path.Length - 10)
                + "/Test_Files/SaveTest.ss");
        }

        [TestMethod]
        public void UpdateRelevantCellsWithPathTest()
        {
            SSWindowStub stub = new SSWindowStub();
            string path = System.IO.Directory.GetCurrentDirectory();
            Controller controller = new Controller(stub, path
                .Remove(path.Length - 10)
                + "/Test_Files/OpenTest.ss");
            SpreadsheetPanel sp = new SpreadsheetPanel();
            stub.FireChangeCurrentEvent(sp);
            stub.FireChangeCellContentEvent("GRR");
        }

        [TestMethod]
        public void TestReplaceDoubleEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            HashSet<string> tester = new HashSet<string>();
            tester.Add("A1");
            tester.Add("B1");
            tester.Add("C1");
            tester.Add("D1");

            Spreadsheet testee = new Spreadsheet();
            testee.SetContentsOfCell("A1", "5");
            testee.SetContentsOfCell("B1", "=2");
            testee.SetContentsOfCell("C1", "2=2");
            controller.setModel(testee);
            controller.setLocation(3, 3);
            stub.FireChangeCellContentEvent("5");
            controller.setCellsToChange(tester);
            stub.FireUpdateRelevantEvent();
            stub.FireChangeCellContentEvent("ok");
            Assert.IsTrue(stub.CalledUpdateRelevantEvent);
        }

        [TestMethod]
        public void TestReplaceFormulaErrorEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            HashSet<string> tester = new HashSet<string>();
            tester.Add("A1");
            tester.Add("B1");
            tester.Add("C1");
            tester.Add("D1");

            Spreadsheet testee = new Spreadsheet();
            testee.SetContentsOfCell("C3", "=A1/0");
            testee.SetContentsOfCell("B1", "=2");
            testee.SetContentsOfCell("C1", "2=2");
            controller.setModel(testee);
            controller.setLocation(3, 3);
            stub.FireChangeCellContentEvent("= A6/0");
            controller.setCellsToChange(tester);
            stub.FireChangeCellContentEvent("= AA1/0");
            stub.FireUpdateRelevantEvent();
            Assert.IsTrue(stub.CalledUpdateRelevantEvent);
        }

        [TestMethod]
        public void TestReplaceGetExceptionEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            HashSet<string> tester = new HashSet<string>();
            tester.Add("A1");
            tester.Add("B1");
            tester.Add("C1");
            tester.Add("D1");

            Spreadsheet testee = new Spreadsheet();
            testee.SetContentsOfCell("C3", "=A1/0");
            testee.SetContentsOfCell("B1", "=2");
            testee.SetContentsOfCell("C1", "2=2");
            controller.setModel(testee);
            controller.setLocation(3, 3);
            stub.FireChangeCellContentEvent("= A6/0");
            controller.setCellsToChange(tester);
            stub.FireChangeCellContentEvent("= +++++AA1/0");
            stub.FireUpdateRelevantEvent();
            Assert.IsTrue(stub.CalledUpdateRelevantEvent);
        }
    }
}

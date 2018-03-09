using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetGUI;
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
            Assert.IsTrue(stub.CalledSaveSpreadsheetEvent);

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
        public void TestOpenSpreadsheetEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);
            stub.FireOpenSpreadsheetEvent("");
            Assert.IsTrue(stub.CalledOpenSpreadsheetEvent);
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
        public void DoCloseTest()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);
            stub.DoClose();
            Assert.IsTrue(stub.CalledDoClose);
        }
    }
}

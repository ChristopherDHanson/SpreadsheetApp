using System;
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
        public void TestRetrieveEditBoxValueEvent2()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

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
        public void TestChangeCurrentEvent2()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

        }

        [TestMethod]
        public void TestMoveLeftEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireMoveLeftEvent(new SpreadsheetPanel());
            Assert.IsTrue(stub.CalledRetrieveEditBoxValueEvent);

        }

        [TestMethod]
        public void TestMoveRightEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireMoveRightEvent(new SpreadsheetPanel());
            Assert.IsTrue(stub.CalledRetrieveEditBoxValueEvent);

        }

        [TestMethod]
        public void TestMoveUpEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireMoveUpEvent(new SpreadsheetPanel());
            Assert.IsTrue(stub.CalledRetrieveEditBoxValueEvent);

        }

        [TestMethod]
        public void TestMoveDownEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireMoveDownEvent(new SpreadsheetPanel());
            Assert.IsTrue(stub.CalledRetrieveEditBoxValueEvent);

            

        }

        [TestMethod]
        public void TestChangeCellContentEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireChangeCellContentEvent("");
            Assert.IsTrue(stub.CalledRetrieveEditBoxValueEvent);

        }

        [TestMethod]
        public void TestChangeCellContentEvent2()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

        }

        [TestMethod]
        public void TestUpdateRelevantEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireUpdateRelevantEvent();
            Assert.IsTrue(stub.CalledRetrieveEditBoxValueEvent);

        }

        [TestMethod]
        public void TestUpdateRelevantEvent2()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

        }

        [TestMethod]
        public void TestSaveSpreadsheetEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireSaveSpreadsheetEvent("");
            Assert.IsTrue(stub.CalledRetrieveEditBoxValueEvent);

        }

        [TestMethod]
        public void TestSaveSpreadsheetEvent2()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

        }

        [TestMethod]
        public void TestUpdateTitleTextEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);


        }

        [TestMethod]
        public void TestNewSpreadsheetEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

        }

        [TestMethod]
        public void TestNewSpreadsheetEvent2()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

        }

        [TestMethod]
        public void TestOpenSpreadsheetEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

        }

        [TestMethod]
        public void TestOpenSpreadsheetEvent2()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

        }

        [TestMethod]
        public void TestUpdateAllNonEmptyEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

        }

        [TestMethod]
        public void TestUpdateAllNonEmptyEvent2()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

        }
    }
}

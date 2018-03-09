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
        public void TestChangeCellContentEvent2()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestUpdateRelevantEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireUpdateRelevantEvent();
        }

        [TestMethod]
        public void TestUpdateRelevantEvent2()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSaveSpreadsheetEvent()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);

            stub.FireSaveSpreadsheetEvent("");

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
        public void DoCloseTest()
        {
            SSWindowStub stub = new SSWindowStub();
            Controller controller = new Controller(stub);
            stub.DoClose();
            Assert.IsTrue(stub.CalledDoClose);
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
    }
}

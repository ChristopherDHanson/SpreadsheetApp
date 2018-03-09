using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetGUI;
using SSGui;

namespace ControllerTester
{    
    
    public class SSWindowStub : ISSWindowView
    {

        public event Action<SpreadsheetPanel> ChangeCurrentEvent;
        public event Action<TextBox> RetrieveEditBoxValueEvent;
        public event Action<SpreadsheetPanel> MoveLeftEvent;
        public event Action<SpreadsheetPanel> MoveRightEvent;
        public event Action<SpreadsheetPanel> MoveUpEvent;
        public event Action<SpreadsheetPanel> MoveDownEvent;
        public event Action<string> ChangeCellContentEvent;
        public event Action UpdateRelevantEvent;
        public event Action<string> SaveSpreadsheetEvent;
        public event Action<string, SSWindow> UpdateTitleTextEvent;
        public event Action NewSpreadsheetEvent;
        public event Action<string> OpenSpreadsheetEvent;
        public event Action UpdateAllNonEmptyEvent;

        public bool CalledChangeCurrent { get;  set; }
        private void DoChangeCurrent(){CalledChangeCurrent = true;}
        public bool CalledRetrieveEditBoxValueEvent { get;  set; }
        private void DoRetrieveEditBoxValueEvent() { CalledRetrieveEditBoxValueEvent = true; }
        public bool CalledMoveLeftEvent { get;  set; }
        private void DoMoveLeftEvent() { CalledMoveLeftEvent = true; }
        public bool CalledMoveRightEvent { get;  set; }
        private void DoMoveRightEvent() { CalledMoveRightEvent = true; }
        public bool CalledMoveUpEvent { get;  set; }
        private void DoMoveUpEvent() { CalledMoveUpEvent = true; }
        public bool CalledMoveDownEvent { get;  set; }
        private void DoMoveDownEvent() { CalledMoveDownEvent = true; }
        public bool CalledChangeCellContentEvent { get;  set; }
        private void DoChangeCellContentEvent() { CalledChangeCellContentEvent = true; }
        public bool CalledUpdateRelevantEvent { get;  set; }
        private void DoUpdateRelevantEvent() { CalledUpdateRelevantEvent = true; }
        public bool CalledSaveSpreadsheetEvent { get;  set; }
        private void DoSaveSpreadsheetEvent() { CalledSaveSpreadsheetEvent = true; }
        public bool CalledUpdateTitleTextEvent { get;  set; }
        private void DoUpdateTitleTextEvent() { CalledUpdateTitleTextEvent = true; }
        public bool CalledNewSpreadsheetEvent { get;  set; }
        private void DoNewSpreadsheetEvent() { CalledNewSpreadsheetEvent = true; }
        public bool CalledOpenSpreadsheetEvent { get;  set; }
        private void DoOpenSpreadsheetEvent() { CalledOpenSpreadsheetEvent = true; }
        public bool CalledUpdateAllNonEmptyEvent { get;  set; }
        private void DoUpdateAllNonEmptyEvent() { CalledUpdateAllNonEmptyEvent = true; }

        public string TitleTextVal { set; get; }

        //Next fourteen methods fire above events
        public void FireChangeCurrentEvent(SpreadsheetPanel testPanel)
        {
            if (ChangeCurrentEvent != null)
            {
                DoChangeCurrent();
                ChangeCurrentEvent(testPanel);
            }
        }

        public void FireRetrieveEditBoxValueEvent(TextBox textBox)
        {
            if (RetrieveEditBoxValueEvent != null)
            {
                DoRetrieveEditBoxValueEvent();
                RetrieveEditBoxValueEvent(textBox);
            }
        }

        public void FireMoveLeftEvent(SpreadsheetPanel testPanel)
        {
            if (MoveLeftEvent != null)
            {
                DoMoveLeftEvent();
                MoveLeftEvent(testPanel);
            }
        }

        public void FireMoveRightEvent(SpreadsheetPanel testPanel)
        {
            if (MoveRightEvent != null)
            {
                DoMoveRightEvent();
                MoveRightEvent(testPanel);
            }
        }

        public void FireMoveDownEvent(SpreadsheetPanel testPanel)
        {
            if (MoveDownEvent != null)
            {
                DoMoveDownEvent();
                MoveDownEvent(testPanel);
            }
        }

        public void FireMoveUpEvent(SpreadsheetPanel testPanel)
        {
            if (MoveUpEvent != null)
            {
                DoMoveUpEvent();
                MoveUpEvent(testPanel);
            }
        }

        public void FireChangeCellContentEvent(string testString)
        {
            if (ChangeCellContentEvent != null)
            {
                DoChangeCellContentEvent();
                ChangeCellContentEvent(testString);
            }
        }

        public void FireUpdateRelevantEvent()
        {
            if (UpdateRelevantEvent != null)
            {
                DoUpdateRelevantEvent();
                UpdateRelevantEvent();
            }
        }

        public void FireSaveSpreadsheetEvent(string saveSheet)
        {
            if (SaveSpreadsheetEvent != null)
            {
                DoSaveSpreadsheetEvent();
                SaveSpreadsheetEvent(saveSheet);
            }
        }

        public void FireUpdateTitleTextEvent(string titleName, SSWindow testWindow)
        {
            if (UpdateTitleTextEvent != null)
            {
                DoUpdateTitleTextEvent();
                UpdateTitleTextEvent(titleName, testWindow);
            }
        }

        public void FireNewSpreadsheetEvent()
        {
            if (NewSpreadsheetEvent != null)
            {
                DoNewSpreadsheetEvent();
                NewSpreadsheetEvent();
            }
        }

        public void FireOpenSpreadsheetEvent(string newFile)
        {
            if (OpenSpreadsheetEvent != null)
            {
                DoOpenSpreadsheetEvent();
                OpenSpreadsheetEvent(newFile);
            }
        }
        public void FireUpdateAllNonEmptyEvent()
        {
            if (UpdateAllNonEmptyEvent != null)
            {
                DoUpdateAllNonEmptyEvent();
                UpdateAllNonEmptyEvent();
            }
        }

        public string CellNameBoxVal { get; set; }
        public string CellValueBoxVal { get; set; }

        //Records if DoClose and OpenNew are called
        public void DoClose()
        {
            CalledDoClose = true;
        }

        public void OpenNew()
        {
            CalledOpenNew = true;
        }

        public bool CalledDoClose
        {
            get; private set;
        }

        public bool CalledOpenNew
        {
            get; private set;
        }
    }
}


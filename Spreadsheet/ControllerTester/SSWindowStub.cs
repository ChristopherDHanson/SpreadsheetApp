using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
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
        public event Action LoadEvent;
        public event Action NewSpreadsheetEvent;
        public event Action<string> OpenSpreadsheetEvent;
        public event Action UpdateAllNonEmptyEvent;

        //Next fourteen methods fire above events
        public void FireChangeCurrentEvent(SpreadsheetPanel testPanel)
        {
            if (ChangeCurrentEvent != null)
            {
                ChangeCurrentEvent(testPanel);
            }
        }

        public void FireRetrieveEditBoxValueEvent(TextBox textBox)
        {
            if (RetrieveEditBoxValueEvent != null)
            {
                RetrieveEditBoxValueEvent(textBox);
            }
        }

        public void FireMoveLeftEvent(SpreadsheetPanel testPanel)
        {
            if (MoveLeftEvent != null)
            {
                MoveLeftEvent(testPanel);
            }
        }

        public void FireMoveRightEvent(SpreadsheetPanel testPanel)
        {
            if (MoveRightEvent != null)
            {
                MoveRightEvent(testPanel);
            }
        }

        public void FireMoveDownEvent(SpreadsheetPanel testPanel)
        {
            if (MoveDownEvent != null)
            {
                MoveDownEvent(testPanel);
            }
        }

        public void FireMoveUpEvent(SpreadsheetPanel testPanel)
        {
            if (MoveUpEvent != null)
            {
                MoveUpEvent(testPanel);
            }
        }

        public void FireChangeCellContentEvent(string testString)
        {
            if (ChangeCellContentEvent != null)
            {
                ChangeCellContentEvent(testString);
            }
        }

        public void FireUpdateRelevantEvent()
        {
            if (UpdateRelevantEvent != null)
            {
                UpdateRelevantEvent();
            }
        }

        public void FireSaveSpreadsheetEvent(string saveSheet)
        {
            if (SaveSpreadsheetEvent != null)
            {
                SaveSpreadsheetEvent(saveSheet);
            }
        }

        public void FireUpdateTitleTextEvent(string titleName, SSWindow testWindow)
        {
            if (UpdateTitleTextEvent != null)
            {
                UpdateTitleTextEvent(titleName, testWindow);
            }
        }

        public void FireLoadEvent()
        {
            if (LoadEvent != null)
            {
                LoadEvent();
            }
        }

        public void FireNewSpreadsheetEvent()
        {
            if (NewSpreadsheetEvent != null)
            {
                NewSpreadsheetEvent();
            }
        }

        public void FireOpenSpreadsheetEvent(string newFile)
        {
            if (OpenSpreadsheetEvent != null)
            {
                OpenSpreadsheetEvent(newFile);
            }
        }
        public void FireUpdateAllNonEmptyEvent()
        {
            if (UpdateAllNonEmptyEvent != null)
            {
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


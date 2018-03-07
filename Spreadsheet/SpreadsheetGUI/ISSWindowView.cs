    using SSGui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public interface ISSWindowView
    {
        event Action<SpreadsheetPanel> ChangeCurrentEvent;
        event Action<string> ChangeCellContentEvent;
        event Action<TextBox> RetrieveEditBoxValueEvent;
        event Action UpdateRelevantEvent;
        event Action<string> SaveSpreadsheetEvent;
        event Action<SpreadsheetPanel> MoveLeftEvent;
        event Action<SpreadsheetPanel> MoveRightEvent;
        event Action<SpreadsheetPanel> MoveUpEvent;
        event Action<SpreadsheetPanel> MoveDownEvent;
        event Action LoadEvent;
        event Action NewSpreadsheetEvent;
        event Action<string> OpenSpreadsheetEvent;
        event Action CloseEvent;

        string CellNameBoxVal { set; }
        string CellValueBoxVal { set; }

        void DoClose();

        void OpenNew();
    }
}

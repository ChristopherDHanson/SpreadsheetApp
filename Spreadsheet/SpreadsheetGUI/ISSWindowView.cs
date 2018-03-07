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
        event Action SaveEvent;
        event Action LoadEvent;
        event Action NewEvent;
        event Action OpenEvent;
        event Action CloseEvent;
        event Action<SpreadsheetPanel> SelectUp;
        event Action<SpreadsheetPanel> SelectDown;
        event Action<SpreadsheetPanel> SelectRight;
        event Action<SpreadsheetPanel> SelectLeft;

        string CellNameBoxVal { set; }
        string CellValueBoxVal { set; }


        void DoClose();

        void OpenNew();

    }

}

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
        event Action SaveEvent;
        event Action DirectionPressEvent;
        event Action LoadEvent;
        event Action NewEvent;
        event Action OpenEvent;
        event Action CloseEvent;

        void DoClose();

        void OpenNew();
    }
}

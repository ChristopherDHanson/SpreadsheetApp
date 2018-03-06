using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    class SSWindowApplicationContext : ApplicationContext
    {
        // Number of open forms
        private int windowCount = 0;

        // Singleton ApplicationContext
        private static SSWindowApplicationContext context;

        /// <summary>
        /// Private constructor for singleton pattern
        /// </summary>
        private SSWindowApplicationContext()
        {
        }

        /// <summary>
        /// Returns the one DemoApplicationContext.
        /// </summary>
        public static SSWindowApplicationContext GetContext()
        {
            if (context == null)
            {
                context = new SSWindowApplicationContext();
            }
            return context;
        }

        /// <summary>
        /// Runs a form in this application context
        /// </summary>
        public void RunNew()
        {
            // Create the window and the controller
            SSWindow window = new SSWindow();
            new Controller(window);

            // One more form is running
            windowCount++;

            // When this form closes, we want to find out
            window.FormClosed += (o, e) => { if (--windowCount <= 0) ExitThread(); };

            // Run the form
            window.Show();
        }
    }
}

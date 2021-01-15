using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proton
{
    public class ProtonEssentials
    {
        Form parent;
        public ProtonEssentials(Form form)
        {
            parent = form;
        }
        public void close()
        {
            parent.Close();
        }

        public void minimize()
        {
            parent.WindowState = FormWindowState.Minimized;
        }
    }
}

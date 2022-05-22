using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.IViews;

namespace UI.WinFormsViews
{
    public class InfoViewWinForm : IInfoView
    {
        public void Close()
        {
            
        }

        public void ShowInfo( string message)
        {
            //MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

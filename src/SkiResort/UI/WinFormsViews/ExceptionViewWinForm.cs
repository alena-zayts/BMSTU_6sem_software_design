using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.IViews;

namespace UI.WinFormsViews
{
    public class ExceptionViewWinForm : IExceptionView
    {
        public void ShowException(Exception exception, string message = "Призошла ошибка")
        {
            MessageBox.Show(exception.ToString(), message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

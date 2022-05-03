using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationComponent
{
    public class ExceptionView : IExceptionView
    {
        public void ShowException(Exception exception, string message)
        {
            MessageBox.Show(exception.ToString(), message, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

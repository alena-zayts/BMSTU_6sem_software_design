using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.IViews
{
    public interface IExceptionView
    {
        public void ShowException(Exception exception, string message="Призошла ошибка");
    }
}

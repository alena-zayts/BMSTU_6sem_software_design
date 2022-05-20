using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.IViews;

namespace TechUI.TechViews
{
    public class ExceptionViewTech : IExceptionView
    {
        public void ShowException(Exception exception, string message)
        {
            Console.WriteLine("!!!!Ошибка");
            Console.WriteLine(message + "\n" + exception.Message);
        }
    }
}

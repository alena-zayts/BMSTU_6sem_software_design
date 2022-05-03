using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationComponent
{
    public interface IExceptionView
    {
        public void ShowException(Exception exception, string message);
    }
}

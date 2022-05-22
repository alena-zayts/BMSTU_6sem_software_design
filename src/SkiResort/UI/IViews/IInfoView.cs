using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.IViews
{
    public interface IInfoView
    {
        public void ShowInfo(string message);
        public void Close();
    }
}

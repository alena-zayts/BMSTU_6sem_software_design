using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UI.IViews;

namespace UI
{
    public interface IViewsFactory
    {
        public IExceptionView CreateExceptionView();

        public IMainView CreateMainView();
        public IProfileView CreateProfileView();
    }
}

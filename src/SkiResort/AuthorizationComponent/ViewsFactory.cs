using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UI.IViews;
using UI.WinFormsViews;

namespace UI
{
    public class ViewsFactory : IViewsFactory
    {
        public IExceptionView CreateExceptionView()
        {
            return new ExceptionView();
        }

        public IMainView CreateMainView()
        {
            return new MainView();
        }

        public IProfileView CreateProfileView()
        {
            return new ProfileView();
        }

        public ISlopeView CreateSlopeView()
        {
            return new SlopeView();
        }
    }
}

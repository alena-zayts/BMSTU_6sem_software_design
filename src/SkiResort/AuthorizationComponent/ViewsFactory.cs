using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationComponent
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
    }
}

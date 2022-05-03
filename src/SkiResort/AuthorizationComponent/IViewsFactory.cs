using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationComponent
{
    public interface IViewsFactory
    {
        public IExceptionView CreateExceptionView();

        public IMainView CreateMainView();
        public IProfileView CreateProfileView();
    }
}

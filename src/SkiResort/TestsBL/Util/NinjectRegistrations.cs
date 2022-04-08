using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;

using BL.IRepositories;
using TestsBL.RepositoriesFake;

using ComponentBL;
using TestsBL;

namespace TestsBL.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositoriesFactory>().To<FakeRepositoriesFactory>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.IViews;
using UI.TechViews;

namespace UI
{
    public class TechViewsFactory : IViewsFactory
    {
        public ICardReadingView CreateCardReadingView()
        {
            throw new NotImplementedException();
        }

        public ICardView CreateCardView()
        {
            throw new NotImplementedException();
        }

        public IExceptionView CreateExceptionView()
        {
            throw new NotImplementedException();
        }

        public ILiftView CreateLiftView()
        {
            throw new NotImplementedException();
        }

        public IMainView CreateMainView()
        {
            return new MainViewTech();
        }

        public IMessageView CreateMessageView()
        {
            throw new NotImplementedException();
        }

        public IProfileView CreateProfileView()
        {
            throw new NotImplementedException();
        }

        public ISlopeView CreateSlopeView()
        {
            throw new NotImplementedException();
        }

        public ITurnstileView CreateTurnstileView()
        {
            throw new NotImplementedException();
        }

        public IUserView CreateUserView()
        {
            throw new NotImplementedException();
        }
    }
}

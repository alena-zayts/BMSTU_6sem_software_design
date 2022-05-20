using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.IViews;
using TechUI.TechViews;
using UI;

namespace TechUI
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
            return new ExceptionViewTech();
        }

        public ILiftView CreateLiftView()
        {
           return new LiftViewTech();
        }

        public IMainView CreateMainView()
        {
            return new MainViewTech();
        }

        public IMessageView CreateMessageView()
        {
            return new MessageViewTech();
        }

        public IProfileView CreateProfileView()
        {
            return new ProfileViewTech();
        }

        public ISlopeView CreateSlopeView()
        {
            return new SlopeViewTech();
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

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
        public ICardReadingView CreateCardReadingView()
        {
            return new CardReadingViewWinForm();
        }

        public ICardView CreateCardView()
        {
            return new CardViewWinForm();
        }

        public IExceptionView CreateExceptionView()
        {
            return new ExceptionViewWinForm();
        }

        public ILiftView CreateLiftView()
        {
            return new LiftViewWinForm();
        }

        public IMainView CreateMainView()
        {
            return new MainViewWinForm();
        }

        public IMessageView CreateMessageView()
        {
            return new MessageViewWinForm();
        }

        public IProfileView CreateProfileView()
        {
            return new ProfileViewWinForm();
        }

        public ISlopeView CreateSlopeView()
        {
            return new SlopeViewWinForm();
        }

        public ITurnstileView CreateTurnstileView()
        {
            return new TurnstileViewWinForm();
        }

        public IUserView CreateUserView()
        {
            return new UserViewWinForm();
        }
    }
}

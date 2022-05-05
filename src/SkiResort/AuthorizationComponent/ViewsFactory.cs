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
            throw new NotImplementedException();
        }

        public IExceptionView CreateExceptionView()
        {
            return new ExceptionView();
        }

        public ILiftView CreateLiftView()
        {
            throw new NotImplementedException();
        }

        public IMainView CreateMainView()
        {
            return new MainViewWinForm();
        }

        public IMessageView CreateMessageView()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IUserView CreateUserView()
        {
            throw new NotImplementedException();
        }
    }
}

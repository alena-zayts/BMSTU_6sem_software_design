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
        public ISlopeView CreateSlopeView();
        public ILiftView CreateLiftView();
        public IMessageView CreateMessageView();
        public IUserView CreateUserView();
        public ICardReadingView CreateCardReadingView();
        public ITurnstileView CreateTurnstileView();
        public ICardView CreateCardView();
        public IInfoView CreateInfoView();
    }
}

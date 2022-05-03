using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationComponent
{
    public interface IMainView
    {
        public bool MessageEnabled { get; set; }
        public bool UserEnabled { get; set; }
        public bool TurnstileEnabled { get; set; }
        public bool CardReadingEnabled { get; set; }

        event EventHandler ProfileClicked;
        event EventHandler LiftClicked;
        event EventHandler SlopeClicked;
        event EventHandler MessageClicked;
        event EventHandler UserClicked;
        event EventHandler TurnstileClicked;
        event EventHandler CardReadingClicked;


        void Open();

        void Close();

        void Refresh();
    }
}

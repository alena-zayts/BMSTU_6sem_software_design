using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using BL.Models;

namespace UI.IViews
{
    public interface ITurnstileView
    {
        public bool GetTurnstileEnabled { get; set; }
        public bool GetTurnstilesEnabled { get; set; }
        public bool UpdateEnabled { get; set; }
        public bool AddEnabled { get; set; }
        public bool DeleteEnabled { get; set; }
       
        public string TurnstileID { get; set; }
        public string LiftID { get; set; }
        public string IsOpen { get; set; }
        public List<Turnstile> Turnstiles { set; }


        event AsyncEventHandler GetTurnstileClicked;
        event AsyncEventHandler GetTurnstilesClicked;
        event AsyncEventHandler UpdateClicked;
        event AsyncEventHandler AddClicked;
        event AsyncEventHandler DeleteClicked;
        event EventHandler CloseClicked;


        void Open();

        void Close();

        void Refresh();
    }
}

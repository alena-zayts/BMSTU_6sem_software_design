using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using BL.Models;

namespace UI.IViews
{
    public interface ILiftView
    {
        public bool GetInfoEnabled { get; set; }
        public bool GetInfosEnabled { get; set; }
        public bool UpdateEnabled { get; set; }
        public bool AddEnabled { get; set; }
        public bool DeleteEnabled { get; set; }
        public bool AddConnectedSlopeEnabled { get; set; }
        public bool DeleteConnectedSlopeEnabled { get; set; }
        public string Name { get; set; }
        public string IsOpen { get; set; }
        public string SeatsAmount { get; set; }
        public string QueueTime { get; set; }
        public string LiftingTime { get; set; }
        public string SlopeName { get; set; }
        public List<Lift> Lifts { set; }


        event AsyncEventHandler GetInfoClicked;
        event AsyncEventHandler GetInfosClicked;
        event AsyncEventHandler UpdateClicked;
        event AsyncEventHandler AddClicked;
        event AsyncEventHandler DeleteClicked;
        event AsyncEventHandler AddConnectedSlopeClicked;
        event AsyncEventHandler DeleteConnectedSlopeClicked;

        event EventHandler CloseClicked;


        void Open();

        void Close();

        void Refresh();
    }
}

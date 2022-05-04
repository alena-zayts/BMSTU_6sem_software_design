using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using BL.Models;

namespace UI.IViews
{
    public interface ISlopeView
    {
        public bool GetInfoEnabled { get; set; }
        public bool UpdateEnabled { get; set; }
        public bool AddEnabled { get; set; }
        public bool DeleteEnabled { get; set; }
        public bool AddConnectedLiftEnabled { get; set; }
        public bool DeleteConnectedLiftEnabled { get; set; }
        public string Name { get; set; }
        public string IsOpen { get; set; }
        public string DifficultyLevel { get; set; }
        public string LiftName { get; set; }
        public List<Slope> Slopes { set; }


        event AsyncEventHandler GetInfoClicked;
        event AsyncEventHandler UpdateClicked;
        event AsyncEventHandler AddClicked;
        event AsyncEventHandler DeleteClicked;
        event AsyncEventHandler AddConnectedLiftClicked;
        event AsyncEventHandler DeleteConnectedLiftClicked;

        event EventHandler CloseClicked;


        void Open();

        void Close();

        void Refresh();
    }
}

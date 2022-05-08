using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using BL.Models;

namespace UI.IViews
{
    public interface ICardView
    {
        public bool GetCardEnabled { get; set; }
        public bool GetCardsEnabled { get; set; }
        public bool UpdateEnabled { get; set; }
        public bool AddEnabled { get; set; }
        public bool DeleteEnabled { get; set; }
       
        public string CardID { get; set; }
        public string Type { get; set; }
        public DateTimeOffset ActivationTime { get; set; }
        public List<Card> Cards { set; }


        event AsyncEventHandler GetCardClicked;
        event AsyncEventHandler GetCardsClicked;
        event AsyncEventHandler UpdateClicked;
        event AsyncEventHandler AddClicked;
        event AsyncEventHandler DeleteClicked;
        event EventHandler CloseClicked;


        void Open();

        void Close();

        void Refresh();
    }
}

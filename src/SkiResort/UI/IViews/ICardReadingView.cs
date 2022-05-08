using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using BL.Models;

namespace UI.IViews
{
    public interface ICardReadingView
    {
        public bool GetCardReadingEnabled { get; set; }
        public bool GetCardReadingsEnabled { get; set; }
        public bool UpdateEnabled { get; set; }
        public bool AddEnabled { get; set; }
        public bool DeleteEnabled { get; set; }
       
        public string RecordID { get; set; }
        public string TurnstileID { get; set; }
        public string CardID { get; set; }
        public DateTimeOffset ReadingTime { get; set; }
        public List<CardReading> CardReadings { set; }


        event AsyncEventHandler GetCardReadingClicked;
        event AsyncEventHandler GetCardReadingsClicked;
        event AsyncEventHandler UpdateClicked;
        event AsyncEventHandler AddClicked;
        event AsyncEventHandler DeleteClicked;
        event EventHandler CloseClicked;


        void Open();

        void Close();

        void Refresh();
    }
}

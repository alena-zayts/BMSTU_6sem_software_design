using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using BL.Models;

namespace UI.IViews
{
    public interface IMessageView
    {
        public bool GetMessageEnabled { get; set; }
        public bool GetMessagesEnabled { get; set; }
        public bool MarkCheckedEnabled { get; set; }
        public bool SendEnabled { get; set; }
        public bool UpdateEnabled { get; set; }
        public bool DeleteEnabled { get; set; }
        public string MessageID { get; set; }
        public string MessageText { get; set; }
        public string SenderID { get; set; }
        public string CheckedByID { get; set; }
        public List<BL.Models.Message> Messages { set; }


        event AsyncEventHandler GetMessageClicked;
        event AsyncEventHandler GetMessagesClicked;
        event AsyncEventHandler MarkCheckedClicked;
        event AsyncEventHandler SendClicked;
        event AsyncEventHandler DeleteClicked;
        event AsyncEventHandler UpdateClicked;

        event EventHandler CloseClicked;


        void Open();

        void Close();

        void Refresh();
    }
}

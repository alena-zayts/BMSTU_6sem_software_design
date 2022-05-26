using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using BL.Models;

namespace UI.IViews
{
    public interface IUserView
    {
        public bool GetUserEnabled { get; set; }
        public bool GetUsersEnabled { get; set; }
        public bool UpdateEnabled { get; set; }
        public bool AddEnabled { get; set; }
        public bool DeleteEnabled { get; set; }
       
        public string UserID { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Permissions { get; set; }
        public string CardID { get; set; }
        public List<User> Users { set; }


        event AsyncEventHandler GetUserClicked;
        event AsyncEventHandler GetUsersClicked;
        event AsyncEventHandler UpdateClicked;
        event AsyncEventHandler AddClicked;
        event AsyncEventHandler DeleteClicked;
        event EventHandler CloseClicked;


        void Open();

        void Close();

        void Refresh();
    }
}

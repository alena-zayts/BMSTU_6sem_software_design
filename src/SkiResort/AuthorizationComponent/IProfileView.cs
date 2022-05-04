using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;

namespace AuthorizationComponent
{
    public interface IProfileView
    {
        public bool LogInEnabled { get; set; }
        public bool LogOutEnabled { get; set; }
        public bool RegisterEnabled { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string cardID { get; set; }


        event AsyncEventHandler LogInClicked;
        event AsyncEventHandler LogOutClicked;
        event AsyncEventHandler RegisterClicked;
        event EventHandler CloseClicked;


        void Open();

        void Close();

        void Refresh();
    }
}

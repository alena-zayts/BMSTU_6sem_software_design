using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.Threading;

using UI.IViews;

namespace UI.WinFormsViews
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
        }

        public bool MessageEnabled { 
            get { return messageButton.Enabled;}
            set { messageButton.Enabled = value;}
        }
        public bool UserEnabled {
            get { return userButton.Enabled;}
            set { userButton.Enabled = value; }
        }
        public bool TurnstileEnabled {
            get { return turnstileButton.Enabled;}
            set { turnstileButton.Enabled = value; }
        }
        public bool CardReadingEnabled {
            get { return cardReadingButton.Enabled; }
            set { cardReadingButton.Enabled = value; }
        }
        public event EventHandler ProfileClicked;
        public event EventHandler LiftClicked;
        public event EventHandler SlopeClicked;
        public event EventHandler MessageClicked;
        public event EventHandler UserClicked;
        public event EventHandler TurnstileClicked;
        public event EventHandler CardReadingClicked;
        public event AsyncEventHandler CloseClicked;

        public void Open()
        {
            base.ShowDialog();
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            ProfileClicked?.Invoke(this, new EventArgs());
        }

        private void slopeButton_Click(object sender, EventArgs e)
        {
            SlopeClicked?.Invoke(this, new EventArgs());
        }

        private void liftButton_Click(object sender, EventArgs e)
        {
            LiftClicked?.Invoke(this, new EventArgs());
        }

        private void messageButton_Click(object sender, EventArgs e)
        {
            MessageClicked?.Invoke(this, new EventArgs());
        }

        private void turnstileButton_Click(object sender, EventArgs e)
        {
            TurnstileClicked?.Invoke(this, new EventArgs());
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            UserClicked?.Invoke(this, new EventArgs());
        }

        private void ccardReadingButton_Click(object sender, EventArgs e)
        {
            CardReadingClicked?.Invoke(this, new EventArgs());
        }

        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseClicked?.Invoke(this, new EventArgs());
        }
    }
}

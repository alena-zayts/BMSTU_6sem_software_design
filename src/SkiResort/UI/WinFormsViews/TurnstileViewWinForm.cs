using BL.Models;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.IViews;

namespace UI.WinFormsViews
{
    public partial class TurnstileViewWinForm : Form, ITurnstileView
    {
        public bool GetTurnstileEnabled
        {
            get { return GetTurnstileButton.Enabled; }
            set { GetTurnstileButton.Enabled = value; }
        }
        public bool GetTurnstilesEnabled
        {
            get { return GetTurnstilesButton.Enabled; }
            set { GetTurnstilesButton.Enabled = value; }
        }

        public bool UpdateEnabled
        {
            get { return UpdateButton.Enabled; }
            set { UpdateButton.Enabled = value; }
        }
        public bool AddEnabled
        {
            get { return AddButton.Enabled; }
            set { AddButton.Enabled = value; }
        }
        public bool DeleteEnabled
        {
            get { return DeleteButton.Enabled; }
            set { DeleteButton.Enabled = value; }
        }
        public string TurnstileID
        {
            get { return TurnstileIDTextBox.Text; }
            set { TurnstileIDTextBox.Text = value; }
        }
        public string LiftID
        {
            get { return LiftIDTextBox.Text; }
            set { LiftIDTextBox.Text = value; }
        }
        public string IsOpen
        {
            get { return IsOpenTextBox.Text; }
            set { IsOpenTextBox.Text = value; }
        }
        public List<Turnstile> Turnstiles
        {
            set
            {
                TurnstilesDataGridView.Rows.Clear();
                foreach (Turnstile turnstile in value)
                {
                    string[] row = new string[3];
                    row[0] = turnstile.TurnstileID.ToString();
                    row[1] = turnstile.LiftID.ToString();
                    row[2] = turnstile.IsOpen.ToString();

                    TurnstilesDataGridView.Rows.Add(row);
                }
            }
        }

        public TurnstileViewWinForm()
        {
            InitializeComponent();
        }

        public event AsyncEventHandler GetTurnstileClicked;
        public event AsyncEventHandler GetTurnstilesClicked;
        public event AsyncEventHandler UpdateClicked;
        public event AsyncEventHandler AddClicked;
        public event AsyncEventHandler DeleteClicked;
        public event EventHandler CloseClicked;

        private void GetTurnstilesButton_Click(object sender, EventArgs e)
        {
            GetTurnstilesClicked?.Invoke(this, new EventArgs());
        }

        private void GetTurnstileButton_Click(object sender, EventArgs e)
        {
            GetTurnstileClicked?.Invoke(this, new EventArgs());
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateClicked?.Invoke(this, new EventArgs());
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddClicked?.Invoke(this, new EventArgs());
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, new EventArgs());
        }

        public void Open()
        {
            Show();
        }

        private void TurnstileViewWinForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseClicked?.Invoke(this, new EventArgs());
        }
    }
}

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
    public partial class LiftViewWinForm : Form, ILiftView
    {
        public LiftViewWinForm()
        {
            InitializeComponent();
        }

        public bool GetInfoEnabled
        {
            get { return GetInfoButton.Enabled; }
            set { GetInfoButton.Enabled = value; }
        }
        public bool GetInfosEnabled
        {
            get { return GetInfosButton.Enabled; }
            set { GetInfosButton.Enabled = value; }
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
        public bool AddConnectedSlopeEnabled
        {
            get { return AddConnectedSlopeButton.Enabled; }
            set { AddConnectedSlopeButton.Enabled = value; }
        }
        public bool DeleteConnectedSlopeEnabled
        {
            get { return DeleteConnectedSlopeButton.Enabled; }
            set { DeleteConnectedSlopeButton.Enabled = value; }
        }
        public string IsOpen
        {
            get { return IsOpenTextBox.Text; }
            set { IsOpenTextBox.Text = value; }
        }
        public string Name
        {
            get { return NameTextBox.Text; }
            set { NameTextBox.Text = value; }
        }
        public string SeatsAmount
        {
            get { return SeatsAmountTextBox.Text; }
            set { SeatsAmountTextBox.Text = value; }
        }
        public string QueueTime 
        {
            set { QueueTimeTextBox.Text = value; }
        }        
        public string LiftingTime
        {
            get { return LiftingTimeTextBox.Text; }
            set { LiftingTimeTextBox.Text = value; }
        }
        public string SlopeName
        {
            get { return SlopeNameTextBox.Text; }
            set { SlopeNameTextBox.Text = value; }
        }
        public List<Lift> Lifts
        {
            set
            {
                LiftsDataGridView.Rows.Clear();
                foreach (Lift lift in value)
                {
                    string[] row = new string[7];
                    row[0] = lift.LiftID.ToString();
                    row[1] = lift.LiftName.ToString();
                    row[2] = lift.IsOpen.ToString();
                    row[3] = lift.SeatsAmount.ToString();
                    row[4] = lift.LiftingTime.ToString();
                    row[5] = lift.QueueTime.ToString();


                    if (lift.ConnectedSlopes != null)
                    {
                        string[] connectedSlopesNames = new string[lift.ConnectedSlopes.Count];
                        for (int i = 0; i < lift.ConnectedSlopes.Count; i++)
                        {
                            Slope slope = lift.ConnectedSlopes[i];
                            connectedSlopesNames[i] = slope.SlopeName;
                        }
                        row[6] = string.Join(", ", connectedSlopesNames);
                    }
                    else
                    {
                        row[6] = "";
                    }

                    LiftsDataGridView.Rows.Add(row);
                }
            }
        }

        public event AsyncEventHandler GetInfoClicked;
        public event AsyncEventHandler GetInfosClicked;
        public event AsyncEventHandler UpdateClicked;
        public event AsyncEventHandler AddClicked;
        public event AsyncEventHandler DeleteClicked;
        public event AsyncEventHandler AddConnectedSlopeClicked;
        public event AsyncEventHandler DeleteConnectedSlopeClicked;
        public event EventHandler CloseClicked;

        public void Open()
        {
            base.ShowDialog();
        }

        private void GetInfoButton_Click(object sender, EventArgs e)
        {
            GetInfoClicked?.Invoke(this, new EventArgs());
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

        private void AddConnectedSlopeButton_Click(object sender, EventArgs e)
        {
            AddConnectedSlopeClicked?.Invoke(this, new EventArgs());
        }

        private void DeleteConnectedSlopeButton_Click(object sender, EventArgs e)
        {
            DeleteConnectedSlopeClicked?.Invoke(this, new EventArgs());
        }

        private void GetInfosButton_Click(object sender, EventArgs e)
        {
            GetInfosClicked?.Invoke(this, new EventArgs());
        }

        private void LiftViewWinForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseClicked?.Invoke(this, new EventArgs());
        }
    }
}

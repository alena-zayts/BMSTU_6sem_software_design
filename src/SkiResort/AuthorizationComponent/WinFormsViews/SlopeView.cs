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
    public partial class SlopeView : Form, ISlopeView
    {
        public SlopeView()
        {
            InitializeComponent();
        }

        public bool GetInfoEnabled
        {
            get { return GetInfoButton.Enabled; }
            set { GetInfoButton.Enabled = value; }
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
        public bool AddConnectedLiftEnabled
        {
            get { return AddConnectedLiftButton.Enabled; }
            set { AddConnectedLiftButton.Enabled = value; }
        }
        public bool DeleteConnectedLiftEnabled
        {
            get { return DeleteConnectedLiftButton.Enabled; }
            set { DeleteConnectedLiftButton.Enabled = value; }
        }
        public string IsOpen
        {
            get { return IsOpenTextBox.Text; }
            set { IsOpenTextBox.Text = value; }
        }
        public string DifficultyLevel
        {
            get { return DifficultyLevelTextBox.Text; }
            set { DifficultyLevelTextBox.Text = value; }
        }
        public string LiftName
        {
            get { return LiftNameTextBox.Text; }
            set { LiftNameTextBox.Text = value; }
        }

        public new string Name
        {
            get { return NameTextBox.Text; }
            set { NameTextBox.Text = value; }
        }

        public List<Slope> Slopes 
        { set
            {
                foreach(Slope slope in value)
                {
                    string[] row = new string[5];
                    row[0] = slope.SlopeID.ToString();
                    row[1] = slope.SlopeName.ToString();
                    row[2] = slope.IsOpen.ToString();
                    row[3] = slope.DifficultyLevel.ToString();

                    string[] connectedLiftNames = new string[0];

                    foreach(Lift lift in slope.ConnectedLifts!)
                    {
                        connectedLiftNames.Append(lift.LiftName);
                    }
                    row[4] = string.Join(" ,", connectedLiftNames);

                    SlopesDataGridView.Rows.Add(row);
                }
            }
        }

        public event AsyncEventHandler GetInfoClicked;
        public event AsyncEventHandler UpdateClicked;
        public event AsyncEventHandler AddClicked;
        public event AsyncEventHandler DeleteClicked;
        public event AsyncEventHandler AddConnectedLiftClicked;
        public event AsyncEventHandler DeleteConnectedLiftClicked;
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

        private void AddConnectedLiftButton_Click(object sender, EventArgs e)
        {
            AddConnectedLiftClicked?.Invoke(this, new EventArgs());
        }

        private void DeleteConnectedLiftButton_Click(object sender, EventArgs e)
        {
            DeleteConnectedLiftClicked?.Invoke(this, new EventArgs());
        }

        private void SlopeView_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseClicked?.Invoke(this, new EventArgs());
        }
    }
}

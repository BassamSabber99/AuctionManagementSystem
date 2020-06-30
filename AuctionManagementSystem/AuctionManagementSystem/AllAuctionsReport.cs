using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using CrystalDecisions.Shared;


namespace AuctionManagementSystem
{
    public partial class AllAuctionsReport : Form
    {
        ReportAllAuction report1;
        public AllAuctionsReport()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AllAuctions allAuction = new AllAuctions();
            allAuction.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
         
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void AllAuctionsReport_Load(object sender, EventArgs e)
        {
           report1 = new ReportAllAuction();
            foreach(ParameterDiscreteValue v in report1.ParameterFields[0].DefaultValues)
            {
                stateCombobox.Items.Add(v.Value);
            }

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void genebtn_Click(object sender, EventArgs e)
        {
            report1.SetParameterValue(0,stateCombobox.Text);
            crystalReportViewer1.ReportSource = report1;
        }
    }
}

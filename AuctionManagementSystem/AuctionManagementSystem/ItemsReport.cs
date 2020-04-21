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
    public partial class ItemsReport : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        ReportShowAllItems cr;
        public ItemsReport()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
            con.Dispose();
            con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            SearchForItems sfi = new SearchForItems();
            sfi.Hide();
            this.Hide();
        }

        private void ItemsReport_Load(object sender, EventArgs e)
        {
            cr = new ReportShowAllItems();
            foreach(ParameterDiscreteValue v in cr.ParameterFields[0].DefaultValues)
            {
                valcombobox.Items.Add(v.Value);
            }
        }

        private void genebtn_Click(object sender, EventArgs e)
        {
            cr.SetParameterValue(0, valcombobox.Text);
            crystalReportViewer1.ReportSource = cr;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}

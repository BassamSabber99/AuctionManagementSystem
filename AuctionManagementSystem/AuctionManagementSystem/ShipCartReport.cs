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
    public partial class ShipCartReport : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        ReportShipCart cr;
        public ShipCartReport()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
            con.Dispose();
            con.Close();
        }

        private void ShipCartReport_Load(object sender, EventArgs e)
        {
            cr = new ReportShipCart();
            cr.SetParameterValue(0, GlobalID.ID);
            crystalReportViewer1.ReportSource = cr;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ShipCartReport scr = new ShipCartReport();
            this.Hide();
            scr.Show();
        }
    }
}

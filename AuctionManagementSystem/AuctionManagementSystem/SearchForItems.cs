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

namespace AuctionManagementSystem
{
    public partial class SearchForItems : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        OracleDataAdapter adapter;
        DataSet ds;
        OracleCommandBuilder builder;
        public SearchForItems()
        {
            InitializeComponent();
        }

        private void SearchForItems_Load(object sender, EventArgs e)
        {
            itmView.ReadOnly = true;
            itmView.Columns.Clear();
            string cmdstr = @"select ITEM_ID , NAME , DESCRIPTION , VALUE , SELLER_ID , CAT_NAME from items i, categories c
                            where i.CAT_ID = c.CAT_ID
                            order by ITEM_ID";
            adapter = new OracleDataAdapter(cmdstr,ordb);
            ds = new DataSet();
            adapter.Fill(ds);
            itmView.DataSource = ds.Tables[0];
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
            HomerBidder homeBidder = new HomerBidder();
            homeBidder.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            string cmdstr = @"select ITEM_ID, NAME, DESCRIPTION, VALUE, SELLER_ID, CAT_NAME from items i, categories c
                            where i.CAT_ID = c.CAT_ID
                            and name = :name
                            order by ITEM_ID";
            adapter = new OracleDataAdapter(cmdstr, ordb);
            adapter.SelectCommand.Parameters.Add("name", itmnametxt.Text.ToString());
            ds = new DataSet();
            adapter.Fill(ds);
            itmView.DataSource = ds.Tables[0];
        }

        private void itmnametxt_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void allaucbtn_Click(object sender, EventArgs e)
        {
            ItemsReport itemReport = new ItemsReport();
            this.Hide();
            itemReport.Show();
        }
    }
}

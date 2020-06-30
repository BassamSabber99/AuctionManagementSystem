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
    public partial class ItemsOperation : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        OracleDataAdapter adabter;
        DataSet ds;
        public ItemsOperation()
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
        public void ReLoad()
        {
            using (con =new OracleConnection(ordb))
            {
                itmView.ReadOnly = true;
                itmView.Columns.Clear();
                itmView.Rows.Clear();
                itmView.ColumnCount = 6;
                itmView.Columns[0].Name = "ID";
                itmView.Columns[1].Name = "Name";
                itmView.Columns[2].Name = "Description";
                itmView.Columns[3].Name = "value";
                itmView.Columns[4].Name = "Seller ID";
                itmView.Columns[5].Name = "Category Name";
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "showAllItems";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("show", OracleDbType.RefCursor, ParameterDirection.Output);
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    itmView.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
                }
                dr.Close();
            }
            
        }
        private void ItemsOperation_Load(object sender, EventArgs e)
        {
            
               ReLoad();
            using (con = new OracleConnection(ordb))
            {
                con.Open();
                allCategoryList.ClearSelected();
                OracleCommand c = new OracleCommand();
                c.Connection = con;
                c.CommandText = "select * from categories order by cat_id";
                c.CommandType = CommandType.Text;
                OracleDataReader r = c.ExecuteReader();
                allCategoryList.Items.Add("ID -  Name");
                while (r.Read())
                {
                    allCategoryList.Items.Add(r[0] + "  -  " + r[1]);
                }
                r.Close();
            }
            /////----------Disconnected Mode-------------//////
            string cmdstr = " select * from items where seller_id = " + GlobalID.ID +"order by item_id";
            adabter = new OracleDataAdapter(cmdstr,ordb);
            ds = new DataSet();
            adabter.Fill(ds);
            myItemView.DataSource = ds.Tables[0];
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomeSeller homeSeller = new HomeSeller();
            homeSeller.Show();
        }

        private void itmview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            OracleCommandBuilder builder = new OracleCommandBuilder(adabter);
            try
            {
                adabter.Update(ds.Tables[0]);
                MessageBox.Show("Changes Saves successfully!!!");
            }
            catch
            {
                MessageBox.Show("Please Check Item_ID and Your ID (Seller_ID) !!!");
                return;
            }
            
            ReLoad();

        }

        private void myitemview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

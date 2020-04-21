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
                itmview.ReadOnly = true;
                itmview.Columns.Clear();
                itmview.Rows.Clear();
                itmview.ColumnCount = 6;
                itmview.Columns[0].Name = "ID";
                itmview.Columns[1].Name = "Name";
                itmview.Columns[2].Name = "Description";
                itmview.Columns[3].Name = "value";
                itmview.Columns[4].Name = "Seller ID";
                itmview.Columns[5].Name = "Category Name";
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "showAllItems";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("show", OracleDbType.RefCursor, ParameterDirection.Output);
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    itmview.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
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
                allcategorylist.ClearSelected();
                OracleCommand c = new OracleCommand();
                c.Connection = con;
                c.CommandText = "select * from categories order by cat_id";
                c.CommandType = CommandType.Text;
                OracleDataReader r = c.ExecuteReader();
                allcategorylist.Items.Add("ID -  Name");
                while (r.Read())
                {
                    allcategorylist.Items.Add(r[0] + "  -  " + r[1]);
                }
                r.Close();
            }
            /////----------Disconnected Mode-------------//////
            string cmdstr = " select * from items where seller_id = " + GlobalID.ID +"order by item_id";
            adabter = new OracleDataAdapter(cmdstr,ordb);
            ds = new DataSet();
            adabter.Fill(ds);
            myitemview.DataSource = ds.Tables[0];
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomeSeller hs = new HomeSeller();
            hs.Show();
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

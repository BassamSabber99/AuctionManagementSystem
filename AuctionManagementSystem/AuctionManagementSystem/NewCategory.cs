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
    public partial class NewCategory : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        public NewCategory()
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            using(con = new OracleConnection(ordb))
            {
                if (string.IsNullOrWhiteSpace(catnametxt.Text.Trim().ToString()))
                {
                    MessageBox.Show("Please Enter Category Name . ");
                }
                else
                {
                    for (int i = 0; i < lcat.Items.Count; i++)
                    {
                        if (lcat.Items[i].ToString().Equals(catnametxt.Text.Trim().ToString(),StringComparison.InvariantCultureIgnoreCase))
                        {
                            MessageBox.Show("This Category Is Already Exist !!!");
                            return;
                        }
                    }
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "addCategory";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("catname", catnametxt.Text);
                    int ret = cmd.ExecuteNonQuery();
                    lcat.Items.Add(catnametxt.Text.Trim().ToString());
                    catnametxt.Focus();
                    catnametxt.Text = " ";
                    if (ret != -1)
                    {
                        MessageBox.Show("ADEDD !!!");
                    }
                }
            }
        }

        private void NewCategory_Load(object sender, EventArgs e)
        {
            using (con = new OracleConnection(ordb))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from CATEGORIES order by cat_id ";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lcat.Items.Add(dr[1]);
                }
                dr.Close();
            }
        }

        private void lcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomeSeller hs = new HomeSeller();
            hs.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

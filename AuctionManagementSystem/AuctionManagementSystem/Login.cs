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
    public partial class Login : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
            con.Close();
            con.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            using (con = new OracleConnection(ordb))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select USER_ID from users where E_MAIL =:email and PASSWORD = :pw";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("email", emailtxt.Text.Trim().ToString());
                cmd.Parameters.Add("pw", passwordtxt.Text.Trim().ToString());
                OracleDataReader dr = cmd.ExecuteReader();
                int id = 0;
                while (dr.Read())
                {
                    id = Convert.ToInt32(dr[0]);

                }
                if (id == 0)
                {
                    MessageBox.Show("Invalid E-Mail Or Password !! ");
                }
                else
                {
                    GlobalID.ID = id;
                    cmd.CommandText = "select USER_ID from sellers where USER_ID = " + id;
                    cmd.CommandType = CommandType.Text;
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        
                        HomeSeller hs = new HomeSeller();
                        hs.Show();
                        this.Hide();
                    }
                    else
                    {
                        
                        HomerBidder hb = new HomerBidder();
                        hb.Show();
                        this.Hide();
                    }
                }
                
                
                
            }
        }

        private void registerbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration r = new Registration();
            r.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}

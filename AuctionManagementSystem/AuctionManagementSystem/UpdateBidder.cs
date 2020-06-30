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
    public partial class UpdateBidder : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        public UpdateBidder()
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
            HomerBidder homeBidder = new HomerBidder();
            homeBidder.Show();
            this.Hide();
        }

        private void UpdateBidder_Load(object sender, EventArgs e)
        {
            using (con = new OracleConnection(ordb))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select NAME , PHONE , ADDRESS , E_MAIL , PASSWORD , GENDER from users where user_id = " + GlobalID.ID;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    nametxt.Text = dr[0].ToString();
                    phonetxt.Text = dr[1].ToString();
                    addresstxt.Text = dr[2].ToString();
                    emailtxt.Text = dr[3].ToString();
                    passwordtxt.Text = dr[4].ToString();
                    gendertxt.Text = dr[5].ToString();
                }
            }
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            using (con = new OracleConnection(ordb))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = @"update  USERS
                                  set name = :nam , phone = :phones , address = :addresse , e_mail = :email , password = :pass , gender = :g
                                  where user_id =" + GlobalID.ID;
                cmd.CommandType = CommandType.Text;
                OracleCommand cmd2 = new OracleCommand();
                cmd2.Connection = con; 
                cmd2.CommandText = "update BIDDERS set BIDDER_NAME = :namb where user_id = " + GlobalID.ID;
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.Add("namb", nametxt.Text.ToString());
                cmd.Parameters.Add("nam", nametxt.Text.ToString());
                cmd.Parameters.Add("phones", Convert.ToInt64(phonetxt.Text.ToString()));
                cmd.Parameters.Add("addresse", addresstxt.Text);
                cmd.Parameters.Add("email", emailtxt.Text);
                cmd.Parameters.Add("pass", passwordtxt.Text);
                cmd.Parameters.Add("g", gendertxt.SelectedItem.ToString());
                
                int ret = cmd.ExecuteNonQuery();
                int ret2 = cmd2.ExecuteNonQuery();
                MessageBox.Show("User Updated !! ");
            }
        }
    }
}

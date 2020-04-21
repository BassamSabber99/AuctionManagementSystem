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
    public partial class Registration : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (nametxt.Text == string.Empty || addresstxt.Text == string.Empty || gendertxt.SelectedItem.ToString() == string.Empty || phonetxt.Text == string.Empty || balancetxt.Text == string.Empty || emailtxt.Text == string.Empty || passwordtxt.Text == string.Empty || typetxt.SelectedItem.ToString() == string.Empty)
            {
                MessageBox.Show("Fill All Input Field correctly !!");
                return;
            }
            else
            {
                using (con = new OracleConnection(ordb))
                {

                    int maxID, newID;
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "getMaxUserId";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        maxID = Convert.ToInt32(cmd.Parameters["id"].Value.ToString());
                        newID = maxID + 1;
                    }
                    catch { newID = 1; }
                    
                    OracleCommand cmd2 = new OracleCommand();
                    cmd2.Connection = con;
                    cmd2.CommandText = "AddUser";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("id", newID);
                    cmd2.Parameters.Add("name", nametxt.Text.Trim().ToString());
                    cmd2.Parameters.Add("phone", Convert.ToInt32(phonetxt.Text.Trim()));
                    cmd2.Parameters.Add("address", addresstxt.Text.Trim().ToString());
                    cmd2.Parameters.Add("balance", Convert.ToInt32(balancetxt.Text.Trim()));
                    cmd2.Parameters.Add("email", emailtxt.Text.Trim().ToString());
                    cmd2.Parameters.Add("pw", passwordtxt.Text.Trim().ToString());
                    cmd2.Parameters.Add("gender", gendertxt.SelectedItem.ToString());
                    cmd2.ExecuteNonQuery();

                    
                    OracleCommand cmd3 = new OracleCommand();
                    cmd3.Connection = con;
                    if (typetxt.SelectedItem.ToString() == "Seller")
                    { 
                        cmd3.CommandText = "insert into SELLERS values (:id,:name)";
                        cmd3.CommandType = CommandType.Text;
                        cmd3.Parameters.Add("id", newID);
                        cmd3.Parameters.Add("name", nametxt.Text.Trim().ToString());
                        int ret = cmd3.ExecuteNonQuery();
                        if(ret != -1)
                        {
                            MessageBox.Show("ADDED Seller SUCCESSFULLY  !!!");
                        }

                    }
                    else
                    {                      
                        cmd3.CommandText = "insert into BIDDERS (USER_ID,BIDDER_NAME) values (:id,:name)";
                        cmd3.CommandType = CommandType.Text;
                        cmd3.Parameters.Add("id", newID);
                        cmd3.Parameters.Add("name", nametxt.Text.Trim().ToString());
                        int ret = cmd3.ExecuteNonQuery();
                        if(ret != -1)
                        {
                            MessageBox.Show("ADDED BIDDER SUCCESSFULLY  !!!");
                            OracleCommand orc = new OracleCommand();
                            orc.Connection = con;
                            orc.CommandText = "shipCart";
                            orc.CommandType = CommandType.StoredProcedure;
                            orc.Parameters.Add("id", newID);
                            orc.ExecuteNonQuery();
                        }
                    }
                    
                    MessageBox.Show("ADD SUCCESSFULLY !!!");
                    Login l = new Login();
                    l.Show();
                    this.Hide();

                }
            }
            
        }
    }
}

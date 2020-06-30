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
    public partial class CreateAuction : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        public CreateAuction()
        {
            InitializeComponent();
        }

        private void calender_DateChanged(object sender, DateRangeEventArgs e)
        {
            
        }

        private void CreateAuction_Load(object sender, EventArgs e)
        {
            sdate.MinDate = DateTime.Now;
            edate.MinDate = DateTime.Now;
            using(con = new OracleConnection(ordb))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = @"select ITEM_ID from ITEMS where SELLER_ID = :id  
                                    and item_id  not in(select ITM_ID from seller_auctions)
                                    order by ITEM_ID ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("id", GlobalID.ID);
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    allItems.Items.Add(dr[0]);
                }
                dr.Close();
            }

        }

        private void calender1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void t1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string startDate = sdate.Value.ToString("dd-MMM-y");
            string endDate = edate.Value.Date.ToString("dd-MMM-y");

            string starttime = stime.Text.ToString();
            string endtime = etime.Text.ToString();

            string datetime1 = startDate+" "+ starttime;
            string datetime2 = endDate +" "+endtime;

            DateTime date = new DateTime();
            date = Convert.ToDateTime(datetime1);


            MessageBox.Show("Datetime1:"+ date + " Datetime2:"+ datetime2);

            

            
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            AllAuctions allAuction = new AllAuctions();
            this.Hide();
            allAuction.Show();
        }

        private void createbtn_Click(object sender, EventArgs e)
        {
            string startDate = sdate.Value.ToString("dd-MMM-y");
            string endDate = edate.Value.Date.ToString("dd-MMM-y");

            string starttime = stime.Text.ToString();
            string endtime = etime.Text.ToString();

            string datetime1 = startDate + " " + starttime;
            string datetime2 = endDate + " " + endtime;

            DateTime date = new DateTime();
            date = Convert.ToDateTime(datetime1);
            int maxID, newID;
            using (con = new OracleConnection(ordb))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "getmaxAuction";
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
                cmd2.CommandText = "createAcution";
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("AID", newID);
                cmd2.Parameters.Add("SID", GlobalID.ID);
                cmd2.Parameters.Add("IID", Convert.ToInt16(allItems.SelectedItem.ToString()));
                cmd2.Parameters.Add("sTime", datetime1);
                cmd2.Parameters.Add("eTime", datetime2);
                if(date <= DateTime.Now)
                {
                    cmd2.Parameters.Add("state", "open");
                    //hh <= hhn && mm <= mmn && day <= dayn && month <= monthn
                }
                else
                {
                    cmd2.Parameters.Add("state", "close");
                }
                int ret = cmd2.ExecuteNonQuery();
                if(ret != -1)
                {
                    
                }
                MessageBox.Show("Auction Has Been Created !! and in time : "+datetime1 +" / time now : " + DateTime.Now);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string startDate = sdate.Value.ToString("dd-MMM-y");
            string endDate = edate.Value.Date.ToString("dd-MMM-y");

            string starttime = stime.Text.ToString();
            string endtime = etime.Text.ToString();

            string datetime1 = startDate + " " + starttime;
            string datetime2 = endDate + " " + endtime;

            DateTime date = new DateTime();
            date = Convert.ToDateTime(datetime1);

            int hh = date.Hour;
            int mm = date.Minute;


            MessageBox.Show("Datetime1:" + hh +":"+mm + " Datetime2:" + datetime2);
        }
    }
}

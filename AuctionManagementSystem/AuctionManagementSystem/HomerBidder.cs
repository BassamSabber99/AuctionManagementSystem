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
    public partial class HomerBidder : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        public HomerBidder()
        {
            InitializeComponent();
            using (con = new OracleConnection(ordb))
            {
                con.Open();

                allAuctionView.ReadOnly = true;
                allAuctionView.Columns.Clear();
                allAuctionView.Rows.Clear();
                allAuctionView.ColumnCount = 4;
                allAuctionView.Columns[0].Name = "ID";
                allAuctionView.Columns[1].Name = "Start Date";
                allAuctionView.Columns[2].Name = "End Date";
                allAuctionView.Columns[3].Name = "Status";

                OracleCommand cmd2 = new OracleCommand();
                cmd2.Connection = con;
                cmd2.CommandText = @"select a.auc_id,a.s_date, a.e_date ,a.status  from auctions a , BIDDER_AUCTIONS s 
                                    where a.auc_id = s.auc_id
                                    and s.user_id = :ids
                                    order by a.auc_id ";
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.Add("ids", GlobalID.ID);
                OracleDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    allAuctionView.Rows.Add(dr2[0], dr2[1], dr2[2],dr2[3]);
                }
                dr2.Close();
            
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from users where user_id =:id";
                cmd.Parameters.Add("id", GlobalID.ID);
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    nametxt.Text = dr[1].ToString();
                    phonetxt.Text = dr[2].ToString();
                    addresstxt.Text = dr[3].ToString();
                    emailtxt.Text = dr[5].ToString();
                    balancetxt.Text = dr[4].ToString();
                    gendertxt.Text = dr[7].ToString();
                }
            }
        }

        private void HomerBidder_Load(object sender, EventArgs e)
        {
            
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

        private void updaccbtn_Click(object sender, EventArgs e)
        {
            UpdateBidder updateBidder = new UpdateBidder();
            this.Hide();
            updateBidder.Show();
        }

        private void serchitmbtn_Click(object sender, EventArgs e)
        {
            SearchForItems searchForItems = new SearchForItems();
            this.Hide();
            searchForItems.Show();
        }

        private void allauctionbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            AllAuctionBidder allAuctionBidder = new AllAuctionBidder();
            allAuctionBidder.Show();
        }

        private void shipbtn_Click(object sender, EventArgs e)
        {
            ShippingCart shippingCart = new ShippingCart();
            this.Hide();
            shippingCart.Show();
        }
    }
}

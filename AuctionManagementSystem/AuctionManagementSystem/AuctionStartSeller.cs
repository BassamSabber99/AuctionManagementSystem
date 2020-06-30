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
    public partial class AuctionStartSeller : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        public AuctionStartSeller()
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
            this.Hide();
            AllAuctions allAuction = new AllAuctions();
            allAuction.Show();
        }
        public void ReLoad()
        {

            bidderView.ReadOnly = true;
            bidderView.Columns.Clear();
            bidderView.Rows.Clear();
            bidderView.ColumnCount = 2;
            bidderView.Columns[0].Name = "Bidder_Name";
            bidderView.Columns[1].Name = "Bid_Value";

            OracleCommand oc = new OracleCommand();
            oc.Connection = con;
            oc.CommandText = @"select u.name , ba.value from bidder_auctions ba , users u
                                    WHERE ba.user_id = u.user_id
                                    and ba.auc_id = :id
                                    order by value";
            oc.CommandType = CommandType.Text;
            oc.Parameters.Add("id", GlobalID.AucID);
            OracleDataReader dr4 = oc.ExecuteReader();
            while (dr4.Read())
            {
                bidderView.Rows.Add(dr4[0], dr4[1]);
            }
            dr4.Close();
        }
        private void AuctionStartSeller_Load(object sender, EventArgs e)
        {
                using (con = new OracleConnection(ordb))
                {
               
                    con.Open();

                    ReLoad();

                    int itm_ID = 0, seller_id = 0;
                    OracleCommand cmdd = new OracleCommand();
                    cmdd.Connection = con;
                    cmdd.CommandText = @"select i.name , i.value , i.DESCRIPTION , a.s_date , a.e_date , i.item_id , s.user_id from auctions a , seller_auctions s , items i
                                        where a.auc_id = s.auc_id
                                        and s.itm_id = i.item_id
                                        and a.auc_id = :id
                                        order by a.auc_id ";
                    cmdd.CommandType = CommandType.Text;
                    cmdd.Parameters.Add("id", GlobalID.AucID);
                    OracleDataReader drr = cmdd.ExecuteReader();
                    while (drr.Read())
                    {
                        itmnametx.Text = drr[0].ToString();
                        itmvaltxt.Text = drr[1].ToString();
                        itmdestxt.Text = drr[2].ToString();
                        sdatetxt.Text = drr[3].ToString();
                        edatetxt.Text = drr[4].ToString();
                        itm_ID = Convert.ToInt32(drr[5].ToString());
                        seller_id = Convert.ToInt32(drr[6].ToString());
                    }
                    drr.Close();
                }
            }
    }
}

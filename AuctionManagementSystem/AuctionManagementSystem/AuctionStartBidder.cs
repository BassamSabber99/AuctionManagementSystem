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
    public partial class AuctionStartBidder : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        public AuctionStartBidder()
        {
            InitializeComponent();
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
        private void AuctionStartBidder_Load(object sender, EventArgs e)
        {
            using(con = new OracleConnection(ordb))
            {
                string status = "";
                con.Open();

                ReLoad();

                int itm_ID = 0 , seller_id = 0;
                 OracleCommand cmdd = new OracleCommand();
                cmdd.Connection = con;
                cmdd.CommandText = @"select i.name , i.value , i.DESCRIPTION , a.s_date , a.e_date , i.item_id , s.user_id from auctions a , seller_auctions s , items i
                                    where a.auc_id = s.auc_id
                                    and s.itm_id = i.item_id
                                    and a.auc_id = :id
                                    order by a.auc_id ";
                cmdd.CommandType = CommandType.Text;
                cmdd.Parameters.Add("id",GlobalID.AucID);
                OracleDataReader drr = cmdd.ExecuteReader();
                while (drr.Read())
                {
                    itmnametx.Text = drr[0].ToString();
                    itmvaltxt.Text = drr[1].ToString();
                    itmdestxt.Text = drr[2].ToString();
                    sdatetxt.Text  = drr[3].ToString();
                    edatetxt.Text  = drr[4].ToString();
                    itm_ID = Convert.ToInt32(drr[5].ToString());
                    seller_id = Convert.ToInt32(drr[6].ToString());
                }
                drr.Close();

                DateTime endDate = new DateTime();
                endDate = Convert.ToDateTime(edatetxt.Text.ToString());
                if (endDate <= DateTime.Now)
                {
                    MessageBox.Show("Closed");
                    OracleCommand cmdu = new OracleCommand();
                    cmdu.Connection = con;
                    cmdu.CommandText = "update auctions set status = 'close' where auc_id = :id";
                    cmdu.CommandType = CommandType.Text;
                    cmdu.Parameters.Add("id", GlobalID.AucID);
                    int ret = cmdu.ExecuteNonQuery();
                    if(ret != -1)
                    {
                        MessageBox.Show("Status Changed Successfully");
                        bidbtn.Enabled = false;
                    }
                    if(bidderView.RowCount == 0)
                    {
                        MessageBox.Show("No One Buy This Items !!!");
                        return;
                    }
                    ////----- Put Item In Ship Cart ---------//////////
                    int maxIDValu = 0 , maxValended= 0 , aucID = GlobalID.AucID;
                    OracleCommand MaxUsVal = new OracleCommand();
                    MaxUsVal.Connection = con;
                    MaxUsVal.CommandText = "select user_id ,value from bidder_auctions where auc_id = :auid and value = (select max(value) from bidder_auctions where auc_id = :aucid )";
                    MaxUsVal.CommandType = CommandType.Text;
                    MaxUsVal.Parameters.Add("auid", GlobalID.AucID);
                    MaxUsVal.Parameters.Add("aucid", aucID);
                    OracleDataReader max = MaxUsVal.ExecuteReader();
                    while (max.Read())
                    {
                        maxIDValu = Convert.ToInt32(max[0].ToString());
                        maxValended = Convert.ToInt32(max[1].ToString());
                    }
                    max.Close();

                    MessageBox.Show("The Winner IS : " + maxIDValu.ToString());

                    int quantity = 0 , shipID = 0;
                    OracleCommand getQuntityAndShip = new OracleCommand();
                    getQuntityAndShip.Connection = con;
                    getQuntityAndShip.CommandText = @"select QUANTITY , ship_id
                                               from SHIPPING_CARTS
                                               where bidder_id = :maxID";
                    getQuntityAndShip.Parameters.Add("maxID", maxIDValu);
                    OracleDataReader drq = getQuntityAndShip.ExecuteReader();
                    while (drq.Read())
                    {
                        quantity = Convert.ToInt32(drq[0].ToString());
                        shipID = Convert.ToInt32(drq[1].ToString());
                    }
                    drq.Close();

                    quantity++;

                    MessageBox.Show("The Ship ID IS :" + shipID + " The Quan : " + quantity);

                    ///---- update balance Bidder ---/////

                    OracleCommand UpdateBalaneceBidder = new OracleCommand();
                    UpdateBalaneceBidder.Connection = con;
                    UpdateBalaneceBidder.CommandText = "update users set balance = balance - :bidval where user_id = :ids";
                    UpdateBalaneceBidder.CommandType = CommandType.Text;
                    UpdateBalaneceBidder.Parameters.Add("bidval", maxValended);
                    UpdateBalaneceBidder.Parameters.Add("ids", maxIDValu);
                    UpdateBalaneceBidder.ExecuteNonQuery();

                    ////-------- update balance Seller ---/////////

                    OracleCommand UpdateBalaneceSeller = new OracleCommand();
                    UpdateBalaneceSeller.Connection = con;
                    UpdateBalaneceSeller.CommandText = "update users set balance = balance + :bidval where user_id = :iduse";
                    UpdateBalaneceSeller.CommandType = CommandType.Text;
                    UpdateBalaneceSeller.Parameters.Add("bidval", maxValended);
                    UpdateBalaneceSeller.Parameters.Add("iduse", seller_id);
                    UpdateBalaneceSeller.ExecuteNonQuery();


                    //-----Update Ship Cart --/////

                    OracleCommand updateSHIP_CaRT = new OracleCommand();
                    updateSHIP_CaRT.Connection = con;
                    updateSHIP_CaRT.CommandText = "update SHIPPING_CARTS set QUANTITY = :newq where bidder_id = :uids and ship_id = :shid";
                    updateSHIP_CaRT.CommandType = CommandType.Text;
                    updateSHIP_CaRT.Parameters.Add("newq", quantity);
                    updateSHIP_CaRT.Parameters.Add("uids", maxIDValu);
                    updateSHIP_CaRT.Parameters.Add("shid", shipID);
                    updateSHIP_CaRT.ExecuteNonQuery();


                    ////-- insert item into ship items---///////

                    OracleCommand addTOShipItems = new OracleCommand();
                    addTOShipItems.Connection = con;
                    addTOShipItems.CommandText = "insert into shipping_items(ship_id, itm_name, value, auc_id) values (:shipid,:itmname,:bidval,:auid)";
                    addTOShipItems.CommandType = CommandType.Text;
                    addTOShipItems.Parameters.Add("shipid", shipID);
                    addTOShipItems.Parameters.Add("itmname", itmnametx.Text.ToString());
                    addTOShipItems.Parameters.Add("bidval", maxValended);
                    addTOShipItems.Parameters.Add("auid", GlobalID.AucID);
                    addTOShipItems.ExecuteNonQuery();


                    ///----- update item Owner ----///////

                    OracleCommand updateItemOwner = new OracleCommand();
                    updateItemOwner.Connection = con;
                    updateItemOwner.CommandText = "update items set seller_id = :OwnID where item_id = :itmids";
                    updateItemOwner.CommandType = CommandType.Text;
                    updateItemOwner.Parameters.Add("OwnID", maxIDValu);
                    updateItemOwner.Parameters.Add("itmids", itm_ID);
                    updateItemOwner.ExecuteNonQuery();



                    //////////////
                }

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select status from auctions where auc_id = :id";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("id", GlobalID.AucID);
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    status = dr[0].ToString();
                }
                dr.Close();
                if(status == "close")
                {
                    bidbtn.Enabled = false;
                }
                else
                {
                    bidbtn.Enabled = true;
                }
            }
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
            AllAuctionBidder allAuctionBidder = new AllAuctionBidder();
            allAuctionBidder.Show();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void bidbtn_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(bidval.Text.ToString()))
            {
                MessageBox.Show("Please Enter Value !");
            }
            else
            {
                using (con =new OracleConnection(ordb))
                {
                    int maxVal = 0;
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "getMAxValue";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("id", GlobalID.AucID);
                    cmd.Parameters.Add("val", OracleDbType.Int32, ParameterDirection.Output);

                    try
                    {

                        cmd.ExecuteNonQuery();
                        maxVal = Convert.ToInt32(cmd.Parameters["val"].Value.ToString());
                        //MessageBox.Show(maxVal.ToString());
                    }
                    catch
                    {
                       
                        
                        maxVal = Convert.ToInt32(bidval.Text.ToString());
                        ReLoad();
                    }

                    if (Convert.ToInt32(bidval.Text.ToString()) > maxVal)
                        {
                            int balance = 0;
                            OracleCommand bids = new OracleCommand();
                            bids.Connection = con;
                            bids.CommandText = "select user_id from bidder_auctions where USER_ID = :uid and AUC_ID = :aid";
                            bids.CommandType = CommandType.Text;
                            bids.Parameters.Add("uid", GlobalID.ID);
                            bids.Parameters.Add("aid", GlobalID.AucID);
                            OracleDataReader drb = cmd.ExecuteReader();
                            if (true)
                            {

                            OracleCommand check = new OracleCommand();
                            check.Connection = con;
                            check.CommandText = "select balance from users where user_id  = :id";
                            check.CommandType = CommandType.Text;
                            check.Parameters.Add("id", GlobalID.ID);
                            OracleDataReader drcheck = check.ExecuteReader();
                            while (drcheck.Read())
                            {
                                balance = Convert.ToInt32(drcheck[0].ToString());
                            }
                            drcheck.Close();
                            if (balance < Convert.ToInt32(bidval.Text.ToString()))
                            {
                                MessageBox.Show("In Sufficient Balance!!!");
                                return;
                            }

                            OracleCommand update = new OracleCommand();
                                update.Connection = con;
                                update.CommandText = @"update BIDDER_AUCTIONS
                                                        set value = :valn
                                                        where auc_id = :auctid
                                                        and user_id = :useid";
                                update.CommandType = CommandType.Text;
                                update.Parameters.Add("valn", Convert.ToInt32(bidval.Text.ToString()));
                                update.Parameters.Add("auctid", GlobalID.AucID);
                                update.Parameters.Add("useid", GlobalID.ID);
                                int ret = update.ExecuteNonQuery();
                                if(ret == 0)
                                {

                                    OracleCommand insert = new OracleCommand();
                                    insert.Connection = con;
                                    insert.CommandText = "insert into BIDDER_AUCTIONS values (:aucid,:userid,:newval)";
                                    insert.CommandType = CommandType.Text;
                                    insert.Parameters.Add("aucid", GlobalID.AucID);
                                    insert.Parameters.Add("userid", GlobalID.ID);
                                    insert.Parameters.Add("newval", Convert.ToInt32(bidval.Text.ToString()));
                                    insert.ExecuteNonQuery();
                                    MessageBox.Show("inserted!!");
                                    ReLoad();
                                }
                                else
                                {
                                    MessageBox.Show("Updated!!");
                                    ReLoad();
                                }   
                                
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please Enter Big Value");
                            return;
                        }
                   
                   
                }
            }
        }
    }
}

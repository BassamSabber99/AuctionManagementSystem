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
    public partial class ShippingCart : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        OracleDataAdapter adabter;
        DataSet ds;
        public ShippingCart()
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomerBidder hb = new HomerBidder();
            hb.Show();
        }

        private void ShippingCart_Load(object sender, EventArgs e)
        {
            myShippItems.ReadOnly = true;
            string cmdstr = @" select si.itm_name as ItemName ,si.value as AuctionValue , u.name as Trader , i.description , i.value as MainValue , c.cat_name
                                from SHIPPING_ITEMS si , shipping_carts sc , users u , seller_auctions sa  , items i , categories c
                                where si.ship_id = sc.ship_id
                                and si.auc_id = sa.auc_id
                                and u.user_id = sa.user_id
                                and sa.itm_id = i.item_id
                                and i.cat_id = c.cat_id
                                and sc.bidder_id =:BidderID";
            adabter = new OracleDataAdapter(cmdstr, ordb);
            adabter.SelectCommand.Parameters.Add("BidderID", GlobalID.ID);
            ds = new DataSet();
            adabter.Fill(ds);
            myShippItems.DataSource = ds.Tables[0];

            using (con = new OracleConnection(ordb))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select sc.quantity from shipping_carts sc where sc.bidder_id = :BidID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("BidID", GlobalID.ID);
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    quantitytxt.Text = dr[0].ToString();
                }
                dr.Close();
            }
            
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            string cmdstr = @" SELECT si.itm_name as Item_Name , si.value as Action_Value , u.name as trader , i.description , i.value as Main_Value , c.cat_name
                                FROM categories c ,items i , shipping_carts sc , shipping_items si , seller_auctions sa , users u
                                where si.auc_id = sa.auc_id
                                and sa.user_id = u.user_id
                                and i.seller_id = sc.bidder_id
                                and c.cat_id = i.cat_id
                                and sc.ship_id = si.ship_id
                                and sc.bidder_id = :BidderID
                                and i.name = :itmname";
            adabter = new OracleDataAdapter(cmdstr, ordb);
            adabter.SelectCommand.Parameters.Add("BidderID", GlobalID.ID);
            adabter.SelectCommand.Parameters.Add("itmname", itmnametxt.Text.ToString());
            ds = new DataSet();
            adabter.Fill(ds);
            myShippItems.DataSource = ds.Tables[0];
        }

        private void allaucbtn_Click(object sender, EventArgs e)
        {
            ShipCartReport scr = new ShipCartReport();
            this.Hide();
            scr.Show();
        }
    }
}

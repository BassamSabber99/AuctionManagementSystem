﻿using System;
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
    public partial class AllAuctionBidder : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        public AllAuctionBidder()
        {
            InitializeComponent();
        }

        private void AllAuctionBidder_Load(object sender, EventArgs e)
        {
            using (con = new OracleConnection(ordb))
            {
                auctionsView.ReadOnly = true;
                auctionsView.Columns.Clear();
                auctionsView.Rows.Clear();
                auctionsView.ColumnCount = 6;
                auctionsView.Columns[0].Name = "ID";
                auctionsView.Columns[1].Name = "Start Date";
                auctionsView.Columns[2].Name = "End Date";
                auctionsView.Columns[3].Name = "Item Name";
                auctionsView.Columns[4].Name = "Item Value";
                auctionsView.Columns[5].Name = "Status";

                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = @"select a.auc_id,a.s_date, a.e_date, i.name,i.value ,a.status  from auctions a , seller_auctions s , items i
                                    where a.auc_id = s.auc_id
                                    and s.itm_id = i.item_id
                                    and a.status = 'open'
                                    order by a.auc_id ";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();


                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "GO";
                btn.Name = "btgrid";
                btn.Text = "Enter";
                btn.UseColumnTextForButtonValue = true;
                auctionsView.Columns.Add(btn);




                while (dr.Read())
                {
                    auctionsView.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);



                }
                dr.Close();

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
            HomerBidder homeBidder = new HomerBidder();
            this.Hide();
            homeBidder.Show();
        }

        private void aucttionview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void aucttionview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (auctionsView.CurrentCell.ColumnIndex.Equals(6) && e.RowIndex != -1)
            {
                if (auctionsView.CurrentCell != null && auctionsView.CurrentCell.Value != null)
                {
                    con = new OracleConnection(ordb);
                    con.Open();
                    GlobalID.AucID = Convert.ToInt32(auctionsView.Rows[e.RowIndex].Cells[0].Value.ToString());
                    int balance = 0 , item_val = 0;
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

                    item_val = Convert.ToInt32(auctionsView.Rows[e.RowIndex].Cells[4].Value.ToString());



                    if (balance < item_val)
                    {
                        MessageBox.Show("In Sufficient Balance!!!");
                        return;
                    }
                    else
                    {
                        OracleCommand insert = new OracleCommand();
                        insert.Connection = con;
                        insert.CommandText = "insert into BIDDER_AUCTIONS values (:aucid,:userid,:newval)";
                        insert.CommandType = CommandType.Text;
                        insert.Parameters.Add("aucid", GlobalID.AucID);
                        insert.Parameters.Add("userid", GlobalID.ID);
                        insert.Parameters.Add("newval", item_val);
                        try
                        {
                            insert.ExecuteNonQuery();
                            MessageBox.Show("Auction Started : " + auctionsView.Rows[e.RowIndex].Cells[0].Value.ToString());
                            this.Hide();
                            AuctionStartBidder auctionStartBidder = new AuctionStartBidder();
                            auctionStartBidder.Show();
                        }
                        catch
                        {
                            MessageBox.Show("Auction Started : " + auctionsView.Rows[e.RowIndex].Cells[0].Value.ToString());
                            this.Hide();
                            AuctionStartBidder auctionStartBidder = new AuctionStartBidder();
                            auctionStartBidder.Show();
                        }
                      
                    }

                   
                }
            }
        }
    }
}

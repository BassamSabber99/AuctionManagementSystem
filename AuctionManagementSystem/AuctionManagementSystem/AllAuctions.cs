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
    public partial class AllAuctions : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        public AllAuctions()
        {
            InitializeComponent();
        }

        private void CreateAuction_Load(object sender, EventArgs e)
        {
            
            using (con = new OracleConnection(ordb))
            {
                aucttionview.ReadOnly = true;
                aucttionview.Columns.Clear();
                aucttionview.Rows.Clear();
                aucttionview.ColumnCount = 6;
                aucttionview.Columns[0].Name = "ID";
                aucttionview.Columns[1].Name = "Start Date";
                aucttionview.Columns[2].Name = "End Date";
                aucttionview.Columns[3].Name = "Item Name";
                aucttionview.Columns[4].Name = "Item Value";
                aucttionview.Columns[5].Name = "Status";
                
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
                aucttionview.Columns.Add(btn);




                while (dr.Read())
                {
                    aucttionview.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
                    

                    
                }
                dr.Close();

            }   
        }

        private void aucttionview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
       
        
        private void aucttionview_RowDividerDoubleClick(object sender, DataGridViewRowDividerDoubleClickEventArgs e)
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

        private void aucttionview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(aucttionview.CurrentCell.ColumnIndex.Equals(6)&&e.RowIndex != -1)
            {
                if(aucttionview.CurrentCell != null && aucttionview.CurrentCell.Value != null)
                {
                    GlobalID.AucID = Convert.ToInt32(aucttionview.Rows[e.RowIndex].Cells[0].Value.ToString());
                    MessageBox.Show("Auction Started : " + aucttionview.Rows[e.RowIndex].Cells[0].Value.ToString());
                    this.Hide();
                    AuctionStartSeller ass = new AuctionStartSeller();
                    ass.Show();


                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            HomeSeller hs = new HomeSeller();
            this.Hide();
            hs.Show();
        }

        private void createbtn_Click(object sender, EventArgs e)
        {
            CreateAuction ca = new CreateAuction();
            this.Hide();
            ca.Show();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            AllAuctionsReport alb = new AllAuctionsReport();
            this.Hide();
            alb.Show();
        }
    }
}

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
    public partial class HomeSeller : Form
    {
        string ordb = "data source = orcl ; user id = hr ; password = hr";
        OracleConnection con;
        public HomeSeller()
        { 
            InitializeComponent();
            ReLoad();
            using (con = new OracleConnection(ordb))
            {
                con.Open();
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
                dr.Close();

                OracleCommand cmd3 = new OracleCommand();
                cmd3.Connection = con;
                cmd3.CommandText =@"select a.auc_id from auctions a , seller_auctions s , items i
                                    where a.auc_id = s.auc_id
                                    and s.itm_id = i.item_id
                                    and a.status = 'close'
                                    and s.user_id = :id
                                    order by a.auc_id ";
                cmd3.CommandType = CommandType.Text;
                cmd3.Parameters.Add("id", GlobalID.ID);
                OracleDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    allauc.Items.Add(dr3[0]);
                }
                dr3.Close();
            }
        }
        public void ReLoad()
        {
            using (con = new OracleConnection(ordb))
            {
                con.Open();
                myAuctions.ReadOnly = true;
                myAuctions.Columns.Clear();
                myAuctions.Rows.Clear();
                myAuctions.ColumnCount = 6;
                myAuctions.Columns[0].Name = "ID";
                myAuctions.Columns[1].Name = "Start Date";
                myAuctions.Columns[2].Name = "End Date";
                myAuctions.Columns[3].Name = "Item Name";
                myAuctions.Columns[4].Name = "Item Value";
                myAuctions.Columns[5].Name = "Status";

                OracleCommand cmd2 = new OracleCommand();
                cmd2.Connection = con;
                cmd2.CommandText = @"select a.auc_id,a.s_date, a.e_date, i.name,i.value ,a.status  from auctions a , seller_auctions s , items i
                                    where a.auc_id = s.auc_id
                                    and s.itm_id = i.item_id
                                    and s.user_id = :ids
                                    order by a.auc_id ";
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.Add("ids", GlobalID.ID);
                OracleDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    myAuctions.Rows.Add(dr2[0], dr2[1], dr2[2], dr2[3], dr2[4], dr2[5]);
                }
                dr2.Close();
            }
        }

        private void HomeSeller_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
            con.Dispose();
            con.Close();
        }

        private void nametxt_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void emailtxt_Click(object sender, EventArgs e)
        {

        }

        private void phonetxt_Click(object sender, EventArgs e)
        {

        }

        private void addresstxt_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void gendertxt_Click(object sender, EventArgs e)
        {

        }

        private void emailtxt_Click_1(object sender, EventArgs e)
        {

        }

        private void balancetxt_Click(object sender, EventArgs e)
        {

        }

        private void addresstxt_Click_1(object sender, EventArgs e)
        {

        }

        private void phonetxt_Click_1(object sender, EventArgs e)
        {

        }

        private void nametxt_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
            con.Dispose();
            con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            NewCategory newCategory = new NewCategory();
            newCategory.Show();
            
        }

        private void updaccbtn_Click(object sender, EventArgs e)
        {
            UpdateSeller updateSeller = new UpdateSeller();
            this.Hide();
            updateSeller.Show();
        }

        private void additmbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ItemsOperation itemOpertation = new ItemsOperation();
            itemOpertation.Show();
        }

        private void createaucbtn_Click(object sender, EventArgs e)
        {
            AllAuctions allAuction = new AllAuctions();
            this.Hide();
            allAuction.Show();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            using(con = new OracleConnection(ordb))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "deleteAuction";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("id", Convert.ToInt16(allauc.SelectedItem));
                cmd.ExecuteNonQuery();
                
                OracleCommand cmd2 = new OracleCommand();
                cmd2.Connection = con;
                cmd2.CommandText = "delete from SELLER_AUCTIONS where AUC_ID = :id";
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.Add("id", Convert.ToInt16(allauc.SelectedItem));
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Auction Deleted !!!");
                ReLoad();


            }
        }
    }
}

namespace AuctionManagementSystem
{
    partial class ShippingCart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShippingCart));
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.savebtn = new Bunifu.Framework.UI.BunifuThinButton2();
            this.label1 = new System.Windows.Forms.Label();
            this.itmnametxt = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.myShippItems = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.quantitytxt = new System.Windows.Forms.Label();
            this.allaucbtn = new Bunifu.Framework.UI.BunifuThinButton2();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myShippItems)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 73);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(842, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(41, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(889, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 47);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(950, 76);
            this.panel1.TabIndex = 55;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Britannic Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(204, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 27);
            this.label2.TabIndex = 78;
            this.label2.Text = "My ShipCart Items:";
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.panel1;
            this.bunifuDragControl1.Vertical = true;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 80;
            this.bunifuElipse1.TargetControl = this;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(82, 423);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(73, 46);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 56;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // savebtn
            // 
            this.savebtn.ActiveBorderThickness = 1;
            this.savebtn.ActiveCornerRadius = 20;
            this.savebtn.ActiveFillColor = System.Drawing.Color.DarkSlateBlue;
            this.savebtn.ActiveForecolor = System.Drawing.Color.White;
            this.savebtn.ActiveLineColor = System.Drawing.Color.DarkSlateBlue;
            this.savebtn.BackColor = System.Drawing.Color.Crimson;
            this.savebtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("savebtn.BackgroundImage")));
            this.savebtn.ButtonText = "Search";
            this.savebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.savebtn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savebtn.ForeColor = System.Drawing.Color.White;
            this.savebtn.IdleBorderThickness = 1;
            this.savebtn.IdleCornerRadius = 20;
            this.savebtn.IdleFillColor = System.Drawing.Color.LightSeaGreen;
            this.savebtn.IdleForecolor = System.Drawing.Color.White;
            this.savebtn.IdleLineColor = System.Drawing.Color.LightSeaGreen;
            this.savebtn.Location = new System.Drawing.Point(501, 323);
            this.savebtn.Margin = new System.Windows.Forms.Padding(5);
            this.savebtn.Name = "savebtn";
            this.savebtn.Size = new System.Drawing.Size(139, 41);
            this.savebtn.TabIndex = 77;
            this.savebtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.savebtn.Click += new System.EventHandler(this.savebtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Britannic Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(72, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 27);
            this.label1.TabIndex = 74;
            this.label1.Text = "Item Name:";
            // 
            // itmnametxt
            // 
            this.itmnametxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.itmnametxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.itmnametxt.ForeColor = System.Drawing.Color.White;
            this.itmnametxt.HintForeColor = System.Drawing.Color.Empty;
            this.itmnametxt.HintText = "";
            this.itmnametxt.isPassword = false;
            this.itmnametxt.LineFocusedColor = System.Drawing.Color.MidnightBlue;
            this.itmnametxt.LineIdleColor = System.Drawing.Color.White;
            this.itmnametxt.LineMouseHoverColor = System.Drawing.Color.MidnightBlue;
            this.itmnametxt.LineThickness = 3;
            this.itmnametxt.Location = new System.Drawing.Point(211, 323);
            this.itmnametxt.Margin = new System.Windows.Forms.Padding(4);
            this.itmnametxt.Name = "itmnametxt";
            this.itmnametxt.Size = new System.Drawing.Size(183, 33);
            this.itmnametxt.TabIndex = 76;
            this.itmnametxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // myShippItems
            // 
            this.myShippItems.BackgroundColor = System.Drawing.Color.MidnightBlue;
            this.myShippItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.myShippItems.Location = new System.Drawing.Point(65, 106);
            this.myShippItems.Name = "myShippItems";
            this.myShippItems.Size = new System.Drawing.Size(598, 210);
            this.myShippItems.TabIndex = 75;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Britannic Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(669, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 27);
            this.label3.TabIndex = 78;
            this.label3.Text = "Number Of Items:";
            // 
            // quantitytxt
            // 
            this.quantitytxt.AutoSize = true;
            this.quantitytxt.Font = new System.Drawing.Font("Britannic Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantitytxt.ForeColor = System.Drawing.Color.White;
            this.quantitytxt.Location = new System.Drawing.Point(873, 196);
            this.quantitytxt.Name = "quantitytxt";
            this.quantitytxt.Size = new System.Drawing.Size(27, 27);
            this.quantitytxt.TabIndex = 79;
            this.quantitytxt.Text = "0";
            // 
            // allaucbtn
            // 
            this.allaucbtn.ActiveBorderThickness = 1;
            this.allaucbtn.ActiveCornerRadius = 20;
            this.allaucbtn.ActiveFillColor = System.Drawing.Color.Olive;
            this.allaucbtn.ActiveForecolor = System.Drawing.Color.Crimson;
            this.allaucbtn.ActiveLineColor = System.Drawing.Color.Olive;
            this.allaucbtn.BackColor = System.Drawing.Color.Crimson;
            this.allaucbtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("allaucbtn.BackgroundImage")));
            this.allaucbtn.ButtonText = "Reports";
            this.allaucbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.allaucbtn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allaucbtn.ForeColor = System.Drawing.Color.White;
            this.allaucbtn.IdleBorderThickness = 1;
            this.allaucbtn.IdleCornerRadius = 20;
            this.allaucbtn.IdleFillColor = System.Drawing.Color.Yellow;
            this.allaucbtn.IdleForecolor = System.Drawing.Color.Black;
            this.allaucbtn.IdleLineColor = System.Drawing.Color.Yellow;
            this.allaucbtn.Location = new System.Drawing.Point(719, 380);
            this.allaucbtn.Margin = new System.Windows.Forms.Padding(5);
            this.allaucbtn.Name = "allaucbtn";
            this.allaucbtn.Size = new System.Drawing.Size(181, 41);
            this.allaucbtn.TabIndex = 91;
            this.allaucbtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.allaucbtn.Click += new System.EventHandler(this.allaucbtn_Click);
            // 
            // ShippingCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Crimson;
            this.ClientSize = new System.Drawing.Size(950, 500);
            this.Controls.Add(this.allaucbtn);
            this.Controls.Add(this.quantitytxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.savebtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.itmnametxt);
            this.Controls.Add(this.myShippItems);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShippingCart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShippingCart";
            this.Load += new System.EventHandler(this.ShippingCart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myShippItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label quantitytxt;
        private System.Windows.Forms.Label label3;
        private Bunifu.Framework.UI.BunifuThinButton2 savebtn;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuMaterialTextbox itmnametxt;
        private System.Windows.Forms.DataGridView myShippItems;
        private Bunifu.Framework.UI.BunifuThinButton2 allaucbtn;
    }
}
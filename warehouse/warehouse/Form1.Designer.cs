namespace warehouse
{
    partial class Form1
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
            this.Select_File = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Starting_Position = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Destionation = new System.Windows.Forms.TextBox();
            this.Find_Path = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.End_location = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Start_location = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Multi_item_set = new System.Windows.Forms.TextBox();
            this.LoadOrder = new System.Windows.Forms.Button();
            this.Find_multi_item = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Clear_Path_Simply = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Find_Path_Multi_No_Change_Order = new System.Windows.Forms.Button();
            this.Clear_Path = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Select_File
            // 
            this.Select_File.Location = new System.Drawing.Point(1713, 24);
            this.Select_File.Name = "Select_File";
            this.Select_File.Size = new System.Drawing.Size(75, 23);
            this.Select_File.TabIndex = 0;
            this.Select_File.Text = "Slelect Map File";
            this.Select_File.UseVisualStyleBackColor = true;
            this.Select_File.Click += new System.EventHandler(this.Select_File_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Starting Position";
            // 
            // Starting_Position
            // 
            this.Starting_Position.Enabled = false;
            this.Starting_Position.Location = new System.Drawing.Point(101, 19);
            this.Starting_Position.Name = "Starting_Position";
            this.Starting_Position.Size = new System.Drawing.Size(100, 20);
            this.Starting_Position.TabIndex = 3;
            this.Starting_Position.Text = "1,1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destionation";
            // 
            // Destionation
            // 
            this.Destionation.Enabled = false;
            this.Destionation.Location = new System.Drawing.Point(101, 61);
            this.Destionation.Name = "Destionation";
            this.Destionation.Size = new System.Drawing.Size(100, 20);
            this.Destionation.TabIndex = 5;
            this.Destionation.Text = "3,1";
            // 
            // Find_Path
            // 
            this.Find_Path.Enabled = false;
            this.Find_Path.Location = new System.Drawing.Point(32, 96);
            this.Find_Path.Name = "Find_Path";
            this.Find_Path.Size = new System.Drawing.Size(75, 23);
            this.Find_Path.TabIndex = 6;
            this.Find_Path.Text = "Find Path";
            this.Find_Path.UseVisualStyleBackColor = true;
            this.Find_Path.Click += new System.EventHandler(this.Find_Path_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1621, 1005);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MAP";
            // 
            // End_location
            // 
            this.End_location.Enabled = false;
            this.End_location.Location = new System.Drawing.Point(100, 68);
            this.End_location.Name = "End_location";
            this.End_location.Size = new System.Drawing.Size(100, 20);
            this.End_location.TabIndex = 11;
            this.End_location.Text = "3,1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "End Location:";
            // 
            // Start_location
            // 
            this.Start_location.Enabled = false;
            this.Start_location.Location = new System.Drawing.Point(100, 23);
            this.Start_location.Name = "Start_location";
            this.Start_location.Size = new System.Drawing.Size(100, 20);
            this.Start_location.TabIndex = 9;
            this.Start_location.Text = "1,1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Start Location:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Itemset:";
            // 
            // Multi_item_set
            // 
            this.Multi_item_set.Enabled = false;
            this.Multi_item_set.Location = new System.Drawing.Point(14, 145);
            this.Multi_item_set.Multiline = true;
            this.Multi_item_set.Name = "Multi_item_set";
            this.Multi_item_set.Size = new System.Drawing.Size(186, 100);
            this.Multi_item_set.TabIndex = 13;
            this.Multi_item_set.Text = "762640,1138,1329222,298112,8659";
            // 
            // LoadOrder
            // 
            this.LoadOrder.Enabled = false;
            this.LoadOrder.Location = new System.Drawing.Point(55, 403);
            this.LoadOrder.Name = "LoadOrder";
            this.LoadOrder.Size = new System.Drawing.Size(113, 23);
            this.LoadOrder.TabIndex = 14;
            this.LoadOrder.Text = "Load Order File";
            this.LoadOrder.UseVisualStyleBackColor = true;
            this.LoadOrder.Click += new System.EventHandler(this.LoadOrder_Click);
            // 
            // Find_multi_item
            // 
            this.Find_multi_item.Enabled = false;
            this.Find_multi_item.Location = new System.Drawing.Point(33, 262);
            this.Find_multi_item.Name = "Find_multi_item";
            this.Find_multi_item.Size = new System.Drawing.Size(151, 23);
            this.Find_multi_item.TabIndex = 15;
            this.Find_multi_item.Text = "Find Path Change Order";
            this.Find_multi_item.UseVisualStyleBackColor = true;
            this.Find_multi_item.Click += new System.EventHandler(this.Find_multi_item_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Clear_Path_Simply);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.Starting_Position);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Destionation);
            this.groupBox2.Controls.Add(this.Find_Path);
            this.groupBox2.Location = new System.Drawing.Point(1639, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 132);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Single item";
            // 
            // Clear_Path_Simply
            // 
            this.Clear_Path_Simply.Enabled = false;
            this.Clear_Path_Simply.Location = new System.Drawing.Point(126, 96);
            this.Clear_Path_Simply.Name = "Clear_Path_Simply";
            this.Clear_Path_Simply.Size = new System.Drawing.Size(75, 23);
            this.Clear_Path_Simply.TabIndex = 17;
            this.Clear_Path_Simply.Text = "Clear Path";
            this.Clear_Path_Simply.UseVisualStyleBackColor = true;
            this.Clear_Path_Simply.Click += new System.EventHandler(this.Clear_Path_Simply_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Find_Path_Multi_No_Change_Order);
            this.groupBox3.Controls.Add(this.Clear_Path);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.Start_location);
            this.groupBox3.Controls.Add(this.Find_multi_item);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.LoadOrder);
            this.groupBox3.Controls.Add(this.End_location);
            this.groupBox3.Controls.Add(this.Multi_item_set);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(1640, 215);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(220, 438);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Multiple items";
            // 
            // Find_Path_Multi_No_Change_Order
            // 
            this.Find_Path_Multi_No_Change_Order.Enabled = false;
            this.Find_Path_Multi_No_Change_Order.Location = new System.Drawing.Point(33, 306);
            this.Find_Path_Multi_No_Change_Order.Name = "Find_Path_Multi_No_Change_Order";
            this.Find_Path_Multi_No_Change_Order.Size = new System.Drawing.Size(151, 23);
            this.Find_Path_Multi_No_Change_Order.TabIndex = 17;
            this.Find_Path_Multi_No_Change_Order.Text = "Find Path Not Change Order";
            this.Find_Path_Multi_No_Change_Order.UseVisualStyleBackColor = true;
            this.Find_Path_Multi_No_Change_Order.Click += new System.EventHandler(this.Find_Path_Multi_No_Change_Order_Click);
            // 
            // Clear_Path
            // 
            this.Clear_Path.Enabled = false;
            this.Clear_Path.Location = new System.Drawing.Point(73, 361);
            this.Clear_Path.Name = "Clear_Path";
            this.Clear_Path.Size = new System.Drawing.Size(75, 23);
            this.Clear_Path.TabIndex = 16;
            this.Clear_Path.Text = "Clear Path";
            this.Clear_Path.UseVisualStyleBackColor = true;
            this.Clear_Path.Click += new System.EventHandler(this.Clear_Path_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1864, 1041);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.Select_File);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Select_File;
        private System.Windows.Forms.TextBox Starting_Position;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Destionation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Find_Path;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox End_location;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Start_location;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Multi_item_set;
        private System.Windows.Forms.Button LoadOrder;
        private System.Windows.Forms.Button Find_multi_item;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button Clear_Path;
        private System.Windows.Forms.Button Clear_Path_Simply;
        private System.Windows.Forms.Button Find_Path_Multi_No_Change_Order;
    }
}


﻿using DevProLauncher.Helpers;

namespace DevProLauncher.Windows
{
    sealed partial class HubGameList_frm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.SearchRequest_Btn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Host_btn = new System.Windows.Forms.Button();
            this.Quick_Btn = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.ActiveGames = new System.Windows.Forms.CheckBox();
            this.IllegalGames = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Format = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GameType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BanList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TimeLimit = new System.Windows.Forms.ComboBox();
            this.lockedChk = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UserFilter = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.maxEloTxtBox = new System.Windows.Forms.TextBox();
            this.maxEloLbl = new System.Windows.Forms.Label();
            this.minEloLbl = new System.Windows.Forms.Label();
            this.minEloTxtBox = new System.Windows.Forms.TextBox();
            this.SpectateBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.DeckSelect = new System.Windows.Forms.ComboBox();
            this.chkmate_btn = new System.Windows.Forms.Button();
            this.UpdateLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RankedList = new DevProLauncher.Windows.Components.DoubleBufferedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UnrankedList = new DevProLauncher.Windows.Components.DoubleBufferedListBox();
            this.SearchReset = new System.Windows.Forms.Timer(this.components);
            this.GameListUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.SpectateTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(915, 506);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.UpdateLabel, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(668, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 379F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(244, 500);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 373);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.SpectateBtn, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(232, 354);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.83186F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.16814F));
            this.tableLayoutPanel5.Controls.Add(this.SearchRequest_Btn, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 260);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(226, 62);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // SearchRequest_Btn
            // 
            this.SearchRequest_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchRequest_Btn.Location = new System.Drawing.Point(3, 3);
            this.SearchRequest_Btn.Name = "SearchRequest_Btn";
            this.SearchRequest_Btn.Size = new System.Drawing.Size(136, 56);
            this.SearchRequest_Btn.TabIndex = 1;
            this.SearchRequest_Btn.Text = "Search";
            this.SearchRequest_Btn.UseVisualStyleBackColor = true;
            this.SearchRequest_Btn.Click += new System.EventHandler(this.SearchRequest_Btn_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.Host_btn);
            this.flowLayoutPanel1.Controls.Add(this.Quick_Btn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(145, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(78, 56);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // Host_btn
            // 
            this.Host_btn.Location = new System.Drawing.Point(3, 3);
            this.Host_btn.Name = "Host_btn";
            this.Host_btn.Size = new System.Drawing.Size(75, 22);
            this.Host_btn.TabIndex = 0;
            this.Host_btn.Text = "Host";
            this.Host_btn.UseVisualStyleBackColor = true;
            this.Host_btn.Click += new System.EventHandler(this.Host_btn_Click);
            // 
            // Quick_Btn
            // 
            this.Quick_Btn.Location = new System.Drawing.Point(3, 31);
            this.Quick_Btn.Name = "Quick_Btn";
            this.Quick_Btn.Size = new System.Drawing.Size(75, 22);
            this.Quick_Btn.TabIndex = 1;
            this.Quick_Btn.Text = "Quick";
            this.Quick_Btn.UseVisualStyleBackColor = true;
            this.Quick_Btn.Click += new System.EventHandler(this.Quick_Btn_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.UserFilter, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel10, 0, 3);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(226, 251);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.ActiveGames, 0, 5);
            this.tableLayoutPanel8.Controls.Add(this.IllegalGames, 1, 5);
            this.tableLayoutPanel8.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.Format, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.GameType, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.BanList, 1, 3);
            this.tableLayoutPanel8.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel8.Controls.Add(this.TimeLimit, 1, 4);
            this.tableLayoutPanel8.Controls.Add(this.lockedChk, 0, 6);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 7;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(220, 152);
            this.tableLayoutPanel8.TabIndex = 2;
            // 
            // ActiveGames
            // 
            this.ActiveGames.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ActiveGames.AutoSize = true;
            this.ActiveGames.Location = new System.Drawing.Point(9, 104);
            this.ActiveGames.Name = "ActiveGames";
            this.ActiveGames.Size = new System.Drawing.Size(92, 17);
            this.ActiveGames.TabIndex = 0;
            this.ActiveGames.Text = "Active Games";
            this.ActiveGames.UseVisualStyleBackColor = true;
            // 
            // IllegalGames
            // 
            this.IllegalGames.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IllegalGames.AutoSize = true;
            this.IllegalGames.Location = new System.Drawing.Point(120, 104);
            this.IllegalGames.Name = "IllegalGames";
            this.IllegalGames.Size = new System.Drawing.Size(89, 17);
            this.IllegalGames.TabIndex = 1;
            this.IllegalGames.Text = "Illegal Games";
            this.IllegalGames.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Format";
            // 
            // Format
            // 
            this.Format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Format.FormattingEnabled = true;
            this.Format.Items.AddRange(new object[] {
            "All",
            "OCG",
            "TCG",
            "OCG/TCG"});
            this.Format.Location = new System.Drawing.Point(113, 3);
            this.Format.Name = "Format";
            this.Format.Size = new System.Drawing.Size(104, 21);
            this.Format.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Game Type";
            // 
            // GameType
            // 
            this.GameType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GameType.FormattingEnabled = true;
            this.GameType.Items.AddRange(new object[] {
            "All",
            "Single",
            "Match",
            "Tag"});
            this.GameType.Location = new System.Drawing.Point(113, 28);
            this.GameType.Name = "GameType";
            this.GameType.Size = new System.Drawing.Size(104, 21);
            this.GameType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Banlist";
            // 
            // BanList
            // 
            this.BanList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BanList.FormattingEnabled = true;
            this.BanList.Items.AddRange(new object[] {
            "All"});
            this.BanList.Location = new System.Drawing.Point(113, 53);
            this.BanList.Name = "BanList";
            this.BanList.Size = new System.Drawing.Size(104, 21);
            this.BanList.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Time Limit";
            // 
            // TimeLimit
            // 
            this.TimeLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TimeLimit.FormattingEnabled = true;
            this.TimeLimit.Items.AddRange(new object[] {
            "All",
            "3 minutes",
            "5 minutes"});
            this.TimeLimit.Location = new System.Drawing.Point(113, 78);
            this.TimeLimit.Name = "TimeLimit";
            this.TimeLimit.Size = new System.Drawing.Size(104, 21);
            this.TimeLimit.TabIndex = 9;
            // 
            // lockedChk
            // 
            this.lockedChk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lockedChk.AutoSize = true;
            this.lockedChk.Location = new System.Drawing.Point(24, 130);
            this.lockedChk.Name = "lockedChk";
            this.lockedChk.Size = new System.Drawing.Size(62, 17);
            this.lockedChk.TabIndex = 10;
            this.lockedChk.Text = "Locked";
            this.lockedChk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Filter";
            // 
            // UserFilter
            // 
            this.UserFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserFilter.Location = new System.Drawing.Point(3, 181);
            this.UserFilter.Name = "UserFilter";
            this.UserFilter.Size = new System.Drawing.Size(220, 20);
            this.UserFilter.TabIndex = 3;
            this.UserFilter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.54546F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.45454F));
            this.tableLayoutPanel10.Controls.Add(this.maxEloTxtBox, 1, 1);
            this.tableLayoutPanel10.Controls.Add(this.maxEloLbl, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.minEloLbl, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.minEloTxtBox, 0, 1);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 206);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.5F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(220, 42);
            this.tableLayoutPanel10.TabIndex = 4;
            // 
            // maxEloTxtBox
            // 
            this.maxEloTxtBox.Location = new System.Drawing.Point(110, 20);
            this.maxEloTxtBox.Margin = new System.Windows.Forms.Padding(1, 3, 0, 3);
            this.maxEloTxtBox.Name = "maxEloTxtBox";
            this.maxEloTxtBox.Size = new System.Drawing.Size(106, 20);
            this.maxEloTxtBox.TabIndex = 3;
            // 
            // maxEloLbl
            // 
            this.maxEloLbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.maxEloLbl.AutoSize = true;
            this.maxEloLbl.Location = new System.Drawing.Point(112, 2);
            this.maxEloLbl.Name = "maxEloLbl";
            this.maxEloLbl.Size = new System.Drawing.Size(41, 13);
            this.maxEloLbl.TabIndex = 1;
            this.maxEloLbl.Text = "maxElo";
            // 
            // minEloLbl
            // 
            this.minEloLbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.minEloLbl.AutoSize = true;
            this.minEloLbl.Location = new System.Drawing.Point(3, 2);
            this.minEloLbl.Name = "minEloLbl";
            this.minEloLbl.Size = new System.Drawing.Size(38, 13);
            this.minEloLbl.TabIndex = 0;
            this.minEloLbl.Text = "minElo";
            // 
            // minEloTxtBox
            // 
            this.minEloTxtBox.Location = new System.Drawing.Point(0, 20);
            this.minEloTxtBox.Margin = new System.Windows.Forms.Padding(0, 3, 1, 3);
            this.minEloTxtBox.Name = "minEloTxtBox";
            this.minEloTxtBox.Size = new System.Drawing.Size(108, 20);
            this.minEloTxtBox.TabIndex = 2;
            // 
            // SpectateBtn
            // 
            this.SpectateBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpectateBtn.Location = new System.Drawing.Point(3, 328);
            this.SpectateBtn.Name = "SpectateBtn";
            this.SpectateBtn.Size = new System.Drawing.Size(226, 23);
            this.SpectateBtn.TabIndex = 2;
            this.SpectateBtn.Text = "Random Spectate";
            this.SpectateBtn.UseVisualStyleBackColor = true;
            this.SpectateBtn.Click += new System.EventHandler(this.SpectateBtn_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkmate_btn, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.77778F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.22222F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(238, 63);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.91597F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.08403F));
            this.tableLayoutPanel9.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.DeckSelect, 1, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(232, 27);
            this.tableLayoutPanel9.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 27);
            this.label6.TabIndex = 0;
            this.label6.Text = "Default Deck";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeckSelect
            // 
            this.DeckSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DeckSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeckSelect.FormattingEnabled = true;
            this.DeckSelect.Location = new System.Drawing.Point(95, 3);
            this.DeckSelect.Name = "DeckSelect";
            this.DeckSelect.Size = new System.Drawing.Size(134, 21);
            this.DeckSelect.TabIndex = 1;
            // 
            // chkmate_btn
            // 
            this.chkmate_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkmate_btn.Location = new System.Drawing.Point(3, 36);
            this.chkmate_btn.Name = "chkmate_btn";
            this.chkmate_btn.Size = new System.Drawing.Size(232, 24);
            this.chkmate_btn.TabIndex = 5;
            this.chkmate_btn.Text = "Checkmate Server";
            this.chkmate_btn.UseVisualStyleBackColor = true;
            this.chkmate_btn.Click += new System.EventHandler(this.chkmate_btn_Click);
            // 
            // UpdateLabel
            // 
            this.UpdateLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.UpdateLabel.AutoSize = true;
            this.UpdateLabel.Location = new System.Drawing.Point(69, 108);
            this.UpdateLabel.Name = "UpdateLabel";
            this.UpdateLabel.Size = new System.Drawing.Size(106, 13);
            this.UpdateLabel.TabIndex = 2;
            this.UpdateLabel.Text = "Status: Not Updating";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(659, 500);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RankedList);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(332, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(324, 494);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ranked";
            // 
            // RankedList
            // 
            this.RankedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RankedList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RankedList.FormattingEnabled = true;
            this.RankedList.IntegralHeight = false;
            this.RankedList.ItemHeight = 50;
            this.RankedList.Location = new System.Drawing.Point(3, 16);
            this.RankedList.Name = "RankedList";
            this.RankedList.Size = new System.Drawing.Size(318, 475);
            this.RankedList.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UnrankedList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 494);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unranked";
            // 
            // UnrankedList
            // 
            this.UnrankedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UnrankedList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.UnrankedList.FormattingEnabled = true;
            this.UnrankedList.IntegralHeight = false;
            this.UnrankedList.ItemHeight = 50;
            this.UnrankedList.Location = new System.Drawing.Point(3, 16);
            this.UnrankedList.Name = "UnrankedList";
            this.UnrankedList.Size = new System.Drawing.Size(317, 475);
            this.UnrankedList.TabIndex = 0;
            // 
            // SearchReset
            // 
            this.SearchReset.Interval = 1000;
            // 
            // GameListUpdateTimer
            // 
            this.GameListUpdateTimer.Interval = 1000;
            // 
            // SpectateTimer
            // 
            this.SpectateTimer.Interval = 1000;
            // 
            // HubGameList_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 506);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HubGameList_frm";
            this.Text = "HubGameList_frm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.GroupBox groupBox3;
        private Components.DoubleBufferedListBox RankedList;
        private System.Windows.Forms.GroupBox groupBox1;
        private Components.DoubleBufferedListBox UnrankedList;
        private System.Windows.Forms.Timer SearchReset;
        private System.Windows.Forms.Label UpdateLabel;
        private System.Windows.Forms.Timer GameListUpdateTimer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button SearchRequest_Btn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button Host_btn;
        private System.Windows.Forms.Button Quick_Btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox DeckSelect;
        private System.Windows.Forms.Button chkmate_btn;
        private System.Windows.Forms.Button SpectateBtn;
        private System.Windows.Forms.Timer SpectateTimer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Format;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox GameType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox BanList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UserFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TextBox maxEloTxtBox;
        private System.Windows.Forms.Label maxEloLbl;
        private System.Windows.Forms.Label minEloLbl;
        private System.Windows.Forms.TextBox minEloTxtBox;
        private System.Windows.Forms.CheckBox ActiveGames;
        private System.Windows.Forms.CheckBox IllegalGames;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox TimeLimit;
        private System.Windows.Forms.CheckBox lockedChk;
    }
}
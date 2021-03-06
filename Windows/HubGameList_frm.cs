﻿using DevProLauncher.Config;
using DevProLauncher.Helpers;
using DevProLauncher.Network.Data;
using DevProLauncher.Network.Enums;
using DevProLauncher.Windows.MessageBoxs;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DevProLauncher.Windows
{
    public sealed partial class HubGameList_frm : Form
    {
        private readonly Dictionary<string, RoomInfos> m_rooms = new Dictionary<string, RoomInfos>();
        private int timer;

        public HubGameList_frm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;

            Format.SelectedIndex = 0;
            GameType.SelectedIndex = 0;
            BanList.SelectedIndex = 0;
            TimeLimit.SelectedIndex = 0;

            BanList.Items.AddRange(LauncherHelper.GetBanListArray());

            Program.ChatServer.AddRooms += OnRoomsList;
            Program.ChatServer.CreateRoom += OnRoomCreate;
            Program.ChatServer.RemoveRoom += OnRoomRemoved;
            Program.ChatServer.UpdateRoomStatus += OnRoomStarted;
            Program.ChatServer.UpdateRoomPlayers += OnRoomPlayersUpdate;
            Program.ChatServer.MatchFound += OnMatchFound;
            Program.ChatServer.MatchCanceled += OnMatchCancel;
            Program.ChatServer.MatchStart += OnMatchStart;
            RankedList.DrawItem += GameListBox_DrawItem;
            UnrankedList.DrawItem += GameListBox_DrawItem;
            UnrankedList.DoubleClick += LoadRoom;
            RankedList.DoubleClick += LoadRoom;

            SearchReset.Tick += ResetSearch;
            QueueTimer.Tick += Timer;
            SpectateTimer.Tick += ResetSpectate;
            GameListUpdateTimer.Tick += UpdateGameListTimer;
            DevBotTimer.Tick += ResetDevBot;
            timer = 0;

            RefreshDeckList();
            LauncherHelper.DeckEditClosed += RefreshDeckList;
            DeckSelect.SelectedIndexChanged += DeckSelect_SelectedValueChanged;

            ApplyTranslation();
            LoadSettings();
        }

        public void SaveSettings()
        {
            Program.Config.SearchFormat = Format.SelectedIndex;
            Program.Config.SearchMode = GameType.SelectedIndex;
            Program.Config.SearchBanList = BanList.SelectedIndex;
            Program.Config.SearchTimeLimit = TimeLimit.SelectedIndex;
            Program.Config.SearchActive = ActiveGames.Checked;
            Program.Config.SearchIllegal = IllegalGames.Checked;
            Program.Config.SearchLocked = lockedChk.Checked;
            Program.Config.SearchMinElo = minEloTxtBox.Text;
            Program.Config.SearchMaxElo = maxEloTxtBox.Text;

            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
        }

        public void LoadSettings()
        {
            Format.SelectedIndex = Program.Config.SearchFormat;
            GameType.SelectedIndex = Program.Config.SearchMode;
            BanList.SelectedIndex = Program.Config.SearchBanList;
            TimeLimit.SelectedIndex = Program.Config.SearchTimeLimit;
            ActiveGames.Checked = Program.Config.SearchActive;
            IllegalGames.Checked = Program.Config.SearchIllegal;
            lockedChk.Checked = Program.Config.SearchLocked;
            minEloTxtBox.Text = Program.Config.SearchMinElo;
            maxEloTxtBox.Text = Program.Config.SearchMaxElo;
        }

        public void ApplyTranslation()
        {
            LanguageInfo info = Program.LanguageManager.Translation;

            groupBox1.Text = info.GameUnranked;
            groupBox3.Text = info.GameRanked;
            groupBox2.Text = info.GameSearch;
            label6.Text = info.GameDefaultDeck;
            label4.Text = info.GameFormat;
            label3.Text = info.GameType;
            label2.Text = info.GameBanList;
            label5.Text = info.GameTimeLimit;
            ActiveGames.Text = info.GameActive;
            IllegalGames.Text = info.GameIlligal;
            lockedChk.Text = info.GameLocked;
            label1.Text = info.GameUserFilter;
            minEloLbl.Text = info.GameMinElo;
            maxEloLbl.Text = info.GameMaxElo;
            SearchRequest_Btn.Text = info.GameBtnSearch;
            Host_btn.Text = info.GameBtnHost;
            Quick_Btn.Text = info.GameBtnQuick;
            UpdateLabel.Text = info.GameNotUpdating;
            SpectateBtn.Text = info.GameSpectate;
            CheckmateBtn.Text = info.GameCheckmate;

            Format.Items[0] = info.GameAll;

            GameType.Items[0] = info.GameAll;
            GameType.Items[1] = info.GameSingle;
            GameType.Items[2] = info.GameMatch;
            GameType.Items[3] = info.GameTag;

            BanList.Items[0] = info.GameAll;

            TimeLimit.Items[0] = info.GameAll;
            TimeLimit.Items[1] = "2 " + info.GameMinutes;
            TimeLimit.Items[2] = "1 " + info.GameMinutes;

            joinBtn.Text = info.GameJoin;
            leaveBtn.Text = info.GameLeave;
            qJoinBtn.Text = info.GameQuick;
            rankedLbl.Text = info.Ranked;
            unrankedLbl.Text = info.Unranked;
            QueueLabel.Text = info.GameQueueLabel;
        }

        public void RefreshDeckList()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(RefreshDeckList));
            }
            else
            {
                DeckSelect.Items.Clear();
                if (Directory.Exists(Program.Config.LauncherDir + "deck/"))
                {
                    string[] decks = Directory.GetFiles(Program.Config.LauncherDir + "deck/");
                    foreach (string deck in decks)
                        // ReSharper disable AssignNullToNotNullAttribute
                        DeckSelect.Items.Add(Path.GetFileNameWithoutExtension(deck));
                    // ReSharper restore AssignNullToNotNullAttribute
                }
                DeckSelect.Text = Program.Config.DefaultDeck;
            }
        }

        private void DeckSelect_SelectedValueChanged(object sender, EventArgs e)
        {
            Program.Config.DefaultDeck = DeckSelect.SelectedItem.ToString();
            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
        }

        private void UpdateGameListTimer(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(UpdateGameListTimer), sender, e);
                return;
            }

            string[] parts = UpdateLabel.Text.Split(' ');
            int value = Int32.Parse(parts[parts.Length - 2]);

            if (value == 0)
            {
                UpdateLabel.Text = Program.LanguageManager.Translation.GameNotUpdating;
                GameListUpdateTimer.Enabled = false;
                RankedList.Items.Clear();
                UnrankedList.Items.Clear();
            }
            else
            {
                UpdateLabel.Text = Program.LanguageManager.Translation.GameUpdating1 + (value - 1) + Program.LanguageManager.Translation.GameUpdating2;
            }
        }

        private void ResetSearch(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(ResetSearch), sender, e);
                return;
            }

            if (SearchRequest_Btn.Text == "1")
            {
                SearchRequest_Btn.Enabled = true;
                SearchRequest_Btn.Text = Program.LanguageManager.Translation.GameBtnSearch;
                SearchReset.Enabled = false;
            }
            else
            {
                int value = Int32.Parse(SearchRequest_Btn.Text);
                SearchRequest_Btn.Text = (value - 1).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void Timer(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(Timer), sender, e);
                return;
            }
            timer++;
            QueueLabel.Text = "Queue Status: Searching for " + timer + " seconds";
        }

        private void ResetSpectate(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(ResetSpectate), sender, e);
                return;
            }

            if (SpectateBtn.Text == "1")
            {
                SpectateBtn.Enabled = true;
                SpectateBtn.Text = Program.LanguageManager.Translation.GameSpectate;
                SpectateTimer.Enabled = false;
            }
            else
            {
                int value = Int32.Parse(SpectateBtn.Text);
                SpectateBtn.Text = (value - 1).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void ResetDevBot(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(ResetSpectate), sender, e);
                return;
            }

            if (DevBotBtn.Text == "1")
            {
                DevBotBtn.Enabled = true;
                DevBotBtn.Text = Program.LanguageManager.Translation.GameDuelDevBot;
                DevBotTimer.Enabled = false;
            }
            else
            {
                int value = Int32.Parse(DevBotBtn.Text);
                DevBotBtn.Text = (value - 1).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void RemoveServerRooms(string server)
        {
            List<string> removerooms = new List<string>();

            foreach (string room in m_rooms.Keys)
            {
                if (m_rooms.ContainsKey(room))
                {
                    if (m_rooms[room].server == server)
                        removerooms.Add(room);
                }
            }

            foreach (string removeroom in removerooms)
            {
                if (m_rooms.ContainsKey(removeroom))
                    m_rooms.Remove(removeroom);
            }

            OnRoomsList(m_rooms.Values.ToArray());
        }

        public void OnRoomsList(RoomInfos[] rooms)
        {
            Invoke(new Action<RoomInfos[]>(InternalRoomsList), new object[] { rooms });
        }

        private void InternalRoomsList(RoomInfos[] rooms)
        {
            m_rooms.Clear();
            UnrankedList.Items.Clear();
            RankedList.Items.Clear();
            foreach (RoomInfos room in rooms)
            {
                InternalRoomCreated(room);
            }
            UpdateLabel.Text = Program.LanguageManager.Translation.GameUpdating1 + 60 + Program.LanguageManager.Translation.GameUpdating2;
            GameListUpdateTimer.Enabled = true;
        }

        public void OnRoomCreated(RoomInfos[] room)
        {
            Invoke(new Action<RoomInfos>(InternalRoomCreated), room[0]);
        }

        private void InternalRoomCreated(RoomInfos room)
        {
            string roomname = room.GetRoomName();
            if (m_rooms.ContainsKey(roomname))
                return;
            m_rooms.Add(roomname, room);
            ListBox rooms = (room.isRanked ? RankedList : UnrankedList);

            //Remove DevBot games
            if (!room.playerList[0].Trim().ToLower().Equals("devbot"))
                rooms.Items.Add(roomname);
        }

        public List<object> ObjectKeys()
        {
            return m_rooms.Keys.Cast<object>().ToList();
        }

        public void OnRoomStarted(string roomname)
        {
            Invoke(new Action<string>(InternalRoomStarted), roomname);
        }

        private void InternalRoomStarted(string roomname)
        {
            if (!m_rooms.ContainsKey(roomname)) return;

            RoomInfos item = m_rooms[roomname];
            ListBox rooms = (item.isRanked ? RankedList : UnrankedList);
            item.hasStarted = true;
            if (!ActiveGames.Checked)
                rooms.Items.Remove(roomname);
        }

        public void OnRoomRemoved(string roomname)
        {
            Invoke(new Action<string>(InternalRoomRemoved), roomname);
        }

        private void InternalRoomRemoved(string roomname)
        {
            if (!m_rooms.ContainsKey(roomname)) return;
            RoomInfos room = m_rooms[roomname];
            if (room.isRanked)
                RankedList.Items.Remove(roomname);
            else
                UnrankedList.Items.Remove(roomname);
            m_rooms.Remove(roomname);
        }

        public void OnRoomCreate(RoomInfos room)
        {
            if (!m_rooms.ContainsKey(room.GetRoomName()))
            {
                Invoke(new Action<RoomInfos>(InternalRoomCreated), room);
            }
        }

        public void OnRoomPlayersUpdate(PacketCommand data)
        {
            if (m_rooms.ContainsKey(data.Command))
            {
                string[] parts = data.Data.Split('|');
                string[] eloparts = parts[1].Split(',');
                string[] players = parts[0].Split(',');
                List<int> elos = new List<int>();
                foreach (string elo in eloparts)
                {
                    int value = 1200;
                    try
                    {
                        value = int.Parse(elo);
                    }
                    catch
                    {
                        //MessageBox.Show("Error in parsing Elo from:" + eloparts.ToString());
                    }
                    elos.Add(value);
                }

                Invoke(new Action<string, string[], int[]>(InternalRoomPlayersUpdate), data.Command, (object)players, (object)elos.ToArray());
            }
        }

        private void InternalRoomPlayersUpdate(string room, string[] players, int[] elos)
        {
            string roomname = room;
            if (!m_rooms.ContainsKey(roomname)) return;
            RoomInfos item = m_rooms[roomname];

            item.playerList = players;
            item.eloList = elos;

            if (item.isRanked)
                RankedList.UpdateList();
            else
                UnrankedList.UpdateList();
        }

        private void SearchRequest_Btn_Click(object sender, EventArgs e)
        {
            uint min = 0, max = 9999;
            try
            {
                min = uint.Parse(minEloTxtBox.Text);
                max = uint.Parse(maxEloTxtBox.Text);
            }
            catch (Exception exc)
            {
                // safe to ignore as it switches to default values
                // MessageBox.Show("Not a valid Elo Number (0-9999)." +min.ToString());
            }
            Program.ChatServer.SendPacket(DevServerPackets.GameList, JsonSerializer.SerializeToString(
                new SearchRequest(
                    (Format.SelectedIndex == -1 ? Format.SelectedIndex : Format.SelectedIndex - 1),
                    (GameType.SelectedIndex == -1 ? GameType.SelectedIndex : GameType.SelectedIndex - 1),
                    (BanList.SelectedIndex == -1 ? BanList.SelectedIndex : BanList.SelectedIndex - 1),
                    (TimeLimit.SelectedIndex == -1 ? TimeLimit.SelectedIndex : TimeLimit.SelectedIndex - 1),
                    ActiveGames.Checked, IllegalGames.Checked, lockedChk.Checked, UserFilter.Text,
                    min, max
                    )));
            SearchRequest_Btn.Enabled = false;
            SearchRequest_Btn.Text = "5";
            SearchReset.Enabled = true;

            SaveSettings();
        }

        private void Host_btn_Click(object sender, EventArgs e)
        {
            HostGame();
            //HostBtn_MouseUp(sender, new MouseEventArgs(MouseButtons.Right, 1, 1, 1, 1));
        }

        private void HostGame()
        {
            var form = new Host(false);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (m_rooms.ContainsKey(form.PasswordInput.Text))
                {
                    MessageBox.Show(Program.LanguageManager.Translation.GamePasswordExsists);
                    return;
                }

                ServerInfo server = form.CardRules.Text == "2099" ? GetServer(Program.Config.Server2099) : GetServer(false);

                if (server != null)
                {
                    LauncherHelper.GenerateConfig(server, form.GenerateURI(false));
                    LauncherHelper.RunGame("-j");
                    Program.ChatServer.SendPacket(DevServerPackets.HostDuel);
                }
                else
                {
                    MessageBox.Show(Program.LanguageManager.Translation.GameNoServers);
                    return;
                }
            }
        }

        private void QuickBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || sender is Button)
            {
                LanguageInfo info = Program.LanguageManager.Translation;
                var mnu = new ContextMenuStrip();

                var mnuSingle = new ToolStripMenuItem("Single") { Name = "Single" };
                var mnuMatch = new ToolStripMenuItem("Match") { Name = "Match" };
                var mnuTag = new ToolStripMenuItem("Tag") { Name = "Tag" };

                mnuSingle.Click += QuickHostItem_Click;
                mnuMatch.Click += QuickHostItem_Click;
                mnuTag.Click += QuickHostItem_Click;

                mnu.Items.AddRange(new ToolStripItem[] { mnuSingle, mnuMatch, mnuTag });

                mnu.DefaultDropDownDirection = ToolStripDropDownDirection.BelowRight;
                mnu.Show((Button)sender, new Point(0, 0 - mnu.Height));
            }
        }

        private void QuickHostItem_Click(object sender, EventArgs e)
        {
            var button = (ToolStripItem)sender;
            QuickHost((button.Name.StartsWith("R")) ? button.Name.Substring(1) : button.Name, (button.Name.StartsWith("R")));
        }

        private void QuickHost(string mode, bool isranked)
        {
            var ran = new Random();
            var form = new Host(false)
            {
                CardRules = { Text = Program.Config.CardRules },
                Mode = { Text = mode },
                Priority = { Checked = Program.Config.EnablePrority },
                CheckDeck = { Checked = Program.Config.DisableCheckDeck },
                ShuffleDeck = { Checked = Program.Config.DisableShuffleDeck },
                LifePoints = { Text = Program.Config.Lifepoints },
                GameName = LauncherHelper.GenerateString().Substring(0, 5),
                BanList = { SelectedItem = Program.Config.BanList },
                TimeLimit = { SelectedItem = Program.Config.TimeLimit }
            };

            ListBox list = (isranked) ? RankedList : UnrankedList;

            if (isranked)
            {
                form.BanList.SelectedIndex = 0;
                form.Prerelease.Checked = false;
                form.CheckDeck.Checked = false;
                form.ShuffleDeck.Checked = false;
                form.Priority.Checked = false;
                form.CardRules.SelectedIndex = 2;
                form.LifePoints.Text = form.Mode.Text == "Tag" ? "16000" : "8000";
            }
            else
            {
                if (Program.Config.Lifepoints != ((mode == "Tag") ? "16000" : "8000"))
                {
                    if (MessageBox.Show(Program.LanguageManager.Translation.GameLPChange, Program.LanguageManager.Translation.hostLifep, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        form.LifePoints.Text = mode == "Tag" ? "16000" : "8000";
                    }
                }
            }

            RoomInfos userinfo = RoomInfos.FromName(form.GenerateURI(isranked));

            var matchedRooms = (from object room in list.Items where m_rooms.ContainsKey(room.ToString()) select m_rooms[room.ToString()] into info where RoomInfos.CompareRoomInfo(userinfo, info) select info).ToList();
            string serverName = string.Empty;
            if (matchedRooms.Count > 0)
            {
                var selectroom = ran.Next(matchedRooms.Count);
                form.GameName = matchedRooms[selectroom].roomName;
                serverName = matchedRooms[selectroom].server;
            }

            // ygo 2099 specific format
            if (form.CardRules.SelectedIndex == 3)
            {
                serverName = Program.Config.Server2099;
            }

            ServerInfo server;
            if (string.IsNullOrEmpty(serverName))
                server = GetServer();
            else
                server = GetServer(serverName);

            if (server == null)
            {
                MessageBox.Show(Program.LanguageManager.Translation.GameNoServers);
                return;
            }

            LauncherHelper.GenerateConfig(server, form.GenerateURI(isranked));
            LauncherHelper.RunGame("-j");
        }

        public void LoadRoom(object sender, EventArgs e)
        {
            var rooms = (ListBox)sender;
            if (rooms.SelectedIndex == -1)
                return;
            if (!m_rooms.ContainsKey(rooms.SelectedItem.ToString()))
                return;

            RoomInfos item = m_rooms[rooms.SelectedItem.ToString()];

            if (item.isRanked && !item.hasStarted)
            {
                MessageBox.Show("Cannot manually join a ranked game.");
                return;
            }

            if (item.isLocked)
            {
                var form = new InputFrm(string.Empty, Program.LanguageManager.Translation.GameEnterPassword, Program.LanguageManager.Translation.QuickHostBtn, Program.LanguageManager.Translation.optionBtnCancel)
                {
                    InputBox = { MaxLength = 4 }
                };
                if (!item.hasStarted)
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (form.InputBox.Text != item.roomName)
                        {
                            MessageBox.Show(Program.LanguageManager.Translation.GameWrongPassword);
                            return;
                        }
                    }
                    else
                        return;
                }
            }

            if (Program.ServerList.ContainsKey(item.server))
            {
                LauncherHelper.GenerateConfig(Program.ServerList[item.server], item.ToName());
                LauncherHelper.RunGame("-j");
            }
        }

        public ServerInfo GetServer(bool official = true)
        {
            Dictionary<string, ServerInfo> serverList;

            if (official)
            {
                serverList = Program.ServerList;
            }
            else
            {
                serverList = Program.ServerList3P;
                if (serverList.Count == 0)
                {
                    serverList = Program.ServerList;
                }
            }

            List<ServerInfo> tempList = new List<ServerInfo>();

            foreach (var KVP in serverList)
            {
                if (KVP.Value.status == ServerInfo.Status.Open)
                {
                    tempList.Add(KVP.Value);
                }
            }

            if (tempList.Count == 0)
                return null;

            int serverselect = Program.Rand.Next(0, tempList.Count);
            return tempList.ElementAt(serverselect);
        }

        public ServerInfo GetServer(string serverName)
        {
            ServerInfo server = null;

            if (serverName.Contains("DevServer") && Program.ServerList.ContainsKey(serverName))
            {
                server = Program.ServerList[serverName];
            }
            else if (Program.ServerList3P.ContainsKey(serverName))
            {
                server = Program.ServerList3P[serverName];
            }

            if (server == null)
            {
                MessageBox.Show(Program.LanguageManager.Translation.GameNoServers);
            }

            return server;
        }

        private void GameListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var list = (ListBox)sender;
            e.DrawBackground();

            if (e.Index == -1)
                return;
            var index = e.Index;

            var room = list.Items[index].ToString();
            var selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            var g = e.Graphics;
            RoomInfos info = null;
            if (m_rooms.ContainsKey(room))
                info = m_rooms[room];

            //item info

            string playerstring;

            if (info == null)
            {
                playerstring = "??? vs ???";
            }
            else
            {
                bool istag = (info.mode == 2);
                string[] players = info.playerList;

                if (players.Length == 0)
                {
                    playerstring = "??? vs ???";
                }
                else
                {
                    if (istag)
                    {
                        string player1 = players[0].Trim() + " (" + info.eloList[0].ToString() + ")";
                        string player2 = (players.Length > 1) ? players[1].Trim() + " (" + info.eloList[1].ToString() + ")" : "???";
                        string player3 = (players.Length > 2) ? players[2].Trim() + " (" + info.eloList[2].ToString() + ")" : "???";
                        string player4 = (players.Length > 3) ? players[3].Trim() + " (" + info.eloList[3].ToString() + ")" : "???";
                        playerstring = player1 + ", " + player2 + " vs " + player3 + ", " + player4;
                    }
                    else
                    {
                        string player1 = players[0].Trim() + " (" + info.eloList[0].ToString() + ")";
                        string player2 = (players.Length > 1) ? players[1].Trim() + " (" + info.eloList[1].ToString() + ")" : "???";
                        playerstring = player1 + " vs " + player2;
                    }
                }
            }
            var bounds = list.GetItemRectangle(index);
            var rulesize = e.Graphics.MeasureString((info == null) ? "???" : RoomInfos.GameRule(info.rule), e.Font);
            var playersSize = e.Graphics.MeasureString(playerstring, e.Font);
            var lockedsize = e.Graphics.MeasureString((info == null) ? "???" : (info.isLocked ? Program.LanguageManager.Translation.GameLocked : Program.LanguageManager.Translation.GameOpen), e.Font);
            SolidBrush backgroundcolor;

            var offset = new Size(5, 5);

            if (info == null)
            {
                backgroundcolor = new SolidBrush(Color.Red);
            }
            else
            {
                backgroundcolor = new SolidBrush(info.hasStarted ? Color.LightGray :
                (info.isIllegal ? Color.LightCoral :
                (info.mode == 2 ? Color.LightGreen :
                (info.mode == 1 ? Color.BlanchedAlmond :
                Color.LightBlue))));
            }
            //draw item
            g.FillRectangle(backgroundcolor, e.Bounds);
            g.DrawLines((selected) ? new Pen(Brushes.Purple, 5) : new Pen(Brushes.Black, 5),
                new[] { new Point(bounds.X, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height), new Point(bounds.X, bounds.Y + bounds.Height), new Point(bounds.X, bounds.Y) });
            //toplet
            g.DrawString((info == null) ? "???/???/???" : RoomInfos.GameMode(info.mode) + " / " + LauncherHelper.GetBanListFromInt(info.banListType) + " / " + (info.timer == 0 ? "3 mins" : "5 mins") + (info.rule >= 4 ? "/Prerelease" : ""), e.Font, Brushes.Black,
                list.GetItemRectangle(index).Location + offset);
            //topright
            g.DrawString((info == null) ? "???" : RoomInfos.GameRule(info.rule), e.Font, Brushes.Black,
                new Rectangle(bounds.X + (bounds.Width - (int)rulesize.Width) - offset.Width, bounds.Y + offset.Height, bounds.Width, bounds.Height));
            ////bottomright
            g.DrawString((info == null) ? "???" : (info.isLocked ? Program.LanguageManager.Translation.GameLocked : Program.LanguageManager.Translation.GameOpen),
                e.Font, Brushes.Black,
                new Rectangle(bounds.X + (bounds.Width - (int)lockedsize.Width) - offset.Width, bounds.Y + (bounds.Height - (int)lockedsize.Height) - offset.Height, bounds.Width, bounds.Height));
            //bottom center
            g.DrawString(playerstring, e.Font, Brushes.Black,
                new Rectangle(bounds.X + ((bounds.Width / 2) - ((int)playersSize.Width / 2)), bounds.Y + (bounds.Height - (int)playersSize.Height) - offset.Height, bounds.Width, bounds.Height));
            e.DrawFocusRectangle();
        }

        private void Quick_Btn_Click(object sender, EventArgs e)
        {
            QuickBtn_MouseUp(sender, new MouseEventArgs(MouseButtons.Right, 1, 1, 1, 1));
        }

        private void SpectateBtn_Click(object sender, EventArgs e)
        {
            Program.ChatServer.SendPacket(DevServerPackets.RandomSpectate);
            SpectateBtn.Enabled = false;
            SpectateBtn.Text = "5";
            SpectateTimer.Enabled = true;
        }

        private bool JoinQueue(bool isQuick = false)
        {
            var form = new Host(true);

            form.Mode.Items.Clear();
            form.HostBtn.Text = "Join Queue";
            form.Mode.Items.AddRange(new object[] { "Single", "Match" });
            form.Mode.SelectedItem = form.Mode.Items.Contains(Program.Config.Mode) ? Program.Config.Mode : "Match";
            if (form.BanList.Items.Count > 0)
                form.BanList.SelectedIndex = 0;
            form.CardRules.SelectedIndexChanged += form.FormatChanged;

            form.CardRules.Items.Clear();
            form.CardRules.Items.AddRange(new object[] { "TCG", "OCG" });
            form.CardRules.SelectedItem = form.CardRules.Items.Contains(Program.Config.CardRules) ? Program.Config.CardRules : "TCG";

            if (isQuick || form.ShowDialog() == DialogResult.OK)
            {
                QueueRequest request = new QueueRequest(form.CardRules.SelectedItem.ToString(), form.Mode.SelectedItem.ToString());

                Program.ChatServer.SendPacket(DevServerPackets.JoinQueue, JsonSerializer.SerializeToString(request));
                QueueLabel.Text = "Queue Status: searching";
                return true;
            }
            return false;
        }

        public void OnMatchFound(string matchnumber)
        {
            var form = new DuelRequestFrm(
            "Found a Match, are you ready?", true);

            if (Program.Config.EnableLauncherSound)
            {
                try
                {
                    System.Media.SoundPlayer simpleSound = new System.Media.SoundPlayer(@"Sound\matchfound.wav");
                    simpleSound.Play();
                }
                catch
                {
                    //ignore
                }
            }

            if (form.ShowDialog() == DialogResult.Yes)
            {
                Program.ChatServer.SendPacket(DevServerPackets.AcceptMatch, matchnumber);
            }
            else
            {
                Program.ChatServer.SendPacket(DevServerPackets.RefuseMatch, matchnumber);
                ResetQueue();
            }
        }

        public void OnMatchCancel(string data)
        {
            MessageBox.Show(Program.LanguageManager.Translation.GameMatchCancel + "(" + data + ")");
            ResetQueue();
        }

        public void OnMatchStart(DuelRequest request)
        {
            ServerInfo server = null;
            if (Program.ServerList.ContainsKey(request.server))
                server = Program.ServerList[request.server];
            if (server != null)
            {
                LauncherHelper.GenerateConfig(server, request.duelformatstring, 1);
                LauncherHelper.RunGame("-f");
            }
            ResetQueue();
        }

        private void joinBtn_Click(object sender, EventArgs e)
        {
            if (JoinQueue())
            {
                QueueTimer.Enabled = true;
                joinBtn.Enabled = false;
                qJoinBtn.Enabled = false;
                leaveBtn.Enabled = true;
            }
        }

        private void LeaveBtn_Click(object sender, EventArgs e)
        {
            Program.ChatServer.SendPacket(DevServerPackets.LeaveQueue);
            ResetQueue();
        }

        private void ResetQueue(/*object sender, EventArgs e*/)
        {
            //stub
            if (InvokeRequired)
            {
                Invoke(new Action(ResetQueue));
                return;
            }
            QueueTimer.Enabled = false;
            timer = 0;
            QueueLabel.Text = Program.LanguageManager.Translation.GameQueueLabel;
            joinBtn.Enabled = true;
            qJoinBtn.Enabled = true;
            leaveBtn.Enabled = false;
        }

        private void qJoinBtn_Click(object sender, EventArgs e)
        {
            if (JoinQueue(true))
            {
                QueueTimer.Enabled = true;
                joinBtn.Enabled = false;
                qJoinBtn.Enabled = false;
                leaveBtn.Enabled = true;
            }
        }

        private void Duel_DevBot_Click(object sender, EventArgs e)
        {
            LauncherHelper.RunGame("-ai");
        }

        private void CheckmateBtn_Click(object sender, EventArgs e)
        {
            ServerInfo server = new ServerInfo("Checkmate", "45.33.106.116", 21001);
            LauncherHelper.GenerateConfig(server, "");
            LauncherHelper.RunGame("-j");
        }
    }
}
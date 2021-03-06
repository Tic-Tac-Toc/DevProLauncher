﻿using DevProLauncher.Network.Data;
using DevProLauncher.Network.Enums;
using DevProLauncher.Windows.Enums;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DevProLauncher.Network
{
    public class ChatClient
    {
        private bool m_isConnected;
        private TcpClient m_client;
        private BinaryReader m_reader;
        private Thread m_clientThread;
        private Thread m_parseThread;
        private DateTime m_pingRequest;
        private Queue<MessageReceived> m_receivedQueue;
        private Queue<byte[]> m_sendQueue;

        public delegate void ServerResponse(string message);

        public delegate void Command(PacketCommand command);

        public delegate void ClientPacket(DevClientPackets packet);

        public delegate void LoginResponse(DevClientPackets type, LoginData data);

        public delegate void ServerRooms(RoomInfos[] rooms);

        public delegate void GameRoomUpdate(RoomInfos room);

        public delegate void ServerDisconnected();

        public delegate void Message(ChatMessage message);

        public delegate void UserDuelRequest(DuelRequest data);

        public delegate void DuelRequestRefused();

        public delegate void UserInfo(UserData info);

        public delegate void UserList(UserData[] info);

        public delegate void ChannelList(ChannelData[] channels);

        public delegate void StringList(string[] data);

        public delegate void ChannelUsersUpdate(ChannelUsers users);

        public UserInfo UpdateUserInfo;
        public UserList UserListUpdate;
        public ChannelUsersUpdate ChannelUserList;
        public ChannelUsersUpdate AddUserToChannel;
        public ChannelUsersUpdate RemoveUserFromChannel;
        public ServerDisconnected Disconnected;
        public LoginResponse LoginReply;
        public ClientPacket ChangeReply;
        public ClientPacket RegisterReply;
        public ClientPacket ValidateReply;
        public ClientPacket ResendReply;
        public ClientPacket RecoverReply;
        public ClientPacket ResetReply;
        public ServerResponse OnFatalError;
        public ServerResponse RemoveRoom;
        public ServerResponse UserStats;
        public ServerResponse TeamStats;
        public ServerResponse Ranking;
        public Command UpdateRoomPlayers;
        public GameRoomUpdate CreateRoom;
        public ServerResponse UpdateRoomStatus;
        public ServerResponse ServerMessage;
        public ServerRooms AddRooms;
        public Command DevPointMsg;
        public UserList FriendList;
        public ServerResponse JoinChannel;
        public Message ChatMessage;
        public UserDuelRequest DuelRequest;
        public UserDuelRequest DuelAccepted;
        public DuelRequestRefused DuelRefused;
        public Command TeamRequest;
        public UserList TeamList;
        public ChannelList ChannelRequest;
        public ServerResponse MatchFound;
        public ServerResponse MatchCanceled;
        public UserDuelRequest MatchStart;

        public string ServerKickBanMessage;
        public bool IsUserBanned = false;

        public ChatClient()
        {
            m_client = new TcpClient();
            m_receivedQueue = new Queue<MessageReceived>();
            m_sendQueue = new Queue<byte[]>();
            m_clientThread = new Thread(HandleClient) { IsBackground = true };
            m_parseThread = new Thread(OnCommand) { IsBackground = true };
            OnFatalError += FatalError;
        }

        public bool Connect(string address, int port)
        {
            try
            {
                m_client.Connect(address, port);
                m_reader = new BinaryReader(m_client.GetStream());
                m_isConnected = true;
                m_clientThread.Start();
                m_parseThread.Start();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Disconnect()
        {
            if (m_isConnected)
            {
                m_isConnected = !m_isConnected;
                if (m_client != null)
                    m_client.Close();
            }
        }

        public void SendPacket(DevServerPackets type, string data)
        {
            SendPacket(type, Encoding.UTF8.GetBytes(data));
        }

        private void SendPacket(DevServerPackets type, byte[] data)
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write((byte)type);
            writer.Write((short)(data.Length));
            writer.Write(data);
            lock (m_sendQueue)
                m_sendQueue.Enqueue(stream.ToArray());
        }

        public void SendPacket(DevServerPackets type)
        {
            if (type == DevServerPackets.Ping) m_pingRequest = DateTime.Now;
            lock (m_sendQueue)
                m_sendQueue.Enqueue(new[] { (byte)type });
        }

        public void SendMessage(MessageType type, CommandType command, string channel, string message)
        {
            SendPacket(DevServerPackets.ChatMessage,
                JsonSerializer.SerializeToString(new ChatMessage(type, command, channel, message)));
        }

        private void SendPacket(byte[] packet)
        {
            if (!m_isConnected)
                return;
            try
            {
                try
                {
                    m_client.Client.Send(packet, packet.Length, SocketFlags.None);
                }
                catch (Exception)
                {
                    Disconnect();
                }
            }
            catch (Exception)
            {
                Disconnect();
            }
        }

        private bool isLargePacket(DevClientPackets packet)
        {
            switch (packet)
            {
                case DevClientPackets.GameList:
                case DevClientPackets.UserList:
                case DevClientPackets.FriendList:
                case DevClientPackets.TeamList:
                case DevClientPackets.ChannelList:
                case DevClientPackets.ChannelUsers:
                    return true;

                default:
                    return false;
            }
        }

        private bool isOneByte(DevClientPackets packet)
        {
            switch (packet)
            {
                case DevClientPackets.LoginFailed:
                case DevClientPackets.Invalid:
                case DevClientPackets.InvalidTemp:
                case DevClientPackets.ValidateAccept:
                case DevClientPackets.ValidateFailed:
                case DevClientPackets.ResendAccept:
                case DevClientPackets.ResendFailed:
                case DevClientPackets.ChangeAccept:
                case DevClientPackets.ChangeFailed:
                case DevClientPackets.RegisterAccept:
                case DevClientPackets.RegisterFailed:
                case DevClientPackets.DuplicateMail:
                case DevClientPackets.BlacklistMail:
                case DevClientPackets.BlacklistName:
                case DevClientPackets.MailFormat:
                case DevClientPackets.QueueFail:
                case DevClientPackets.Pong:
                case DevClientPackets.RefuseDuelRequest:
                case DevClientPackets.RecoverAccept:
                case DevClientPackets.RecoverFailed:
                case DevClientPackets.ResetAccept:
                case DevClientPackets.ResetFailed:
                    return true;

                default:
                    return false;
            }
        }

        private void HandleClient()
        {
            try
            {
                while (m_isConnected)
                {
                    if (CheckDisconnected())
                    {
                        Disconnect();
                        return;
                    }
                    //handle incoming
                    if (m_client.Available >= 1)
                    {
                        var packet = (DevClientPackets)m_reader.ReadByte();
                        int len = 0;
                        byte[] content = null;
                        if (!isOneByte(packet))
                        {
                            if (isLargePacket(packet))
                            {
                                len = m_reader.ReadInt32();
                                content = m_reader.ReadBytes(len);
                            }
                            else
                            {
                                len = m_reader.ReadInt16();
                                content = m_reader.ReadBytes(len);
                            }
                        }

                        if (len > 0)
                        {
                            if (content != null)
                            {
                                var reader = new BinaryReader(new MemoryStream(content));
                                lock (m_receivedQueue)
                                    m_receivedQueue.Enqueue(new MessageReceived(packet, content, reader));
                            }
                        }
                        else
                            lock (m_receivedQueue)
                                m_receivedQueue.Enqueue(new MessageReceived(packet, null, null));
                    }
                    //send packet
                    if (m_sendQueue.Count > 0)
                    {
                        byte[] packet;
                        lock (m_sendQueue)
                            packet = m_sendQueue.Dequeue();
                        SendPacket(packet);
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception)
            {
                Disconnect();
            }
        }

        private bool CheckDisconnected()
        {
            return (m_client.Client.Poll(1, SelectMode.SelectRead) && m_client.Available == 0);
        }

        private void OnCommand()
        {
            while (m_isConnected)
            {
                if (m_receivedQueue.Count > 0)
                {
                    MessageReceived packet;
                    lock (m_receivedQueue)
                        packet = m_receivedQueue.Dequeue();

                    OnCommand(packet);
                }
                Thread.Sleep(1);
            }
        }

        private void OnCommand(MessageReceived e)
        {
            switch (e.Packet)
            {
                case DevClientPackets.QueueFail:
                    MessageBox.Show("You can only join once.");
                    break;

                case DevClientPackets.MatchFound:
                    if (MatchFound != null)
                        MatchFound(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;

                case DevClientPackets.MatchCanceled:
                    if (MatchCanceled != null)
                        MatchCanceled(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;

                case DevClientPackets.MatchStart:
                    if (MatchStart != null)
                        MatchStart(JsonSerializer.DeserializeFromString<DuelRequest>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.LoginAccepted:
                    if (LoginReply != null)
                        LoginReply(e.Packet, JsonSerializer.DeserializeFromString<LoginData>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.LoginFailed:
                    if (LoginReply != null)
                        LoginReply(e.Packet, null);
                    break;

                case DevClientPackets.Invalid:
                    if (LoginReply != null)
                        LoginReply(e.Packet, null);
                    break;

                case DevClientPackets.InvalidTemp:
                    if (LoginReply != null)
                        LoginReply(e.Packet, null);
                    break;

                case DevClientPackets.Banned:
                    IsUserBanned = true;
                    ServerKickBanMessage = Encoding.UTF8.GetString(e.Raw);
                    MessageBox.Show(ServerKickBanMessage, "Server", MessageBoxButtons.OK);
                    Application.Exit();
                    break;

                case DevClientPackets.RegisterAccept:
                    if (RegisterReply != null)
                        RegisterReply(e.Packet);
                    break;

                case DevClientPackets.RegisterFailed:
                    if (RegisterReply != null)
                        RegisterReply(e.Packet);
                    break;

                case DevClientPackets.DuplicateMail:
                    if (RegisterReply != null)
                        RegisterReply(e.Packet);
                    break;

                case DevClientPackets.BlacklistMail:
                    if (RegisterReply != null)
                        RegisterReply(e.Packet);
                    break;

                case DevClientPackets.BlacklistName:
                    if (RegisterReply != null)
                        RegisterReply(e.Packet);
                    break;

                case DevClientPackets.MailFormat:
                    if (RegisterReply != null)
                        RegisterReply(e.Packet);
                    break;

                case DevClientPackets.ChangeAccept:
                    if (ChangeReply != null)
                        ChangeReply(e.Packet);
                    break;

                case DevClientPackets.ChangeFailed:
                    if (ChangeReply != null)
                        ChangeReply(e.Packet);
                    break;

                case DevClientPackets.ValidateAccept:
                    if (ValidateReply != null)
                        ValidateReply(e.Packet);
                    break;

                case DevClientPackets.ValidateFailed:
                    if (ValidateReply != null)
                        ValidateReply(e.Packet);
                    break;

                case DevClientPackets.ResendAccept:
                    if (ResendReply != null)
                        ResendReply(e.Packet);
                    break;

                case DevClientPackets.ResendFailed:
                    if (ResendReply != null)
                        ResendReply(e.Packet);
                    break;

                case DevClientPackets.RecoverAccept:
                    if (RecoverReply != null)
                        RecoverReply(e.Packet);
                    break;

                case DevClientPackets.RecoverFailed:
                    if (RecoverReply != null)
                        RecoverReply(e.Packet);
                    break;

                case DevClientPackets.ResetAccept:
                    if (ResetReply != null)
                        ResetReply(e.Packet);
                    break;

                case DevClientPackets.ResetFailed:
                    if (ResetReply != null)
                        ResetReply(e.Packet);
                    break;

                case DevClientPackets.UserStats:
                    if (UserStats != null)
                        UserStats(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;

                case DevClientPackets.TeamStats:
                    if (TeamStats != null)
                        TeamStats(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;

                case DevClientPackets.Ranking:
                    //MessageBox.Show(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    if (Ranking != null)
                        Ranking(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;

                case DevClientPackets.Pong:
                    MessageBox.Show("PONG!: " + -(int)m_pingRequest.Subtract(DateTime.Now).TotalMilliseconds);
                    break;

                case DevClientPackets.ServerMessage:
                    if (ServerMessage != null)
                        ServerMessage(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;

                case DevClientPackets.ChannelUsers:
                    if (ChannelUserList != null)
                        ChannelUserList(JsonSerializer.DeserializeFromString<ChannelUsers>(
                            Encoding.UTF8.GetString(e.Raw)));
                    break;

                case DevClientPackets.AddChannelUser:
                    if (AddUserToChannel != null)
                        AddUserToChannel(JsonSerializer.DeserializeFromString<ChannelUsers>(
                            Encoding.UTF8.GetString(e.Raw)));
                    break;

                case DevClientPackets.RemoveChannelUser:
                    if (RemoveUserFromChannel != null)
                        RemoveUserFromChannel(JsonSerializer.DeserializeFromString<ChannelUsers>(
                            Encoding.UTF8.GetString(e.Raw)));
                    break;

                case DevClientPackets.UpdateUserInfo:
                    if (UpdateUserInfo != null)
                        UpdateUserInfo(JsonSerializer.DeserializeFromString<UserData>(Encoding.UTF8.GetString(e.Raw)));
                    break;

                case DevClientPackets.UserList:
                    if (UserListUpdate != null)
                        UserListUpdate(JsonSerializer.DeserializeFromString<UserData[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.GameServers:
                    var servers = JsonSerializer.DeserializeFromString<ServerInfo[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    foreach (ServerInfo server in servers)
                    {
                        if (server.serverName.Contains("DevServer") && !Program.ServerList.ContainsKey(server.serverName))
                        {
                            Program.ServerList.Add(server.serverName, server);
                        }
                        else if (!Program.ServerList3P.ContainsKey(server.serverName))
                        {
                            Program.ServerList3P.Add(server.serverName, server);
                        }
                    }
                    break;

                case DevClientPackets.AddServer:
                    var serverinfo = JsonSerializer.DeserializeFromString<ServerInfo>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    if (serverinfo.serverName.Contains("DevServer") && !Program.ServerList.ContainsKey(serverinfo.serverName))
                    {
                        Program.ServerList.Add(serverinfo.serverName, serverinfo);
                    }
                    else if (!Program.ServerList3P.ContainsKey(serverinfo.serverName))
                    {
                        Program.ServerList3P.Add(serverinfo.serverName, serverinfo);
                    }
                    break;

                case DevClientPackets.RemoveServer:
                    string removeserver = Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length));
                    if (removeserver.Contains("DevServer") && Program.ServerList.ContainsKey(removeserver))
                    {
                        Program.ServerList.Remove(removeserver);
                    }
                    else if (Program.ServerList3P.ContainsKey(removeserver))
                    {
                        Program.ServerList3P.Remove(removeserver);
                    }
                    break;

                case DevClientPackets.UpdateServerStatus:
                    ServerInfo serverInfo = JsonSerializer.DeserializeFromString<ServerInfo>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    Program.ServerList[serverInfo.serverName] = serverInfo;
                    break;

                case DevClientPackets.JoinChannelAccept:
                    if (JoinChannel != null)
                        JoinChannel(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;

                case DevClientPackets.ChannelList:
                    if (ChannelRequest != null)
                        ChannelRequest(JsonSerializer.DeserializeFromString<ChannelData[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.FriendList:
                    if (FriendList != null)
                        FriendList(JsonSerializer.DeserializeFromString<UserData[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.TeamList:
                    if (TeamList != null)
                        TeamList(JsonSerializer.DeserializeFromString<UserData[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.Message:
                    if (ChatMessage != null)
                        ChatMessage(JsonSerializer.DeserializeFromString<ChatMessage>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.DevPoints:
                    if (DevPointMsg != null)
                        DevPointMsg(JsonSerializer.DeserializeFromString<PacketCommand>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.DuelRequest:
                    if (DuelRequest != null)
                        DuelRequest(JsonSerializer.DeserializeFromString<DuelRequest>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.AcceptDuelRequest:
                    string user = Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length));
                    if (ChatMessage != null)
                        ChatMessage(new ChatMessage(MessageType.Server, CommandType.None, null, user + " has accepted your duel request."));
                    break;

                case DevClientPackets.StartDuel:
                    if (DuelAccepted != null)
                        DuelAccepted(JsonSerializer.DeserializeFromString<DuelRequest>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.RefuseDuelRequest:
                    if (DuelRefused != null)
                        DuelRefused();
                    break;

                case DevClientPackets.TeamRequest:
                    if (TeamRequest != null)
                        TeamRequest(JsonSerializer.DeserializeFromString<PacketCommand>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.GameList:
                    if (AddRooms != null)
                        AddRooms(JsonSerializer.DeserializeFromString<RoomInfos[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.CreateRoom:
                    if (CreateRoom != null)
                        CreateRoom(JsonSerializer.DeserializeFromString<RoomInfos>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.RemoveRoom:
                    if (RemoveRoom != null)
                        RemoveRoom(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;

                case DevClientPackets.UpdatePlayers:
                    if (UpdateRoomPlayers != null)
                        UpdateRoomPlayers(JsonSerializer.DeserializeFromString<PacketCommand>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;

                case DevClientPackets.RoomStart:
                    if (UpdateRoomStatus != null)
                        UpdateRoomStatus(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;

                case DevClientPackets.Kicked:
                    ServerKickBanMessage = Encoding.UTF8.GetString(e.Raw) + "\n\r Do you want to restart DevPro ?";
                    break;

                default:
                    if (OnFatalError != null)
                        OnFatalError("Unknown packet received: " + e.Packet.ToString());
                    break;
            }
        }

        public bool Connected()
        {
            return m_client.Connected;
        }

        private void FatalError(string message)
        {
            MessageBox.Show(message);
        }
    }
}
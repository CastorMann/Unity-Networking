using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Guest User");

            SendTCPData(_packet);
        }
    }

    public static void ClientDisconnected()
    {
        using (Packet _packet = new Packet((int)ClientPackets.clientDisconnected))
        {
            SendTCPData(_packet);
        }
    }

    public static void CreateLobby(int _lobbyId)
    {
        using (Packet _packet = new Packet((int)ClientPackets.createLobby))
        {
            _packet.Write(_lobbyId);
            Debug.Log($"Requesting Creating Lobby with id: {_lobbyId}");
            SendTCPData(_packet);
        }
    }

    public static void JoinLobby(int _lobbyId)
    {
        using (Packet _packet = new Packet((int)ClientPackets.joinLobby))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(_lobbyId);
            Debug.Log($"Requesting Client with id: {Client.instance.myId} Joining Lobby with id: {_lobbyId}");
            SendTCPData(_packet);
        }
    }

    public static void LeaveLobby()
    {
        if (Client.instance.lobby == null)
        {
            Debug.Log("Cannot leave lobby - Client does not have lobby");
            return;
        }
        using (Packet _packet = new Packet((int)ClientPackets.leaveLobby))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(Client.instance.lobby.id);
            Debug.Log($"Requesting Leaving Lobby with id {Client.instance.lobby.id}");
            SendTCPData(_packet);
        }
    }

    public static void DeleteLobby(int _lobbyId)
    {
        using (Packet _packet = new Packet((int)ClientPackets.deleteLobby))
        {
            _packet.Write(_lobbyId);
            Debug.Log($"Requesting Deleting Lobby with id: {_lobbyId}");
            SendTCPData(_packet);
        }
    }

    public static void SendPrivateChatMessage(int _toClient, string _message)
    {
        using (Packet _packet = new Packet((int)ClientPackets.sendPrivateChatMessage))
        {
            _packet.Write(_toClient);
            _packet.Write(_message);

            SendTCPData(_packet);
        }
    }

    public static void SendGroupChatMessage(int _toLobby, string _message)
    {
        using (Packet _packet = new Packet((int)ClientPackets.sendGroupChatMessage))
        {
            _packet.Write(_toLobby);
            _packet.Write(_message);

            SendTCPData(_packet);
        }
    }
    #endregion
}
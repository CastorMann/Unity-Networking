using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();
        Debug.Log($"Message from server: {_msg} " + "client ID: " + _myId.ToString());
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void ClientDisconnected(Packet _packet)
    {
        int _clientId = _packet.ReadInt();

        // TODO: Handle disconnection
    }

    public static void LobbyCreated(Packet _packet) 
    {
        int _lobbyId = _packet.ReadInt();
        Debug.Log($"Creating Lobby with id: {_lobbyId}");
        GameManager.openLobbies.Add(_lobbyId, new Lobby(_lobbyId));
    }

    public static void LobbyJoined(Packet _packet)
    {
        int _clientId = _packet.ReadInt();
        int _lobbyId = _packet.ReadInt();
        Debug.Log($"Client with id: {_clientId} Joining Lobby with id: {_lobbyId}");
        GameManager.openLobbies[_lobbyId].clientIds.Add(_clientId);
        if (_clientId == Client.instance.myId)
        {
            Client.instance.lobby = GameManager.openLobbies[_lobbyId];
        }
    }

    public static void LobbyLeft(Packet _packet)
    {
        int _clientId = _packet.ReadInt();
        int _lobbyId = _packet.ReadInt();
        Debug.Log($"Client with id: {_clientId} Leaving Lobby with id: {_lobbyId}");
        GameManager.openLobbies[_lobbyId].clientIds.Remove(_clientId);
        if (Client.instance.lobby != null)
        {
            if (Client.instance.lobby.id == _lobbyId)
            {
                Client.instance.lobby = null;
            }
        }

    }

    public static void LobbyDeleted(Packet _packet)
    {
        int _lobbyId = _packet.ReadInt();
        Debug.Log($"Deleting Lobby with id: {_lobbyId}");
        GameManager.openLobbies.Remove(_lobbyId);
        if (Client.instance.lobby != null)
        {
            if (Client.instance.lobby.id == _lobbyId)
            {
                Client.instance.lobby = null;
            }
        }
    }

    public static void PrivateChatMessageSent(Packet _packet)
    {
        int _fromClient = _packet.ReadInt();
        string _message = _packet.ReadString();

        Debug.Log($"Message received from client {_fromClient}: " + _message);

        // TODO: Handle private message
    }

    public static void GroupChatMessageSent(Packet _packet)
    {
        int _fromClient = _packet.ReadInt();
        int _toLobby = _packet.ReadInt();
        string _message = _packet.ReadString();

        Debug.Log($"Client {_fromClient} sent a message to lobby {_toLobby}: {_message}");

        // TODO: Handle group message
    }
}
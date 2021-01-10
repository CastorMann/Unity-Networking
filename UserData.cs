using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string username;
    public string email;
    public string ip;
    public string id;
    public List<UserData> friends;

    public UserData(string _username, string _email, string _ip, string _id)
    {
        username = _username;
        email = _email;
        ip = _ip;
        id = _id;
        friends = new List<UserData>();
    }
    public void reset()
    {
        username = null;
        ip = null;
        id = null;
        email = null;
        friends = null;
    }
}

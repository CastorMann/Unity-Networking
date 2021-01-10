using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby
{
    public int id;
    public List<int> clientIds = new List<int>();

    public Lobby(int _id) 
    {
        id = _id;
    }
}

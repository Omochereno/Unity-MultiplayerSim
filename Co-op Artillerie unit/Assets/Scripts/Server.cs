using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Networking;

public class Server : MonoBehaviourPunCallbacks
{

    [PunRPC]
    void ChatMessage(string a, string b){
        Debug.Log(string.Format("Chatmess {0} {1}", a, b));
    }
}

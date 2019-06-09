using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Buildings : MonoBehaviourPunCallbacks
{

    enum Damage {RED, ORANGE, YELLOW};
    PhotonView photonview;
    // Start is called before the first frame update
    void Start()
    {
        photonview = PhotonView.Get(this);
    }

    public void DoDamage(int damage){
        //if(PhotonNetwork.IsMasterClient == true){
            photonView.RPC("OnHit", RpcTarget.AllBuffered, damage);
        //}
    }

    [PunRPC]
    void OnHit(int damage){
        Color color;
        if(damage == (int)Damage.RED)
            color = Color.red;
        else if(damage == (int)Damage.ORANGE)
            color = new Color(1.0f, 0.64f, 0.0f);
        else 
            color = Color.yellow;
        Renderer rend = this.GetComponent<Renderer>();
        rend.material.SetColor("_Color", color);
    }
}

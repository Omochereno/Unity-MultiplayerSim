using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), transform.position, Quaternion.identity, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

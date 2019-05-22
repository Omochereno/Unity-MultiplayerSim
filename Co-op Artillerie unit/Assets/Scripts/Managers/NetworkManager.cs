using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerCustmom : NetworkManager
{
    public void StartupHost()
    {
        SetPort();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

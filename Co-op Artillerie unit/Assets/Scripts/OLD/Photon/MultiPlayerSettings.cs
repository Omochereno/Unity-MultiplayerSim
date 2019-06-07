using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerSettings : MonoBehaviour
{
    public static MultiPlayerSettings multiPlayerSetting;

    public bool delayStart;
    public int maxPlayers;

    public int menuScene;
    public int multiPlayScene;
    //public int spotterScene;

    private void Awake(){
        if(MultiPlayerSettings.multiPlayerSetting == null){
            MultiPlayerSettings.multiPlayerSetting = this;
        } else {
            if(MultiPlayerSettings.multiPlayerSetting != this){
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}

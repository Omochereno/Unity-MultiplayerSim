using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiPlaySceneController : MonoBehaviour
{
    public static MultiPlaySceneController mScenecontroller;
    // Start is called before the first frame update
    private void Awake(){
        if(MultiPlaySceneController.mScenecontroller == null){
            MultiPlaySceneController.mScenecontroller = this;
        } else {
            if(MultiPlaySceneController.mScenecontroller != this){
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisconnectPlayer(){
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad(){
        //PhotonNetwork.Disconnect();
        PhotonNetwork.LeaveRoom();
        while(PhotonNetwork.InRoom){
            yield return null;
        }
        SceneManager.LoadScene(MultiPlayerSettings.multiPlayerSetting.menuScene);
    }
}

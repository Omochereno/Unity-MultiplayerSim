using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{


    public void OnClickCharacterPick(int whichRole){
        if(PlayerInfo.PI != null){
            PlayerInfo.PI.mySelectedCharacter = whichRole;
            Debug.Log(whichRole);
            PlayerPrefs.SetInt("MyCharacter", whichRole);
            SceneManager.LoadScene(MultiPlayerSettings.multiPlayerSetting.multiPlayScene);
        }
    }



}

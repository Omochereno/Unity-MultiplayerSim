using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public static Manager manager;

    public int spotterRol;
    public int schutterRol;
    public int menuScene;
    public int simScene;

    public int maxPlayers;

 private void Awake(){
     if(Manager.manager == null){
         Manager.manager = this;
     } else {
         if(Manager.manager != this){
             Destroy(this.gameObject);
         }
     }
    DontDestroyOnLoad(this.gameObject);
 }
}

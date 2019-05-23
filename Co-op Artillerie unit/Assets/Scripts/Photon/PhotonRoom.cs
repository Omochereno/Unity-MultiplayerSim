using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{

    //Room info
    public static PhotonRoom room;
    private PhotonView PV;

   // public bool isGameLoaded;
    public int currentScene;
    public int multiPlayScene;

    //player info
   // Player[] photonPlayers;
   // public int playersInRoom;
   // public int myNumberInRoom;

  //  public int playersInGame;

    private void Awake()
    {
        if(PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        } else
        {
            if(PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnEnable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are now in a room");
        //photonPlayers = PhotonNetwork.PlayerList;
        //playersInRoom = photonPlayers.Length;
        //myNumberInRoom = playersInRoom;
        //PhotonNetwork.NickName = myNumberInRoom.ToString();
        if (!PhotonNetwork.IsMasterClient)
            return;
        StartGame();
    }

     void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.LoadLevel(multiPlayScene);
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {

        currentScene = scene.buildIndex;
        if(currentScene == multiPlayScene)
        {
            {
                CreatePlayer();
            }
        }
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
    }
}

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Room info
    public static RoomController room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;

    //player info
   private Player[] photonPlayers;
   public int playersInRoom;
   public int myNumberInRoom;
   public int playersInGame;


    private void Awake()
    {
        if(RoomController.room == null)
        {
            RoomController.room = this;
        } else
        {
            if(RoomController.room != this)
            {
                Destroy(RoomController.room.gameObject);
                RoomController.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
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

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are now in a room");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = myNumberInRoom.ToString();
        RPC_CreatePlayer();
        //StartGame();
    }

    void StartGame()
    {
        isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient){
            return;
        }

        PhotonNetwork.LoadLevel(1);
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        // currentScene = scene.buildIndex;
        // if(currentScene == 1)
        // {
        //     isGameLoaded = true;
        //     RPC_CreatePlayer();
        // }
    }

    [PunRPC]
    private void RPC_LoadedGameScene(){
        playersInGame++;
        if(playersInGame == PhotonNetwork.PlayerList.Length){
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer(){
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new player has joined the room");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
        Debug.Log(photonPlayers.Length);
    }
}

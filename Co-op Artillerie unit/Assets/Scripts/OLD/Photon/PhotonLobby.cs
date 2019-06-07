using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
  public GameObject rolePanel;
   public GameObject networkPanel;

    public static PhotonLobby lobby;

    RoomInfo[] rooms;

    public GameObject joinButton;
    public GameObject cancelButton;

    
    void Start()
    {
        if(PhotonNetwork.IsConnected){
            OnConnectedToMaster();
        } else 
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Awake()
    {
        lobby = this; // create singleton, lives within the main menu scene
        // if(PhotonLobby.lobby == null)
        // {
        //     PhotonLobby.lobby = this;
        // } else
        // {
        //     if(PhotonLobby.lobby != this)
        //     {
        //         Destroy(PhotonLobby.lobby.gameObject);
        //         PhotonLobby.lobby = this;
        //     }
        // }
    }

    public override void OnConnectedToMaster() // When connected to the master server this will be executed
    {
        Debug.Log("Player has connected to the photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        joinButton.SetActive(true);
    }

    public void onJoinButtonClicked() // when clicked, join a random room.
    {
        // joinButton.SetActive(false);
        // //cancelButton.SetActive(true);
        networkPanel.SetActive(false);
        rolePanel.SetActive(true);
        PhotonNetwork.JoinRandomRoom();

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a random game but failed. There must be no open games available");
        CreateRoom();
        
    }

    void CreateRoom()
    {
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiPlayerSettings.multiPlayerSetting.maxPlayers };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("CreateRoomFailed");
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        cancelButton.SetActive(false);
        joinButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{

    public static PhotonLobby lobby;

    RoomInfo[] rooms;

    public GameObject joinButton;
    public GameObject cancelButton;

    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Awake()
    {
        lobby = this; // create singleton, lives within the main menu scene
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        joinButton.SetActive(true);
    }

    public void onJoinButtonClicked()
    {
        joinButton.SetActive(false);
        cancelButton.SetActive(true);
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
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("");
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

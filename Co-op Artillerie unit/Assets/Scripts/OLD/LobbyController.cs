using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyController : MonoBehaviourPunCallbacks
{

    public GameObject joinButton;

    public GameObject networkPanel;

    public GameObject rolePanel;


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if(!PhotonNetwork.IsConnected){
            PhotonNetwork.ConnectUsingSettings();
        }
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
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 3 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("CreateRoomFailed");
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        //cancelButton.SetActive(false);
        joinButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}

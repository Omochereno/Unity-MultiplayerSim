using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController sceneController;
    public GameObject schutterPanel;
    public GameObject spotterPanel;

    public Camera mapCamera;

    public Camera firstPersonCamera;

    public Button switchViewButton;

    enum Role { SCHUTTER, SPOTTER};
    public int role;

    private FireButtonHandler fireButtonHandler;

    private Boolean firstLoad;

    void Start(){
        if(SceneController.sceneController == null){
            SceneController.sceneController = this;
        } else
        {
            if(SceneController.sceneController != this)
            {
                Destroy(SceneController.sceneController.gameObject);
                SceneController.sceneController= this;
            }
        }
        DontDestroyOnLoad(this.gameObject);

        // Set variables
        firstLoad = true;
        role = (int)Role.SCHUTTER;
        fireButtonHandler = this.GetComponent<FireButtonHandler>();

        //Enable the right views
        activateSchutterPanel();
        firstPersonCamera.enabled = false;
        switchViewButton.gameObject.SetActive(false);
        ShowMapView();

    }

    public void OnClickSwitchRole(){
        role = role == (int)Role.SCHUTTER ? (int)Role.SPOTTER : (int)Role.SCHUTTER;
        
        if(role == (int)Role.SCHUTTER)
            activateSchutterPanel();
        else 
            activateSpotterPanel();
    }

    private void activateSpotterPanel()
    {
        schutterPanel.SetActive(false);
        spotterPanel.SetActive(true);
    }

    private void activateSchutterPanel()
    {
        spotterPanel.SetActive(false);    
        schutterPanel.SetActive(true);
    }

    public void OnClickSwitchView(){
        if(firstPersonCamera.enabled == true)
            ShowMapView();
        else
            ShowFirstPersonView();
    }

    private void ShowMapView() {
        firstPersonCamera.enabled = false;
        switchViewButton.gameObject.SetActive(false);
        if(!firstLoad) {
            setBuildings(false);
            fireButtonHandler.targets.SetActive(false);
        }
        else {
            firstLoad = false;
        }
    }
    
    private void ShowFirstPersonView() {
        firstPersonCamera.enabled = true;
        switchViewButton.gameObject.SetActive(true);
        setBuildings(true);
        fireButtonHandler.targets.SetActive(true);
    }

    private void setBuildings(bool active){
        foreach(GameObject g in fireButtonHandler.buildings)
        {
            //g.SetActive(active);
            g.GetComponent<MeshRenderer>().enabled = active;
        }
    }
}

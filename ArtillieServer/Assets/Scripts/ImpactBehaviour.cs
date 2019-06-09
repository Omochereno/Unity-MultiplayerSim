using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImpactBehaviour : MonoBehaviour
{
    private SceneController controller;
    void Start()
    {
        controller = GameObject.Find("SceneController").GetComponent<SceneController>();
        impactVisibility();

        Object.Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        impactVisibility();
    }

    private void impactVisibility(){
        if(controller.role == 0)
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;   
        else 
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    void OnTriggerEnter(Collider collision){
        GameObject bridge = GameObject.FindGameObjectWithTag("Target");
        Debug.Log("Target hit!");
        Renderer rend = bridge.GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.red);
    }
}

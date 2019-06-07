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
        if(controller.role == 0)
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;   
        else 
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

        Object.Destroy(gameObject, 10.0f);
    }

    public void SetImpact(int val){
         this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = val; 
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.role == 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;   
        }
        else 
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    void OnTriggerEnter(Collider collision){
        GameObject bridge = GameObject.FindGameObjectWithTag("Target");
        Renderer rend = bridge.GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.red);
        


    }
}

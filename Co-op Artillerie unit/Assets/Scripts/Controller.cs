using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEditor;
using Models;
using UnityEngine.Networking;

public class Controller : MonoBehaviour
{
      private readonly string basePath = "https://localhost:8080";
      private RequestHelper currentRequest;

      public GameObject xInput; 

      public GameObject yInput;

      public void Post(){

		currentRequest = new RequestHelper {
			Uri = basePath + "/coor",
			Body = new Coordinates {
				id = 1,
				x = 33,
				y = 45
			}
		};
		RestClient.Post<Post>(currentRequest)
		.Then(res => EditorUtility.DisplayDialog ("Success", JsonUtility.ToJson(res, true), "Ok"))
		.Catch(err => EditorUtility.DisplayDialog ("Error", err.Message, "Ok"));
	}
}

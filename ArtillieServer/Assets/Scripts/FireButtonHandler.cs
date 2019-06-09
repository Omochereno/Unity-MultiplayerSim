using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class FireButtonHandler : MonoBehaviourPunCallbacks
{
    public InputField InputRichting;
    public InputField InputHoek;
    public GameObject Map;
    //public GameObject Impact;

    double deviationX;
    double deviationY;

    public GameObject[] buildings;
    public GameObject targets;
    private bool readyToFire;

    private void applyDamageToBuildings(Vector3 vector)//Geeft hier de berkende Vector3 uit FireButton mee als parameter;
    {
        //Vector3 vector = new Vector3(4, -1, 0); //Dit wordt vector van Impact uit parameter
        Color color = new Color(1.0f, 0.64f, 0.0f);

        foreach(GameObject g in buildings)
        {
            Buildings buildingScript = g.GetComponent<Buildings>();
            if (Vector3.Distance(vector, g.transform.position) <= 10)
            {
                buildingScript.DoDamage(0);
            }

            if (Vector3.Distance(vector, g.transform.position) <= 15 && Vector3.Distance(vector, g.transform.position) >= 10)
            {
                Renderer rend = g.GetComponent<Renderer>();
                if(rend.material.GetColor("_Color") != Color.red){
                    buildingScript.DoDamage(1);
                }

            }
            if (Vector3.Distance(vector, g.transform.position) <= 20 && Vector3.Distance(vector, g.transform.position) >= 15)
            {
                Renderer rend = g.GetComponent<Renderer>();
                if(rend.material.GetColor("_Color") != Color.red && rend.material.GetColor("_Color") != color){
                    buildingScript.DoDamage(2);
                }  
 
            }
        }
    }

    public void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        buildings = GameObject.FindGameObjectsWithTag("Building");
        targets = GameObject.FindGameObjectWithTag("Target");

        foreach(GameObject g in buildings)
        {
            //g.SetActive(false);
            g.GetComponent<MeshRenderer>().enabled = false;
        }

        targets.GetComponent<MeshRenderer>().enabled = false;
        System.Random random = new System.Random();
        deviationX = random.NextDouble() * 10 + 5;
        deviationY = random.NextDouble() * 10 + 5;
    }

    float timer;
    public int waitingTime = 3;

    public void Update(){
        if(!readyToFire) {
            timer += Time.deltaTime;
            if(timer > waitingTime){
                readyToFire = true;
                timer = 0;
            }
        }
    }

    public void FireButton()
    {
        if(readyToFire){
            Vector3 vector = calculateVector();
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Impact"), vector, Quaternion.identity);
            applyDamageToBuildings(vector);
            readyToFire = false;
        }
    }

    private double CalculateDistance()
    {
        double maxDistanceAngle = 45.0;
        double maxDistance = Math.Sqrt(Math.Pow(Map.GetComponent<RectTransform>().rect.width, 2) + 
               Math.Pow(Map.GetComponent<RectTransform>().rect.height, 2));
        double distancePerDegree = maxDistance / maxDistanceAngle;

        double hoek = double.Parse(InputHoek.GetComponent<InputField>().text);

        if(hoek > maxDistanceAngle)
        {
            hoek = maxDistanceAngle - (hoek - maxDistanceAngle);
        }

        return hoek * distancePerDegree;
    }

    private double CalculateY()
    {
        double direction = double.Parse(InputRichting.GetComponent<InputField>().text);
        return (Math.Sin(DegreeToradian(direction)) * CalculateDistance());
    }

    private double CalculateX()
    {
        double direction = double.Parse(InputRichting.GetComponent<InputField>().text);
        return (Math.Cos(DegreeToradian(direction)) * CalculateDistance());
    }

    private double DegreeToradian(double angle)
    {
        return Math.PI * angle / 180.0;
    }

    private Vector3 calculateVector()
    {

        float impactX = (float)(CalculateX() + deviationX);
        float impactY = (float)(CalculateY() + deviationY);

        return new Vector3(impactX, impactY, 0);
    }
}

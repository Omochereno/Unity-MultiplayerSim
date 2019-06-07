using System;
using UnityEngine;
using UnityEngine.UI;

public class FireButtonHandler : MonoBehaviour
{
    public InputField InputRichting;
    public InputField InputHoek;
    public GameObject Map;
    public GameObject Impact;

    double deviationX;
    double deviationY;

    public GameObject[] buildings;

    private void applyDamageToBuildings(Vector3 vector)//Geeft hier de berkende Vector3 uit FireButton mee als parameter;
    {
        //Vector3 vector = new Vector3(4, -1, 0); //Dit wordt vector van Impact uit parameter
        Color color = new Color(1.0f, 0.64f, 0.0f);

        foreach(GameObject g in buildings)
        {
            if (Vector3.Distance(vector, g.transform.position) <= 10)
            {
                Renderer rend = g.GetComponent<Renderer>();
                Debug.Log("Hij komt in rood");
                rend.material.SetColor("_Color", Color.red);
            }

            if (Vector3.Distance(vector, g.transform.position) <= 15 && Vector3.Distance(vector, g.transform.position) >= 10)
            {
                Renderer rend = g.GetComponent<Renderer>();
                Debug.Log("Hij komt in oranje");
                if(rend.material.GetColor("_Color") != Color.red){
                    rend.material.SetColor("_Color", color);
                }
            }

            if (Vector3.Distance(vector, g.transform.position) <= 20 && Vector3.Distance(vector, g.transform.position) >= 15)
            {
                Renderer rend = g.GetComponent<Renderer>();
                Debug.Log("Hij komt in geel");
                if(rend.material.GetColor("_Color") != Color.red && rend.material.GetColor("_Color") != color){
                    rend.material.SetColor("_Color", Color.yellow);
                }
            }
        }
    }

    public void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        buildings = GameObject.FindGameObjectsWithTag("Building");

        foreach(GameObject g in buildings)
        {
            g.SetActive(false);
        }
        System.Random random = new System.Random();
        deviationX = random.NextDouble() * 10 + 5;
        deviationY = random.NextDouble() * 10 + 5;
    }

    public void FireTest()
    {
        Vector3 vector = calculateVector();
        Instantiate(Impact, vector, Quaternion.identity);
        applyDamageToBuildings(vector);
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

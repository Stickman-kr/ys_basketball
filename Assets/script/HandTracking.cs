using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandTracking : MonoBehaviour
{
    // Start is called before the first frame update
    public TCPReceive tcPReceive;
    public GameObject[] handPoints;
    public GameObject catchpoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string data = tcPReceive.data;
        data = data.Remove(0,1);
        data = data.Remove(data.Length-1,1);
        data = data.Replace("]","");
        string[] points = data.Split(',');
        //0~41 까지 point 이동
        for (int i = 0; i<42; i ++){
            float x = 5- float.Parse(points[i*3])/100; //x 값
            float y = float.Parse(points[i*3+1])/100;
            float z = -2 + float.Parse(points[i*3+2])/20; // distance 
            handPoints[i].transform.localPosition = new Vector3(x,y,z);
        }
        catchpoint.transform.position = (handPoints[7].transform.position + handPoints[28].transform.position)/2;

    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCtrl : MonoBehaviour
{
    public GameObject GameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            GameManager.BroadcastMessage("onGoal");
        }
    }
}

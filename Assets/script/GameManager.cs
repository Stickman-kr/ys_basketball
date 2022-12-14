using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public TextMesh goal;
    public Text scoretxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onGoal()
    {
        ++score;
        scoretxt.text = $"{score}";
        goal.text = "Goal!!";
        Invoke("clear",1f);
    }

    void clear()
    {
        goal.text = "";
    }
}

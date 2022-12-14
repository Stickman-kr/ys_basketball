using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject Ball;

    bool isDelay = false;
    //public bool isstop = false;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (isDelay) return;
        if (BallCtrl.iscatch) return;
        Instantiate(Ball, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

        isDelay = true;
        StartCoroutine("DelayOff");
    }
    IEnumerator DelayOff()
    {
        yield return new WaitForSeconds(5f);
        isDelay = false;
    }

    

}

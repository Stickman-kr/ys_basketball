using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* class ballscript : MonoBehaviour
{
    float currTime;
    public GameObject ball;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltatime;
        if (currtime > 5)   //5초마다 생성
        {
        float newX = Random.Range(-5f, 5f), newY = Random.Range(-5f, 5f), newZ = Random.Range(-10f, -5f);
        
        clone = Instantiate(ball);
        clone.transform.position = new Vector3(newX,newY,newZ);
        destroy(clone,3f);
        currTime = 0;
        
        }
    }
}
*/
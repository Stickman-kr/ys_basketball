using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallCtrl : MonoBehaviour
{
    public float backSpeed = 8f;
    public float upSpeed = 3f;
    public int catchCount = 0;
    public static bool iscatch = false; // collide check
    bool iscarry = false; // carry ball check
    bool isthrow = false; // throw check
    GameObject catchPoint;
    private Vector3 oldPosition;
    private Vector3 currentPosition;
    private float velocity;
    //public AudioClip[] soundToPlay;
    //private AudioSource audio;

    private Rigidbody rb;
    BallSpawner ballSpawner;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        catchPoint = GameObject.Find("CatchPoint");
        ballSpawner = GameObject.Find("BallSpawner").GetComponent<BallSpawner>();
        //audio = GetComponent<AudioSource>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(Vector3.up * upSpeed + Vector3.back * backSpeed, ForceMode.Impulse);
        oldPosition = catchPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 손 가속도 계산
        currentPosition = catchPoint.transform.position;
        var dis = (currentPosition - oldPosition);
        var distance = Mathf.Sqrt(Mathf.Pow(dis.x,2)+Mathf.Pow(dis.y,2)+Mathf.Pow(dis.z,2));
        velocity = distance / Time.deltaTime;
        oldPosition = currentPosition;  
        if (velocity > 20) isthrow = true; 
        else isthrow = false;


        if(iscatch){
            // collide되면 parent로 종속시켜 손이랑 같이 움직이게 함.
            rb.isKinematic = false;
            transform.parent = catchPoint.transform;
            transform.localPosition = Vector3.zero;
            transform.rotation = new Quaternion(0,0,0,0);
            iscarry = true;
        }
        // carry ball with hand
        if (iscarry)
        {
            //when throw
            if (isthrow)
            {
                rb.isKinematic = false;
                transform.parent = null;
                iscarry = false;
                rb.AddForce(Vector3.up * 3 * upSpeed + Vector3.forward * velocity/20, ForceMode.Impulse);
                isthrow = false;
                iscatch = false;
                
            }
        }

    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.collider.gameObject.CompareTag("hand"))
        {
            iscatch =true;
            //ballSpawner.isstop = true;
        }
        if(other.collider.gameObject.CompareTag("Base")){
            Destroy(gameObject,5f);
        }

    }
   

}

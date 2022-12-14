using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform PlayerTransform;
    Vector3 Offset;
    bool CameraView = false;
    
    //1인칭위치, 3인칭 위치 저장
    void Awake() 
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - PlayerTransform.position;
    }
    //Tab키가 눌릴때마다 1,3인칭 여부 변경
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Tab))
            CameraView = !CameraView;
    }
    // 1,3인칭 바뀜에 따라 카메라위치를 3인칭 ->1인칭 ->3인칭으로 바꿈
    void LateUpdate()
    {
        if(CameraView == false)
            transform.position = PlayerTransform.position + Offset;
        else
            transform.position = PlayerTransform.position;
    }
}

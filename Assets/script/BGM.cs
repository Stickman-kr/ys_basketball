using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// BGM object가 씬이 바뀌어도 파괴되지 않고 계속 됩니다. End를 만나면 파괴합니다.
public class BGM : MonoBehaviour
{
    void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Update() 
    {
        if (SceneManager.GetActiveScene().name == "End" )
            Destroy(gameObject);
    }

}

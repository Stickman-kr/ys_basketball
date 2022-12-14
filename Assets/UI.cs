using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public Text timer, result, countdown, score, title;
    public GameObject restart_btn, quit_btn, start_btn, exit_btn;
    public double time_left, cnt;
    public double elapsedTime = 0;
    public double Tickingloop = 1;
    public double Gameoverloop = 1;
    public double Cntdownloop = 1;
    public Image main;
    public AudioSource BGM;
    public AudioSource Ticking;
    public AudioSource Gameover;
    public AudioSource Cntdown;
    public double is_ingame = 0;

    public void Start()
    {
        result.enabled = false;
        restart_btn.SetActive(false);
        quit_btn.SetActive(false);
        timer.enabled = false;
        score.enabled = false;
        start_btn.SetActive(true);
        exit_btn.SetActive(true);
        main.gameObject.SetActive(true);
        title.enabled = true;
        BGM.Play(0);
    }

    public void Game_Start()
    {
        
        countdown.enabled = true;
        result.enabled = false;
        restart_btn.SetActive(false);
        quit_btn.SetActive(false);
        timer.enabled = false;
        score.enabled = false;
        start_btn.SetActive(false);
        exit_btn.SetActive(false);
        main.gameObject.SetActive(false);
        title.enabled = false;
        elapsedTime = 0;
        BGM.Stop();
        is_ingame = 1;
        Tickingloop = 1;
        Gameoverloop = 1;
        Cntdownloop = 1;
    }

  
    // Update is called once per frame
    public void Update()
    {
        elapsedTime += Time.deltaTime;
        time_left = 19 - elapsedTime;
        cnt = 3.5 - elapsedTime;

        
        timer.text = "<b>Time left : " + time_left.ToString("N1") + "</b>";
        score.text = "<b>Score : " + $"{GameManager.score}" + "</b>";

        countdown.text = cnt.ToString("N0");
        
        if (Cntdownloop == 1 && is_ingame == 1)
        {
            Cntdown.pitch = 1.05f;
            Cntdown.Play();
            Cntdownloop = 0;
        }

        if (cnt < 0.5)
        {
            countdown.text = "START";
            timer.color = Color.black;
        }

        if (cnt < -0.5)
        {
            timer.enabled = true;
            score.enabled = true;
            countdown.enabled = false;
        }


        if (time_left < 10)
        {
            timer.color = Color.red;
                if (Tickingloop == 1 && is_ingame == 1)
                {
                    Ticking.loop = true;
                    Ticking.Play(0);
                    Tickingloop = 0;
                }
        }

        if (time_left < 0)
        {
            timer.text = "<b>Time's out!</b>";
            score.enabled = false; 
            result.enabled = true;
            result.text = "Your Score\n\n<b>" + $"{GameManager.score}" + "</b>\n";
            restart_btn.SetActive(true);
            quit_btn.SetActive(true);     
            Ticking.Stop(); 
            if (Gameoverloop == 1 && is_ingame == 1)
            {
            Gameover.Play(0);
            Gameoverloop = 0;
            }
        }

    }

}

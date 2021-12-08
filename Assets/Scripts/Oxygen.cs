using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //For loading the scene after death
using UnityEngine.UI; // This is for getting image componenet of the bar

public class Oxygen : MonoBehaviour
{
    [SerializeField] AudioClip pickupChime;
    [SerializeField] AudioSource Footsteps;
    [SerializeField] AudioSource BGAudio;

    [SerializeField]
    private CanvasGroup DeathCan;
    [SerializeField]
    private CanvasGroup FakeMain;
    [SerializeField]
    GameObject Bar;
    public PlayerController MoveScript;

    float MaxO2 = 120;
    public float O2 = 120; //120 is the seconds of oxygen left (2 minutes)


    private void Start()
    {
        Bar.GetComponent<Image>().color = new Color(0, 200, 0);
    }

    void Update()
    {
        if (O2 > 0)
        {
            MoveScript.enabled = true; // Allow movement        
            DeathCan.alpha = 0f; //Hide death canvas while alive

            float Size = O2 / MaxO2;
            O2 -= Time.deltaTime;
            Bar.transform.localScale = new Vector2(0.9f, Size); //Set scale to percentage of max oxygen remaining

        }
        else
        {
            //Mute audio (footsteps audio persisted while movement was disabled if player was moving as they died)
            Footsteps.mute = true;
            MoveScript.enabled = false;
            if (DeathCan.alpha < 1)
            {
                DeathCan.alpha += Time.deltaTime / 3;
            }
            else
            {
                if(FakeMain.alpha < 1)
                {
                    BGAudio.mute = true;
                    FakeMain.alpha += Time.deltaTime; //Fake menu shows up faster to mask the fact that it's fake 
                }
                else //Load Main
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Oxygen"))
        {
            O2 += 15; //Add 15 seconds of oxygen
            if (O2 > 120) { O2 = 120; } // Prevent o2 from going over max
            GetComponent<AudioSource>().PlayOneShot(pickupChime);
            Destroy(collision.gameObject);
        }
    }
}

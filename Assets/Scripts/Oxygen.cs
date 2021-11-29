using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is for getting image componenet of the bar

public class Oxygen : MonoBehaviour
{
    [SerializeField] 
    private CanvasGroup DeathCan;
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
        if(O2 > 0){
            MoveScript.enabled = true; // Allow movement        
            DeathCan.alpha = 0f; //Hide death canvas while alive
            DeathCan.interactable = false; //Disable buttons on canvas while not visible

            float Size = O2 / MaxO2;
            O2 -= Time.deltaTime;
            Bar.transform.localScale = new Vector2(0.9f, Size); //Set scale to percentage of max oxygen remaining

        }
        else
        {
            MoveScript.enabled = false;
            DeathCan.alpha = 1f;
            DeathCan.interactable = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Oxygen"))
        {
            O2 += 15; //Add 15 seconds of oxygen
            if(O2 > 120) { O2 = 120; } // Prevent o2 from going over max
            Destroy(collision.gameObject);
        }
    }
}

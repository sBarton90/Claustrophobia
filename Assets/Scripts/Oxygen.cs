using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is for getting image componenet of the bar

public class Oxygen : MonoBehaviour
{
    float MaxO2 = 120;
    [SerializeField] float O2 = 10; //120 is the seconds of oxygen left (2 minutes)
    [SerializeField] GameObject Bar;

    private void Start()
    {
        Bar.GetComponent<Image>().color = new Color(0, 200, 0);
    }

    void Update()
    {
        if(O2 > 0){
            float Size = O2 / MaxO2;
            O2 -= Time.deltaTime;
            Bar.transform.localScale = new Vector2(0.9f, Size); //Set scale to percentage of max oxygen remaining
        }
        else
        {
            //Do something with player and scene
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Messages : MonoBehaviour {

    public static Messages instance;

    public Text landingMessage;
    public Text pointMessage;
    public Text skyMessage;
    Animation landingMessageAnimation;
    Animation pointMessageAnimation;
    Animation skyMessageAnimation;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        landingMessage.enabled = false;
        pointMessage.enabled = false;
        skyMessage.enabled = false;

        landingMessageAnimation = landingMessage.gameObject.GetComponent<Animation>();
        pointMessageAnimation = pointMessage.gameObject.GetComponent<Animation>();
        skyMessageAnimation = skyMessage.gameObject.GetComponent<Animation>();

    }

    public void TriggerSkyMessage(int value,int type)
    {

        skyMessage.enabled = true;

        string messageString = "";

        if (type == 1)
        {
            messageString = "TO THE STARS...\n+"+value;
        }
        if (type == 2)
        {
            messageString = "...AND BEYOND!\n+"+value;
        }

        skyMessage.text = messageString;
        skyMessageAnimation.Stop();
        skyMessageAnimation.Play();
    }

    public void TriggerPointMessage(int value)
    {
        pointMessage.enabled = true;

        string pointString = "+"+ value;

        pointMessage.text = pointString;
        pointMessageAnimation.Play();

    }

    public void TriggerLandingMessage(int value = 0)
    {
        landingMessage.enabled = true;

        string landingString = "";

        if (value == 2)
        {
            landingString = "SMOOTH LANDING\nX2";
        }
        if (value == 4)
        {
            landingString = "PERFECT LANDING\nX4";
        }

        landingMessage.text = landingString;
        landingMessageAnimation.Play();
    }


}

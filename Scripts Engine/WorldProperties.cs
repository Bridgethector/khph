using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldProperties : MonoBehaviour {
    public Color camBackgroundColor,camBackgroundSpecialColor;
    public GameObject normalWorld, specialWorld;
    public static WorldProperties instance;
	// Use this for initialization
	void Start () {
        instance = this;
        ActivateNormal();
	}


    public void ActivateNormal()
    {
        Camera.main.backgroundColor = camBackgroundColor;
        specialWorld.SetActive(false);
        normalWorld.SetActive(true);
    }
    public void ActivateSpecial()
    {
        Camera.main.backgroundColor = camBackgroundSpecialColor;
        specialWorld.SetActive(true);
        normalWorld.SetActive(false);
    }


}

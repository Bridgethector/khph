using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentBallGraphic : MonoBehaviour {


    public static CurrentBallGraphic instance;

    public SpriteRenderer sprite;
    public GameObject specialFireParticle, trail;

    private void Awake()
    {
        instance = this;
    }

}

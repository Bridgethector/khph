using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGraphicsManager : MonoBehaviour {
    public static WorldGraphicsManager instance;

    public Grounds currentGround;
    public GameObject currentWorldObject;
    public GameObject currentBallObject;

    public GameObject scene_world;
    public GameObject scene_ball;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (ObliusGameManager.instance.gameState == ObliusGameManager.GameState.menu)
        {
            if (scene_world.name != currentWorldObject.name)
            {
                Destroy(scene_world);
                GameObject newWorld = (GameObject)Instantiate(currentWorldObject);
                newWorld.transform.parent = Camera.main.transform;
                newWorld.name = currentWorldObject.name;
                scene_world = newWorld;
                newWorld.transform.localPosition = Vector3.zero;
            }


            if (scene_ball.name != currentBallObject.name)
            {
                Destroy(scene_ball);
                GameObject newBall = (GameObject)Instantiate(currentBallObject);
                newBall.name = currentBallObject.name;
                scene_ball = newBall;
            }
        }
    }
}

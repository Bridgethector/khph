using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGraphicMovement : MonoBehaviour {

    Rigidbody2D rigid;
    public bool doLean = true;
    public bool doRotation = false;
    // Update is called once per frame

    private void Start()
    {
        rigid = BallManager.instance.rigid;
        if (doLean) doRotation = false;

    }

    void Update () {

        if (doLean)
        {
            ControlGraphicsLean();
        }
        if (doRotation)
        {
            ControlGraphicsRotation();
        }


        transform.position = BallManager.instance.transform.position;

    }

  
    void ControlGraphicsRotation()
    {
        float rotSpeed = 100;
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.z += -rotSpeed * Time.deltaTime * BallManager.instance.rigid.velocity.x;
        transform.eulerAngles = eulerAngles;
    }


    void ControlGraphicsLean()
    {

        Vector3 eulerAngles = transform.eulerAngles;
        float lerper = leanLerper();
        eulerAngles.z = Mathf.LerpAngle(-90, 90, lerper);
        transform.eulerAngles = eulerAngles;

    }


    float leanLerper()
    {

        float leanSensivity = 1.5f;

        if (rigid.velocity.x > 0)
        {
            return (1 + (rigid.velocity.y / BallManager.instance.maxYvel * leanSensivity)) / 2f;
        }
        else
        {
            return (1 + (-rigid.velocity.y / BallManager.instance.maxYvel * leanSensivity)) / 2f;

        }
    }
}

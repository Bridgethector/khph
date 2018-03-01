using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float xOffset = 3;
    bool isDead;
    public static CameraManager instance;

  public  float maxCamY = 12;
    public float minCamY = 2.40f;

    public float maxCamOrtographic = 12;
    public float minCamOrtographic = 4;

    float maxBallY = 10;
    float minBallY = 3f;

    Camera cam;
    public float hitShakes = 10;
    public float hitShakeTime = 0.025f;

    public bool isOnSelectMenu;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isDead == false && isOnSelectMenu==false)
        {


            Vector3 pos = transform.position;

            pos.x = BallManager.instance.transform.position.x + xOffset;

            float ballPos = WorldGraphicsManager.instance.scene_ball.transform.position.y;

            ballPos = Mathf.Clamp(ballPos, minBallY, maxBallY);

            float lerper = (ballPos - minBallY) / maxBallY;

            float calculatedPosY = Mathf.Lerp(minCamY, maxCamY, lerper);

            pos.y = Mathf.Lerp(pos.y,calculatedPosY,10*Time.deltaTime);

            float calculatedOrtographic = Mathf.Lerp(minCamOrtographic, maxCamOrtographic, lerper);


            transform.position = pos;


            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, calculatedOrtographic, 10 * Time.deltaTime);


        }
    }

    public void Die()
    {
        isDead = true;
        StartCoroutine(CameraShake());
    }

    IEnumerator CameraShake() {
        Vector3 pos = transform.position;

        for (int i = 0; i < hitShakes; i++) {

            Vector3 target = pos+ Random.onUnitSphere*0.1f;

            yield return new WaitForSeconds(hitShakeTime);
            transform.position = target;

        }


    }

  
}

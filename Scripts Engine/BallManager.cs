using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallManager : MonoBehaviour {
    public static BallManager instance;
    public Transform ballTip;
    public Rigidbody2D rigid;
    public ParticleSystem hitparticle;
    float acceleration = 50;

  public  float maxYvel =13; // max falling speed
    float upperPoint = 1.8f; //point where the ball if click hold will act for descending
    int landingBonus;
    int maxLandingBonus = 6;
    float minMagnitudeForLandingBonus = 4.5f; // after this level the player will loose his landing bonus
    float minYVelocityForLandingBonus = -2; // only if descending it's considered a landing

    bool isGround;
    bool prepareForLanding;
    bool jumped = false;
    float savedMagnitude = 0;

    public Text landingBonusDebugText;

    bool dead;

   public int lastLandingBonus;



	void Awake () {
        instance = this;
	}
    private void Start()
    {
        Physics2D.gravity = new Vector2(0, -7.5f);
        StartCoroutine(CheckStoppingCrash());
    }

    public void EnableMovement()
    {
        rigid.simulated = true;
        rigid.AddForce(Vector2.right/10);
    }

    private void Update()
    {
        landingBonusDebugText.text = landingBonus.ToString();

        landingBonus = Mathf.Clamp(landingBonus, 0, maxLandingBonus);



        CheckLandingStatus();


    }



    void CheckLandingStatus()
    {
        if (prepareForLanding == false)
        {
            if (transform.position.y >= upperPoint)
            {
                
                prepareForLanding = true;
            }
        }

        if (transform.position.y >= upperPoint && jumped ==false)
        {
            jumped = true;

            StartCoroutine(BoostRoutine());

        }


        if (transform.position.y < upperPoint && jumped == true)
        {
            jumped = false;

        }

    }

    IEnumerator BoostRoutine()
    {
        Vector2 velocity = rigid.velocity;
        Vector2 targetVelocity = velocity;
         targetVelocity.y = targetVelocity.y*(0.5f + ((float)(landingBonus) /maxLandingBonus));

        float lerper = 0;
        float lerperTime = 0.25f;
        while (lerper <= 1)
        {
            rigid.velocity = Vector2.Lerp(velocity, targetVelocity, lerper);
            lerper += Time.deltaTime / lerperTime;
            yield return new WaitForEndOfFrame();
        }
    }




 
    // Update is called once per frame
    void FixedUpdate () {
        if (dead || rigid.simulated==false) return;

        float maxXVelocity = 10f;
        if (landingBonus >= 1)
        {
            maxXVelocity += ((float)landingBonus / 1.75f);
        }

        if (rigid.velocity.magnitude < minMagnitudeForLandingBonus && landingBonus!=0)
        {
            landingBonus = 0;
            SpecialEffectManager.instance.TriggerSpecialEffect(false);

        }

        Vector2 tr_right = ballTip.right;
        tr_right.x = Mathf.Abs(tr_right.x);


        if (Input.GetMouseButton(0)) // when you press 
        {

            if (!isGround)
            {

                if (transform.position.y >= upperPoint)
                {
                    rigid.gravityScale =  4f;
                    rigid.AddForce(tr_right * -1.5f); // wind resistence

                }
                else
                {
                    rigid.gravityScale = 5f;

                }
            }
            else {

                rigid.AddForce(tr_right * acceleration );
                rigid.gravityScale = 5f;

            }

        }
        else        // when you are not pressing
        {
            if (!isGround)
            {

                float velYVal = rigid.velocity.y;
                if (velYVal > 0) { velYVal = 0; }
                else { velYVal = velYVal / 10; }
                    rigid.gravityScale = 0.50f- ((float)velYVal/1.5f);
                rigid.AddForce(tr_right * -0.40f); // wind resistence

            }
            else
            {
                rigid.gravityScale = 5f;
                rigid.AddForce(tr_right * 6.5f);

            }
        }

        Vector2 vel = rigid.velocity;
        vel.x = Mathf.Clamp(vel.x, -1, maxXVelocity);
        vel.y = Mathf.Clamp(vel.y, -maxYvel, maxYvel);     


        rigid.velocity = vel;
        savedMagnitude = rigid.velocity.magnitude;

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (prepareForLanding)
        {

            LandingCheck();
        }

        transform.LookAt(collision.contacts[0].point);
        isGround = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (prepareForLanding)
        {

            LandingCheck();
        }

        transform.LookAt(collision.contacts[0].point);
        isGround = true;
    }

    IEnumerator CheckStoppingCrash()
    {

        float currentX = rigid.velocity.x;

        while (!dead)
        { // this is made because the unity physics engine sucks so 
            // cannot detect the collision properly
            currentX = rigid.velocity.magnitude;
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            float vel = rigid.velocity.magnitude;
            if (vel < currentX - LandingManager.instance.stoppingCrashMagnitude)
            { //death
                LandingManager.instance.Landing(99);

            }
        }


    }

    void LandingCheck() {
        prepareForLanding = false;
        StartCoroutine(CheckContactOnlanding());
    }

    IEnumerator CheckContactOnlanding()
    {
        float saved = savedMagnitude;
        yield return new WaitForFixedUpdate();
        float currentMagnitude = rigid.velocity.magnitude;

        if (rigid.velocity.x > minMagnitudeForLandingBonus) {
        LandingManager.instance.Landing(saved - currentMagnitude);
        }
       
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
    void RoughHit()
    {
        hitparticle.Play();
       

    }


    public void Land(bool smooth=false, bool perfect=false, bool crash=false, bool bad=false) {


        bool specialLanding = false;
            if (smooth)
            {

            landingBonus += 1;
            lastLandingBonus = 2;
            GameEventsCollection.instance.GetSpecialPoints(1);

            }
            if (perfect)
            {

                landingBonus += 3;
               specialLanding = true;
            lastLandingBonus = 4;
            GameEventsCollection.instance.GetSpecialPoints(1);


        }
        if (rigid.velocity.y >= minYVelocityForLandingBonus) // this will prevent that a landing bonus occur when sliding on a surface
             {
            RoughHit();
            landingBonus = 0;
            lastLandingBonus = 0;
            specialLanding = false;
              }

      
        if (crash)
        {
            RoughHit();
            landingBonus = 0;
            CameraManager.instance.Die();
            StartCoroutine(DeathRoutine());
            SpecialEffectManager.instance.TriggerSpecialEffect(false);

            if (VibrationButton.vibrationEnabled)
            {
                Handheld.Vibrate();
            }
            return;

        }

        if (bad)
        {
            RoughHit();
            landingBonus = 0;
            lastLandingBonus = 0;
        }

       if (specialLanding) {
            SpecialEffectManager.instance.TriggerSpecialEffect(true);
        } else {   // for the fire effect
            SpecialEffectManager.instance.TriggerSpecialEffect(false);
        }

        Messages.instance.TriggerLandingMessage(lastLandingBonus);
    }
    
    IEnumerator DeathRoutine()
    {
        yield return new WaitForEndOfFrame();
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 0.25f;
        dead = true;
        yield return null;

    }

}

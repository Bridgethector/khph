using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingManager : MonoBehaviour
{

    float perfect_landing_val = 1.4f;
    float smooth_landing_val = 4f;
    float bad_landing_val = 5.4f;

    [HideInInspector]
    public float stoppingCrashMagnitude = 6; // used directly from Ball Manager


    public static LandingManager instance;

    void Awake()
    {
        instance = this;
    }

    public void Landing(float landingValue)
    {
        if (landingValue <= perfect_landing_val) // perfect landing
        {
            PerfectLanding();
            return;
        }

        if (landingValue <= smooth_landing_val) // smooth landing
        {
            SmoothLanding();
            return;
        }

        if (landingValue <= bad_landing_val && landingValue> smooth_landing_val) // crash
        {
            BadLanding();
            return;
        }

        if (landingValue >= bad_landing_val) // crash
        {
            CrashLanding();
            return;
        }



    }

    void BadLanding()
    {
        BallManager.instance.Land(false,false,false,true);
    }
  

    void SmoothLanding()
    {
        BallManager.instance.Land(true, false, false);
    }

    void PerfectLanding()
    {
        BallManager.instance.Land(false, true, false);
    }
    void CrashLanding()
    {
       
        BallManager.instance.Land(false, false, true);
        GameEventsCollection.instance.GameOver();
    }
}

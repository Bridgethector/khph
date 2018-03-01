using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectManager : MonoBehaviour {
    public static SpecialEffectManager instance;

    private void Awake()
    {
        instance = this;


    }
    private void Start()
    {
        TriggerSpecialEffect(false);

    }
    public void TriggerSpecialEffect(bool val)
    {
      

        if (CurrentBallGraphic.instance.specialFireParticle.activeInHierarchy != val)
        {
            CurrentBallGraphic.instance.specialFireParticle.SetActive(val);
            if (!val)
            {
                WorldProperties.instance.ActivateNormal();
            }
            else
            {
                WorldProperties.instance.ActivateSpecial();

            }
            CurrentBallGraphic.instance.trail.SetActive(!val);

        }

    }
}

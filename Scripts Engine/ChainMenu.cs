using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChainMenu : MonoBehaviour {
    public RectTransform[] chainRectTransforms;
     Vector2[] anchorPositions;
    Image[] anchoredImages;
    RectTransform thisRectTransform;
    Vector3 currentEuler,openedEuler;
    public bool opened;
	// Use this for initialization
	void Start () {
 
        thisRectTransform = GetComponent<RectTransform>();
        anchorPositions = new Vector2[chainRectTransforms.Length];
        anchoredImages = new Image[chainRectTransforms.Length];
        currentEuler = thisRectTransform.transform.eulerAngles;
        openedEuler = currentEuler; openedEuler.z += 90;
        for (int i = 0; i < chainRectTransforms.Length; i++)
        {
            anchorPositions[i] = chainRectTransforms[i].anchoredPosition;
            anchoredImages[i] = chainRectTransforms[i].gameObject.GetComponent<Image>();
        }

        Animate(false, true);

	}

    public void PressMenu()
    {
        opened = !opened;
        Animate(opened);
    }



    // immediate is used to hide instantly the elements
    public void Animate(bool inOut, bool immediate=false) {
        StartCoroutine(AnimationRoutine(inOut,immediate));
    }

    IEnumerator AnimationRoutine(bool inOut,bool immediate)
    {
        float lerper = 0;
        float lerperTime = 0.3f;
        Color a = Color.white;
        Color b = a;
        b.a = 0;
        if (immediate) lerper = 1;

        for (int i = 0; i < chainRectTransforms.Length; i++)
        {

            chainRectTransforms[i].gameObject.GetComponent<Button>().interactable = inOut;

        }

        while (lerper <= 1) {
            lerper += Time.deltaTime / lerperTime;

            for (int i = 0; i < chainRectTransforms.Length; i++)
            {

                if (inOut) {

                    chainRectTransforms[i].anchoredPosition =
                        Vector2.Lerp(thisRectTransform.anchoredPosition , anchorPositions[i], lerper);

                    anchoredImages[i].color = Color.Lerp(b, a, lerper);


                    Vector3 euler = thisRectTransform.transform.eulerAngles;
                    euler.z = Mathf.LerpAngle(currentEuler.z, openedEuler.z, lerper);
                    thisRectTransform.transform.eulerAngles = euler;


                } else {


                    chainRectTransforms[i].anchoredPosition =
                        Vector2.Lerp(anchorPositions[i],thisRectTransform.anchoredPosition, lerper);

                   anchoredImages[i].color = Color.Lerp(a, b, lerper);


                    Vector3 euler = thisRectTransform.transform.eulerAngles;
                    euler.z = Mathf.LerpAngle(openedEuler.z, currentEuler.z, lerper);
                    thisRectTransform.transform.eulerAngles = euler;
                }




            }

            yield return new WaitForEndOfFrame();

        }
    }

}

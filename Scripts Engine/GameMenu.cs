using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {
    public static GameMenu instance;
    public GameObject buttonsCompound;
    public GameObject selectMenu;
    public GameObject mainMenu;
    Vector3 cameraNormalPosition;
    Vector3 cameraMenuPosition;
    void Awake()
    {
        instance = this;
        buttonsCompound.SetActive(false);

    }
    private void Start()
    {
    }
    public void DetachMenuElements()
    {
        this.transform.parent = null;
        buttonsCompound.SetActive(true);
    }

    public void ActivateSelectMenu()
    {
        if (cameraNormalPosition == Vector3.zero)
        {
            cameraNormalPosition = Camera.main.transform.position;
            cameraMenuPosition = Camera.main.transform.position+Vector3.up*5;

        }
        StartCoroutine(MenuAnimationMovement(true));
    }

    public void DeactivateSelectMenu()
    {
       
        StartCoroutine(MenuAnimationMovement(false));
    }



    IEnumerator MenuAnimationMovement(bool inOut)
    {

        float lerper = 0;
        float lerperTime = 0.35f;
        Vector3 a = cameraNormalPosition;
        Vector3 b = cameraMenuPosition;
        if (inOut == false)
        {
            Vector3 tempA = a;
            a = b;
            b = tempA;
        }
        CameraManager.instance.isOnSelectMenu = inOut;
       while (lerper <= 1)
        {
            lerper += Time.deltaTime / lerperTime;
            yield return new WaitForEndOfFrame();
            Camera.main.transform.position = Vector3.Lerp(a, b, lerper);

        }

        selectMenu.SetActive(inOut);
        mainMenu.SetActive(!inOut);

    }
}

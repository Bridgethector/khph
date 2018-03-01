using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSelect : MonoBehaviour {
    public Button button;
    public Image buttonImage;
    public Image isSelectedImage;
    public Color notUnlockedColor;
    ButtonSelectProperties property;
    public bool isUnlocked;
    public bool isSelected;

    public void SetProperty(ButtonSelectProperties prop)
    {
        property = prop;
        buttonImage.sprite = prop.spriteForButtons;
        prop.CalculateMessage();
         isUnlocked = prop.isUnlocked;
     
    }

    private void Update()
    {
        if (isSelected)
        {
            isSelectedImage.enabled = true;
        } else
        {
            isSelectedImage.enabled = false;

        }

        if (!isUnlocked)
        {
            buttonImage.color = notUnlockedColor;
        }
        else
        {
            buttonImage.color = Color.white;
        }
    }
}

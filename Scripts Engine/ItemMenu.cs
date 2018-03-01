using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemMenu : MonoBehaviour {
    public ButtonSelect originalItemButton;
   public  List<ButtonSelect> buttonsList = new List<ButtonSelect>();
    public ButtonSelectProperties[] properties;
    float currentX = 0;
    float distanceBetweenButtons = 225;
    RectTransform originalButtonRectTransform;
  public   MenuScroller menuScroller;
    public int currentMenuItem;
    public MenuType menuType;
    public enum MenuType
    {
        balls,grounds,worlds
    }
    private void Awake()
    {
        menuScroller = gameObject.GetComponent<MenuScroller>();
        originalButtonRectTransform = originalItemButton.gameObject.GetComponent<RectTransform>();

        SpawnButtons();
        SetUpButtons();
    }
    // Use this for initialization
    void Start () {

	}
    private void Update()
    {
        currentMenuItem = menuScroller.nearestElement;
    }

    public void SpawnButtons()
    {
        foreach(ButtonSelectProperties item in properties) {
            GameObject newButton = (GameObject)Instantiate(originalItemButton.gameObject);
            // cloning the properties of the original button
            newButton.transform.parent = originalItemButton.transform.parent;
            newButton.transform.localScale = new Vector3(1, 1, 1);
            newButton.transform.eulerAngles = Vector3.zero;
            RectTransform rectTransform = newButton.GetComponent<RectTransform>();
            Vector2 pos = originalButtonRectTransform.anchoredPosition;
            pos.x = currentX; /* increasing distance*/ currentX += distanceBetweenButtons;
            rectTransform.anchoredPosition = pos;
            buttonsList.Add(newButton.GetComponent<ButtonSelect>());
        }
       menuScroller.SetScrollSize(distanceBetweenButtons, properties.Length);
        originalItemButton.gameObject.SetActive(false);
    }


    void SetUpButtons()
    {
        for (int i = 0; i < buttonsList.Count; i++) {

            buttonsList[i].SetProperty(properties[i]);
        }
    }
}

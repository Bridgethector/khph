using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSelectionMenu : MonoBehaviour {

    public ItemMenu currentMenu;
    public ItemMenu[] menus;
    int currentMenuItem;
    public Text infoMessage;
    public Button selectButton;

    bool infoMessageActive;
    bool setButtonActive;
    Color currentInfoMessageColor;
    Color fadedInfoMessageColor;
    Color currentSetButtonColor;
    Color fadedSetButtonColor;

   public Button[] navigationButtons;
    public Text[] infoTextButtons;
    public Image[] fillImageButtons;

    public string[] savedMenuSelectPrefs;
    int[] savedMenuSelectInts = new int[3];


    void LoadPrefs()
    {
        for (int i = 0; i < savedMenuSelectPrefs.Length; i++)
        {
            savedMenuSelectInts[i] = PlayerPrefs.GetInt(savedMenuSelectPrefs[i],0);
        }
    }
    void SavePrefs() {
        for (int i = 0; i < savedMenuSelectPrefs.Length; i++)
        {
           PlayerPrefs.SetInt(savedMenuSelectPrefs[i], savedMenuSelectInts[i]);
         }
    }


    private void Start()
    {
        LoadPrefs();

        currentInfoMessageColor = infoMessage.color;
        fadedInfoMessageColor = currentInfoMessageColor;
        fadedInfoMessageColor.a = 0;

        currentSetButtonColor = selectButton.image.color;
        fadedSetButtonColor = currentSetButtonColor;
        fadedSetButtonColor.a = 0;

        SetStartingItems();
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckInfosNumber();

        if (currentMenu.gameObject.activeInHierarchy) {
            ControlSelectButtons();

        }

        if (this.gameObject.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                ActivateMenu(0);
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                ActivateMenu(1);
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                ActivateMenu(2);
            }
        }

    }

    void SetStartingItems()
    {
        ActivateMenu(1);
        SetItem(false);
        ActivateMenu(2);
        SetItem(false);
        ActivateMenu(0);    
        SetItem(false);
    }

    public void SetItem(bool byButton=true)
    {
        int itemNumber = 0;
        if (byButton)
        {
            itemNumber = currentMenu.currentMenuItem;
        }
        else
        {
            if (currentMenu.menuType == ItemMenu.MenuType.balls)
            {
                itemNumber = savedMenuSelectInts[0];
            }
            if (currentMenu.menuType == ItemMenu.MenuType.worlds)
            {
                itemNumber = savedMenuSelectInts[1];

            }
            if (currentMenu.menuType == ItemMenu.MenuType.grounds)
            {
                itemNumber = savedMenuSelectInts[2];

            }
        }

        if (currentMenu.menuType == ItemMenu.MenuType.balls)
        {
            WorldGraphicsManager.instance.currentBallObject = currentMenu.properties[itemNumber].gameObject;
            savedMenuSelectInts[0] = itemNumber;
        }
        if (currentMenu.menuType == ItemMenu.MenuType.worlds)
        {
            WorldGraphicsManager.instance.currentWorldObject = currentMenu.properties[itemNumber].gameObject;
            savedMenuSelectInts[1] = itemNumber;

        }
        if (currentMenu.menuType == ItemMenu.MenuType.grounds)
        {
            WorldGraphicsManager.instance.currentGround = currentMenu.properties[itemNumber].gameObject.GetComponent<Grounds>();
            savedMenuSelectInts[2] = itemNumber;

        }
        SetTheUsingImageOnButtons(itemNumber);
        SavePrefs();
    }

    void SetTheUsingImageOnButtons(int item) {

        

            for (int i = 0; i < currentMenu.buttonsList.Count; i++)
            {
                if (i != item)
                {
                currentMenu.buttonsList[i].isSelected = false;
                }
                else
                {
                currentMenu.buttonsList[i].isSelected = true;
                }
            }
        
    }


    void CheckInfosNumber() {
        for (int i = 0; i < menus.Length; i++)
        {
            int numberOfElements = menus[i].buttonsList.Count;
            int numberOfUnlockeds = 0;
            for (int z = 0; z < menus[i].buttonsList.Count; z++)
            {
                if (menus[i].buttonsList[z].isUnlocked)
                {
                    numberOfUnlockeds++;
                }
            }
            infoTextButtons[i].text = "(" + numberOfUnlockeds + "/" + numberOfElements + ")";
            fillImageButtons[i].fillAmount = (float)numberOfUnlockeds / numberOfElements;
        }
    }

    public void ActivateMenu(int menuId)
    {
        for (int i =0;i<menus.Length;i++)
        {
            menus[i].gameObject.SetActive(false);
            navigationButtons[i].transform.localScale = new Vector3(1, 1, 1);
            fillImageButtons[i].transform.localScale = new Vector3(1, 1, 1);
        }

        menus[menuId].gameObject.SetActive(true);
        navigationButtons[menuId].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        fillImageButtons[menuId].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        currentMenu = menus[menuId];
    }


    public void ControlSelectButtons() // control fades the buttons and the info message
    {
        currentMenuItem = currentMenu.currentMenuItem;

        try
        {
            selectButton.gameObject.SetActive(currentMenu.buttonsList[currentMenuItem].isUnlocked);
        }
        catch { selectButton.gameObject.SetActive(false); }
        if (currentMenu.menuScroller.isOnItemElement)
        {

            infoMessage.text = currentMenu.properties[currentMenuItem].infoMessage;
            if (infoMessageActive == false) { StartCoroutine(InfoMessageFade()); }
            if (setButtonActive == false ) { StartCoroutine(SetButtonFade()); }

        }
        else
        {
            if (infoMessageActive == true) { StartCoroutine(InfoMessageFade()); }
            if (setButtonActive == true) { StartCoroutine(SetButtonFade()); }

        }
    }


    IEnumerator SetButtonFade()
    {
        float lerper = 0;
        float lerperTime = 0.2f;
        Color a = Color.white;
        Color b = Color.white;


        if (setButtonActive == false)
        {
            b = currentSetButtonColor;
            a = fadedSetButtonColor;
            setButtonActive = true;
        }
        else
        {
            a = currentSetButtonColor;
            b = fadedSetButtonColor;
            setButtonActive = false;

        }

        selectButton.interactable = setButtonActive;


        while (lerper <= 1)
        {
            lerper += Time.deltaTime / lerperTime;

            selectButton.image.color = Color.Lerp(a, b, lerper);

            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator InfoMessageFade()
    {
        float lerper = 0;
        float lerperTime = 0.2f;
        Color a = Color.white;
        Color b = Color.white;

        if (infoMessageActive == false)
        {
             b = currentInfoMessageColor;
             a = fadedInfoMessageColor;
            infoMessageActive = true;
        }
        else
        {
             a = currentInfoMessageColor;
             b = fadedInfoMessageColor;
            infoMessageActive = false;

        }
        while (lerper <= 1)
        {
            lerper += Time.deltaTime / lerperTime;
            
                infoMessage.color = Color.Lerp(a, b, lerper);
           
                yield return new WaitForEndOfFrame();
        }

    }
}

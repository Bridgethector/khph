using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLinesManager : MonoBehaviour {
    public static BonusLinesManager instance;
    public Transform bottomLine;
    public Transform topLine;
    public Transform skyLine;
    public SpriteRenderer bottomLineSprite;
    Color bottomLineSpriteStartColor;
    Color bottomLineFadeColor; 
    bool bottomTaken, topTaken, skyTaken;

	void Awake () {

        bottomLineSpriteStartColor = bottomLineSprite.color;
        bottomLineFadeColor = bottomLineSpriteStartColor;
        bottomLineFadeColor.a = 0;

        topLine.GetComponent<SpriteRenderer>().enabled = false;
        skyLine.GetComponent<SpriteRenderer>().enabled = false;

        instance = this;
	}

    private void Start()
    {
        bottomLineSprite.color = bottomLineFadeColor;
    }

    public void FadeBottomLine(bool inOut)
    {
        StartCoroutine(fadeBottomLineAnimation(inOut));
    }
    IEnumerator fadeBottomLineAnimation(bool inOut)
    {
        float lerper = 0;
        float lerperTime = 0.5f;
        Color a = bottomLineSpriteStartColor;
        Color b = bottomLineFadeColor;
        if (inOut == true)
        {
            Color tempA = a;
            a = b;
            b = tempA;
        }
        while (lerper <= 1)
        {
            bottomLineSprite.color = Color.Lerp(a, b, lerper);
            yield return new WaitForEndOfFrame();
            lerper += Time.deltaTime / lerperTime;
        }

    }


	// Update is called once per frame
	void Update () {

        float ballYPos = BallManager.instance.transform.position.y;
        Vector3 bottomlinepos = bottomLine.transform.position;
        bottomlinepos.x = Camera.main.transform.position.x;
        bottomLine.transform.position = bottomlinepos;
        if (ballYPos > bottomLine.position.y && bottomTaken == false)
        {
            bottomTaken = true;
            TakeBottomLine();
        }
        if (ballYPos > topLine.position.y && topTaken == false)
        {
            topTaken = true;
            TakeTopLine();
        }

        if (ballYPos > skyLine.position.y && skyTaken == false)
        {
            skyTaken = true;
            TakeSkyLine();
        }

        if (ballYPos < bottomLine.position.y)
        {
            if (bottomTaken)
            {
                topTaken = false;
                bottomTaken = false;
                skyTaken = false;
            }

        }

    }

    int getBonus (int multiplier)
    {
        int bonus = BallManager.instance.lastLandingBonus;
        bonus = Mathf.Clamp(bonus, 1, 999);
        bonus = bonus * multiplier;
        GameEventsCollection.instance.IncreasePoint(bonus);
        return bonus;
    }

    void TakeBottomLine() {
        int bonus = getBonus(1);
        Messages.instance.TriggerPointMessage(bonus);

    }

    void TakeTopLine() {
        int bonus = getBonus(2);
        Messages.instance.TriggerSkyMessage(bonus,1);

    }

    void TakeSkyLine()    {
        int bonus = getBonus(4);
        Messages.instance.TriggerSkyMessage(bonus, 2);



    }
}

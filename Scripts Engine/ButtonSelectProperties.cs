using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelectProperties : MonoBehaviour {
    public Sprite spriteForButtons;
    public enum CostType
    {
        landing,games,score,lifetimescore,nothing
    }
    public CostType costType;
    public int price;
    [HideInInspector]
    public string infoMessage;
    [HideInInspector]

    public bool isUnlocked;
    public void CalculateMessage()
    {
        if (costType == CostType.nothing)
        { infoMessage = "";
            isUnlocked = true;
        }

        if (costType == CostType.landing)
        {
            int currentScore = ScoreHandler.instance.specialPoints;
            currentScore = Mathf.Clamp(currentScore, 0, price);
            infoMessage = "Land " + price + " times correctly to unlock (" + currentScore + "/" + price + ")";

            if (currentScore == price)
            {
                isUnlocked = true;
            }
            else { isUnlocked = false; }
        }

        if (costType == CostType.games)
        {
            int currentScore = ScoreHandler.instance.numberOfGames;
            currentScore = Mathf.Clamp(currentScore, 0, price);

            infoMessage = "Play " + price + " times to unlock (" + currentScore + "/" + price + ")";

            if (currentScore == price)
            {
                isUnlocked = true;
            }
            else { isUnlocked = false; }

        }

        if (costType == CostType.score)
        {
            infoMessage = "Get more than " + price + " points in a game to unlock";

            if (ScoreHandler.instance.highScore >= price)
            {
                isUnlocked = true;
            }
            else { isUnlocked = false; }

        }

        if (costType == CostType.lifetimescore)
        {

            int currentScore = ScoreHandler.instance.highScore;
            currentScore = Mathf.Clamp(currentScore, 0, price);

            infoMessage = "Get a total score of " + price + " to unlock (" + currentScore + "/" + price + ")";

            if (currentScore == price)
            {
                isUnlocked = true;
            }
            else { isUnlocked = false; }


        }
    }

}

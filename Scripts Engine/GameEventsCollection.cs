using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsCollection : MonoBehaviour
{
    public static GameEventsCollection instance;
    bool gameStarted = false;
    void Awake()
    {
        instance = this;
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
       
    }

    public void StartGame()
    {
        gameStarted = true;

        BallManager.instance.EnableMovement();
        GameMenu.instance.DetachMenuElements();
        BonusLinesManager.instance.FadeBottomLine(true);

    }

    public void IncreasePoint(int val)
    {
       ScoreHandler.instance.increaseScore(val);

    }

    public void GetSpecialPoints(int value)
    {
        //     SoundsManager.instance.PlaySpecialPoint();
           ScoreHandler.instance.increaseSpecialPoints(value);
    }

    public void GameOver()
    {
        BonusLinesManager.instance.FadeBottomLine(false);

        ObliusGameManager.instance.GameOver(0.5f);
    }



}

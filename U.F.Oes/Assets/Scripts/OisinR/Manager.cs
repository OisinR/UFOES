using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public RectTransform slider;
    public RectTransform sliderBackground;

    public static float turnDistanceMax = 50;
    public static float turnDistanceTotal = 20;
    public static float turnDistance;
    private bool waitingForTurn;
    public PathMover[] aliens;
    public static int numberSelected = 0;

    void Start()
    {
        aliens = FindObjectsOfType<PathMover>();
        NewRound();
    }

    private void Update()
    {
        ScaleBar();
        if(waitingForTurn)
        {
            foreach (PathMover p in aliens)
            {
                if(p.turnOver == true)
                {
                    waitingForTurn = true;
                    return;
                }
            }
            NewRound();
            waitingForTurn = false;
        }
    }

    void ScaleBar()
    {
        sliderBackground.localScale = new Vector3(1, 1 - (turnDistanceTotal / turnDistanceMax), 1);

        float scale = 1 - (turnDistance / turnDistanceTotal);
        if(scale < 0)
        {
            scale = 0;
        }
        slider.localScale = new Vector3(1, scale, 1);
    }

    public void NewRound()
    {
        turnDistanceTotal = 20;
        turnDistance = 0;
    }


    public void EndTurn()
    {
        foreach (PathMover p in aliens)
        {
            p.turnOver = true;
        }
        waitingForTurn = true;
       
    }


    public void SelectAlien(int i)
    {
        numberSelected = i;
    }
}

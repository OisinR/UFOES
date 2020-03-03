using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public RectTransform slider;

    public static float turnDistanceMax;
    public float turnDistance;
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

        //Debug.Log(turnDistance);

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
            //Debug.Log(2342342);
            NewRound();
            waitingForTurn = false;
        }
    }

    void ScaleBar()
    {
        slider.localScale = new Vector3(1, 1 - (turnDistance/turnDistanceMax), 1);
        //Debug.Log(1 - (turnDistance / turnDistanceMax));
    }

    public void NewRound()
    {
        //Debug.Log(23423452525);
        turnDistanceMax = 20;
    }


    public void EndTurn()
    {
        foreach (PathMover p in aliens)
        {
            p.turnOver = true;
        }
        waitingForTurn = true;
        turnDistance = 0;
    }


    public void SelectAlien(int i)
    {
        numberSelected = i;
    }
}

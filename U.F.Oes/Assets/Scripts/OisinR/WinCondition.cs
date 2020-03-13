using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinCondition : MonoBehaviour
{
    public Base a, b, c;
    public GameObject winTxt;

    bool won;

    ManagerScript myManager;

    private void Awake()
    {
        myManager = FindObjectOfType<ManagerScript>();
    }
    float timer = 2f;

    void Update()
    {
        if(a.complete && b.complete && c.complete)
        {
            won = true;
            ManagerScript.ManagerAS.PlayOneShot(myManager.win);
            timer -= Time.deltaTime;
            if(timer<0f)
            {
                SceneManager.LoadScene(0);
            }
            winTxt.SetActive(true);
        }
    }
}

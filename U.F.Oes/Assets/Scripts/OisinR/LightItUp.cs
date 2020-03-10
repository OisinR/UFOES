using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightItUp : MonoBehaviour
{

    public bool LightSwitch;
    public GameObject[] lights;
    public Material[] materials;

    void Update()
    {
        if(LightSwitch)
        {
            foreach(GameObject g in lights)
            {
                g.SetActive(true);
            }
            foreach(Material m in materials)
            {
                m.EnableKeyword("_EMISSION");
            }
        }
        else
        {
            foreach (GameObject g in lights)
            {
                g.SetActive(false);
            }
            foreach (Material m in materials)
            {
                m.DisableKeyword("_EMISSION");
            }
        }
    }
}

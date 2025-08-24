using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class SwitchSceneScript : MonoBehaviour
{
    public GameObject presentTowerA;
    public GameObject pastTowerA;
    public XRLever lever;

    void Start()
    {
        pastTowerA.SetActive(false);
        presentTowerA.SetActive(true);
    }
    void Update()
    {
        
    }

    public void ActivateLever()
    {
        if(lever.value == false)//present Tower A; if lever.value = 0 -> false
        {
            pastTowerA.SetActive(false);
            presentTowerA.SetActive(true);
        }
        else if(lever.value == true)// past Tower A; if lever.value = 1 ->true
        {
            pastTowerA.SetActive(true);
            presentTowerA.SetActive(false);
        }
    }

}

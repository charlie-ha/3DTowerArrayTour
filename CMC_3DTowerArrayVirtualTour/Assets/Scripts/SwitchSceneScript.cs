using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneScript : MonoBehaviour
{
    public GameObject presentTowerA;
    public GameObject pastTowerA;

    void Start()
    {
        pastTowerA.SetActive(false);
        presentTowerA.SetActive(true);
    }
    public void TravelToThePast()
    {
        pastTowerA.SetActive(true);
        presentTowerA.SetActive(false);
    }
    public void TravelToThePresent()
    {
        pastTowerA.SetActive(false);
        presentTowerA.SetActive(true);
    }
}

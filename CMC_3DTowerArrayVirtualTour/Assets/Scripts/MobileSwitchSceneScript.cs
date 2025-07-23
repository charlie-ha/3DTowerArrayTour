using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileSwitchSceneScript : MonoBehaviour
{
    public GameObject presentTowerA;
    public GameObject pastTowerA;
    public GameObject past_PRESENT;//present menu button
    public GameObject PAST_Present;//past menu button

    void Start()
    {
        pastTowerA.SetActive(false);
        presentTowerA.SetActive(true);

        past_PRESENT.SetActive(true);//present menu button
        PAST_Present.SetActive(false);//past menu button
    }
    public void TravelToThePast()
    {
        pastTowerA.SetActive(true);
        presentTowerA.SetActive(false);
        
        past_PRESENT.SetActive(false);
        PAST_Present.SetActive(true);
    }
    public void TravelToThePresent()
    {
        pastTowerA.SetActive(false);
        presentTowerA.SetActive(true);

        past_PRESENT.SetActive(true);
        PAST_Present.SetActive(false);
    }
}

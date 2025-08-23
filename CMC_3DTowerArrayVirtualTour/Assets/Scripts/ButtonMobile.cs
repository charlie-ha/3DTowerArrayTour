using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem; // for debugging purposes




public class ButtonMobile : MonoBehaviour
{
    //button press logic: press button->light above button turns on->unlock handle->turn handle-> lock handle->light above button turns off
    public GameObject button;
    public Renderer lightIndicator; // Assign the Electro Pneumatic Interlocking Control button light
    public Material lightOn;
    public Material lightOff;
    AudioSource sound;
    bool isPressed;
    private Vector3 buttonOriginalPosition;

    //Train Handle logic: only move when button is pressed, when get to Reverse Position->Reverse Light turns on; Normal Position->Normal Light turns on, after 10s locks lever
    public EPICLeverScript epicLeverScript;//get access to unlock or lock lever 

    private float timer = 0f;//count time
    private float timeLimit = 30f;//10 seconds

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
        buttonOriginalPosition = button.transform.localPosition;
        lightIndicator.material = lightOff;//button light is off
    }

    void Update()
    {
        if(isPressed == true)//unlock lever
        {
            timer += 1 * Time.deltaTime;
            if(timer >= timeLimit)
            {
                LockHandle();//lock handle after 10 seconds
            }
        }
        
    }


    private void LockHandle()
    {
        button.transform.localPosition = buttonOriginalPosition;//button reverts to original position
        lightIndicator.material = lightOff;//button lights off
        timer = 0f;//reset timer
        epicLeverScript.unlocked = false;//dont let player interact lever
        isPressed = false;
        Debug.Log("light off");
    }

    public void PressButton()//lights up button light and unlock lever
    {
        if (!isPressed)
        {
            isPressed = true;
            Vector3 currentPosition = button.transform.localPosition;
            currentPosition.y -= 0.002f;
            currentPosition.z -= 0.0008f; 
            button.transform.localPosition = currentPosition;//press button down

            sound.Play();
            lightIndicator.material = lightOn;//light above button is on

            epicLeverScript.unlocked = true;//let player interact lever
        }
    }


}

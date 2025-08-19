using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem; // for debugging purposes
using UnityEngine.XR.Content.Interaction;



public class ButtonVR : MonoBehaviour
{
    //button press logic: press button->light above button turns on->unlock handle->turn handle-> lock handle->light above button turns off
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public Renderer lightIndicator; // Assign the Electro Pneumatic Interlocking Control button light
    public Material lightOn;
    public Material lightOff;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    private Vector3 buttonOriginalPosition;

    //Train Handle logic: only move when button is pressed, when get to Reverse Position->Reverse Light turns on; Normal Position->Normal Light turns on, after 10s locks lever
    public XRKnob trainControllerHandle;//get access to which angle is the lever
    public Renderer trainTerminalLightReverse;//this light indicates reverse position
    public Renderer trainTerminalLightNormal;//this light indicates normal position
    public Material greenLight;//swap this material in trainTerminalLight when lever is in reverse or normal position
    public GameObject lever;//to get the Sphere Collider in the lever

    private float timer = 0f;//count time
    private float timeLimit = 30f;//10 seconds

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
        buttonOriginalPosition = button.transform.localPosition;
        lightIndicator.material = lightOff;//button light is off
        trainTerminalLightReverse.material = lightOff;//terminal light is off
        trainTerminalLightNormal.material = lightOff;//terminal light is off
        lever.GetComponent<SphereCollider>().enabled = false;//stop the player from pulling lever
    }

    void Update()
    {
        if(Keyboard.current.fKey.wasPressedThisFrame)//for debuggign, will delete after finish debug
        {
            Vector3 currentPosition = button.transform.localPosition;
            currentPosition.y -= 0.002f;
            currentPosition.z -= 0.0008f; 
            button.transform.localPosition = currentPosition;//press button down

            onPress.Invoke();
            sound.Play();
            lightIndicator.material = lightOn;
            isPressed = true;
            lever.GetComponent<SphereCollider>().enabled = true;//let player interact lever
            Debug.Log("light on");
        }
        
        if(isPressed == true)//unlock lever
        {
            timer += 1 * Time.deltaTime;
            if(timer >= timeLimit)
            {
                LockHandle();//lock handle after 10 seconds
            }
            if(trainControllerHandle.value == 0f)//train handle turns left-> handle is in reverse position
            {
                trainTerminalLightReverse.material = greenLight;//Reverse terminal light is off
                trainTerminalLightNormal.material = lightOff;//Normal terminal light is off

            }
            else if(trainControllerHandle.value == 1f)//train handle turns right-> handle is in normal position
            {
                trainTerminalLightReverse.material = lightOff;//Reverse terminal light is off
                trainTerminalLightNormal.material = greenLight;//Normal terminal light is off

            }
            else if(trainControllerHandle.value == 0.5f)//train handle in the center-> handle is in neutral position
            {
                trainTerminalLightReverse.material = lightOff;//Reverse terminal light is off
                trainTerminalLightNormal.material = lightOff;//Normal terminal light is off
                 
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            Vector3 currentPosition = button.transform.localPosition;
            currentPosition.y -= 0.002f;
            currentPosition.z -= 0.0008f; 
            button.transform.localPosition = currentPosition;//press button down

            onPress.Invoke();
            sound.Play();
            lightIndicator.material = lightOn;//light above button is on
            isPressed = true;
            lever.GetComponent<SphereCollider>().enabled = true;//let player interact lever
        }
    }

    private void LockHandle()
    {
        button.transform.localPosition = buttonOriginalPosition;//button reverts to original position

        onRelease.Invoke();
        lightIndicator.material = lightOff;//button lights off
        timer = 0f;
        lever.GetComponent<SphereCollider>().enabled = false;//dont let player interact lever
        isPressed = false;
        Debug.Log("light off");
    }


}

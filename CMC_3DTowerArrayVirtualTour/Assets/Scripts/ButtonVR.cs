using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using NUnit.Framework.Constraints;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public Renderer lightIndicator; // Assign the Electro Pneumatic Interlocking Control button light
    public Material lightOn;
    public Material lightOff;
    GameObject presser;
    AudioSource sound;
    bool isPressed;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0,0.0,0.003f);//press button down
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            lightIndicator.material = lightOn;
            isPressed = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser) 
        {
            button.transform.localPosition = new Vector3(0, 0, 0.015f);//revert button back 
            onRelease.Invoke();
            isPressed = false;
        }
    }



}

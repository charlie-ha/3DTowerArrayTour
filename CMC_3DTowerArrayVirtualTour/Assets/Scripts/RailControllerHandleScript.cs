using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class RailControllerHandleScript : MonoBehaviour
{
    public XRKnob trainControllerHandle;
    public Light trainTerminalLight;
    //public AudioSource knobAudio;
    public Renderer terminalLightBulb; // Assign the train terminal light
    public Material activatedMaterial;
    public Material defaultMaterial;
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trainControllerHandle.value >= 0.8f)
        {
            trainTerminalLight.intensity = 100f;//turn on the light when handle turned right
            //knobAudio.Play();
            terminalLightBulb.material = activatedMaterial;//turn handle, activate light
        }
        else if (trainControllerHandle.value <= 0.5f)
        {
            trainTerminalLight.intensity = 0f;//turn off the light when handle turned left
            //knobAudio.Stop();
            terminalLightBulb.material = defaultMaterial;//turn handle, deactivate light
        }
    }
}

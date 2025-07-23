using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTerminalLeverScript : MonoBehaviour
{
    public Light trainTerminalLight;
    //public AudioSource knobAudio;
    public Renderer terminalLightBulb; // Assign the train terminal light
    public Material activatedMaterial;
    public Material defaultMaterial;
    
    public void ActivateLight()
    {
        terminalLightBulb.material = activatedMaterial;//activate lever, turns light blue
        trainTerminalLight.intensity = 100f;
    }
    public void DeactivateLight()
    {
        terminalLightBulb.material = defaultMaterial;//deactivate lever, turns light white
        trainTerminalLight.intensity = 0f;
    }
}

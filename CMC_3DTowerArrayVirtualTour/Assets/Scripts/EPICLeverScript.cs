using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class EPICLeverScript : MonoBehaviour
{
    

    //Train Handle logic: only move when button is pressed, when get to Reverse Position->Reverse Light turns on; Normal Position->Normal Light turns on, after 10s locks lever

    public bool unlocked = false;
    public Renderer trainTerminalLightReverse;//this light indicates reverse position
    public Renderer trainTerminalLightNormal;//this light indicates normal position
    public Material lightOff;
    public Material greenLight;//swap this material in trainTerminalLight when lever is in reverse or normal position

    void Start()
    {
        trainTerminalLightReverse.material = lightOff;//terminal light is off
        trainTerminalLightNormal.material = lightOff;//terminal light is off

    }

    void Update()
    {
        
        
    }
    


    public void TurnLever()//lights up button light and unlock lever
    {
        if(unlocked == true)//lever is unlocked
        {
            if(this.transform.eulerAngles.x == 0f)//train handle in neutral position (0 degree)->turns lever left-> handle is in reverse position
            {
                trainTerminalLightReverse.material = greenLight;//Reverse terminal light is on
                trainTerminalLightNormal.material = lightOff;//Normal terminal light is off
                this.transform.eulerAngles = new Vector3(335f, 0f, 0f);//rotate lever to -25 degrees
            }
            else if(this.transform.eulerAngles.x == -25f)//train handle in reverse position (-25 degree)->turns lever all to the right-> handle is in normal position
            {
                trainTerminalLightReverse.material = lightOff;//Reverse terminal light is on
                trainTerminalLightNormal.material = greenLight;//Normal terminal light is on
                this.transform.eulerAngles = new Vector3(25f, 0f, 0f);//rotate lever to 25 degrees
            }
            else if(this.transform.eulerAngles.x == 25f)//train handle in normal position(25 degree)->turns lever left-> handle is in neutral position
            {
                trainTerminalLightReverse.material = lightOff;//Reverse terminal light is off
                trainTerminalLightNormal.material = lightOff;//Normal terminal light is off
                this.transform.eulerAngles = new Vector3(0f, 0f, 0f);//rotate lever to 0 degree
            }
        }

    }


}

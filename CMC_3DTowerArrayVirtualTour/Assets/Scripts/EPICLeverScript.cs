using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class EPICLeverScript : MonoBehaviour
{
    

    //Train Handle logic: only move when button is pressed, when get to Reverse Position->Reverse Light turns on; Normal Position->Normal Light turns on, after 10s locks lever
    
    public bool coolingDown = false;
    private float timer = 0f;//count time
    private float coolDownTime = 0.5f;//0.5 seconds

    public bool unlocked = false;

    public Renderer ReverseTrainTerminalLight;//this light indicates reverse position
    public Renderer NormalTrainTerminalLight;//this light indicates normal position
    public Material lightOff;
    public Material greenLight;//swap this material in trainTerminalLight when lever is in reverse or normal position
    
    public AudioSource leverSound;
    public enum LeverState //3 states, if we need more lever position, just add to this list
    { 
        Neutral, 
        Reverse, 
        Normal 
    }
    public LeverState currentState = LeverState.Neutral;


    void Start()
    {
        ReverseTrainTerminalLight.material = lightOff;//terminal light is off
        NormalTrainTerminalLight.material = lightOff;//terminal light is off
        leverSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(coolingDown == true)//make sure the func TurnLever() is called once when you turn lever
        {
            timer += 1 * Time.deltaTime;
            if(timer >= coolDownTime)
            {
                coolingDown = false;
                timer = 0f;
            }
        }
        
    }
    

    public void TurnLever()
    {
        if(coolingDown == false)//make sure the func TurnLever() is called once when you turn lever
        {
            if (!unlocked) return;

            leverSound.pitch = Random.Range(0.9f, 1.1f);
            leverSound.Play();

            // Cycle states
            switch (currentState)
            {
                case LeverState.Neutral:
                    SetState(LeverState.Reverse);//if lever in Neutral position, rotate lever to Reverse position
                    break;

                case LeverState.Reverse:
                    SetState(LeverState.Normal);//if lever in Reverse position, rotate lever to Normal position
                    break;

                case LeverState.Normal:
                    SetState(LeverState.Neutral);//if lever in Normal position, rotate lever to Neutral position
                    break;
            }
            coolingDown = true;
        }
    }

    private void SetState(LeverState newState)//state machine
    {
        currentState = newState;
        switch (newState)
        {
            case LeverState.Neutral:
                transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                ReverseTrainTerminalLight.material = lightOff;
                NormalTrainTerminalLight.material = lightOff;
                break;

            case LeverState.Reverse:
                transform.localEulerAngles = new Vector3(0f, -25f, 0f);
                ReverseTrainTerminalLight.material = greenLight;
                NormalTrainTerminalLight.material = lightOff;
                break;

            case LeverState.Normal:
                transform.localEulerAngles = new Vector3(0f, 25f, 0f);
                ReverseTrainTerminalLight.material = lightOff;
                NormalTrainTerminalLight.material = greenLight;
                break;
        }
    }


}

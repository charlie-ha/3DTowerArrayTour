using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileFirstTutorial : MonoBehaviour
{
    private float lastYaw = 0f;
    [SerializeField] private float swipeThreshold = 5f; // degrees of movement before closing
    
    private float timer = 0f;
    [SerializeField] private float delayBeforeCanClose = 1.0f; // seconds
    [SerializeField] private GameObject firstTutorialPanel;

    void Start()
    {
        firstTutorialPanel.SetActive(true);
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > delayBeforeCanClose)
        {
            float currentYaw = transform.eulerAngles.y;
            float yawDelta = Mathf.DeltaAngle(lastYaw, currentYaw);

            if (Mathf.Abs(yawDelta) > swipeThreshold)
            {
                firstTutorialPanel.SetActive(false); // hide first tutorial UI
            }

            lastYaw = currentYaw;
        }

    }
}

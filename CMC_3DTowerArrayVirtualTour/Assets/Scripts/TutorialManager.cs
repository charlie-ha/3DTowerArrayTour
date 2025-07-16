using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject firstTutorialPanel;

    [SerializeField] private GameObject tutorialPanel; // UI panel with narration/text
    public Text tutorialText;

    [SerializeField] private GameObject highlightHotspot;
    [SerializeField] private GameObject hotspotToTap;

    [SerializeField] private GameObject contentIconsParent; // Parent object holding image/video/text icons

    public float swipeThreshold = 2f;
    public float delayBeforeSwipeClose = 1.0f;

    private float lastYaw;
    private float timer = 0f;
    private bool hasSwiped = false;

    private enum TutorialStep
    {
        None,
        HotspotIntro,
        WaitingForHotspotTap,
        ContentIntro,
        Ending
    }

    private TutorialStep currentStep = TutorialStep.None;

    void Start()
    {
        lastYaw = transform.eulerAngles.y;
        firstTutorialPanel.SetActive(true);
    }

    void Update()
    {
         
        if (hasSwiped) return;
        timer += Time.deltaTime;

        if (timer > delayBeforeSwipeClose)
        {
            float currentYaw = transform.eulerAngles.y;
            float yawDelta = Mathf.DeltaAngle(lastYaw, currentYaw);
            
            if (Mathf.Abs(yawDelta) > swipeThreshold)
            {
                hasSwiped = true;
                firstTutorialPanel.SetActive(false);//close first panel
                StartCoroutine(RunTutorial());
            }

            lastYaw = currentYaw;
        }
    }

    private IEnumerator RunTutorial()
    {
        yield return new WaitForSeconds(1f);

        // Step 2: Hotspot movement tutorial
        currentStep = TutorialStep.HotspotIntro;
        tutorialPanel.SetActive(true);//open second panel
        tutorialText.text = "See a glowing icon or circle? That’s a hotspot. Tap it to move to a new location.";
        
        highlightHotspot.SetActive(true);

        currentStep = TutorialStep.WaitingForHotspotTap;
        while (hotspotToTap != null && hotspotToTap.activeInHierarchy)
        {
            yield return null; // wait until user taps hotspot
        }

        highlightHotspot.SetActive(false);
        yield return new WaitForSeconds(1f);

        // Step 3: Content viewing tutorial
        currentStep = TutorialStep.ContentIntro;
        tutorialText.text = "When you see an icon, tap it to view special content.\n\n🖼 Tap the image icon to view a photo\n📹 Tap the video icon to watch a short video\n📄 Tap the text icon to read more";
        tutorialPanel.SetActive(true);
        contentIconsParent.SetActive(true);

        yield return new WaitForSeconds(4f);

        // Step 4: Ending
        currentStep = TutorialStep.Ending;
        tutorialText.text = "That’s it! Explore at your own pace. You can always come back to this tutorial from the settings menu.";
        yield return new WaitForSeconds(4f);

        EndTutorial();
    }

    private void EndTutorial()
    {
        tutorialPanel.SetActive(false);
        contentIconsParent.SetActive(false);
        this.enabled = false; // disable this script
    }
}

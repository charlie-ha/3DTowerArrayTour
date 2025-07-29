using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileTutorialManager : MonoBehaviour
{
    public Text tutorialDescription;
    public int index = 1;
    public GameObject nextButton;
    public GameObject previousButton;

    public GameObject swipeAnimationVideo;
    public GameObject tapAnimationVideo;
    public GameObject tapSpecialIconsAnimationVideo;
    public GameObject tapMenuButtonAnimationVideo;
    public GameObject hotspotIcon;
    public GameObject specialIcons;//image/video/text
    public GameObject invisibleCloseButton;

    public GameObject tutorialPanel;


    // Start is called before the first frame update
    void Start()
    {
        swipeAnimationVideo.SetActive(false);
        tapAnimationVideo.SetActive(false);
        tapSpecialIconsAnimationVideo.SetActive(false);
        tapMenuButtonAnimationVideo.SetActive(false);
        hotspotIcon.SetActive(false);
        specialIcons.SetActive(false);
        invisibleCloseButton.SetActive(false);

        UpdateTutorialStep();
    }

     
    public void UpdateTutorialStep()
    {
        if(index == 1)//tutorial step 1: swipe screen
        {
            tutorialDescription.text = "Welcome to the virtual tour!\n To look around, swipe your finger across the screen.";

            swipeAnimationVideo.SetActive(true);//
            tapAnimationVideo.SetActive(false);
            tapSpecialIconsAnimationVideo.SetActive(false);
            tapMenuButtonAnimationVideo.SetActive(false);
            hotspotIcon.SetActive(false);
            specialIcons.SetActive(false);

            previousButton.SetActive(false);
            nextButton.SetActive(true);

        }
        else if(index == 2)//tutorial step 2: tap on hotspot
        {
            tutorialDescription.text = "This glowing icon is a hotspot.\n Tap it to move to a new location.";

            swipeAnimationVideo.SetActive(false);
            tapAnimationVideo.SetActive(true);//
            tapSpecialIconsAnimationVideo.SetActive(false);
            tapMenuButtonAnimationVideo.SetActive(false);
            hotspotIcon.SetActive(true);//
            specialIcons.SetActive(false);
            
            previousButton.SetActive(true);
            nextButton.SetActive(true);
        }
        else if(index == 3)//tutorial step 3: tap on image/video/text icon
        {
            tutorialDescription.text = "These are special icons.\n Tap the image icon to view a photo.\n Tap the video icon to watch a short video.\n Tap the text icon to read more";

            swipeAnimationVideo.SetActive(false);
            tapAnimationVideo.SetActive(false);
            tapSpecialIconsAnimationVideo.SetActive(true);//
            tapMenuButtonAnimationVideo.SetActive(false);
            hotspotIcon.SetActive(false);
            specialIcons.SetActive(true);
            invisibleCloseButton.SetActive(false);

            previousButton.SetActive(true);
            nextButton.SetActive(true);
        }
        else if(index == 4)//tutorial step 4: tap on menu button to close tutorial
        {
            tutorialDescription.text = "That’s it!\n Explore at your own pace.\n You can always refer to the Menu button \n to find  this tutorial.";

            swipeAnimationVideo.SetActive(false);
            tapAnimationVideo.SetActive(false);
            tapSpecialIconsAnimationVideo.SetActive(false);
            tapMenuButtonAnimationVideo.SetActive(true);//
            hotspotIcon.SetActive(false);
            specialIcons.SetActive(false);
            invisibleCloseButton.SetActive(true);//

            previousButton.SetActive(false);
            nextButton.SetActive(false);
        }

    }


    public void NextStep()//call this on nextButton
    {
        index += 1;
        UpdateTutorialStep();
    }
    public void PreviousStep()//call this on previousButton
    {
        index -= 1;
        UpdateTutorialStep();
    }
    public void CloseTutorial()
    {
        index = 1;
        UpdateTutorialStep();
        tutorialPanel.SetActive(false);
    }
}

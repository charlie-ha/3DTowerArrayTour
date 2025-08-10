3D VR Tower Array Project Documentation
GitHub project: https://github.com/charlie-ha/3DTowerArrayTour

Build a 3D VR virtual tour of the tower array
https://www.youtube.com/results?search_query=cincinati+terminal+tower
https://www.youtube.com/watch?v=0h6CyRuKCKQ
Format: VR headset Meta Quest Pro (self-contained) and a point click web based virtual tour (on iPad).
Why: Visitors can’t always go see the tower, needs staff or mobility limited.
What: If you were there in 1920, what would you see.
Platform: Ipad (IOS build) and PC touchscreen (Windows build)
Web? 

Unity version? 6000.0.10f1?

Blueprint with measurements of the tower array.
How to use Testflight
https://www.youtube.com/watch?v=6VuLukGpNv8

References
https://www.trains.com/ctr/photos-videos/photo-of-the-day/nerve-center-of-cincinnati-union-terminal/
https://www.trainaficionado.com/tower-a/

Project Features:
(somewhat) Stylized but close to 3D rendered realism
Switch the tower array space between different eras (1930s v now)
Pull crank handles and turn signal light on
Stretch goals:
Models of trains run through the tracks
A virtual mannequin gives narration of the space

Note to self:
If Android build doesn't work, change back to URP Performance Config.

Sizing
1 unit in Unity = 1 meter

Scenes
Note for versioning: Different people working on different scenes to avoid conflicts when versioning. If you need to bring other people’s work into the main scene, just 1 person does that and then push to GitHub.

MainTowerScene_VR
Description: This is the main scene used for VR

MainTowerScene_Touchscreen
Description: This is the main scene used for Mobile Tour project.

TowerScene_LevelDesign
Description: This is the scene used for level design. 

Codes
ButtonVR
[VR Scene] Attached to buttons in Electro Pneumatic Interlocking Control (EPIC) Machine.
This script will handle how levers work.
How the lights above the EPIC machine are laid out: 
The 1st and 3rd rows are Reverse Light, 2nd and 4th rows are Normal Light. 
1st and 2nd rows are for Upper levers (levers that point upwards⬆️). 3rd and 4th are for Lower levers (levers that point downwards⬇️).
Each lever controls a set of Reverse Light and Normal Light, corresponding to the levers’ positions - Reverse Position and Normal Position. Reverse Position is when the lever is turned left, Normal Position is when the lever is turned right, Neutral Position is when the lever is in the middle.
How the EPIC machine works: 
Choose a set of light, i.e. the 1st light set.
Press the 1st button to unlock lever 1. (You need to press the button to use the lever, if not, the lever won’t work)
Turn lever 1 to left or right to turn on Reverse or Normal Light.

Simple diagram on how the EPIC machine works

Downside of this script is you need to wire all of the lightIndicator, trainControllerHandle, trainTerminalLightReverse, trainTerminalLightNormal, lever to each lever for each button.

CanvasUIScript. 
Attached in MediaImage>RawImageMedia>CloseButton
Used to spawn images for player in VR project
Used in close button to close the image/video on the screen in Mobile Tour project.
For tutorial, used to make Tutorial Panel 4 appear.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanvasUIScript : MonoBehaviour
{
   public GameObject prefab;
   public Transform spawnPosition;
  
   public GameObject tutorialPanel4;
  
   [SerializeField] private VirtualTourCameraController playerCamera;
   void Start()
   {
       playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VirtualTourCameraController>();
   }


   public void DestroyGameObject(GameObject thisGameObject)
   {
       Destroy(thisGameObject);
       if(playerCamera.tutorial == true)
           Instantiate(tutorialPanel4);
           playerCamera.tutorial = false;
   }
   public void EnableRaycast(bool boolean)
   {
       PlayerRayCast.instance.enabled = boolean;
   }


   public void SpawnPrefab()// spawn prefab/images/videos for VR project
   {
       Instantiate(prefab, spawnPosition.position, Quaternion.identity);
   }
}
PlayerRaycast
Attached to VirtualTourCamera
Used for player to move around the scene by clicking on hotspots

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerRayCast : MonoBehaviour
{
   Ray ray;
   RaycastHit hit;
   public static PlayerRayCast instance;


   private Camera mainCam;
   public Transform cameraTransform; // Drag your main camera here in Inspector
   private GameObject previousHotspot;
   private GameObject lastHoveredHotspot = null;


   public List<GameObject> allHotspots = new List<GameObject>();//put all hotspots in a list




   private Vector3 defaultScale = new Vector3(1f,1f,0.0001f);//default scale of the hotspot
   private Vector3 hoverScale = new Vector3(1.2f,1.2f,0.0001f);//hovered scale


   private Color defaultColor = Color.white;
   private Color hoverColor = new Color(203f/255f, 4f/255f, 4f/255f);


   public bool enabled = true;


   private InputAction pointerPositionAction;


   void Awake()
   {
       // Binds to both mouse and touchscreen pointer position
       pointerPositionAction = new InputAction(
           name: "PointerPosition",
           type: InputActionType.Value,
           binding: "<Pointer>/position"
       );
       pointerPositionAction.Enable();
   }
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
       // Cache all hotspots to the list at the start
       GameObject[] hotspots = GameObject.FindGameObjectsWithTag("hotspot");
       foreach (GameObject hs in hotspots)
       {
           allHotspots.Add(hs);
           
       }
       mainCam = Camera.main;
       instance = this;
   }


   // Update is called once per frame
   void Update()
   {
       if(enabled)//make sure we dont click through UI
       {
           //Vector2 pointerPosition = pointerPositionAction.ReadValue<Vector2>();
           Vector2 pointerPosition = Vector2.zero;


           if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
           {
               pointerPosition = Touchscreen.current.primaryTouch.position.ReadValue();
           }
           else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
           {
               pointerPosition = Mouse.current.position.ReadValue();
           }


           ray = mainCam.ScreenPointToRay(pointerPosition);//create ray from mouse to object
           if(Physics.Raycast(ray, out hit))
           {
               if (hit.collider.CompareTag("hotspot"))
               {
                   //hover on hotspot increase size and turn them red
                   GameObject hoveredHotspot = hit.collider.gameObject;


                   if (lastHoveredHotspot != null && lastHoveredHotspot != hoveredHotspot)
                   {
                       // Reset previous
                       lastHoveredHotspot.transform.localScale = defaultScale;
                       lastHoveredHotspot.GetComponent<Renderer>().material.color = defaultColor;
                   }


                   hoveredHotspot.transform.localScale = hoverScale;
                   Renderer hoveredRenderer = hoveredHotspot.GetComponent<Renderer>();
                   hoveredRenderer.material.color = hoverColor;


                   lastHoveredHotspot = hoveredHotspot;


                   if(Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)//click on hotspot
                   {  
                       if(hoveredHotspot.GetComponent<HotspotScript>().isSpawningPrefab == false)
                       {
                           GameObject clickedHotspot = hit.collider.gameObject;




                           //close tutorial panel at is above tutorial hotspot
                           CloseTutorialPanel tutorialHotspot = hit.collider.gameObject.GetComponent<CloseTutorialPanel>();
                           if (tutorialHotspot != null)
                           {
                               tutorialHotspot.tutorialPanel.SetActive(false);
                           }


                           // Move the camera to the hotspot's x and y position
                           Vector3 newPosition = new Vector3(
                               clickedHotspot.transform.position.x,
                               cameraTransform.position.y,
                               clickedHotspot.transform.position.z
                           );


                           cameraTransform.position = newPosition;


                           // Reactivate all other hotspots
                           foreach (GameObject hs in allHotspots)
                           {
                               if (hs != clickedHotspot)
                                   hs.SetActive(true);
                           }


                           // Deactivate the one just clicked
                           clickedHotspot.SetActive(false);
                          
                       }
                       else if(hoveredHotspot.GetComponent<HotspotScript>().isSpawningPrefab == true)//spawn image/video/text
                       {
                           Instantiate(hoveredHotspot.GetComponent<HotspotScript>().prefabToSpawn);
                           enabled = false;//disable raycast so we dont click through UI


                           //close tutorial panel at is above tutorial hotspot
                           CloseTutorialPanel tutorialHotspot = hit.collider.gameObject.GetComponent<CloseTutorialPanel>();
                           if (tutorialHotspot != null)
                           {
                               tutorialHotspot.tutorialPanel.SetActive(false);
                           }
                       }
                       // else if()
                       // {
                       //     //close tutorial panel at is above tutorial hotspot
                       //     CloseTutorialPanel tutorialHotspot = hit.collider.gameObject.GetComponent<CloseTutorialPanel>();
                       //     if (tutorialHotspot != null)
                       //     {
                       //         tutorialHotspot.tutorialPanel.SetActive(false);
                       //     }
                       // }


                      
                   }
               }
               else
               {
                   // foreach (GameObject hs in allHotspots)//make sure all hotpots are at default scale
                   // {
                   //     hs.transform.localScale = defaultScale;
                   //     Renderer defaultRenderer = hs.GetComponent<Renderer>();
                   //     defaultRenderer.material.color = defaultColor;
                   // }


                   // Reset last hovered if we're not on any hotspot
                   if (lastHoveredHotspot != null)
                   {
                       lastHoveredHotspot.transform.localScale = defaultScale;
                       lastHoveredHotspot.GetComponent<Renderer>().material.color = defaultColor;
                       lastHoveredHotspot = null;
                   }
               }
              
           }
       }
   }
   void OnDisable()
   {
       pointerPositionAction?.Disable();
   }


   void OnDestroy()
   {
       pointerPositionAction?.Dispose();
   }
}






HotspotScript
Attached to Hotspots and MediaImage and MediaVideo
Used to control what images or videos to spawn in Mobile Tour project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HotspotScript : MonoBehaviour
{
  


   public bool isSpawningPrefab =false;//spawn video/text/image
   public GameObject prefabToSpawn;
   
}



VirtualTourCameraController
Attached to VirtualTourCamera
Used to rotate the camera around by swiping on screen on using the mouse.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class VirtualTourCameraController : MonoBehaviour
{
   public float sensitivity = 0.1f; // lower this for touch-friendly control


   private float _yaw = 0f;//rotate y axis (vertical)
   private float _pitch = 0f;//rotate x axics (horizontal)
   [SerializeField] private float pitchClamp = 80f;//stop camera from flipping
  
   private Vector2 _inputDelta;


   private InputAction lookAction;


   private bool hasSwiped = false;
   [SerializeField] private GameObject firstTutorialPanel;


   public bool tutorial;//spawn tutorial panel 4
  
   void Awake()
   {
       // Create a new InputAction for look control (mouse delta or touch drag)
       lookAction = new InputAction(
           name: "Look",
           type: InputActionType.Value,
           binding: "<Pointer>/delta"
       );
       lookAction.Enable();
       firstTutorialPanel.SetActive(true);
   }


   void Update()
   {
       HandleInput();//handle input from player


       // Clamp pitch so the camera doesn't flip upside down
       _pitch = Mathf.Clamp(_pitch, -pitchClamp, pitchClamp);


       Quaternion yawRotation = Quaternion.Euler(_pitch, _yaw, 0f);
       //create Euler rotation based on user input; Quaternion represent rotation in 3D space
       
       RotateCamera(yawRotation);//do the rotation
   }
   public void HandleInput()
   {
       _inputDelta = lookAction.ReadValue<Vector2>();
       _yaw += _inputDelta.x * sensitivity * Time.deltaTime;
       _pitch -= _inputDelta.y * sensitivity * Time.deltaTime;
       if(hasSwiped) return;
       if(Mathf.Abs(_yaw) >= 3f || Mathf.Abs(_pitch)>= 3f)
       {
           firstTutorialPanel.SetActive(false);
       }
   }
   void RotateCamera(Quaternion rotation)
   {
       transform.rotation = rotation;
   }
   void OnDisable()
   {
       lookAction?.Disable();
   }


   void OnDestroy()
   {
       lookAction?.Dispose();
   }
}



LookAtMe
Attached to VirtualTourCamera> LookAtGameobject
Used to make all hotspots (objects with “hotspot” tag) look at camera in Mobile Tour project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookAtMe : MonoBehaviour
{
  


   // Update is called once per frame
   void Update()
   {
       GameObject[] hotspots = GameObject.FindGameObjectsWithTag("hotspot");
       foreach (GameObject hs in hotspots)
       {
           hs.transform.LookAt(transform.position);
       }
   }
}


RailControllerHandleScript
[VR] attached to valve/handle to change train track rails or turn on lights on the terminal?

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;


public class RailControllerHandleScript : MonoBehaviour
{
   public XRKnob trainControllerHandle;
   public Light trainTerminalLight;
   //public AudioSource knobAudio;
   
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
       }
       else if (trainControllerHandle.value <= 0.5f)
       {
           trainTerminalLight.intensity = 0f;//turn off the light when handle turned left
           //knobAudio.Stop();
       }
   }
}


CloseTutorialPanel
[Mobile] Attached to UIs created by pre
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloseTutorialPanel : MonoBehaviour
{
   public GameObject tutorialPanel;
   void Start()
   {
       tutorialPanel.SetActive(true);
   }


}




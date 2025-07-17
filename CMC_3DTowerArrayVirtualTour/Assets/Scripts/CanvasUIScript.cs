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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Make sure this namespace is used

[RequireComponent(typeof(RectTransform))]
public class UIScale : MonoBehaviour
{
    public float baseHeight = 1080f; // Reference design height
    public float minScale = 1f;    // Minimum scale clamp
    public float maxScale = 2.0f;    // Maximum scale clamp

    void Start()
    {
        AdjustScale();
    }
    void Update()
    {
        if (Keyboard.current.fKey.isPressed)
        {
            AdjustScale();
        }
    }

    void AdjustScale()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        float scale = Screen.height / baseHeight;

        if (screenRatio < 1.5f) // e.g., iPad 4:3
        {
            scale = 0.95f;
        }
        // if (screenRatio >=1.5f && screenRatio <= 2.0f)//~ 1.7
        // {
        //     scale = 0.95f;
        //     Debug.Log("activated");
        // }
        if (screenRatio > 2.0f) // e.g., ultra-wide phones
        {
            scale *= 0.95f;
        }
        
        //scale = Mathf.Clamp(scale, minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
        // Debug.Log("screen scale is " +scale );
        // Debug.Log($"Screen.width: {Screen.width}, Screen.height: {Screen.height}");
        // Debug.Log($"Calculated screenRatio: {(float)Screen.width / Screen.height:F5}");

    }
}
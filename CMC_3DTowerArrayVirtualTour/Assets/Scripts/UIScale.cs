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
            scale *= 1.15f;
        }
        else if (screenRatio > 2.0f) // e.g., ultra-wide phones
        {
            scale *= 0.95f;
        }

        //scale = Mathf.Clamp(scale, minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
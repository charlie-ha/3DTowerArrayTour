using UnityEngine;
using TMPro; // only if using TextMeshPro

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // assign in Inspector
    private float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString() + " FPS";
    }
}

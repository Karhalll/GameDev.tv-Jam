using UnityEngine;
using UnityEngine.UI;

public class FSPShowUI : MonoBehaviour
{
    Text fpsText;

    int frameCount = 0;
    float dt = 0.0f;
    float fps = 0.0f;
    float updateRate = 4.0f;  // 4 updates per sec.

    private void Awake() 
    {
        fpsText = GetComponent<Text>();
    }


    void Update()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0f / updateRate)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt = 0f;
        }

        fpsText.text = fps.ToString("##0");
    }
}

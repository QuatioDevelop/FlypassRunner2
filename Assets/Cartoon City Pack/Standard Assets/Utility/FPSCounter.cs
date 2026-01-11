using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSCounter : MonoBehaviour {
    public Text fpsText;
    float fpsMeasurePeriod = 0.5f;
    int fpsAccumulator = 0;
    float fpsNextPeriod = 0;
    int currentFps;
    string display = "{0} FPS";

    void Start()
    {
        fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
    }

    void Update()
    {
        fpsAccumulator++;
        if (Time.realtimeSinceStartup > fpsNextPeriod)
        {
            currentFps = (int)(fpsAccumulator / fpsMeasurePeriod);
            fpsAccumulator = 0;
            fpsNextPeriod += fpsMeasurePeriod;
            if (fpsText != null)
                fpsText.text = string.Format(display, currentFps);
        }
    }
}

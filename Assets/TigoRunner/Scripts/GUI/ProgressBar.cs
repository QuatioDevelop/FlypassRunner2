using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public PatternSystem PatternSystem;
    public GameObject hud;
    public Slider ProgressBarSlider;

    void Start()
    {
        StartCoroutine(LoadProgress());
    }

    IEnumerator LoadProgress()
    {
        while (PatternSystem.loadingComplete == false)
        {
            ProgressBarSlider.value = PatternSystem.loadingPercent / 100;
            yield return 0;
        }
        gameObject.SetActive(false);
        hud.SetActive(true);
        yield return 0;
    }
}

// <summary>
// Set FPS
// this script use to set FPS if platform is mobile
// </summary>

using UnityEngine;

public class SetFPS : MonoBehaviour
{

    public int FpsTarget;

    void Start()
    {
        Application.targetFrameRate = FpsTarget;
    }
}

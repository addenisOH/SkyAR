using UnityEngine;

public class SensorSceneDirector : MonoBehaviour
{
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void OnDestroy()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
}

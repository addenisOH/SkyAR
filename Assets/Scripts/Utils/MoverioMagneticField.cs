using MoverioBasicFunctionUnityPlugin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverioMagneticField : MonoBehaviour
{
    // Start is called before the first frame update
    public static float GetHeading()
    {
        float angle = 0f;
        if (MoverioInput.IsActive())
        {
            Vector3 currentMag = MoverioInput.GetMag();
            angle = GetAngle(-currentMag.x, -currentMag.z);
        }

        return angle;
    }

    private static float GetAngle(float a, float b)
    {
        float angle = 0f;
        if (Mathf.Atan2(a, b) >= 0)
        {
            angle = Mathf.Atan2(a, b) * (180 / Mathf.PI);
        }
        else
        {
            angle = (Mathf.Atan2(a, b) + 2 * Mathf.PI) * (180 / Mathf.PI);
        }

        return angle;
    }
}

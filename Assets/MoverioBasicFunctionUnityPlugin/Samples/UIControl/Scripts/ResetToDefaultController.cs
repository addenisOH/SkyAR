using MoverioBasicFunctionUnityPlugin;
using UnityEngine;

public class ResetToDefaultController : MonoBehaviour
{
    public void OnClick()
    {
        if (!MoverioUI.IsActive())
        {
            return;
        }

        MoverioUI.ResetToDefault();
    }
}

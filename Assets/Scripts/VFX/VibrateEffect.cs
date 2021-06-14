using UnityEngine;

public class VibrateEffect : MonoBehaviour
{
    public void Vibrate()
    {
        if(!OptionsManager.Instance.vibrateValue) { return; }
        Handheld.Vibrate();
    }
}

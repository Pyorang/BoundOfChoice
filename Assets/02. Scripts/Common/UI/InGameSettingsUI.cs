using UnityEngine;
using UnityEngine.UI;

public class InGameSettingsUI : SettingsUI
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}

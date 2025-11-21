using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private Button _inGameSettingsButton;

    public void OnClickInGameSettingsButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<InGameSettingsUI>(uiData);
        AudioManager.Instance.Play(AudioType.SFX, "ui_openUI_button_click");
    }
}

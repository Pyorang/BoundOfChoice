using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    public const string UI_OPEN_BUTTON_CLICK = "ui_openUI_button_click";

    [Header("로비 버튼")]
    [Space]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    public void OnClickPlayButton()
    {
        SceneLoader.Instance.LoadSceneAsync(ESceneType.InGame);
    }

    public void OnClickSettingsButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<LobbyGameSettingsUI>(uiData);
        AudioManager.Instance.Play(AudioType.SFX, UI_OPEN_BUTTON_CLICK);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}

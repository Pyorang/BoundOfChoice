using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    public const string UiOpenButtonClick = "ui_openUI_button_click";

    public void OnClickPlayButton()
    {
        SceneLoader.Instance.LoadSceneAsync(ESceneType.InGame);
    }

    public void OnClickSettingsButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<LobbyGameSettingsUI>(uiData);
        AudioManager.Instance.Play(AudioType.SFX, UiOpenButtonClick);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    public const string Button = "Button";

    public void OnClickPlayButton()
    {
        SceneLoader.Instance.LoadSceneAsync(ESceneType.InGame);
        AudioManager.Instance.Play(AudioType.SFX, Button);
    }

    public void OnClickSettingsButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<LobbyGameSettingsUI>(uiData);
        AudioManager.Instance.Play(AudioType.SFX, Button);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}

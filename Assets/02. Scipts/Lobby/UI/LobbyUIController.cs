using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    public const string Button = "Button";

    [Header("씬 전환 연출")]
    [Space]
    [SerializeField] private float _duration = 3.0f;
    [Space]
    [SerializeField] private GameObject _loadingScene;
    [SerializeField] private TextMeshProUGUI _loadingText;

    private IEnumerator ProcessLoadingEffect()
    {
        AsyncOperation loadingOperation = SceneLoader.Instance.LoadSceneAsync(ESceneType.InGame);
        
        if (loadingOperation == null)
        {
            yield break;
        }

        loadingOperation.allowSceneActivation = false;

        AudioManager.Instance.Stop(AudioType.BGM);
        _loadingScene.SetActive(true);
        AudioManager.Instance.Play(AudioType.SFX, "Loading");

        float timeElapsed = 0;
        Color startColor = _loadingText.color;

        while(timeElapsed < _duration)
        {
            float ratio = timeElapsed / _duration;

            Color newColor = startColor;
            newColor.a = ratio;
            _loadingText.color = newColor;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        while (true)
        {
            if (loadingOperation.isDone)
            {
                break;
            }

            if (loadingOperation.progress >= 0.9f)
            {
                loadingOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void OnClickPlayButton()
    {
        StartCoroutine(ProcessLoadingEffect());
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

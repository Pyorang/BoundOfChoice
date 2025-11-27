using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameClearUI : BaseUI
{
    [Header("이펙트 설정")]
    [SerializeField] private float _duration = 1f;

    [Header("UI 바인딩")]
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TextMeshProUGUI _gameClearTextUI;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _exitButton;

    Color _startImageColor;
    Color _startTextColor;

    private void Awake()
    {
        _startImageColor = _backgroundImage.color;
        _startTextColor = _gameClearTextUI.color;
    }

    private void OnEnable()
    {
        StartCoroutine(GameClearFadeIn());
    }
    private void OnDisable()
    {
        _backgroundImage.color = _startImageColor;
        _gameClearTextUI.color = _startTextColor;
        _restartButton.SetActive(false);
        _exitButton.SetActive(false);
    }

    private IEnumerator GameClearFadeIn()
    {
        float timer = 0f;

        float invDuration = 1f / _duration;

        while (timer < _duration)
        {
            timer += Time.deltaTime;

            float t = timer * invDuration;

            if (t > 1f) t = 1f;

            Color currentBgColor = _startImageColor;
            currentBgColor.a = t;
            _backgroundImage.color = currentBgColor;

            Color currentTextColor = _startTextColor;
            currentTextColor.a = t;
            _gameClearTextUI.color = currentTextColor;

            yield return null;
        }

        _restartButton.SetActive(true);
        _exitButton.SetActive(true);
    }
    public void OnClickRestartButton()
    {
        UIManager.Instance.CloseUI(this);
        SceneLoader.Instance.ReloadScene();
    }

    public void OnClickExitButton()
    {
        UIManager.Instance.CloseUI(this);
        SceneLoader.Instance.LoadScene(ESceneType.Lobby);
    }
}

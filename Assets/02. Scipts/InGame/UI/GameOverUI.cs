using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [Header("이펙트 설정")]
    [Space]
    [SerializeField] private float _totalDuration = 1f;
    [SerializeField] private float _slowTimeScale = 0.3f;
    [SerializeField] private float _slowTimeDuration = 0.7f;

    [Header("UI 바인딩")]
    [Space]
    [SerializeField] private Image _upFrame;
    [SerializeField] private Image _redFrame;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Image _downFrame;

    private Color _redFrameColor;
    private Color _gameOverTextColor;

    private void Awake()
    {
        _redFrameColor = _redFrame.color;
        _gameOverTextColor = _gameOverText.color;
    }

    public void OnEnable()
    {
        StartCoroutine(GameOverEffect());
    }

    public void OnDisable()
    {
        _upFrame.fillAmount = 0f;
        _downFrame.fillAmount = 0f;
        _redFrame.color = _redFrameColor;
        _gameOverText.color = _gameOverTextColor;
        _restartButton.gameObject.SetActive(false);
        _exitButton.gameObject.SetActive(false);
    }

    private IEnumerator GameOverEffect()
    {
        Time.timeScale = _slowTimeScale;

        float elapsedTime = 0f;

        while (elapsedTime < _slowTimeDuration)
        {
            elapsedTime += Time.deltaTime;

            float ratio = (elapsedTime / _totalDuration);

            Color newColor = _redFrame.color;
            newColor.a = _redFrameColor.a * (1.0f - ratio);
            _redFrame.color = newColor;

            _upFrame.fillAmount = ratio;
            _downFrame.fillAmount = ratio;

            yield return null;
        }

        Time.timeScale = 1f;
        float textElapsedTime = 0f;
        float totalTextDuration = _totalDuration - elapsedTime;

        while (elapsedTime < _totalDuration)
        {
            elapsedTime += Time.deltaTime;
            textElapsedTime += Time.deltaTime;

            float ratio = (elapsedTime / _totalDuration);
            float textRatio = (textElapsedTime / totalTextDuration);

            Color newColor = _redFrame.color;
            newColor.a = _gameOverTextColor.a * (1.0f - ratio);
            _redFrame.color = newColor;

            Color newTextColor = _gameOverText.color;
            newTextColor.a = textRatio;
            _gameOverText.color = newTextColor;

            _upFrame.fillAmount = ratio;
            _downFrame.fillAmount = ratio;

            yield return null;
        }

        _restartButton.gameObject.SetActive(true);
        _exitButton.gameObject.SetActive(true);
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

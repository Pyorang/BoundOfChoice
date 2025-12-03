using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceManager : SingletonBehaviour<ChoiceManager>
{
    [Header("선택을 표시할 Text")]
    [Space]
    [SerializeField] private TextMeshProUGUI _textLeft;
    [SerializeField] private TextMeshProUGUI _textRight;
    [SerializeField] private float _typingDelay = 0.1f;

    public bool IsLeftChoice;

    private IEnumerator _typingCoroutineLeft;
    private IEnumerator _typingCoroutineRight;
    private bool _typeLeftDone;
    private bool _typeRightDone;

    private ChoiceModel _currentChoiceModel;
    
    private ChoiceBase _currentChoice;
    private List<ChoiceBase> _choices = new List<ChoiceBase>();

    private List<int> _nonAppearableChoicesID = new List<int>();

    public static event Action OnLeftLeverInteracted;
    public static event Action OnRightLeverInteracted;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void Start()
    {
        InitChoices();

        // NOTE : 1번과 2번 선택지는 게임 시작 시에만 나옴.
        // 2번 선택지는 추후에 추가됨.
        _nonAppearableChoicesID.Add(1);

        SetChoice(1);
    }

    private void OnDestroy()
    {
        OnLeftLeverInteracted = null;
        OnRightLeverInteracted= null;
    }

    private void InitChoices()
    {
        int tableCount = DataTableManager.Instance.GetChoiceCount();

        for (int i = 1; i <= tableCount; i++)
        {
            string className = $"Choice{i}";

            Type type = System.Reflection.Assembly.GetExecutingAssembly().GetType(className);

            if (type != null)
            {
                if (typeof(ChoiceBase).IsAssignableFrom(type) && !type.IsAbstract)
                {
                    try
                    {
                        ChoiceBase choiceInstance = Activator.CreateInstance(type) as ChoiceBase;
                        if (choiceInstance != null)
                        {
                            _choices.Add(choiceInstance);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Choice 클래스 인스턴스 생성 실패 ({className}): {e.Message}");
                    }
                }
                else
                {
                    Debug.LogWarning($"{className} 타입은 존재하지만, IChoice를 구현하지 않았거나 추상 클래스입니다.");
                }
            }
            else
            {
                Debug.LogWarning($"클래스 {className}을(를) 찾을 수 없습니다. 인덱스 {i}에 해당하는 Choice 클래스가 없습니다.");
            }
        }
    }

    public void SetChoice(int choiceID)
    {
        _currentChoiceModel = DataTableManager.Instance.GetChoice(choiceID);
        UpdateChoiceText(_currentChoiceModel.Text1, _currentChoiceModel.Text2);

        _currentChoice = _choices[GetCurrentChoiceID() - 1];
    }

    public void GetNewChoice()
    {
        ProcessLeftTurns();

        _currentChoiceModel = DataTableManager.Instance.GetRandomChoice(_nonAppearableChoicesID.ToArray());
        UpdateChoiceText(_currentChoiceModel.Text1, _currentChoiceModel.Text2);

        _currentChoice = _choices[GetCurrentChoiceID() - 1];
    }

    public void UpdateChoiceText(string textA, string textB)
    {
        if(_typingCoroutineLeft != null)
        {
            StopCoroutine(_typingCoroutineLeft);
        }
        if(_typingCoroutineRight != null)
        {
            StopCoroutine(_typingCoroutineRight);
        }

        _typingCoroutineLeft = TypingEffect(_textLeft, textA, _typingDelay);
        _typingCoroutineRight = TypingEffect(_textRight, textB, _typingDelay);

        _typeLeftDone = false;
        _typeRightDone = false;

        StartCoroutine(_typingCoroutineLeft);
        StartCoroutine(_typingCoroutineRight);
    }

    private IEnumerator TypingEffect(TextMeshProUGUI textObject, string fullText, float delay)
    {
        textObject.text = "";
        var sb = new System.Text.StringBuilder();

        WaitForSeconds waitDelay = new WaitForSeconds(delay);

        foreach (char c in fullText)
        {
            sb.Append(c);
            textObject.text = sb.ToString();
            AudioManager.Instance.Play(AudioType.SFX, "Typing");
            yield return waitDelay;
        }

        if(textObject == _textLeft)
        {
            _typeLeftDone = true;
        }
        if(textObject == _textRight)
        {
            _typeRightDone = true;
        }

        if(_typeLeftDone && _typeRightDone)
        {
            Angel.Instance.EnableLevers();
        }
    }

    public int GetCurrentChoiceID()
    {
        string numberString = _currentChoiceModel.ID.Substring(2);
        int number = int.Parse(numberString);
        return number;
    }

    public void ExecuteChoice()
    {
        if (IsLeftChoice)
        {
            OnLeftLeverInteracted?.Invoke();
            _currentChoice.ExecuteA();
        }
        else
        {
            OnRightLeverInteracted?.Invoke();
            _currentChoice.ExecuteB();
        }
    }

    public void ProcessLeftTurns()
    {
        //NOTE : 초반 선택지 2개는 게임 시작 안내 멘트

        for(int i = 2; i < _choices.Count; i++)
        {
            _choices[i].ProcessLeftTurn();
            ManageAppearList(i, _choices[i].CanAppear);
        }

        _nonAppearableChoicesID.Add(GetCurrentChoiceID());
    }

    private void ManageAppearList(int choiceID, bool canAppear)
    {
        if(!canAppear)
        {
            _nonAppearableChoicesID.Add(choiceID + 1);
        }
        else
        {
            _nonAppearableChoicesID.Remove(choiceID + 1);
        }
    }
}

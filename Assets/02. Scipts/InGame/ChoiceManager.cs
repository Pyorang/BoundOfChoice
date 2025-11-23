using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceManager : SingletonBehaviour<ChoiceManager>
{
    [Header("선택을 표시할 Text")]
    [Space]
    [SerializeField] private TextMeshProUGUI _text1;
    [SerializeField] private TextMeshProUGUI _text2;

    private ChoiceModel _currentChoiceModel;
    
    private ChoiceBase _currentChoice;
    private List<ChoiceBase> _choices = new List<ChoiceBase>();

    protected override void Init()
    {
        base.Init();
    }

    private void Start()
    {
        IsDestroyOnLoad = true;
        InitChoices();
        GetNewChoice();
    }

    public void GetNewChoice()
    {
        _currentChoiceModel = DataTableManager.Instance.GetRandomChoice();
        UpdateChoiceText(_currentChoiceModel.Text1, _currentChoiceModel.Text2);

        _currentChoice = _choices[GetCurrentChoiceID() - 1];
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

    public void UpdateChoiceText(string text1, string text2)
    {
        _text1.text = text1;
        _text2.text = text2;
    }

    public int GetCurrentChoiceID()
    {
        string numberString = _currentChoiceModel.ID.Substring(2);
        int number = int.Parse(numberString);
        return number;
    }


    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentChoice.Execute1();
        }
        else if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentChoice.Execute2();
        }
    }
}

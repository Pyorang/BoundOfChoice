using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Windows;

public class ChoiceManager : SingletonBehaviour<ChoiceManager>
{
    [Header("선택을 표시할 Text")]
    [Space]
    [SerializeField] private TextMeshProUGUI Text1;
    [SerializeField] private TextMeshProUGUI Text2;

    private ChoiceModel _currentChoiceModel;
    
    private IChoice _currentChoice;
    private List<IChoice> _choices = new List<IChoice>();

    protected override void Init()
    {
        base.Init();
    }

    private void Start()
    {
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
                if (typeof(IChoice).IsAssignableFrom(type) && !type.IsAbstract)
                {
                    try
                    {
                        IChoice choiceInstance = Activator.CreateInstance(type) as IChoice;
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
        Text1.text = text1;
        Text2.text = text2;
    }

    public int GetCurrentChoiceID()
    {
        string numberString = _currentChoiceModel.ID.Replace("C_", "");
        int number = int.Parse(numberString);
        return number;
    }
}

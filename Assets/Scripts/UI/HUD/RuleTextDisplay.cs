using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RuleTextDisplay : MonoBehaviour
{
    public WinCondition winCondition;
    
    [SerializeField]
    private Text ruleText;

    private void OnEnable()
    {
        ruleText.text = winCondition.Description;
    }
}
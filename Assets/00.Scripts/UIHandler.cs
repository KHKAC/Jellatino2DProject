using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    // 기본적인 싱글톤 패턴(여기에서 더 안전하게 쓸 수 있는 방법이 존재함. 이건 나중에)
    // [public static UIHandler instance { get; private set; }] + [void Awake() {instance = this;}]
    public static UIHandler instance { get; private set; }
    public float displayTime = 4.0f;
    VisualElement m_NPCDialogue;
    float m_TimerDisplay;
    VisualElement mHealthBar;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // UIDocument uiDocument = GetComponent<UIDocument>();
        // VisualElement healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        // healthBar.style.width = Length.Percent(currentHealth * 100.0f);
        UIDocument uiDocument = GetComponent<UIDocument>();
        mHealthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);
        m_NPCDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NPCDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
    }

    void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NPCDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    public void DisplayDialogue()
    {
        m_NPCDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }

    public void DisplayDialogue(string str)
    {
        m_NPCDialogue.style.display = DisplayStyle.Flex;
        m_NPCDialogue.Q<VisualElement>("BackGround").Q<Label>("Label").text = str;
        m_TimerDisplay = displayTime;
    }

    public void SetHealthValue(float percent)
    {
        mHealthBar.style.width = Length.Percent(percent * 100.0f);
    }
}

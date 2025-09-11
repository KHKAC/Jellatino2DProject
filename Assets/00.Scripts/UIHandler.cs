using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    // 기본적인 싱글톤 패턴(여기에서 더 안전하게 쓸 수 있는 방법이 존재함. 이건 나중에)
    // [public static UIHandler instance { get; private set; }] + [void Awake() {instance = this;}]
    public static UIHandler instance { get; private set; }
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
    }

    public void SetHealthValue(float percent)
    {
        mHealthBar.style.width = Length.Percent(percent * 100.0f);
    }
}

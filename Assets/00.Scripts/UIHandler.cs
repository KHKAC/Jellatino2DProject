using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    VisualElement mHealthBar;
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

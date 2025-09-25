// #define UI_TEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForAndroidUI : MonoBehaviour
{
    [SerializeField] GameObject androidUI;
    void Start()
    {
#if UNITY_ANDROID || UI_TEST
        androidUI.SetActive(true);
#else
        androidUI.SetActive(false);
#endif
    }
}

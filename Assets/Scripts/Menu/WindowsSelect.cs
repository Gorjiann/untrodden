using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WindowsSelect : MonoBehaviour
{
    [SerializeField] private List<GameObject> Windows;
    [SerializeField] private int DefaultOption;
    [SerializeField] private bool SetDefaultOption;

    private void Awake()
    {
        if (SetDefaultOption) ActivateOption(DefaultOption);
    }

    public void ActivateOption(int index)
    {
        foreach (var option in Windows)
        {
            if (option != Windows[index])
                option.SetActive(false);
            else
                option.SetActive(true);
        }
    }
}

using TMPro;
using UnityEngine;

public class NumberUnit : MonoBehaviour
{
    public int value = 1;

    TMP_Text text;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        UpdateText();
    }

    public void SetValue(int v)
    {
        value = v;

        UpdateText();
    }

    void UpdateText()
    {
        if (text != null)
        {
            text.text = value.ToString();
        }
    }
}
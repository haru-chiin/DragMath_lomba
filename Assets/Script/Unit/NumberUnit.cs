using TMPro;
using UnityEngine;

public class NumberUnit : MonoBehaviour
{
    public int value = 1;

    public SpriteRenderer spriteRenderer;

    TMP_Text text;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
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

    public void SetSprite(Sprite newSprite)
    {
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }

    void UpdateText()
    {
        if (text != null)
        {
            text.text = value.ToString();
        }
    }
}
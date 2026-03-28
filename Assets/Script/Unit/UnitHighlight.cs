using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHighlight : MonoBehaviour
{
    public SpriteRenderer sr;

    Coroutine blinkRoutine;

    private void Start()
    {
        SetAlpha(0f);
    }

    public void Show()
    {
        if (blinkRoutine != null) return;

        blinkRoutine = StartCoroutine(Blink());
    }

    public void Hide()
    {
        if (blinkRoutine != null)
        {
            StopCoroutine(blinkRoutine);
            blinkRoutine = null;
        }

        SetAlpha(0f);
    }

    IEnumerator Blink()
    {
        while (true)
        {
            SetAlpha(1f);

            yield return new WaitForSeconds(0.25f);

            SetAlpha(0f);

            yield return new WaitForSeconds(0.25f);
        }
    }

    void SetAlpha(float a)
    {
        if (sr == null) return;

        Color c = sr.color;
        c.a = a;
        sr.color = c;
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public TMP_Text targetDisplay;

    [Header("Hit Effect Settings")]
    public SpriteRenderer spriteRenderer;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;
    public int flashCount = 6;

    private Color originalColor;

    void Start()
    {
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void SetTarget(int target)
    {
        if (targetDisplay != null)
        {
            targetDisplay.text = "" + target;
        }
    }

    public void PlayHitEffect()
    {
        if (spriteRenderer != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashRoutine());
        }
        if (SFXManager.Instance != null) SFXManager.Instance.PlayHit();
    }

    IEnumerator FlashRoutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OutlineSync : MonoBehaviour
{
    private SpriteRenderer parentRenderer;
    private SpriteRenderer myRenderer;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();

        if (transform.parent != null)
        {
            parentRenderer = transform.parent.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogWarning("Object ini tidak memiliki Parent! Pastikan script dipasang di Child Object.");
        }
    }

    void LateUpdate()
    {
        if (parentRenderer != null && parentRenderer.sprite != null)
        {

            myRenderer.sprite = parentRenderer.sprite;
            myRenderer.flipX = parentRenderer.flipX;
            myRenderer.flipY = parentRenderer.flipY;
        }
    }
}
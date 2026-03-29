using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public Animator anim;

    public void PlayHit()
    {
        anim.SetTrigger("Hit");
        if (SFXManager.Instance != null) SFXManager.Instance.PlayHit();
    }

    public void PlayAttack()
    {
        anim.SetTrigger("Attack");
        if (SFXManager.Instance != null) SFXManager.Instance.PlayAttack();
    }
}
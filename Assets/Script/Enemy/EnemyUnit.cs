using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public TMP_Text targetDisplay;

    public void SetTarget(int target)
    {
        if (targetDisplay != null)
        {
            targetDisplay.text = "" + target;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    public NumberUnit numberUnit;
    public UnitHighlight highlight;
    private void OnMouseDown()
    {
        DragLineController.Instance.StartDrag(this);
    }
}

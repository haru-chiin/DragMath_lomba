using UnityEngine;

public class OperatorButton : MonoBehaviour
{
    public OperatorType type;

    public void Click()
    {
        OperatorManager.Instance.SelectOperator(type);
    }
}
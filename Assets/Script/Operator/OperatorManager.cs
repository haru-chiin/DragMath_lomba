using UnityEngine;

public class OperatorManager : MonoBehaviour
{
    public static OperatorManager Instance;

    public OperatorType currentOperator = OperatorType.None;
    public GameModeType currentMode = GameModeType.None;

    public int plusCount = 2;
    public int minusCount = 1;
    public int multiplyCount = 1;
    public int divideCount = 1;

    void Awake()
    {
        Instance = this;
        Debug.Log("OperatorManager Ready");
    }

    public void SelectOperator(OperatorType op)
    {
        if (!CanUse(op)) return;

        currentOperator = op;
        currentMode = GameModeType.Operator;

        Debug.Log("Operator = " + op);
    }

    public bool CanUse(OperatorType op)
    {
        switch (op)
        {
            case OperatorType.Plus:
                return plusCount > 0;

            case OperatorType.Minus:
                return minusCount > 0;

            case OperatorType.Multiply:
                return multiplyCount > 0;

            case OperatorType.Divide:
                return divideCount > 0;
        }

        return false;
    }

    public void UseOperator()
    {
        switch (currentOperator)
        {
            case OperatorType.Plus:
                plusCount--;
                break;

            case OperatorType.Minus:
                minusCount--;
                break;

            case OperatorType.Multiply:
                multiplyCount--;
                break;

            case OperatorType.Divide:
                divideCount--;
                break;
        }

        currentOperator = OperatorType.None;
    }

    public void SelectAttack()
    {
        currentMode = GameModeType.Attack;
        currentOperator = OperatorType.None;

        Debug.Log("Mode = Attack");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLineController : MonoBehaviour
{
    public static DragLineController Instance;

    public LineRenderer line;

    UnitSelector startUnit;
    UnitSelector targetUnit;

    Camera cam;

    void Awake()
    {
        Instance = this;
        cam = Camera.main;

        line.enabled = false;
    }

    void Update()
    {
        if (startUnit == null) return;

        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);

        mouse.z = 0;

        line.SetPosition(0, startUnit.transform.position);

        line.SetPosition(1, mouse);

        CheckTarget(mouse);

        if (Input.GetMouseButtonUp(0))
        {
            Release();
        }
    }

    public void StartDrag(UnitSelector u)
    {
        startUnit = u;

        line.enabled = true;

        line.SetPosition(
            0,
            startUnit.transform.position
        );

        if (startUnit.highlight != null)
            startUnit.highlight.Show();
    }

    void Release()
    {
        if (startUnit != null && startUnit.highlight != null)
        {
            startUnit.highlight.Hide();
        }

        if (targetUnit != null && targetUnit.highlight != null)
        {
            targetUnit.highlight.Hide();
        }

        if (targetUnit != null)
        {
            DoAction();
        }

        startUnit = null;
        targetUnit = null;

        line.enabled = false;

    }

    void CheckTarget(Vector3 pos)
    {
        Collider2D[] hits =
        Physics2D.OverlapCircleAll(pos, 0.5f);

        UnitSelector newTarget = null;

        foreach (var h in hits)
        {
            UnitSelector u = h.GetComponent<UnitSelector>();

            if (u != null && u != startUnit)
            {
                newTarget = u;
                break;
            }
        }

        if (newTarget == targetUnit)
        {
            if (targetUnit != null)
            {
                line.SetPosition(
                    1,
                    targetUnit.transform.position
                );
            }

            return;
        }

        if (targetUnit != null &&
            targetUnit.highlight != null)
        {
            targetUnit.highlight.Hide();
        }

        targetUnit = newTarget;

        if (targetUnit != null &&
            targetUnit.highlight != null)
        {
            targetUnit.highlight.Show();

            line.SetPosition(
                1,
                targetUnit.transform.position
            );
        }
    }

    void DoAction()
    {
        if (OperatorManager.Instance.currentMode == GameModeType.Operator)
        {
            Merge();
        }
        else if (OperatorManager.Instance.currentMode == GameModeType.Attack)
        {
            Attack();
        }
    }

    void Merge()
    {
        NumberUnit a =
            startUnit.numberUnit;

        NumberUnit b =
            targetUnit.numberUnit;

        int result =
            Calculate(a.value, b.value);

        b.SetValue(result);

        OperatorManager.Instance.UseOperator();

        UnitManager.Instance.DisableUnit(startUnit.gameObject);

        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayMerge();
        }
    }

    void Attack()
    {
        EnemyUnit enemy = targetUnit.GetComponent<EnemyUnit>();

        if (enemy == null) return;

        int attackValue = startUnit.numberUnit.value;

        StageManager.Instance.EvaluateAttack(attackValue);

        OperatorManager.Instance.currentMode = GameModeType.None;
    }

    int Calculate(int a, int b)
    {
        OperatorType op =
            OperatorManager.Instance.currentOperator;

        switch (op)
        {
            case OperatorType.Plus:
                return a + b;

            case OperatorType.Minus:
                return a - b;

            case OperatorType.Multiply:
                return a * b;

            case OperatorType.Divide:
                if (b == 0) return a;
                return a / b;
        }

        return a;
    }
}

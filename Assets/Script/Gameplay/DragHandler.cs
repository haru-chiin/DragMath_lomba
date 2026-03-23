using UnityEngine;
using System.Collections;

public class DragHandler : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging;

    private Vector3 startPos;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        startPos = transform.position;
    }

    private void OnMouseDown()
    {
        startPos = transform.position;

        isDragging = true;

        Vector3 mousePos =
            cam.ScreenToWorldPoint(Input.mousePosition);

        offset =
            transform.position -
            new Vector3(mousePos.x, mousePos.y, 0);
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 mousePos =
            cam.ScreenToWorldPoint(Input.mousePosition);

        transform.position =
            new Vector3(mousePos.x, mousePos.y, 0) + offset;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (!CheckDrop())
        {
            transform.position = startPos;
        }
    }

    bool CheckDrop()
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                transform.position,
                0.6f
            );

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject)
                continue;

            // MERGE MODE
            if (OperatorManager.Instance.currentMode == GameModeType.Operator)
            {
                NumberUnit other =
                    hit.GetComponent<NumberUnit>();

                if (other != null)
                {
                    if (OperatorManager.Instance.currentOperator
                        == OperatorType.None)
                        return false;

                    Merge(other);

                    return true;
                }
            }
            // ATTACK MODE
            if (OperatorManager.Instance.currentMode == GameModeType.Attack)
            {
                EnemyUnit enemy =
                    hit.GetComponent<EnemyUnit>();

                if (enemy != null)
                {
                    Attack(enemy);
                    return true;
                }
            }
        }

        return false;
    }

    void Attack(EnemyUnit enemy)
    {
        NumberUnit me =
            GetComponent<NumberUnit>();

        int dmg = me.value;

        enemy.TakeDamage(dmg);

        transform.position = startPos;

        OperatorManager.Instance.currentMode =
            GameModeType.None;
    }

    private void Merge(NumberUnit other)
    {
        NumberUnit me =
            GetComponent<NumberUnit>();

        int result =
            Calculate(me.value, other.value);

        other.SetValue(result);

        OperatorManager.Instance.UseOperator();

        StartCoroutine(DisableDelayed());
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

                if (b == 0)
                    return a;

                return a / b;
        }

        return a;
    }

    private IEnumerator DisableDelayed()
    {
        yield return new WaitForEndOfFrame();

        UnitManager.Instance.DisableUnit(gameObject);
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public UnitData unitData;
    public int lastActionTimeUnitsCost;

    public IEnumerator PerformAction()
    {
        if (unitData.isPlayerControlled)
        {
            yield return StartCoroutine(PlayerPerformAction());
        }
        else
        {
            yield return StartCoroutine(AIPerformAction());
        }
    }

    IEnumerator PlayerPerformAction()
    {
        UIManager uiManager = UIManager.Instance;
        uiManager.ShowActionMenu(this);

        while (!uiManager.actionSelected)
        {
            yield return null;
        }

        ActionData action = uiManager.selectedAction;
        yield return StartCoroutine(ExecuteAction(action));

        uiManager.HideActionMenu();
    }

    IEnumerator AIPerformAction()
    {
        ActionData action = Actions.Wait;
        yield return StartCoroutine(ExecuteAction(action));
    }

    IEnumerator ExecuteAction(ActionData action)
    {
        lastActionTimeUnitsCost = action.timeUnitsCost;

        if (action.actionName == "Move")
        {
            yield return StartCoroutine(PlayerMoveAction());
        }
        else
        {
            action.executeAction(this);
            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator PlayerMoveAction()
    {
        UIManager uiManager = UIManager.Instance;
        uiManager.ShowMovementIndicator();

        GridManager gridManager = GridManager.Instance;
        gridManager.SetUnitForMovement(this);

        while (!gridManager.destinationSelected)
        {
            yield return null;
        }

        List<Node> path = gridManager.GetPath();
        if (path != null && path.Count > 0)
        {
            GridMapGenerator mapGenerator = FindObjectOfType<GridMapGenerator>();
            foreach (Node node in path)
            {
                int tileHeight = mapGenerator.GetTileHeight(node.x, node.y);
                Vector3 targetPosition = new Vector3(node.x, tileHeight + 1, node.y);
                transform.position = targetPosition;
                unitData.map.x = node.x;
                unitData.map.y = node.y;
                yield return new WaitForSeconds(0.1f);
            }
        }

        gridManager.ResetMovement();
        uiManager.HideMovementIndicator();
    }
}

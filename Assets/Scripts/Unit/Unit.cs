using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public UnitData unitData;
    public int lastActionTimeUnitsCost;
    public List<IUnitBehavior> behaviors = new List<IUnitBehavior>();

    void Start()
    {
        if (!unitData.isPlayerControlled)
        {
            behaviors.Add(new PickupItemBehavior());
            behaviors.Add(new WaitBehavior());
        }

        GridManager.Instance.SetNodeWalkable(unitData.map.x, unitData.map.y, false);
    }

    public IEnumerator PerformAction()
    {
        if (unitData.timeUnits.remaining <= 0)
        {
            yield break;
        }

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
        yield return ExecuteAction(action);

        uiManager.HideActionMenu();
    }

    IEnumerator AIPerformAction()
    {
        IUnitBehavior selectedBehavior = null;
        int highestPriority = int.MinValue;

        foreach (IUnitBehavior behavior in behaviors)
        {
            if (behavior.IsApplicable(this) && behavior.Priority > highestPriority)
            {
                highestPriority = behavior.Priority;
                selectedBehavior = behavior;
            }
        }

        if (selectedBehavior != null)
        {
            yield return selectedBehavior.Execute(this);
        }
        else
        {
            yield return ExecuteAction(Actions.Wait);
        }
    }

    public IEnumerator ExecuteAction(ActionData action)
    {
        lastActionTimeUnitsCost = action.timeUnitsCost;

        if (unitData.timeUnits.remaining < lastActionTimeUnitsCost)
        {
            yield break;
        }

        unitData.timeUnits.remaining -= lastActionTimeUnitsCost;
        action.executeAction(this);
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator MoveToPosition(int x, int y)
    {
        GridManager gridManager = GridManager.Instance;
        GridMapGenerator mapGenerator = FindObjectOfType<GridMapGenerator>();

        Node currentNode = gridManager.GetNodeAtPosition(unitData.map.x, unitData.map.y);
        Node targetNode = gridManager.GetNodeAtPosition(x, y);

        if (!targetNode.walkable)
        {
            yield break;
        }

        List<Node> path = Pathfinding.FindPath(gridManager.grid, currentNode, targetNode);

        if (path != null && path.Count > 0)
        {
            gridManager.SetNodeWalkable(currentNode.x, currentNode.y, true);

            foreach (Node node in path)
            {
                if (unitData.timeUnits.remaining <= 0)
                {
                    break;
                }

                if (!node.walkable)
                {
                    break;
                }

                int tileHeight = mapGenerator.GetTileHeight(node.x, node.y);
                Vector3 targetPosition = new Vector3(node.x, node.height + 1, node.y);
                transform.position = targetPosition;
                unitData.map.x = node.x;
                unitData.map.y = node.y;

                unitData.timeUnits.remaining -= 1;

                gridManager.SetNodeWalkable(currentNode.x, currentNode.y, true);
                gridManager.SetNodeWalkable(node.x, node.y, false);

                currentNode = node;

                yield return new WaitForSeconds(0.1f);
            }
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
            Node currentNode = gridManager.GetNodeAtPosition(unitData.map.x, unitData.map.y);

            gridManager.SetNodeWalkable(currentNode.x, currentNode.y, true);

            foreach (Node node in path)
            {
                if (unitData.timeUnits.remaining <= 0)
                {
                    break;
                }

                if (!node.walkable)
                {
                    break;
                }

                int tileHeight = mapGenerator.GetTileHeight(node.x, node.y);
                Vector3 targetPosition = new Vector3(node.x, tileHeight + 1, node.y);
                transform.position = targetPosition;
                unitData.map.x = node.x;
                unitData.map.y = node.y;

                unitData.timeUnits.remaining -= 1;

                gridManager.SetNodeWalkable(currentNode.x, currentNode.y, true);
                gridManager.SetNodeWalkable(node.x, node.y, false);

                currentNode = node;

                yield return new WaitForSeconds(0.1f);
            }
        }

        gridManager.ResetMovement();
        uiManager.HideMovementIndicator();
    }

    public bool HasHealingItem()
    {
        foreach (Item item in unitData.carriedItems)
        {
            if (item.id == "healthPotion")
                return true;
        }
        return false;
    }

    public void UseHealingItemOn(Unit target)
    {
        Item healthPotion = unitData.carriedItems.Find(item => item.id == "healthPotion");
        if (healthPotion != null)
        {
            unitData.carriedItems.Remove(healthPotion);
            target.unitData.attributes.stamina += 10;
        }
    }
}

using UnityEngine;
using System.Collections;

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
        action.executeAction(this);

        lastActionTimeUnitsCost = action.timeUnitsCost;

        uiManager.HideActionMenu();
    }

    IEnumerator AIPerformAction()
    {
        ActionData action = ChooseAIAction();
        action.executeAction(this);
        lastActionTimeUnitsCost = action.timeUnitsCost;
        yield return new WaitForSeconds(1f);
    }

    ActionData ChooseAIAction()
    {
        return Actions.Attack;
    }
}

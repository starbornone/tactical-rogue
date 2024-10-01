using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject actionMenuPanel;
    public Button attackButton;
    public Button waitButton;

    [HideInInspector]
    public bool actionSelected = false;
    [HideInInspector]
    public ActionData selectedAction;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            actionMenuPanel.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowActionMenu(Unit unit)
    {
        actionSelected = false;
        selectedAction = null;
        actionMenuPanel.SetActive(true);

        attackButton.onClick.RemoveAllListeners();
        waitButton.onClick.RemoveAllListeners();

        attackButton.onClick.AddListener(() => OnActionSelected(Actions.Attack));
        waitButton.onClick.AddListener(() => OnActionSelected(Actions.Wait));
    }

    void OnActionSelected(ActionData action)
    {
        selectedAction = action;
        actionSelected = true;
    }

    public void HideActionMenu()
    {
        actionMenuPanel.SetActive(false);
    }
}

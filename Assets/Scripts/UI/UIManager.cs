using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject actionMenuPanel;
    public Button attackButton;
    public Button waitButton;
    public Button moveButton;
    public Button startButton;

    public GameObject movementIndicatorPanel;

    [HideInInspector]
    public bool actionSelected = false;
    [HideInInspector]
    public ActionData selectedAction;

    private bool unitsReady = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            actionMenuPanel.SetActive(false);
            if (movementIndicatorPanel != null)
                movementIndicatorPanel.SetActive(false);

            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UnitManager.OnUnitsSpawned += EnableStartButton;
    }

    void OnDestroy()
    {
        UnitManager.OnUnitsSpawned -= EnableStartButton;
    }

    void EnableStartButton()
    {
        unitsReady = true;
        startButton.interactable = true;
    }

    void OnStartButtonClicked()
    {
        if (!unitsReady)
        {
            Debug.LogError("Units are not ready yet!");
            return;
        }

        TurnManager turnManager = FindObjectOfType<TurnManager>();
        if (turnManager != null)
        {
            turnManager.StartGame();
        }

        startButton.gameObject.SetActive(false);
    }

    public void ShowActionMenu(Unit unit)
    {
        actionSelected = false;
        selectedAction = null;
        actionMenuPanel.SetActive(true);

        attackButton.onClick.RemoveAllListeners();
        waitButton.onClick.RemoveAllListeners();
        moveButton.onClick.RemoveAllListeners();

        attackButton.onClick.AddListener(() => OnActionSelected(Actions.Attack));
        waitButton.onClick.AddListener(() => OnActionSelected(Actions.Wait));
        moveButton.onClick.AddListener(() => OnActionSelected(Actions.Move));
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

    public void ShowMovementIndicator()
    {
        if (movementIndicatorPanel != null)
        {
            movementIndicatorPanel.SetActive(true);
        }
    }

    public void HideMovementIndicator()
    {
        if (movementIndicatorPanel != null)
        {
            movementIndicatorPanel.SetActive(false);
        }
    }
}

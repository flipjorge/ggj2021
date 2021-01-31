using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private UIGameStatePrefabs uiSO;

    private GameObject currentGO;

    public void OnStateChanged(GameState state)
    {
        Object.Destroy(currentGO);

        GameObject go = uiSO.GetPrefab(state);
        if (go != null)
            currentGO = Instantiate(uiSO.GetPrefab(state), transform);
    }
}

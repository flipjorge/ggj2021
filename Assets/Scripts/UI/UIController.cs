using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private UIGameStatePrefabs uiSO;

    private GameObject currentGO;

    public void OnStateChanged(GameState state)
    {
        DestroyObject(currentGO);

        GameObject go = uiSO.GetPrefab(state);
        if (go != null)
            currentGO = Instantiate(uiSO.GetPrefab(state), transform);
    }
}

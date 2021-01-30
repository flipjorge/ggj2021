using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField]
    private GameStatePrefabs stageSO;

    private GameObject currentGO;
    
    public void OnStateChanged(GameState state)
    {
        DestroyObject(currentGO);

        GameObject go = stageSO.GetPrefab(state);
        if(go != null)
            currentGO = Instantiate(stageSO.GetPrefab(state), transform);
    }
}

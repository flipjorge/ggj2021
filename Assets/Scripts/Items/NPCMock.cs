using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMock : MonoBehaviour
{
    public GameObject item;
    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = Random.Range(5f, 10f);

        StartCoroutine(DropItemAfterSeconds());
    }

    IEnumerator DropItemAfterSeconds()
    {
        yield return new WaitForSeconds(waitTime);
        SpawnItem();
    }

    private void SpawnItem()
    {
        item.transform.position = gameObject.transform.position;
        Instantiate(item);
        item.GetComponent<Item>().owner = gameObject;
    }
}

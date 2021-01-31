using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookAtCamera : MonoBehaviour
{
    #region Lifecycle

    private void Awake()
    {
        _camera = Camera.main;
        transform.LookAt(_camera.transform);
        var rotation = transform.eulerAngles;
        rotation.y = 180 - transform.parent.rotation.y;
        transform.rotation = Quaternion.Euler(rotation);

        if (revertLookAt)
            transform.forward *= -1;
    }

    private void Update()
    {
        if (!useUpdate) return;

        transform.LookAt(_camera.transform);
        var rotation = transform.eulerAngles;
        rotation.y = 180 - transform.parent.rotation.y;
        transform.rotation = Quaternion.Euler(rotation);
    }

    #endregion

    #region Camera

    private Camera _camera;

    #endregion

    #region RefreshOnUpdate

    public bool useUpdate;

    #endregion

    public bool revertLookAt = false;
}
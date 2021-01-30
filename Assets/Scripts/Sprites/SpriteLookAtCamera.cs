using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookAtCamera : MonoBehaviour
{
    #region Lifecycle
    private void Awake()
    {
        _camera = Camera.main;
        var rotation = transform.eulerAngles;
        rotation.y = _camera.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(rotation);
    }
    #endregion

    #region Camera
    private Camera _camera;
    #endregion
}

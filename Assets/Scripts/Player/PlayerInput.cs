using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Lifecycle
    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        _player.movementDirection = new Vector3(horizontal, 0, vertical);
    }
    #endregion

    #region Player
    private Player _player;
    #endregion
}

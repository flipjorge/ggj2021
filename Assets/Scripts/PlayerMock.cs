using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMock : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 10.0f;
    private float gravityValue = -9.81f;

    //NOTE: Important stuff
    private static PlayerTrail playerTrail;


    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        //NOTE: Important stuff
        playerTrail = gameObject.GetComponent<PlayerTrail>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Destroy First Item
        if (Input.GetButtonDown("Jump"))
        {
            playerTrail.destroyFirst();
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    //NOTE: Important stuff
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            CheckFirstItemOwnership(collision.gameObject);
        }
    }

    //NOTE: Important stuff
    private void CheckFirstItemOwnership(GameObject gameObject)
    {
        if(playerTrail.LeaderTrail.Count > 0)
        {
            GameObject owner = playerTrail.LeaderTrail[0].GetComponent<Item>().GetOwner();
            if (gameObject.Equals(owner))
            {
                print("Oh thanks for finding this!");
                playerTrail.destroyFirst();
            }
        }
    }
}
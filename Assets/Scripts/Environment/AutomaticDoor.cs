using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{

    public Transform leftDoor;
    public Transform rightDoor;

    private Sequence seq;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("NPC")) return;

        Open();
    }



    private void OnTriggerStay(Collider other)
    {
        Open();
    }

    private void OnTriggerExit(Collider other)
    {
        Close();
    }
    
    private void Open()
    {
        seq.Kill();
        seq = DOTween.Sequence();
        seq.Append(leftDoor.DOLocalMoveX(-2.5f, 1f));
        seq.Join(rightDoor.DOLocalMoveX(2.5f, 1f));
    }
    
    private void Close()
    {
        seq.Kill();
        seq = DOTween.Sequence();
        seq.Append(leftDoor.DOLocalMoveX(-1f, 1f));
        seq.Join(rightDoor.DOLocalMoveX(1f, 1f));
    }
}

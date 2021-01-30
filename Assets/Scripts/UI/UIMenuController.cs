using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleSection;

    [SerializeField]
    private TextMeshProUGUI startSection;

    Sequence seq;

    void Start()
    {
        titleSection.DOFade(1f, 1.5f).OnComplete(OnTitleFaded);
    }

    // Update is called once per frame
    void OnTitleFaded()
    {
        seq = DOTween.Sequence();
        seq.Append(startSection.DOFade(1, 1f));
        seq.Append(startSection.DOFade(0, 1f));
        seq.SetLoops(-1, LoopType.Restart);
    }

    private void OnDisable()
    {
        DOTween.Kill(seq);
    }
}

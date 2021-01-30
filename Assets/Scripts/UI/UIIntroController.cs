using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIntroController : MonoBehaviour
{
    [SerializeField]
    private Image logo;

    void Start()
    {
        logo.DOFade(1f, 1.5f).OnComplete(OnFadeIn).SetDelay(.35f);
    }

    void OnFadeIn()
    {
        logo.DOFade(0f, 1f).SetDelay(1f).OnComplete(() => GameManager.Instance.SetGameState(GameState.Menu));
    }
}

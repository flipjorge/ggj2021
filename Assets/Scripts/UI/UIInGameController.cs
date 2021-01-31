using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI counterTxt;

    [SerializeField]
    private Image background;

    [SerializeField]
    private int counter = 3;

    private int initialCounter;

    // Start is called before the first frame update
    void Start()
    {
        initialCounter = counter;
        Init();
    }

    public void Init()
    {
        StartCounter();
    }

    private void StartCounter()
    {
        counterTxt.text = initialCounter.ToString();
        counterTxt.DOFade(1f, .25f);

        background.DOFade(0, initialCounter);
        DOTween.To(() => counter, x => { counter = x; counterTxt.text = counter.ToString();}, 0, initialCounter).OnComplete(OnCounterComplete).SetUpdate(true);
        //Time.timeScale = 0;

    }

    private void OnCounterComplete()
    {
        counterTxt.DOFade(0f, .25f).OnComplete(() => GameManager.Instance.ActivateGameplay());
    }
}

using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI initialCounterTxt;

    [SerializeField]
    private TextMeshProUGUI counterTxt;

    [SerializeField]
    private TextMeshProUGUI scoreTxt;

    [SerializeField]
    private Image background;

    [SerializeField]
    private int counter = 3;

    private int initialCounter;

    // Start is called before the first frame update
    void Start()
    {
        initialCounter = counter;
        GameManager.Instance.RegisterOnStartEvent(OnGameplayStarted);
        GameManager.Instance.RegisterOnScoreEvent(OnScoreChanged);
        Init();
    }

    public void Init()
    {
        StartInitialCounter();
    }

    private void StartInitialCounter()
    {
        initialCounterTxt.text = initialCounter.ToString();

        background.DOFade(0, initialCounter);
        DOTween.To(() => counter, x => { counter = x; initialCounterTxt.text = counter.ToString();}, 0, initialCounter).OnComplete(OnCounterComplete).SetUpdate(true);
        //Time.timeScale = 0;

    }

    private void OnCounterComplete()
    {
        Time.timeScale = 1;
        initialCounterTxt.DOFade(0f, .25f).OnComplete(() => GameManager.Instance.ActivateGameplay());
    }

    private void OnGameplayStarted()
    {
        
    }

    private void OnScoreChanged(int newScore)
    {
        scoreTxt.text = newScore.ToString();
    }

    void OnDisable()
    {
        GameManager.Instance.UnRegisterOnStartEvent(OnGameplayStarted); 
    }
}

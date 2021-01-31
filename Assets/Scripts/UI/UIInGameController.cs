using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private TextMeshProUGUI finalScoreTxt;

    [SerializeField]
    private GameObject finalScoreContainer;

    [SerializeField]
    private Image background;

    [SerializeField]
    private int counter = 3;

    private int initialCounter;

    private GameManager GMref;

    private bool gameplayActive = false;
    private bool allowRestart = false;

    // Start is called before the first frame update
    void Start()
    {
        GMref = GameManager.Instance;
        initialCounter = counter;
        GMref.RegisterOnGameplayStartEvent(OnGameplayStarted);
        GMref.RegisterOnGameplayEndingEvent(OnGameplayEnded);
        GMref.RegisterOnScoreEvent(OnScoreChanged);
        Init();
    }

    private void Update()
    {
        if (gameplayActive)
        {
            counterTxt.text = string.Format("{0:D2}:{1:D2}", GMref.TimeLeft.Minutes, GMref.TimeLeft.Seconds); 
        }

        if (allowRestart)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene(2);
                allowRestart = false;
                Init();
            }
        }
    }

    public void Init()
    {
        finalScoreContainer.SetActive(false);
        counterTxt.text = "";
        scoreTxt.text = "";
        GMref.ResetGameplay();
        StartInitialCounter();
    }

    private void StartInitialCounter()
    {
        counter = initialCounter;
        initialCounterTxt.DOFade(1f, 0);
        initialCounterTxt.text = initialCounter.ToString();

        background.DOFade(0, initialCounter);
        DOTween.To(() => counter, x => { counter = x; initialCounterTxt.text = counter.ToString(); }, 0, initialCounter).OnComplete(OnCounterComplete);
    }

    private void OnCounterComplete()
    {
        initialCounterTxt.DOFade(0f, .25f).OnComplete(() => GMref.ActivateGameplay());
        gameplayActive = true;
    }

    private void OnGameplayStarted()
    {
        
    }

    private void OnGameplayEnded()
    {
        gameplayActive = false;
        allowRestart = true;
        finalScoreTxt.text = "final score: " + scoreTxt.text;
        finalScoreContainer.SetActive(true);
    }

    private void OnScoreChanged(int newScore)
    {
        scoreTxt.text = newScore.ToString();
    }

    void OnDisable()
    {
        GMref.UnRegisterOnGameplayStartEvent(OnGameplayStarted); 
    }
}

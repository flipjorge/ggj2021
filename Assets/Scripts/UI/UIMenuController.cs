using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleSection;

    [SerializeField]
    private TextMeshProUGUI startSection;

    [SerializeField]
    private TextMeshProUGUI message1;

    [SerializeField]
    private TextMeshProUGUI message2;
    
    [SerializeField]
    private TextMeshProUGUI message3;

    Sequence seq;
    Sequence seq2;

    void Start()
    {
        titleSection.DOFade(1f, 1.5f).OnComplete(OnTitleFaded);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartGame();
        }
    }

    // Update is called once per frame
    void OnTitleFaded()
    {
        seq = DOTween.Sequence();
        seq.Append(startSection.DOFade(1, 1f));
        seq.Append(startSection.DOFade(0, 1f));
        seq.SetLoops(-1, LoopType.Restart);
        seq.SetId("seq");
    }

    private void OnDisable()
    {
        DOTween.Kill(seq2);
    }

    public void StartGame()
    {
        DOTween.Kill(seq);
        DOTween.Kill("seq");
        seq2 = DOTween.Sequence();
        seq2.Append(titleSection.DOFade(0, .35f));
        seq2.Join(startSection.DOFade(0, .35f));
        seq2.Append(message1.DOFade(1, 1f));
        seq2.AppendInterval(3f);
        seq2.Append(message1.DOFade(0, .75f));
        seq2.Append(message2.DOFade(1, 1f));
        seq2.AppendInterval(3f);
        seq2.Append(message2.DOFade(0, .75f));
        seq2.Append(message3.DOFade(1, 1f));
        seq2.AppendInterval(3f);
        seq2.Append(message3.DOFade(0, .75f));
        seq2.OnComplete(() => GameManager.Instance.SetGameState(GameState.InGame));
    }
}

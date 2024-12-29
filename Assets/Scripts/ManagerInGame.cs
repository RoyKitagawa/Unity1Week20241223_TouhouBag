using DG.Tweening;
using TMPro;
using UnityEngine;

public class ManagerInGame : MonoBehaviourSingleton<ManagerInGame>
{
    // デバッグ処理用
    [SerializeField]
    private bool startWithBattle;

    [SerializeField]
    private GameObject rootBagEdit;
    [SerializeField]
    private GameObject rootBattle;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject waveClearPanel;
    [SerializeField]
    private GameObject gameClearPanel;
    [SerializeField]
    private TextMeshProUGUI moneyText;

    // 画面サイズ
    private Rect screenCorners;

    // 所持金情報
    private int initialMoneyAmt = 999;
    private int currentMoneyAmt = 999;

    // 現状のステージ進捗ステータス
    private int currentWave;
    private int totalWave;

    private void Initialize()
    {
        currentWave = 0;
        totalWave = 20;        
        screenCorners = BasicUtil.GetScreenWorldCorners(Camera.main);
    }

    public int GetCurrentMoney()
    {
        return currentMoneyAmt;
    }

    public void AddMoney(int moneyAmt)
    {
        SetMoneyAmt(currentMoneyAmt + moneyAmt);
    }

    public void SetMoneyAmt(int moneyAmt)
    {
        currentMoneyAmt = moneyAmt;
        moneyText.text = "所持金：" + currentMoneyAmt + "コイン";
    }

    public void StartModeBagEdit()
    {
        rootBagEdit.SetActive(true);
        rootBattle.SetActive(false);
        SetMoneyAmt(currentMoneyAmt);
        StageManager.Instance.InitializeStage();
        StageManager.Instance.OnStartBagEditMode();
    }

    public void StartModeBattle()
    {
        rootBagEdit.SetActive(false);
        rootBattle.SetActive(true);

        ManagerBattlePhase.Instance.OnStartBattlePhase();
    }

    public void ShowGameClearResult()
    {
        gameClearPanel.transform.position = new Vector2(BasicUtil.GetScreenWorldCorners(Camera.main).width, 0.0f);
    }

    public void ShowWaveClearResult()
    {
        // 時間を止める
        waveClearPanel.gameObject.SetActive(true);
        waveClearPanel.transform.position = new Vector3(screenCorners.width, 0.0f, 0.0f);
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        sequence.Append(waveClearPanel.transform.DOMove(Vector3.zero, 0.5f).SetEase(Ease.Linear));
        sequence.Append(waveClearPanel.transform.DOMove(new Vector3(-screenCorners.width, 0.0f, 0.0f), 0.5f).SetDelay(1.0f).SetEase(Ease.Linear));
        sequence.OnComplete(() => {
            // TODO ユーザーがクリックしたらタイトルに遷移するようにしたい
            ManagerSceneTransition.Instance.Move2Scene(SceneType.Title);
            waveClearPanel.gameObject.SetActive(false);
            ResumeTimer();
        }).SetDelay(0.5f);
        PauseTimer();
    }

    public void ShowGameOverResult()
    {
        // 時間を止める
        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.transform.position = new Vector3(screenCorners.width, 0.0f, 0.0f);
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        sequence.Append(gameOverPanel.transform.DOMove(Vector3.zero, 0.5f).SetEase(Ease.Linear));
        sequence.Append(gameOverPanel.transform.DOMove(new Vector3(-screenCorners.width, 0.0f, 0.0f), 0.5f).SetDelay(1.0f).SetEase(Ease.Linear));
        sequence.OnComplete(() => {
            // TODO ユーザーがクリックしたらタイトルに遷移するようにしたい
            ManagerSceneTransition.Instance.Move2Scene(SceneType.Title);
            gameOverPanel.gameObject.SetActive(false);
            ResumeTimer();
        }).SetDelay(0.5f);
        PauseTimer();
    }

    private void PauseTimer()
    {
        Time.timeScale = 0.0f;
    }

    private void ResumeTimer()
    {
        Time.timeScale = 1.0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
        if(startWithBattle) StartModeBattle();
        else StartModeBagEdit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using DG.Tweening;
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

    // 画面サイズ
    private Rect screenCorners;

    // 現状のステージ進捗ステータス
    private int currentWave;
    private int totalWave;

    private void Initialize()
    {
        currentWave = 0;
        totalWave = 20;
        screenCorners = BasicUtil.GetScreenWorldCorners(Camera.main);
    }

    public void StartModeBagEdit()
    {
        rootBagEdit.SetActive(true);
        rootBattle.SetActive(false);
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
        waveClearPanel.transform.position = new Vector2(BasicUtil.GetScreenWorldCorners(Camera.main).width, 0.0f);
    }

    public void ShowGameOverResult()
    {
        // 時間を止める
        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.transform.position = new Vector3(screenCorners.width, 0.0f, 0.0f);
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        sequence.Append(gameOverPanel.transform.DOMove(Vector3.zero, 1.0f).SetEase(Ease.Linear));
        sequence.Append(gameOverPanel.transform.DOMove(new Vector3(-screenCorners.width, 0.0f, 0.0f), 1.0f).SetDelay(1.0f).SetEase(Ease.Linear));
        sequence.OnComplete(() => {
            // GameOver終了時にリザルト画面へと遷移
            ManagerSceneTransition.Instance.Move2Scene(SceneType.Result);
            gameOverPanel.gameObject.SetActive(false);
            ResumeTimer();
        });
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

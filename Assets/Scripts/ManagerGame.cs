using UnityEngine;

/// <summary>
/// ゲーム全体で管理する必要がある要素用マネージャー
/// </summary>
public class ManagerGame : MonoBehaviourSingleton<ManagerGame>
{
    // 所持金情報
    public int InitialMoneyAmt = 999;
    private int moneyAmt;
    // WAVE関連
    private int currentWave;
    private int clearedWave;

    // 金銭関連
    public int GetMoney() { return moneyAmt; }
    public void SetMoney(int value) { moneyAmt = value; }
    public void AddMoney(int value) { moneyAmt += value; }
    public void AddMoneyForNewWave(int nextWave) { AddMoney(7 + nextWave); }
    // WAVE関連
    public int GetCurrentWave() { return currentWave; }
    public void SetCurrentWave(int wave) { currentWave = wave; }
    public int GetClearedWave() { return clearedWave; }
    public void SetClearedWave(int wave) { clearedWave = wave; }

}

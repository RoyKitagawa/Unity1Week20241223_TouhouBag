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

    public void StartModeBagEdit()
    {
        InitRootObj();
        rootBagEdit.SetActive(true);
        rootBattle.SetActive(false);
    }

    public void StartModeBattle()
    {
        InitRootObj();
        rootBagEdit.SetActive(false);
        rootBattle.SetActive(true);

        ManagerBattlePhase.Instance.OnStartBattlePhase();
    }

    private void InitRootObj()
    {
        // if(rootBagEdit == null) rootBagEdit = BasicUtil.GetRootObject(Consts.Roots.BagEdit);
        // if(rootBattle == null) rootBattle = BasicUtil.GetRootObject(Consts.Roots.Battle);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(startWithBattle) StartModeBattle();
        else StartModeBagEdit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    /*[SerializeField] List<BattleUnit> PlayerUnitList;
    [SerializeField] List<BattleUnit> EnemyUnitList;
    [SerializeField] List<BattleHud> PlayerHudList;
    [SerializeField] List<BattleHud> EnemyHudList;*/
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialog dialogText;

    private GameObject BattleDialogSW;
    private GameObject BattleDialog;
    private GameObject BattleMoveBox;
    private GameObject BattleActionUISw;
    private GameObject BattleEnemyHud;
    private GameObject BattlePlayerHud;
    void Start()
    {
        InitBattle();
    }
    public void InitBattle()
    {
        playerUnit.Init();
        enemyUnit.Init();
        playerHud.SetPokemonInfo(playerUnit.PokemonUnit);
        enemyHud.SetPokemonInfo(enemyUnit.PokemonUnit);
        InitPanel();
    }

    public void InitPanel()
    {
        BattleDialogSW = (transform.Find("BattleCanvas/BattleDialogSW") as Transform).gameObject;
        BattleDialog = (transform.Find("BattleCanvas/BattleDialog") as Transform).gameObject;
        BattleMoveBox = (transform.Find("BattleCanvas/BattleMoveBox") as Transform).gameObject;
        BattleActionUISw = (transform.Find("BattleCanvas/BattleActionUISw") as Transform).gameObject;
        BattleEnemyHud = (transform.Find("BattleCanvas/BattleEnemyHud") as Transform).gameObject;
        BattlePlayerHud = (transform.Find("BattleCanvas/BattleOwnHud") as Transform).gameObject;
        if (BattleDialog != null)
        {
            Debug.Log("BattleDialog");
            BattleMoveBox.SetActive(false);
            BattleDialog.SetActive(false);
            BattleActionUISw.SetActive(false);
            BattlePlayerHud.SetActive(false);
        }
        TransitionForStartBattle().Forget();
    }

    public async UniTaskVoid TransitionForStartBattle()
    {
        await dialogText.SetDialogByWord($"野生的{enemyUnit.PokemonUnit.Name}跳了出来");
        SwitchPanelShowOrHiden(dialogText.gameObject, false);
        SwitchPanelShowOrHiden(BattleActionUISw, true);
        SwitchPanelShowOrHiden(playerHud, true);
    }

    public void TransitionForEndBattle()
    {

    }

    public void SwitchPanelShowOrHiden<T>(T t,bool bIsShow)
    {
        var TTransform = t as GameObject;
        if(TTransform != null)
        {
            Debug.Log("SwitchPanelShowOrHiden succ");
            TTransform.gameObject.SetActive(bIsShow);
        }
        else
        {
            Debug.Log("BattleSystem TTransform is null");
        }
    }

    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class BattleInfoUI : MonoBehaviour
{
    static GameObject prefab => Resources.Load<GameObject>("UI/BattleInfoUI");

    List<EntityButton> playerUnitsButtons = new List<EntityButton>();
    List<EntityButton> enemyUnitsButtons = new List<EntityButton>();

    public Text BattleState;
    public Text BattleLog;
    public Text WinResourcesText;

    public Transform playerUnitsParent;
    public Transform enemyUnitsParent;

    public Transform BattleStartUI;
    public Transform BattleEndedUI;


    public Button NextTurnButton;
    public Button GoAwayButton;



    SimpleTextBattle battle;


    public static void ShowBattle(SimpleTextBattle battle)
    {
        var instance = Instantiate(prefab, UIManager.canvasTransform).GetComponent< BattleInfoUI>();

        //instance.transform.SetSiblingIndex(1);

        instance.Init(battle);       
    }

    private void Init(SimpleTextBattle battle)
    {
        this.battle = battle;

        for (int i = 0; i < battle.playerUnits[0].UnitTypesCount; i++)
        {
            var unit = battle.playerUnits[0].units[i];
            var count = battle.playerUnits[0].unitCount[i];

            var _button = EntityButton.CreateEntityButton(unit, playerUnitsParent, (GameEntity ent) => {
                EntityInfoUI.ShowUnitUI(unit, false, battle.enemies[0].unitCount[i]);
            }, count);

            playerUnitsButtons.Add(_button);
        }

        for (int i = 0; i < battle.enemies[0].UnitTypesCount; i++)
        {
            var unit = battle.enemies[0].units[i];
            var count = battle.enemies[0].unitCount[i];

            var _button = EntityButton.CreateEntityButton(unit, enemyUnitsParent, (GameEntity ent) => {
                EntityInfoUI.ShowUnitUI(unit, false, battle.enemies[0].unitCount[i]);
            }, count);

            enemyUnitsButtons.Add(_button);
        }

        battle.onBattleEvent += onBattleEvent;
        battle.onUnitDead += onUnitDead;


        NextTurnButton.onClick.AddListener(battle.NextTurn);
    }

    private void onBattleEnded(bool win)
    {
        BattleStartUI.gameObject.SetActive(false);
        BattleEndedUI.gameObject.SetActive(true);

        if(battle.playerUnits[1].UnitTypesCount != playerUnitsButtons.Count)
        {
            for (int i = 0; i < playerUnitsButtons.Count; i++)
            {
               Destroy(playerUnitsButtons[i].gameObject);
            }

            for (int i = 0; i < battle.playerUnits[1].UnitTypesCount; i++)
            {
                var unit = battle.playerUnits[1].units[i];

                var count = battle.playerUnits[1].unitCount[i];

                EntityButton.CreateEntityButton(unit, playerUnitsParent, (GameEntity ent) => { EntityInfoUI.ShowUnitUI(unit, false, count); }, count);
            }
        }
        else
        {
            for (int i = 0; i < battle.playerUnits[1].UnitTypesCount; i++)
            {
                var unit = battle.playerUnits[1].units[i];

                var count = battle.playerUnits[1].unitCount[i];

                playerUnitsButtons[i].Initialize(unit, (GameEntity ent) => { EntityInfoUI.ShowUnitUI(unit, false, 0); }, count);
            }
        }

        if (battle.enemies[1].UnitTypesCount != enemyUnitsButtons.Count)
        {
            for (int i = 0; i < enemyUnitsButtons.Count; i++)
            {
                Destroy(enemyUnitsButtons[i].gameObject);
            }

            for (int i = 0; i < battle.enemies[1].UnitTypesCount; i++)
            {
                var unit = battle.enemies[1].units[i];

                var count = battle.enemies[1].unitCount[i];

                EntityButton.CreateEntityButton(unit, enemyUnitsParent, (GameEntity ent) => { EntityInfoUI.ShowUnitUI(unit, false, count); }, count);
            }
        }
        else
        {
            for (int i = 0; i < battle.enemies[1].UnitTypesCount; i++)
            {
                var unit = battle.enemies[1].units[i];

                var count = battle.enemies[1].unitCount[i];

                enemyUnitsButtons[i].Initialize(unit, (GameEntity ent) => { EntityInfoUI.ShowUnitUI(unit, false, 0); }, count);
            }
        }

        for (int i = 0; i < battle.playerUnits[1].UnitTypesCount; i++)
        {
            var unit = battle.playerUnits[1].units[i];
        }

        for (int i = 0; i < battle.enemies[1].UnitTypesCount; i++)
        {
            var unit = battle.enemies[1].units[i];
        }

        if (win)
        {
            BattleState.text = "VICTORY!";

            WinResourcesText.text = "Resources collected:\n" + battle.WinResources.ToString();
        }
        else
        {
            BattleState.text = "DEFEATED!";

            WinResourcesText.text = "Return when come stronger";
        }
    }

    public void Close()
    {
        battle.StopBattle();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        battle.onBattleEvent -= onBattleEvent;
        battle.onUnitDead -= onUnitDead;

    }

    private void onUnitDead(BattleUnit unit, bool isPlayerSide, int countLast)
    {
        if (isPlayerSide)
        {
            foreach(var button in playerUnitsButtons)
            {
                if(button.linkedEntity.Hash == unit.Hash)
                {
                    button.SetCount(countLast);

                    if (countLast <= 0)
                    {
                        playerUnitsButtons.Remove(button);
                        Destroy(button.gameObject);
                    }

                    break;
                }
            }
        }
        else
        {
            foreach (var button in enemyUnitsButtons)
            {
                if (button.linkedEntity.Hash == unit.Hash)
                {
                    button.SetCount(countLast);

                    if (countLast <= 0)
                    {
                        enemyUnitsButtons.Remove(button);
                        Destroy(button.gameObject);
                    }
                      
                    break;
                }
            }
        }
    }

    private void onBattleEvent(string obj)
    {
        var text = (BattleLog.text + "\n" +  obj);

        if (text.Length > 250)
        {
            int startIndex = text.IndexOf('\n') + 1;

            text = text.Substring(startIndex);
        }
        BattleLog.text = text;
    }
}
*/
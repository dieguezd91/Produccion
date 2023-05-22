using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Menu, Bag, BattleBag }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] InventoryUI inventoryUI;

    GameState state;

    MenuController menuController;

    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        menuController = GetComponent<MenuController>();
    }

    private void Start()
    {
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;

        menuController.onBack += () =>
        {
            state = GameState.FreeRoam;
        };

        menuController.onMenuSelected += OnMenuSelected;
    }

    void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        battleSystem.StartBattle();
    }

    void EndBattle(bool won)
        {
            state = GameState.FreeRoam;
            battleSystem.gameObject.SetActive(false);
            worldCamera.gameObject.SetActive(true);
        }

    private void Update()
    {
        if(state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();

            if(Input.GetKeyDown(KeyCode.M))
            {
                menuController.OpenMenu();
                state = GameState.Menu;
            }
        }
        else if(state== GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
        else if (state == GameState.Menu)
        {
            menuController.HandleUpdate();
        }
        else if( state == GameState.Bag)
        {
            Action onBack = () =>
            {
                inventoryUI.gameObject.SetActive(false);
                state = GameState.FreeRoam;
            };

            inventoryUI.HandleUpdate(onBack);
        }
    }

    void OnMenuSelected(int selectedItem)
    {
        if(selectedItem == 0)
        {
            inventoryUI.gameObject.SetActive(true);
            state = GameState.Bag;            
        }
        else if(selectedItem == 1)
        {
            //menuItem (2)
        }
        else if(selectedItem == 2)
        {
            state = GameState.FreeRoam;
            //menuItem (3)
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public bool isBattleActive;
    bool inventoryIsOpen;

    [SerializeField] GameObject battleScene;
    [SerializeField] List<BattleCharacters> activeCharacters = new List<BattleCharacters>();

    [SerializeField] Transform[] playersPositions;
    [SerializeField] Transform[] enemiesPositions;

    [SerializeField] BattleCharacters[] playerPrefabs, enemiesPrefabs;

    [SerializeField] int currentTurn;
    [SerializeField] bool waitingForTurn;
    [SerializeField] GameObject UIButtonHolder;

    [SerializeField] BattleMoves[] battleMovesList;

    [SerializeField] GameObject[] playerBattleStats;
    [SerializeField] Text[] playersNameText;
    [SerializeField] Slider[] playerHealthSlider;

    [SerializeField] float chanceToRunAway = 0.5f;
    public GameObject itemsToUseMenu;
    [SerializeField] ItemsManager selectedItem;
    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotContainerParent;
    [SerializeField] Text itemName, itemDescription;

    [SerializeField] GameObject characterChoicePanel;
    [SerializeField] Text[] playerChoiceName;

    private int amountOfXp = 99;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.B))
        {
            StartBattle(new string[] { "Police", "Patrol" });
        }*/

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
        }

        CheckPlayerButtonHolder();
    }

    private void CheckPlayerButtonHolder()
    {
        if (isBattleActive)
        {
            if (waitingForTurn)
            {
                if (activeCharacters[currentTurn].IsPlayer())
                    UIButtonHolder.SetActive(true);
                else
                {
                    UIButtonHolder.SetActive(false);
                    StartCoroutine(EnemyMoveCoroutine());
                }
            }
        }
    }

    public void StartBattle(string[] enemiesToSpawn)
    {
        if (!isBattleActive)
        {
            SettingUpBattle();
            AddingPlayers();
            AddingEnemies(enemiesToSpawn);
            UpdatePlayerStats();
            

            waitingForTurn = true;
            currentTurn = 0;//Random.Range(0, activeCharacters.Count);
        }
    }

    private void AddingEnemies(string[] enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            if (enemiesToSpawn[i] != "")
            {
                for (int j = 0; j < enemiesPrefabs.Length; j++)
                {
                    if (enemiesPrefabs[j].characterName == enemiesToSpawn[i])
                    {
                        BattleCharacters newEnemy = Instantiate(
                            enemiesPrefabs[j],
                            enemiesPositions[i].position,
                            enemiesPositions[i].rotation,
                            enemiesPositions[i]
                            );
                        activeCharacters.Add(newEnemy);
                    }
                }
            }
        }
    }

    private void AddingPlayers()
    {
        for (int i = 0; i < GameManager.instance.GetPlayerStats().Length; i++)
        {
            if (GameManager.instance.GetPlayerStats()[i].gameObject.activeInHierarchy)
            {
                for (int j = 0; j < playerPrefabs.Length; j++)
                {
                    if (playerPrefabs[j].characterName == GameManager.instance.GetPlayerStats()[i].playerName)
                    {
                        BattleCharacters newPlayer = Instantiate(
                            playerPrefabs[j],
                            playersPositions[i].position,
                            playersPositions[i].rotation,
                            playersPositions[i]
                            );

                        activeCharacters.Add(newPlayer);
                        ImportPlayerStats(i);
                    }
                }
            }
        }
    }

    private void ImportPlayerStats(int i)
    {
        PlayerStats player = GameManager.instance.GetPlayerStats()[i];

        activeCharacters[i].currentHP = player.currentHP;
        activeCharacters[i].maxHP = player.maxHP;

        activeCharacters[i].dexterity = player.dexterity;
        activeCharacters[i].strength = player.strength;
        activeCharacters[i].defence = player.defence;

        activeCharacters[i].meleeWeaponDamage = player.meleeDamage;
        activeCharacters[i].rangeWeaponDamage = player.rangeDamage;
    }

    private void ExportPlayerStats(int i)
    {
        PlayerStats player = GameManager.instance.GetPlayerStats()[i];

        player.currentHP = activeCharacters[i].currentHP;
        player.maxHP = activeCharacters[i].maxHP;

        player.dexterity = activeCharacters[i].dexterity;
        player.strength = activeCharacters[i].strength;
        player.defence = activeCharacters[i].defence;

        player.meleeDamage = activeCharacters[i].meleeWeaponDamage;
        player.rangeDamage = activeCharacters[i].rangeWeaponDamage;
    }

    private void SettingUpBattle()
    {
        isBattleActive = true;
        GameManager.instance.battleIsActive = true;

        transform.position = new Vector3(
        Camera.main.transform.position.x,
        Camera.main.transform.position.y,
        transform.position.z
        );
        
        battleScene.SetActive(true);    
    }

    private void NextTurn()
    {
        currentTurn++;
        if (currentTurn >= activeCharacters.Count)
            currentTurn = 0;

        waitingForTurn = true;
        UpdateBattle();
        UpdatePlayerStats();
    }

    private void UpdateBattle()
    {
        bool allEnemiesAreDead = true;
        bool allPlayersAreDead = true;

        for(int i = 0; i < activeCharacters.Count; i++)
        {
            if(activeCharacters[i].currentHP < 0)
            {
                activeCharacters[i].currentHP = 0;
            }
            
            if(activeCharacters[i].currentHP == 0)
            {
                //kill character
            }
            else
            {
                if (activeCharacters[i].IsPlayer())
                    allPlayersAreDead = false;

                else
                    allEnemiesAreDead = false;
            }
        }

        if(allEnemiesAreDead || allPlayersAreDead)
        {
            if (allEnemiesAreDead)
            {
                PlayerStats.instance.AddXP(amountOfXp);
                ExportPlayerStats(0);
                Debug.Log("Won");
            }
            else if (allPlayersAreDead)
            {
                print("Lost");
                ExportPlayerStats(0);
                GameManager.instance.RespawnPlayer();
            }

            battleScene.SetActive(false);
            GameManager.instance.battleIsActive = false;
            isBattleActive = false;
        }
        else
        {
            while(activeCharacters[currentTurn].currentHP == 0)
            {
                currentTurn++;
                if(currentTurn >= activeCharacters.Count)
                {
                    currentTurn = 0;
                }
            }
        }
    }

    public IEnumerator EnemyMoveCoroutine()
    {
        waitingForTurn = false;

        yield return new WaitForSeconds(1f);
        EnemyAttack();
        
        yield return new WaitForSeconds(1f);
        NextTurn();        
    }  
    
    private void EnemyAttack()
    {
        List<int> players = new List<int>();

        for(int i = 0; i < activeCharacters.Count; i++)
        {
            if(activeCharacters[i].IsPlayer() && activeCharacters[i].currentHP > 0)
            {
                players.Add(i);
            }
        }

        int selectedPlayerToAttack = players[Random.Range(0, players.Count)];

        int selectedAttack = Random.Range(0, activeCharacters[currentTurn].AttackMovesAvailable().Length);

        /*for(int i = 0; i < battleMovesList.Length; i++)
        {
            if(battleMovesList[i].moveName == activeCharacters[currentTurn].AttackMovesAvailable()[selectedAttack])
            {
                PARA LAS ANIMACIONES
                Instantiate(
                    battleMovesList[i],
                    activeCharacters[selectedPlayerToAttack].transform.position,
                    activeCharacters[selectedPlayerToAttack].transform.rotation
                    );
            }
        }*/

        int movePower = 1;
        DealDamageToCharacters(selectedPlayerToAttack, movePower);

        UpdatePlayerStats();
    }

    private void DealDamageToCharacters(int selectedCharacterToAttack, int movePower)
    {

        //float attackMelee = activeCharacters[currentTurn].strength + activeCharacters[currentTurn].meleeWeaponDamage;
        float attackPower = activeCharacters[currentTurn].dexterity + activeCharacters[currentTurn].rangeWeaponDamage;
        float defenceAmount = activeCharacters[selectedCharacterToAttack].defence; //ACA SE PUEDE IMPLEMENTAR ALGO QUE SUME DEFENSA COMO UN CHALECO ANTIBALAS 

        //float meleeDamageAmount = (attackMelee / defenceAmount) * movePower * Random.Range(0.9f, 1.1f); 
        float damageAmount = (attackPower - defenceAmount) * movePower * Random.Range(0.9f, 1.1f);
        //int meleeDamageToGive = (int)meleeDamageAmount;
        int rangeDamageToGive = (int)damageAmount;

        //iguala el valor del daño a critico si es necesario
        rangeDamageToGive = CalculateCritical(rangeDamageToGive);

        //Debug.Log(activeCharacters[currentTurn].characterName + " use melee attack and cause " + (int)meleeDamageAmount + "(" + meleeDamageToGive + ") to " + activeCharacters[selectedCharacterToAttack]);
        Debug.Log(activeCharacters[currentTurn].characterName + " attack and causes " + (int)damageAmount + "(" + rangeDamageToGive + ") of damage to " + activeCharacters[selectedCharacterToAttack]);

        //activeCharacters[selectedCharacterToAttack].TakeHPMeleeDamage(meleeDamageToGive);
        activeCharacters[selectedCharacterToAttack].TakeHPDamage(rangeDamageToGive);
    }

    private int CalculateCritical(int damageToGive)
    {
        if(Random.value <= 0.1f)// si es critico multiplica x2
        {
            Debug.Log("Critical hit! instead of " + damageToGive + " points. " + (damageToGive * 2));

            return (damageToGive * 2);
        }
        // sino hace el daño normal
        return damageToGive;
    }

    private void UpdatePlayerStats()
    {
        for(int i = 0; i < playersNameText.Length; i++)
        {
            if(activeCharacters.Count > i)
            {
                if(activeCharacters[i].IsPlayer())
                {
                    BattleCharacters playerData = activeCharacters[i];

                    playersNameText[i].text = playerData.characterName;

                    playerHealthSlider[i].maxValue = playerData.maxHP;
                    playerHealthSlider[i].value = playerData.currentHP;                    
                }
                else
                {
                    playerBattleStats[i].SetActive(false);
                }
            }
            else
            {
                playerBattleStats[i].SetActive(false);
            }
        }
    }

    public void PlayerAttack(string moveName)//, int selectEnemyTarget)
    {
        int selectEnemyTarget = 1;
        int movePower = 1;

        DealDamageToCharacters(selectEnemyTarget, movePower);

        NextTurn();
    }

    public void RunAway()
    {
        if(Random.value > chanceToRunAway)
        {
            //Hay 50% de chances de no poder escapar y perdes el turno
            isBattleActive = false;
            battleScene.SetActive(false);
        }
        else
        {
            NextTurn();
        }
        ExportPlayerStats(0);
    }

    public void UpdateItemsInInventory()
    {
        if (!inventoryIsOpen)
            itemsToUseMenu.SetActive(true);
        else
            itemsToUseMenu.SetActive(false);
        inventoryIsOpen = !inventoryIsOpen;

        foreach (Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        }

        foreach (ItemsManager item in Inventory.instance.GetItemsList())
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("Item image").GetComponent<Image>();
            itemImage.sprite = item.itemsImage;

            Text itemsAmountText = itemSlot.Find("Amount Text").GetComponent<Text>();
            if (item.amount > 1)
                itemsAmountText.text = item.amount.ToString();
            else
                itemsAmountText.text = "";

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }

    public void SelectedItemToUse(ItemsManager itemToUse)
    {
        selectedItem = itemToUse;
        itemName.text = itemToUse.itemName;
        itemDescription.text = itemToUse.itemDescription;
    }

    public void OpenCharacterMenu()
    {
        if(selectedItem)
        {
            characterChoicePanel.SetActive(true);
            for(int i = 0; i < activeCharacters.Count; i++)
            {
                if(activeCharacters[i].IsPlayer())
                {
                    PlayerStats activePlayer = GameManager.instance.GetPlayerStats()[i];
                    playerChoiceName[i].text = activePlayer.playerName;

                    bool activePlayerInHierarchy = activePlayer.gameObject.activeInHierarchy;
                    playerChoiceName[i].transform.parent.gameObject.SetActive(activePlayerInHierarchy);
                }
            }            
        }
        else
        {
            print("no item selected");
        }
    }

    public void UseItemButton(int selectedPlayer)
    {
        activeCharacters[selectedPlayer].UseItemInBattle(selectedItem);
        Inventory.instance.RemoveItem(selectedItem);

        UpdatePlayerStats();
        CloseCharacterChoicePanel();
        UpdateItemsInInventory();
        itemsToUseMenu.SetActive(false);
        NextTurn();
    }

    public void CloseCharacterChoicePanel()
    {
        characterChoicePanel.SetActive(false);
        itemsToUseMenu.SetActive(false);
    }
}


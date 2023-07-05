using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public bool isBattleActive;
    bool inventoryIsOpen;

    [SerializeField] GameObject battleScene;
    public Camera battleCamera;
    public AudioListener battleAudioListener;
    public Camera worldCamera;
    public AudioListener worldAudioListener;
    [SerializeField] List<BattleCharacters> activeCharacters = new List<BattleCharacters>();
    [SerializeField] GameObject lastEnemy;
    [SerializeField] GameObject enemyGO;
    Collider2D enemyCollider;

    [SerializeField] Transform playersPositions;
    [SerializeField] Transform enemiesPositions;

    [SerializeField] BattleCharacters[] playerPrefabs, enemiesPrefabs;

    [SerializeField] int currentTurn;
    [SerializeField] bool waitingForTurn;
    [SerializeField] GameObject UIButtonHolder;

    [SerializeField] BattleMoves[] battleMovesList;

    [SerializeField] GameObject[] playerBattleStats;
    [SerializeField] Text[] playersNameText;
    [SerializeField] Slider[] playerHealthSlider;

    [SerializeField] GameObject[] enemyBattleStats;
    [SerializeField] Text[] enemysNameText;
    [SerializeField] Slider[] enemyHealthSlider;

    [SerializeField] float chanceToRunAway = 0.5f;
    public GameObject itemsToUseMenu;
    [SerializeField] ItemsManager selectedItem;
    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotContainerParent;
    [SerializeField] Text itemName, itemDescriptionText;
    [SerializeField] GameObject itemDescription;

    [SerializeField] GameObject characterChoicePanel;
    [SerializeField] Text[] playerChoiceName;

    [SerializeField] TextMeshProUGUI damageReceived;
    [SerializeField] TextMeshProUGUI damageDealt;

    [SerializeField] TextMeshProUGUI log;

    private int amountOfXp;

    public bool allEnemiesAreDead = true;
    public bool allPlayersAreDead = true;

    public bool randomBattle;
    public bool dinniesBattle;

    public event EventHandler OnBattleEnd;

    [SerializeField] AudioClip[] clips;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        worldCamera = PlayerController.instance.worldCamera.GetComponent<Camera>();
        worldAudioListener = worldCamera.gameObject.GetComponent<AudioListener>();
        battleAudioListener = battleCamera.gameObject.GetComponent<AudioListener>();
    }

    void Update()
    {
        CheckPlayerButtonHolder();
    }

    private void CheckPlayerButtonHolder()
    {
        if (isBattleActive)
        {
            if (waitingForTurn)
            {
                if (activeCharacters[currentTurn].IsPlayer())
                {
                    UIButtonHolder.SetActive(true);
                }
                else
                {
                    UIButtonHolder.SetActive(false);
                    StartCoroutine(EnemyMoveCoroutine());
                }
            }
        }
    }

    public void StartBattle(GameObject enemy, string enemiesToSpawn, bool isRandom)
    {
        randomBattle = isRandom;
        dinniesBattle = isRandom;
        Destroy(lastEnemy);
        if (enemy != null)
        {
            enemyGO = enemy;
            enemyCollider = enemyGO.GetComponent<Collider2D>();
        }
        log.text = string.Empty;

        if (!isBattleActive)
        {
            SettingUpBattle();
            AddingPlayers();
            AddingEnemies(enemiesToSpawn);
            UpdatePlayerStats();
            UpdateEnemyStats();


            waitingForTurn = true;
            currentTurn = 0;//Random.Range(0, activeCharacters.Count);
        }
        GameManager.instance.player.SetActive(false);
        //enemyGO.SetActive(false);
    }

    private void AddingEnemies(string enemiesToSpawn)
    {
        for (int j = 0; j < enemiesPrefabs.Length; j++)
        {
            if (enemiesPrefabs[j].characterName == enemiesToSpawn)
            {
                BattleCharacters newEnemy = Instantiate(
                    enemiesPrefabs[j],
                    enemiesPositions.position,
                    enemiesPositions.rotation,
                    enemiesPositions
                    );
                if (activeCharacters.Count == 1)
                    activeCharacters.Add(newEnemy);
                else
                    activeCharacters[1] = newEnemy;
                lastEnemy = activeCharacters[1].gameObject;
            }
        }
    }

    private void AddingPlayers()
    {
        if (activeCharacters.Count > 0)
            Destroy(activeCharacters[0].gameObject);
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
                            playersPositions.position,
                            playersPositions.rotation,
                            playersPositions
                            );

                        if (activeCharacters.Count == 0)
                            activeCharacters.Add(newPlayer);
                        else
                            activeCharacters[0] = newPlayer;
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

        activeCharacters[i].level = player.playerLevel;

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
        if (PlayerStats.instance.playerLevel > activeCharacters[i].level)
            PlayerStats.instance.currentHP = PlayerStats.instance.maxHP;
    }

    private void SettingUpBattle()
    {
        isBattleActive = true;
        GameManager.instance.battleIsActive = true;

        battleScene.SetActive(true);
        worldCamera.gameObject.SetActive(false);
        battleCamera.gameObject.SetActive(true);
        worldAudioListener.enabled = false;
        battleAudioListener.enabled = true;
    }

    private void NextTurn()
    {
        currentTurn++;
        if (currentTurn >= activeCharacters.Count)
            currentTurn = 0;

        waitingForTurn = true;
        UpdateBattle();
        UpdatePlayerStats();
        UpdateEnemyStats();
    }

    private void UpdateBattle()
    {
        bool allEnemiesAreDead = true;
        bool allPlayersAreDead = true;

        for (int i = 0; i < activeCharacters.Count; i++)
        {
            if (activeCharacters[i].currentHP < 0)
            {
                activeCharacters[i].currentHP = 0;
            }

            if (activeCharacters[i].currentHP == 0)
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

        if (allEnemiesAreDead || allPlayersAreDead)
        {
            GameManager.instance.player.SetActive(true);
            //enemyGO.SetActive(true);

            if (allEnemiesAreDead)
            {
                PlayerStats.instance.AddXP(amountOfXp);
                MenuManager.instance.AddCreditsUI();
                ExportPlayerStats(0);
                if (!randomBattle)   Destroy(enemyGO);
                Debug.Log("Victoria!");
                  if (randomBattle || dinniesBattle)
                    OnBattleEnd?.Invoke(this, EventArgs.Empty);

            }
            else if (allPlayersAreDead)
            {
                ExportPlayerStats(0);
                GameManager.instance.RespawnPlayer();
                Debug.Log("Derrota...");
            }

            EndBattle();
        }
        else
        {
            while (activeCharacters[currentTurn].currentHP == 0)
            {
                currentTurn++;
                if (currentTurn >= activeCharacters.Count)
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

        for (int n = 0; n < activeCharacters.Count; n++)
        {
            if (activeCharacters[n].IsPlayer() && activeCharacters[n].currentHP > 0)
            {
                players.Add(n);
            }
        }
        int selectedPlayerToAttack = players[UnityEngine.Random.Range(0, players.Count)];

        int selectedAttack = UnityEngine.Random.Range(0, activeCharacters[currentTurn].AttackMovesAvailable().Length);

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
        if (activeCharacters[currentTurn].attacksAvailable.Length == 2)
        {
            int i = UnityEngine.Random.Range(1, 10);
            if (i <= 5) DealRangeDamageToCharacters(selectedPlayerToAttack, movePower);
            else DealMeleeDamageToCharacters(selectedPlayerToAttack, movePower);
        }
        else if (activeCharacters[currentTurn].attacksAvailable[0] == "Ataque melee") DealMeleeDamageToCharacters(selectedPlayerToAttack, movePower);
        else if (activeCharacters[currentTurn].attacksAvailable[0] == "Ataque rango") DealRangeDamageToCharacters(selectedPlayerToAttack, movePower);



        UpdatePlayerStats();
    }

    public void PlayerRangeAttack(string moveName)
    {
        int selectEnemyTarget = 1;
        int movePower = 1;

        DealRangeDamageToCharacters(selectEnemyTarget, movePower);

        AudioManager.instance.PlaySound(clips[1]);

        NextTurn();
    }

    public void PlayerMeleeAttack(string moveName)
    {
        int selectEnemyTarget = 1;
        int movePower = 1;

        DealMeleeDamageToCharacters(selectEnemyTarget, movePower);

        AudioManager.instance.PlaySound(clips[0]);

        NextTurn();
    }

    private void DealRangeDamageToCharacters(int selectedCharacterToAttack, int movePower = 1)
    {
        float attackPower = activeCharacters[currentTurn].dexterity + activeCharacters[currentTurn].rangeWeaponDamage;
        float defenceAmount = activeCharacters[selectedCharacterToAttack].defence;
        float damageAmount = (attackPower - defenceAmount) * movePower * UnityEngine.Random.Range(0.8f, 1.2f);

        int damageToGive = (int)damageAmount;
        if (damageToGive <= 0)
            damageToGive = 0;

        //iguala el valor del da�o a critico si es necesario
        damageToGive = CalculateCritical(damageToGive);
        Debug.Log(activeCharacters[currentTurn].characterName + " usa ataque a rango y causa " + (int)damageAmount + " de dano a " + activeCharacters[selectedCharacterToAttack].characterName);
        StartCoroutine(UpdateLog(activeCharacters[currentTurn].characterName + " usa ataque a rango y causa " + (int)damageAmount + " de dano a " + activeCharacters[selectedCharacterToAttack].characterName));

        StartCoroutine(ShowDamage(damageToGive));
        activeCharacters[selectedCharacterToAttack].TakeHPDamage(damageToGive);
    }

    private void DealMeleeDamageToCharacters(int selectedCharacterToAttack, int movePower = 1)
    {
        float attackPower = activeCharacters[currentTurn].strength + activeCharacters[currentTurn].meleeWeaponDamage;
        float defenceAmount = activeCharacters[selectedCharacterToAttack].defence;
        float damageAmount = (attackPower - defenceAmount) * movePower * UnityEngine.Random.Range(0.8f, 1.2f);

        int damageToGive = (int)damageAmount;
        if (damageToGive <= 0)
            damageToGive = 0;
        Debug.Log(activeCharacters[currentTurn].characterName + " usa ataque melee y causa " + (int)damageAmount + " de dano a " + activeCharacters[selectedCharacterToAttack].characterName);
        StartCoroutine(UpdateLog(activeCharacters[currentTurn].characterName + " usa ataque melee y causa " + (int)damageAmount + " de dano a " + activeCharacters[selectedCharacterToAttack].characterName));

        StartCoroutine(ShowDamage(damageToGive));
        activeCharacters[selectedCharacterToAttack].TakeHPDamage(damageToGive);
    }

    private int CalculateCritical(int damageToGive)
    {
        if (UnityEngine.Random.value <= 0.1f)// si es critico multiplica x2
        {
            Debug.Log("Golpe crítico! En lugar de " + damageToGive + " puntos, " + (damageToGive * 2));
            StartCoroutine(UpdateLog("Golpe crítico! en lugar de " + damageToGive + " puntos, " + (damageToGive * 2)));

            return (damageToGive * 2);
        }
        // sino hace el daño normal
        return damageToGive;
    }

    private void UpdatePlayerStats()
    {
        for (int i = 0; i < playersNameText.Length; i++)
        {
            if (activeCharacters.Count > i)
            {
                if (activeCharacters[i].IsPlayer())
                {
                    BattleCharacters playerData = activeCharacters[0];

                    playersNameText[i].text = playerData.characterName;

                    playerHealthSlider[i].maxValue = playerData.maxHP;
                    playerHealthSlider[i].value = playerData.currentHP;

                    PlayerStats player = GameManager.instance.GetPlayerStats()[i];
                    activeCharacters[i].meleeWeaponDamage = player.meleeDamage;
                    activeCharacters[i].rangeWeaponDamage = player.rangeDamage;
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

    private void UpdateEnemyStats()
    {
        for (int i = 0; i < enemysNameText.Length; i++)
        {
            if (activeCharacters.Count > i)
            {
                BattleCharacters enemyData = activeCharacters[1];

                enemysNameText[i].text = enemyData.characterName;

                enemyHealthSlider[i].maxValue = enemyData.maxHP;
                enemyHealthSlider[i].value = enemyData.currentHP;
            }
            else
            {
                enemyBattleStats[i].SetActive(false);
            }
        }
    }

    public void RunAway()
    {
        //enemyGO.SetActive(true);
        if (UnityEngine.Random.value > chanceToRunAway)
        {
            //Hay 50% de chances de no poder escapar y perdes el turno
            StartCoroutine(ScapingTime());
            OnBattleEnd?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            StartCoroutine(UpdateLog("Intentas escapar pero fallas."));
            NextTurn();
        }
        ExportPlayerStats(0);

    }

    public void UpdateItemsInInventory()
    {
        if (!inventoryIsOpen && isBattleActive)
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
            itemImage.sprite = item.icon;

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
        itemDescriptionText.text = itemToUse.itemDescription;
        itemDescription.SetActive(true);
    }

    public void OpenCharacterMenu()
    {
        if (selectedItem)
        {
            characterChoicePanel.SetActive(true);
            for (int i = 0; i < activeCharacters.Count; i++)
            {
                if (activeCharacters[i].IsPlayer())
                {
                    PlayerStats activePlayer = GameManager.instance.GetPlayerStats()[i];
                    playerChoiceName[i].text = activePlayer.playerName;

                    bool activePlayerInHierarchy = activePlayer.gameObject.activeInHierarchy;
                    playerChoiceName[i].transform.parent.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("no item selected");
        }
    }

    public void UseItemButton(int selectedPlayer)
    {
        activeCharacters[selectedPlayer].UseItemInBattle(selectedItem);
        Inventory.instance.RemoveItem(selectedItem);
        StartCoroutine(UpdateLog(activeCharacters[currentTurn].characterName + " usa " + selectedItem.name + " y se cura " + selectedItem.amountOfAffect + " puntos de vida."));

        UpdatePlayerStats();
        UpdateItemsInInventory();
        NextTurn();
    }

    IEnumerator ShowDamage(int damage)
    {
        if (currentTurn == 0)
        {
            damageDealt.text = damage.ToString();
            yield return new WaitForSeconds(5f);
            damageDealt.text = string.Empty;
        }
        else if (currentTurn == 1)
        {
            damageReceived.text = damage.ToString();
            yield return new WaitForSeconds(5f);
            damageReceived.text = string.Empty;
        }
    }

    IEnumerator UpdateLog(string newText)
    {
        log.text = string.Empty;
        log.text += newText;
        //if (log.isTextOverflowing)
        //{
        //    Debug.Log("Overflowing");
        //    log.text = string.Empty;
        //    log.text += newText;
        //}
        yield return new WaitForSeconds(2f);
    }

    IEnumerator ScapingTime()
    {
        if (!randomBattle)  enemyCollider.enabled = false;
        StartCoroutine(UpdateLog("Intentas escapar y lo logras."));
        yield return new WaitForSeconds(2f);
        EndBattle();
        yield return new WaitForSeconds(3f);
        if(!randomBattle) enemyCollider.enabled = true;
    }

    private void EndBattle()
    {
        GameManager.instance.player.SetActive(true);
        GameManager.instance.battleIsActive = false;
        isBattleActive = false;
        worldCamera.gameObject.SetActive(true);
        battleCamera.gameObject.SetActive(false);
        worldAudioListener.enabled = true;
        battleAudioListener.enabled = false;
        battleScene.SetActive(false);
    }
}
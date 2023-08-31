using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] TextMeshProUGUI damageReceived;
    [SerializeField] TextMeshProUGUI damageDealt;

    [SerializeField] Text amountOfAmmo;

    [SerializeField] TextMeshProUGUI log;

    private int amountOfXp;
    private int ammoRewards;

    public bool allEnemiesAreDead = true;
    public bool allPlayersAreDead = true;

    public bool randomBattle;
    public bool dinniesBattle;
    public bool bossBattle;

    public event EventHandler OnBattleEnd;

    AudioSource combatSong;

    RandomBattle randomCombat;
    float lastRandomBattle;
    [SerializeField] float inmunityTime;

    [SerializeField] FeedbackAfterCombat rewardsTexts;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
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
        combatSong = MusicManager.instance.GetComponent<AudioSource>();
        lastRandomBattle = 0;
    }

    void Update()
    {
        CheckPlayerButtonHolder();

        if (PlayerStats.instance.equipedRangeWeapon != null)
        {
            Debug.Log(PlayerStats.instance.equipedRangeWeapon.itemName);
            switch (PlayerStats.instance.equipedRangeWeapon.itemName)
            {
                case "Escopeta":
                    if (Inventory.instance.shotgunAmmo > 0)
                        Inventory.instance.hasAmmo = true;
                    else Inventory.instance.hasAmmo = false;
                    amountOfAmmo.text = Inventory.instance.shotgunAmmo.ToString();                    
                    break;
                case "Subfusil":
                    if (Inventory.instance.SMGAmmo > 0)
                        Inventory.instance.hasAmmo = true;
                    else Inventory.instance.hasAmmo = false;
                    amountOfAmmo.text = Inventory.instance.SMGAmmo.ToString();
                    break;
                case "Pistola":
                    if (Inventory.instance.pistolAmmo > 0)
                        Inventory.instance.hasAmmo = true;
                    else Inventory.instance.hasAmmo = false;
                    amountOfAmmo.text = Inventory.instance.pistolAmmo.ToString();
                    break;
            }
        }

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

    public void StartBattle(GameObject enemy, string enemiesToSpawn, bool isDinniesBAttle, bool isRandomBattle, bool isBossBattle)
    {
        Debug.Log("Batalla!");
        randomBattle = isRandomBattle;
        dinniesBattle = isDinniesBAttle;
        bossBattle = isBossBattle;

        if (randomBattle)
        {
            if (Time.time >= lastRandomBattle + inmunityTime)
                randomCombat = enemy.GetComponent<RandomBattle>();
            else
            {
                Debug.Log("Combate evitado");
                return;
            }
        }
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

        if (i == 0 && player.equipedRangeWeapon != null)
        {
            activeCharacters[i].equipedRangeWeapon = player.equipedRangeWeapon;
            Debug.Log(player.equipedRangeWeapon.itemName + " equipada");
        }
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
        MusicManager.instance.audioSource.Stop();
        combatSong.clip = MusicManager.instance.songs[4];
        MusicManager.instance.audioSource.Play();
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

        int ammoRewards = UnityEngine.Random.Range(3, 5);        

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
                Inventory.instance.pistolAmmo += ammoRewards;
                StartCoroutine(rewardsTexts.ShowAmmoRewards(ammoRewards.ToString()));
                ExportPlayerStats(0);
                if (!randomBattle)   Destroy(enemyGO);
                Debug.Log("Victoria!");
                  if (randomBattle || dinniesBattle)
                    OnBattleEnd?.Invoke(this, EventArgs.Empty);
                if(bossBattle)
                    StartCoroutine(rewardsTexts.ShowLifeRestored());
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
        StartCoroutine(Shake(activeCharacters[0].GetComponent<Rigidbody2D>()));
        List<int> players = new List<int>();

        for (int n = 0; n < activeCharacters.Count; n++)
        {
            if (activeCharacters[n].IsPlayer() && activeCharacters[n].currentHP > 0)
            {
                players.Add(n);
            }
        }
        int selectedPlayerToAttack = players[UnityEngine.Random.Range(0, players.Count)];

        if (activeCharacters[currentTurn].attacksAvailable.Length == 3)
        {
            int n = UnityEngine.Random.Range(1, 10);
            Debug.Log(n);
            if (n == 9) Heal();
            else if (n >= 5) DealRangeDamageToCharacters(selectedPlayerToAttack);
            else DealMeleeDamageToCharacters(selectedPlayerToAttack);
        }
        if (activeCharacters[currentTurn].attacksAvailable.Length == 2)
        {
            int i = UnityEngine.Random.Range(1, 10);
            if (i <= 5) DealRangeDamageToCharacters(selectedPlayerToAttack);
            else DealMeleeDamageToCharacters(selectedPlayerToAttack);
        }
        if (activeCharacters[currentTurn].attacksAvailable.Length == 1)
        {
            if (activeCharacters[currentTurn].attacksAvailable[0] == "Ataque melee") DealMeleeDamageToCharacters(selectedPlayerToAttack);
            else if (activeCharacters[currentTurn].attacksAvailable[0] == "Ataque rango") DealRangeDamageToCharacters(selectedPlayerToAttack);
        }

        UpdatePlayerStats();
    }

    public void PlayerRangeAttack()
    {
        StartCoroutine(Shake(activeCharacters[1].GetComponent<Rigidbody2D>()));
        Debug.Log("AtaqueRango");
        DealRangeDamageToCharacters(1);
        switch (activeCharacters[0].equipedRangeWeapon.itemName)
        {
            case "Pistola":
                Inventory.instance.pistolAmmo--;
                break;
            case "Subfusil":
                Inventory.instance.SMGAmmo--;
                break;
            case "Escopeta":
                Inventory.instance.shotgunAmmo--;
                break;
            default:
                Debug.Log("No weapon equiped");
                break;
        }
        AudioManager.instance.selectRangeAttackSFX(activeCharacters[0].equipedRangeWeapon);

        NextTurn();
    }

    public void PlayerMeleeAttack()
    {
        StartCoroutine(Shake(activeCharacters[1].GetComponent<Rigidbody2D>()));
        Debug.Log("AtaqueMelee");
        DealMeleeDamageToCharacters(1);

        if (activeCharacters[0].equipedRangeWeapon == null) AudioManager.instance.SelectMeleeAttackSFX(null);
        else AudioManager.instance.SelectMeleeAttackSFX(activeCharacters[0].equipedMeleeWeapon);

        NextTurn();
    }

    private void DealRangeDamageToCharacters(int selectedCharacterToAttack)
    {
        float attackPower = activeCharacters[currentTurn].dexterity + activeCharacters[currentTurn].rangeWeaponDamage;
        float defenceAmount = activeCharacters[selectedCharacterToAttack].defence;
        float damageAmount = (attackPower - defenceAmount) * UnityEngine.Random.Range(0.8f, 1.2f);

        int damageToGive = (int)damageAmount;
        if (damageToGive <= 0)
            damageToGive = 0;

        //iguala el valor del da�o a critico si es necesario
        damageToGive = CalculateCritical(damageToGive);
        Debug.Log(activeCharacters[currentTurn].characterName + " usa ataque a rango y causa " + (int)damageAmount + " de dano a " + activeCharacters[selectedCharacterToAttack].characterName);
        StartCoroutine(UpdateLog(activeCharacters[currentTurn].characterName + " usa ataque a rango y causa " + (int)damageAmount + " de dano a " + activeCharacters[selectedCharacterToAttack].characterName));

        StartCoroutine(ShowEffect(damageToGive, false));
        activeCharacters[selectedCharacterToAttack].TakeHPDamage(damageToGive);
    }

    private void DealMeleeDamageToCharacters(int selectedCharacterToAttack)
    {
        float attackPower = activeCharacters[currentTurn].strength + activeCharacters[currentTurn].meleeWeaponDamage;
        float defenceAmount = activeCharacters[selectedCharacterToAttack].defence;
        float damageAmount = (attackPower - defenceAmount) * UnityEngine.Random.Range(0.8f, 1.2f);

        int damageToGive = (int)damageAmount;
        if (damageToGive <= 0)
            damageToGive = 0;
        Debug.Log(activeCharacters[currentTurn].characterName + " usa ataque melee y causa " + (int)damageAmount + " de dano a " + activeCharacters[selectedCharacterToAttack].characterName);
        StartCoroutine(UpdateLog(activeCharacters[currentTurn].characterName + " usa ataque melee y causa " + (int)damageAmount + " de dano a " + activeCharacters[selectedCharacterToAttack].characterName));

        StartCoroutine(ShowEffect(damageToGive, false));
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

    private void Heal()
    {
        activeCharacters[currentTurn].AddHP(50);
        StartCoroutine(UpdateLog(activeCharacters[currentTurn].name + " se cura 50 puntos de vida."));
        StartCoroutine(ShowEffect(50, true));
        Debug.Log("Heal");
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
                    activeCharacters[i].equipedRangeWeapon = player.equipedRangeWeapon;
                    activeCharacters[i].equipedMeleeWeapon = player.equipedMeleeWeapon;
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
            Debug.Log("Huir");
            NextTurn();
            //waitingForTurn = true;
            StartCoroutine(ScapingTime());
            if(randomBattle || dinniesBattle)
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

    public void UseItemButton(int selectedPlayer)
    {
        activeCharacters[selectedPlayer].UseItemInBattle(selectedItem);
        Inventory.instance.RemoveItem(selectedItem);
        StartCoroutine(UpdateLog(activeCharacters[currentTurn].characterName + " usa " + selectedItem.name + " y se cura " + selectedItem.amountOfAffect + " puntos de vida."));
        StartCoroutine(ShowEffect(selectedItem.amountOfAffect, true));
        UpdatePlayerStats();
        UpdateItemsInInventory();
        itemsToUseMenu.SetActive(false);
        NextTurn();
    }

    IEnumerator ShowEffect(int damage, bool healing)
    {
        if(!healing)
        {
            damageDealt.color = Color.red;
            damageReceived.color = Color.red;
            if (currentTurn == 0)
            {
                damageDealt.text = "-" + damage.ToString();
                yield return new WaitForSeconds(5f);
                damageDealt.text = string.Empty;
            }
            else if (currentTurn == 1)
            {
                damageReceived.text = "-" + damage.ToString();
                yield return new WaitForSeconds(5f);
                damageReceived.text = string.Empty;
            }
        }
        else
        {
            damageDealt.color = Color.green;
            damageReceived.color = Color.green;
            if (currentTurn == 1)
            {
                damageDealt.text = "+" + damage.ToString();
                yield return new WaitForSeconds(5f);
                damageDealt.text = string.Empty;
            }
            else if (currentTurn == 0)
            {
                damageReceived.text = "+" + damage.ToString();
                yield return new WaitForSeconds(5f);
                damageReceived.text = string.Empty;
            }
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
        if (!randomBattle) enemyCollider.enabled = true;
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
        if (randomBattle)
        {
            Debug.Log(lastRandomBattle);
            lastRandomBattle = Time.time;
            StartCoroutine(randomCombat.Inmunity());
        }        
        combatSong.Stop();
        combatSong.clip = MusicManager.instance.activeClip;
        combatSong.Play();
    }

    public IEnumerator Shake(Rigidbody2D rb)
    {
        Vector2 originalPos = rb.position;
        int directionX;
        int directionY;

        for (int i = 0; i < 5; i++)
        {
            directionY = UnityEngine.Random.Range(-1, 2);
            directionX = UnityEngine.Random.Range(-1, 2);

            rb.velocity = new Vector2(directionX, directionY) * 10;
            yield return new WaitForSeconds(0.05f);
            rb.velocity = Vector2.zero;
            rb.position = originalPos;
        }
    }
}
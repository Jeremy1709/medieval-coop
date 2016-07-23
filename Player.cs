using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {

    public PlayerController playerController;

    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }//Makes sure only player class, and classes that derive from that can change value
    }

    [SerializeField]
    private int maxBlood = 100;
    [SerializeField]
    private int maxStamina = 100;
    [SerializeField]
    private int maxMana = 100;
    [SerializeField]
    private int maxSanity = 100;

    //STATS

    [SyncVar]//Every time value changes, it will be pushed out to all the clients
    private int currentHealth;
    [SyncVar]
    private int currentStamina;
    [SyncVar]
    private int currentMana;
    [SyncVar]
    private int currentSanity;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    public void Setup() //called whenever playersetup is ready,
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    /*
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(99999);
        }
    }
    */

    [ClientRpc]//To mark as an RPC call, it will make sure to do it to all computers connected to networks
    public void RpcTakeDamage(float _amountInFloat)
    {
        int _amount = (int)_amountInFloat;

        if (isDead)
            return;

        currentHealth -= _amount;     
        Debug.Log(transform.name + " now has "+currentHealth + " health.");
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        sendToUI();
    }

    private void Die()
    {
        isDead = true;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;

        Debug.Log(transform.name + " is DEAD!");

        //Call RESPAWN METHOD
        StartCoroutine(Respawn());

    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        SetDefaults();
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition(); //singleton is instance of networkmanager in scene
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;

        Debug.Log(transform.name + " respawned.");
    }

    public void SetDefaults()
    {
        isDead = false;

        currentHealth = maxBlood;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];

        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;

    }

    public void sendToUI()
    {
        playerController.sendVitals(currentHealth, currentStamina, currentMana, currentSanity);
    }
}
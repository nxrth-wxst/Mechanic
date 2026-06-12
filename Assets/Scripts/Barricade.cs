using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Barricade : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100f;
    private float maxHealth = 100f;
    [SerializeField] private float emyCooldown;
    [SerializeField] private float timeBetweenPlanks;
    private float firstHitTimer;

    [SerializeField] private GameObject[] Boards;

    [SerializeField] private bool canEnter;
    private bool plrNearBarricade;
    private bool interacting;
    private bool allowRepair;
    private bool startRepair;
    [SerializeField] private bool emyNearBarricade;
    private bool firstHit;

    // Track all enemies near barricade instead of just one
    private List<EnemyAI> nearbyEnemies = new List<EnemyAI>();
    private HashSet<EnemyAI> triggeredEnemies = new HashSet<EnemyAI>();

    public event Action OnDamaged;
    public event EventHandler<BarricadeEventArgs> OnDestroyed;
    public event EventHandler<BarricadeEventArgs> OnRepaired;

    [SerializeField] private Transform navMeshTarget;
    

    public Transform getNavmeshTarget() => navMeshTarget;
    [SerializeField] private GameObject promptUI;
    [SerializeField] private TextMeshProUGUI promptText;

    public static Barricade Instance { get; private set; }
    private Controls controls;

    private void Awake()
    {
        currentHealth = maxHealth;
        canEnter = false;
        controls = new Controls();
        Instance = this;
        promptUI.SetActive(false);
    }

    private void AllowEntry()
    {
        if (currentHealth == 0)
        {
            foreach (EnemyAI enemy in nearbyEnemies)
            {
                
                if (enemy != null && !triggeredEnemies.Contains(enemy))
                {
                    triggeredEnemies.Add(enemy);
                    enemy.setPassthru(true);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (enemy != null && !nearbyEnemies.Contains(enemy))
                nearbyEnemies.Add(enemy);

            emyNearBarricade = nearbyEnemies.Count > 0;
            AllowEntry();
        }
        else
        {
            emyNearBarricade = false;
        }
       
        if (other.CompareTag("Player"))
        {
            plrNearBarricade = true;
           
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (enemy != null)
                nearbyEnemies.Remove(enemy);

            emyNearBarricade = nearbyEnemies.Count > 0;
        }

        if (other.CompareTag("Player"))
            plrNearBarricade = false;
    }

    public void NotifyEnemyDead()
    {
        emyNearBarricade = nearbyEnemies.Count > 0;
    }

    private void Update()
    {
        timeBetweenPlanks = Mathf.Clamp(timeBetweenPlanks, 0, 0.75f);
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        emyCooldown = Mathf.Clamp(emyCooldown, 0, 0.25f);

        if (!allowRepair)
            timeBetweenPlanks = 0.75f;

        if (!emyNearBarricade)
        {
            emyCooldown = 0.25f;
            firstHitTimer = 0f;
        }

        if (plrNearBarricade && !emyNearBarricade)
        {
            if (currentHealth < 100f)
            {
            promptUI.SetActive(true);
            }
            controls.Enable();
        }
        else
        {
            allowRepair = false;
            controls.Disable();
            promptUI.SetActive(false);
        }

        if (interacting)
        {
            if (currentHealth < 100)
            {
                allowRepair = true;
                StartToRepair();
            }
        }

        if (emyNearBarricade)
        {
            firstHitTimer += 1;
            if (firstHitTimer == 1)
                doDamage();
        }

        if (emyNearBarricade && emyCooldown == 0f)
            doDamage();

        EmyCooldown();
        UpdateBoards();
    }

    private void OnEnable()
    {
        controls.Player.Interaction.started += OnInteractionStarted;
        controls.Player.Interaction.canceled += OnInteractionCanceled;
    }

    private void OnDisable()
    {
        controls.Player.Interaction.started -= OnInteractionStarted;
        controls.Player.Interaction.canceled -= OnInteractionCanceled;
    }

    private void OnInteractionStarted(InputAction.CallbackContext context)
    {
        interacting = true;
    }

    private void OnInteractionCanceled(InputAction.CallbackContext context)
    {
        timeBetweenPlanks = 0.75f;
        startRepair = false;
        interacting = false;
    }

    private void StartToRepair()
    {
        timeBetweenPlanks -= 0.5f * Time.deltaTime;
        if (timeBetweenPlanks <= 0)
        {
            var args = new BarricadeEventArgs
            {
                CurrentHealth = currentHealth,
                MaxHealth = maxHealth,
            };

            OnRepaired?.Invoke(this, args);
            timeBetweenPlanks += 0.75f;
            currentHealth += 20f;
            startRepair = true;

            
            triggeredEnemies.Clear();
        }
    }

    private void UpdateBoards()
    {
        if (currentHealth == 0)
        {
            canEnter = true;
            SetActiveBoards(0);
        }
        else if (currentHealth <= 20f) { SetActiveBoards(1); canEnter = false; }
        else if (currentHealth <= 40f) { SetActiveBoards(2); canEnter = false; }
        else if (currentHealth <= 60f) { SetActiveBoards(3); canEnter = false; }
        else if (currentHealth <= 80f) { SetActiveBoards(4); canEnter = false; }
        else if (currentHealth <= 100f) { SetActiveBoards(5); canEnter = false; }
    }

    private void SetActiveBoards(int count)
    {
        for (int i = 0; i < Boards.Length; i++)
            Boards[i].SetActive(i < count);
    }

    private void EmyCooldown()
    {
        if (emyNearBarricade && !canEnter)
            emyCooldown -= 0.070f * Time.deltaTime;
    }

    private void doDamage()
    {
        currentHealth -= 10f;
        emyCooldown += 0.35f;
        OnDamaged?.Invoke();
    }

    public bool EmyNearBarricade
    {
        get { return emyNearBarricade; }
        private set { emyNearBarricade = value; }
    }

    public float EmyCoolDown
    {
        get { return emyCooldown; }
        private set { emyCooldown = value; }
    }
}
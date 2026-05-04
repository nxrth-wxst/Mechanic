using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.GPUSort;

public class Barricade : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100f;
    private float maxHealth = 100f;
    [SerializeField] private float emyCooldown;
    [SerializeField] private float timeBetweenPlanks;
    [SerializeField] private GameObject[] Boards;
    private bool canEnter;
    private bool plrNearBarricade;
    private bool interacting;
    private bool allowRepair;
    private bool startRepair;
    [SerializeField] private bool emyNearBarricade;
   
    
    public event EventHandler<BarricadeEventArgs> OnDamaged;
    public event EventHandler<BarricadeEventArgs> OnDestroyed;
    public event EventHandler<BarricadeEventArgs> OnRepaired;

    [SerializeField] private Transform navMeshTarget;
    public Transform getNavmeshTarget() => navMeshTarget;
    public static Barricade Instance { get; private set; }
    private Controls controls;
    private void Awake()
    {
        currentHealth = maxHealth;
        canEnter = false;
        controls = new Controls();
        Instance = this;
        
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
             plrNearBarricade = true;

            
        }

     

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            plrNearBarricade = false;
        }
       
    }


    private void OnTriggerStay(Collider other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();
        emyNearBarricade = enemy != null;
    }


    private void Update()
    {
        timeBetweenPlanks = Mathf.Clamp(timeBetweenPlanks, 0, 0.75f);
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        emyCooldown = Mathf.Clamp(emyCooldown, 0, 0.35f);
      
        if (!allowRepair)
        {
            timeBetweenPlanks = 0.75f;
        }

        if (!emyNearBarricade)
        {
            emyCooldown = 0.35f;
        }
        
        if (plrNearBarricade && !emyNearBarricade)
        {
            controls.Enable();
        }
        else
        {
            allowRepair = false;
            controls.Disable();
         }
    
        if (interacting)
        {
            if (currentHealth < 100)
            {
                
                  allowRepair = true;
                  StartToRepair();
                
            }
        }

        if (emyNearBarricade && emyCooldown == 0f)
        {
            doDamage();
        }

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
        }
    }

    private void UpdateBoards()
    {
       
        if (currentHealth == 0)
        {
            SetActiveBoards(0);
        }
        else if (currentHealth <= 20f)
        {
            SetActiveBoards(1);
        }
        else if (currentHealth <= 40f)
        {
            SetActiveBoards(2);
        }
        else if (currentHealth <= 60f)
        {
            SetActiveBoards(3);
        }
        else if (currentHealth <= 80f)
        {
            SetActiveBoards(4);
        }
        else if (currentHealth <= 100f)
        {
            SetActiveBoards(5);
            
        }
    }

    private void SetActiveBoards(int count)
    {
        for (int i = 0; i < Boards.Length; i++)
        {
            Boards[i].SetActive(i < count);
        }
    }

    private void EmyCooldown()
    {
        if (emyNearBarricade)
        {
            emyCooldown -= 0.070f * Time.deltaTime;
        }
    }



    private void doDamage()
    {
        currentHealth -= 10f;
        emyCooldown += 0.35f;
    }

    public bool EmyNearBarricade
    {
        get { return emyNearBarricade; }
        private set { emyNearBarricade = value; }   
    }



}




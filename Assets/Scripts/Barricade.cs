using System;
using UnityEngine;

public class Barricade : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] planks;
    [SerializeField] private float repairHoldTime = 1.5f;

    
    public event EventHandler<PlankEventArgs> OnPlankRemoved;
    public event EventHandler<PlankEventArgs> OnPlankRepaired;
    public event EventHandler<BarricadeDestroyedEventArgs> OnBarricadeDestroyed;
    public event EventHandler<RepairEventArgs> OnRepairProgress;
    public event EventHandler<RepairEventArgs> OnRepairStopped;

    private int currentPlanks;
    private float repairTimer = 0f;
    private bool isRepairing = false;

    void Start()
    {
        currentPlanks = planks.Length;
        RefreshPlanks();
    }


    public void OnInteractEnter() { }

    public void OnInteractExit()
    {
        repairTimer = 0f;

        if (isRepairing)
        {
            isRepairing = false;
            OnRepairStopped?.Invoke(this, new RepairEventArgs(gameObject, 0f));
        }
    }

    public void OnInteractHeld(float deltaTime)
    {
        if (currentPlanks >= planks.Length) return;

        repairTimer += deltaTime;
        isRepairing = true;

       
        OnRepairProgress?.Invoke(this, new RepairEventArgs(
            gameObject,
            repairTimer / repairHoldTime
        ));

        if (repairTimer >= repairHoldTime)
        {
            AddPlank();
            repairTimer = 0f;
        }
    }

  
    public void OnInteractReleased()
    {
        if (!isRepairing) return;

        isRepairing = false;
        repairTimer = 0f;
        OnRepairStopped?.Invoke(this, new RepairEventArgs(gameObject, 0f));
    }


    public string GetPromptText()
        => currentPlanks < planks.Length ? "Hold F to repair" : string.Empty;

  
    public void RemovePlank()
    {
        if (currentPlanks <= 0) return;

        currentPlanks--;
        RefreshPlanks();

        OnPlankRemoved?.Invoke(this, new PlankEventArgs(
            currentPlanks, planks.Length, gameObject
        ));

        if (currentPlanks <= 0)
        {
            OnBarricadeDestroyed?.Invoke(this, new BarricadeDestroyedEventArgs(
                gameObject, transform.position
            ));
        }
    }

    public bool IsDestroyed() => currentPlanks <= 0;


    private void AddPlank()
    {
        if (currentPlanks >= planks.Length) return;

        currentPlanks++;
        RefreshPlanks();

        OnPlankRepaired?.Invoke(this, new PlankEventArgs(
            currentPlanks, planks.Length, gameObject
        ));
    }

    private void RefreshPlanks()
    {
        for (int i = 0; i < planks.Length; i++)
            planks[i].SetActive(i < currentPlanks);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerInteractor>(out var interactor))
        interactor.SetInteractable(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerInteractor>(out var interactor))
            interactor.ClearInteractable(this);
    }
}
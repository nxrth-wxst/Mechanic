using UnityEngine;
using System;
using System.Collections.Generic;
[RequireComponent(typeof(Collider))]
public class Barricade : MonoBehaviour
{

    public static event EventHandler<BoardDamagedEventArgs> OnBoardDamaged;
    public static event EventHandler<BoardRepairedEventArgs> OnBoardRepaired;
    public static event EventHandler<BarricadeEventArgs> OnBoardBroken;
    public static event EventHandler<BarricadeBrokenEventArgs> OnBarricadeBroken;

    [Header("Board Config")]
    [SerializeField] private int boardCount = 4;
    [SerializeField] private float boardMaxHealth = 100f;

    [Header("Visual Boards")]
    [Tooltip("Assign the GameObject for each board plank in order")]
    [SerializeField] private List<GameObject> boardVisuals = new();

    private List<Board> _boards = new();
    private bool _isBroken;

    public bool IsBroken => _isBroken;
    public int BoardCount => _boards.Count;
    public int IntactBoards => _boards.FindAll(b => !b.IsBroken).Count;


    private void Awake()
    {
        InitialiseBoards();
    }

    private void InitialiseBoards()
    {
        _boards.Clear();
        for (int i = 0; i < boardCount; i++)
            _boards.Add(new Board(boardMaxHealth));
        _isBroken = false;
    }


    public void DamageBoard(float damage)
    {
        if (_isBroken) return;

        int index = GetFirstIntactBoardIndex();
        if (index < 0) return;

        Board board = _boards[index];
        float before = board.CurrentHealth;
        float actual = board.TakeDamage(damage);

        if (actual <= 0f) return;

        
        OnBoardDamaged?.Invoke(this,
            new BoardDamagedEventArgs(index, this, before, board.CurrentHealth));

        
        if (board.IsBroken)
        {
            SetBoardVisual(index, false);
            OnBoardBroken?.Invoke(this, new BarricadeEventArgs(index, this));

           
            if (IntactBoards == 0)
            {
                _isBroken = true;
                GetComponent<Collider>().enabled = false; 
                OnBarricadeBroken?.Invoke(this,
                    new BarricadeBrokenEventArgs(index, this, transform.position));
            }
        }
    }


    public bool RepairBoard(int boardIndex)
    {
        if (boardIndex < 0 || boardIndex >= _boards.Count) return false;

        Board board = _boards[boardIndex];
        if (!board.IsBroken) return false;

        float restored = board.Repair(board.MaxHealth); 
        bool fullRepair = restored > 0f;

        if (fullRepair)
        {
            _isBroken = false;
            GetComponent<Collider>().enabled = true;
            SetBoardVisual(boardIndex, true);

            OnBoardRepaired?.Invoke(this,
                new BoardRepairedEventArgs(boardIndex, this, restored, true));
        }

        return fullRepair;
    }

    public void ResetBarricade()
    {
        InitialiseBoards();
        for (int i = 0; i < boardVisuals.Count; i++)
            SetBoardVisual(i, true);
        GetComponent<Collider>().enabled = true;
    }

    private int GetFirstIntactBoardIndex()
    {
        for (int i = 0; i < _boards.Count; i++)
            if (!_boards[i].IsBroken) return i;
        return -1;
    }

    private void SetBoardVisual(int index, bool visible)
    {
        if (index < boardVisuals.Count && boardVisuals[index] != null)
            boardVisuals[index].SetActive(visible);
    }
}


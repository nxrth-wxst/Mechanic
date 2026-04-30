using UnityEngine;
using System;
public class BarricadeEventArgs : EventArgs
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public string AttackerTag { get; set; }

}


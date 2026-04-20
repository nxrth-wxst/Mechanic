using UnityEngine;
using System;
public class BarricadeEventArgs : EventArgs
{
    public int boardIndex { get; }
    public Barricade Barricade { get; }
}

using System.Collections.Generic;
using BasicArchitecturalStructure;
using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public struct SendFingerState : IEvent
    {
        public bool isPressing;
    }

    public struct EdgePlaced : IEvent
    {
        public bool IsVertical { get; set; }
        public Vector2Int GridPosition { get; set; }
    }

    public struct CellFilled : IEvent
    {
        public CellManager ownDatas;
    }

    public struct ShapePlaced : IEvent
    {
        public int shapePieceCount;
    }

    public struct ShapeSelected : IEvent
    {
        public ShapeManager shapeManager;
    } 
    public struct ShapeDropped : IEvent
    {
    }

    public struct HitCombo : IEvent
    {
        public int comboCount;
    }
}
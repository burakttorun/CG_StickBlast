using System;
using BasicArchitecturalStructure;
using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class EdgeManager : MonoBehaviour
    {
        public bool IsFull { get; private set; } = false;
        public bool IsVertical { get; set; }
        public Vector2Int GridPosition { get; set; }

        private SpriteRenderer _ownSpriteRenderer;

        private void Awake()
        {
            _ownSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void HoverEdge()
        {
            _ownSpriteRenderer.color = Color.cyan;
        }

        public void ResetHover()
        {
            _ownSpriteRenderer.color = Color.white;
        }

        public void MarkAsFull()
        {
            IsFull = true;
            _ownSpriteRenderer.color = Color.blue;
            EventBus<EdgePlaced>.Publish(new EdgePlaced() { GridPosition = GridPosition, IsVertical = IsVertical });
        }

        public void MarkAsEmpty()
        {
            IsFull = false;
        }
    }
}
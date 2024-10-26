using System;
using System.Collections.Generic;
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
        [SerializeField] private List<Color> _colors;

        private void Awake()
        {
            _ownSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void HoverEdge()
        {
            _ownSpriteRenderer.color = _colors[1];
        }

        public void ResetHover()
        {
            _ownSpriteRenderer.color = _colors[0];
        }

        public void MarkAsFull()
        {
            IsFull = true;
            _ownSpriteRenderer.color = _colors[2];

            EventBus<EdgePlaced>.Publish(new EdgePlaced() { GridPosition = GridPosition, IsVertical = IsVertical });
            if (IsVertical)
            {
                GameManager.Instance.GridDataManager.SetVerticalEdge(GridPosition.x, GridPosition.y, true);
            }
            else
            {
                GameManager.Instance.GridDataManager.SetHorizontalEdge(GridPosition.x, GridPosition.y, true);
            }
        }

        public void MarkAsEmpty()
        {
            IsFull = false;
            _ownSpriteRenderer.color = _colors[0];
        }
    }
}
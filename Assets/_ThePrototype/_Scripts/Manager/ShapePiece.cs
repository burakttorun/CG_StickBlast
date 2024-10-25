using System;
using System.Linq;
using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class ShapePiece : MonoBehaviour
    {
        [field: SerializeField] public bool IsVertical { get; set; }

        public EdgeManager OverlappedEdge
        {
            get => _overlappedEdge;
            private set => _overlappedEdge = value;
        }

        private EdgeManager _overlappedEdge;
        public bool CanPlaceable { get; set; }

        private bool _triggerOnce;
        private GameObject _currentEdge;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_currentEdge != null && _currentEdge != other.gameObject)
            {
                return;
            }

            _currentEdge = other.gameObject;
            CheckPlaceable(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_currentEdge != null && _currentEdge != other.gameObject)
            {
                return;
            }

            _currentEdge = other.gameObject;
            CheckPlaceable(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject == _currentEdge)
            {
                _currentEdge = null;
            }

            ResetInfos();
        }

        private void CheckPlaceable(Collider2D other)
        {
            if (other.TryGetComponent<EdgeManager>(out _overlappedEdge))
            {
                if (_overlappedEdge != null && _overlappedEdge.IsVertical == IsVertical && !_overlappedEdge.IsFull)
                {
                    CanPlaceable = true;
                }
                else
                {
                    CanPlaceable = false;
                }
            }
        }

        public void ResetInfos()
        {
            CanPlaceable = false;
            OverlappedEdge = null;
        }

    }
}
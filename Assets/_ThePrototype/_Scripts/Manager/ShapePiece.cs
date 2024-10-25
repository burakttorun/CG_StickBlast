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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_triggerOnce)
            {
                CheckPlaceable(other);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            CheckPlaceable(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ResetInfos();
            _triggerOnce = false;
        }

        private void CheckPlaceable(Collider2D other)
        {
            if (other.TryGetComponent<EdgeManager>(out _overlappedEdge))
            {
                _triggerOnce = true;
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

        void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 0.2f);
        }
    }
}
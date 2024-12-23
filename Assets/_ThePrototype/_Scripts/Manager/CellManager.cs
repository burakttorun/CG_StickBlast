using System;
using System.Collections.Generic;
using BasicArchitecturalStructure;
using Unity.VisualScripting;
using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class CellManager : MonoBehaviour
    {
        public List<EdgeManager> edges = new List<EdgeManager>();
        public bool IsFilled { get; private set; }
        public int Row { get; set; }
        public int Column { get; set; }
        private EventBinding<EdgePlaced> _edgePlacedEventBinding;
        private Transform _ownModelTransform;

        private void Awake()
        {
            _ownModelTransform = gameObject.transform.GetChild(0);
            _ownModelTransform.gameObject.SetActive(false);
            _edgePlacedEventBinding = new EventBinding<EdgePlaced>(CheckAndPaintCell);
        }

        private void OnEnable()
        {
            EventBus<EdgePlaced>.Subscribe(_edgePlacedEventBinding);
        }

        private void OnDisable()
        {
            EventBus<EdgePlaced>.Unsubscribe(_edgePlacedEventBinding);
        }

        private void Start()
        {
            FindSurroundingEdges();
        }

        private void FindSurroundingEdges()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);

            foreach (var hitCollider in hitColliders)
            {
                var edgeManager = hitCollider.GetComponent<EdgeManager>();
                if (edgeManager != null && !edges.Contains(edgeManager))
                {
                    edges.Add(edgeManager);
                }
            }
        }

        public void CheckAndPaintCell()
        {
            var isFilled = edges.TrueForAll(edge => edge.IsFull);
            if (!IsFilled && isFilled)
            {
                PaintCell();
            }
        }

        private void PaintCell()
        {
            _ownModelTransform.gameObject.SetActive(true);
            IsFilled = true;
            EventBus<CellFilled>.Publish(new CellFilled() { ownDatas = this });
        }

        public void ResetCell()
        {
            _ownModelTransform.gameObject.SetActive(false);
            IsFilled = false;
            edges.ForEach(x => x.MarkAsEmpty());
        }

        public void CleanPaint()
        {
            var isFilled = edges.TrueForAll(edge => edge.IsFull);
            if (IsFilled && !isFilled)
            {
                _ownModelTransform.gameObject.SetActive(false);
                IsFilled = false;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        }
    }
}
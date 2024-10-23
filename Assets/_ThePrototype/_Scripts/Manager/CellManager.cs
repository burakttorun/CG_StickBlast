using System.Collections.Generic;
using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class CellManager : MonoBehaviour
    {
        public List<EdgeManager> edges = new List<EdgeManager>();
        public bool IsFilled { get; private set; }

        private void Start()
        {
            FindSurroundingEdges();
        }

        private void FindSurroundingEdges()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 2f);

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
            IsFilled = edges.TrueForAll(edge => edge.IsFull);
            if (IsFilled)
            {
                PaintCell();
            }
        }

        private void PaintCell()
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 2f);
        }
    }
}
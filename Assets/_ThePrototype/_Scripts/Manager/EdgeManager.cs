using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class EdgeManager : MonoBehaviour
    {
        public bool IsFull { get; private set; } = false;

        public void MarkAsFull()
        {
            IsFull = true;
        }

        public void MarkAsEmpty()
        {
            IsFull = false;
        }
    }
}
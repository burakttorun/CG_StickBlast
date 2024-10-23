using BasicArchitecturalStructure;
using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public GridDataManager GridDataManager { get; private set; }
        [field: SerializeField] public int Rows { get; set; }
        [field: SerializeField] public int Columns { get; set; }

        protected override void Awake()
        {
            base.Awake();
            GridDataManager = new GridDataManager(Rows, Columns);
        }
    }
}
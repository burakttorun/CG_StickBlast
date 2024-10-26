using System;
using UnityEngine;
using System.Collections.Generic;
using BasicArchitecturalStructure;
using ThePrototype.Scripts.Utils;
using ThePrototype.Scripts.Managers;

namespace ThePrototype.Scripts.Management
{
    public class SelectionManager : BasicSingleton<SelectionManager>
    {
        [Header("References")] [field: SerializeField]
        public List<GameObject> shapesList;

        [field: SerializeField] public List<Transform> spawnPos;

        private ShuffleBag<GameObject> _shuffleBag;
        private int _movementCounter = 0;
        private EventBinding<ShapePlaced> _shapePlacedEventBinding;

        protected override void Awake()
        {
            base.Awake();
            _shapePlacedEventBinding = new EventBinding<ShapePlaced>(RespawnShapes);
            _shuffleBag = new ShuffleBag<GameObject>();
            _shuffleBag.AddRange(shapesList);
            SelectBatchOfShapes(3);
        }

        private void OnEnable()
        {
            EventBus<ShapePlaced>.Subscribe(_shapePlacedEventBinding);
        }

        private void OnDisable()
        {
            EventBus<ShapePlaced>.Unsubscribe(_shapePlacedEventBinding);
        }

        private void RespawnShapes()
        {
            _movementCounter++;
            if (_movementCounter == 3)
            {
                _movementCounter = 0;
                SelectBatchOfShapes(3);
            }
        }

        private void SelectBatchOfShapes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var shapeGameObject = _shuffleBag.GetNext();
                var shapeTransform = shapeGameObject.GetComponent<Transform>();
                shapeTransform.position = spawnPos[i].transform.position;
                shapeGameObject.GetComponent<DragAndDropManager>().SetOriginalPosition(spawnPos[i].transform);
                shapeTransform.parent = spawnPos[i].transform;
                shapeGameObject.SetActive(true);
            }
        }
    }
}
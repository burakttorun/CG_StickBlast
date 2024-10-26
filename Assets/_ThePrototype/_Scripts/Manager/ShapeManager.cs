using System;
using System.Collections.Generic;
using System.Linq;
using BasicArchitecturalStructure;
using Unity.VisualScripting;
using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class ShapeManager : MonoBehaviour
    {
        private ShapePiece[] _shapePieces;
        private bool _canHoverShape;
        private HashSet<EdgeManager> _hoverEdges;
        private EventBinding<SendFingerState> _fingerStateEventBinding;
        private bool _isPressing;

        private void Awake()
        {
            _fingerStateEventBinding = new EventBinding<SendFingerState>(SetPressingState);
            _fingerStateEventBinding.Add(FingerUpState);
        }


        private void OnEnable()
        {
            EventBus<SendFingerState>.Subscribe(_fingerStateEventBinding);
        }

        private void OnDisable()
        {
            EventBus<SendFingerState>.Unsubscribe(_fingerStateEventBinding);
        }

        private void Start()
        {
            _hoverEdges = new();
            _shapePieces = GetComponentsInChildren<ShapePiece>();
        }

        private void Update()
        {
            if (_isPressing)
            {
                CheckShapePiecesCanPlaceable();
            }

            if (_canHoverShape)
            {
                foreach (var hoverEdge in _hoverEdges)
                {
                    hoverEdge.HoverEdge();
                }
            }
            else
            {
                ClearHoverItemsInfos();
            }
        }

        private void SetPressingState(SendFingerState args)
        {
            _isPressing = args.isPressing;
            if (_isPressing)
            {
                EventBus<ShapeSelected>.Publish(new ShapeSelected() { shapeManager = this });
            }
        }

        private void ClearHoverItemsInfos()
        {
            foreach (var hoverEdge in _hoverEdges)
            {
                if (!hoverEdge.IsFull)
                {
                    hoverEdge.ResetHover();
                }
            }

            foreach (var shapePiece in _shapePieces)
            {
                shapePiece.ResetInfos();
            }

            _hoverEdges.Clear();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _canHoverShape = false;
        }

        private void CheckShapePiecesCanPlaceable()
        {
            _canHoverShape = _shapePieces.All(x => x.CanPlaceable);
            if (_canHoverShape)
            {
                for (int i = 0; i < _shapePieces.Length; i++)
                {
                    _hoverEdges.Add(_shapePieces[i].OverlappedEdge);
                }
            }
        }

        private void FingerUpState(SendFingerState args)
        {
            if (args.isPressing) return;

            if (_canHoverShape)
            {
                foreach (var hoverEdge in _hoverEdges)
                {
                    hoverEdge.MarkAsFull();
                }

                gameObject.SetActive(false);
                EventBus<ShapePlaced>.Publish(new ShapePlaced() { shapePieceCount = _shapePieces.Length });
            }
        }
    }
}
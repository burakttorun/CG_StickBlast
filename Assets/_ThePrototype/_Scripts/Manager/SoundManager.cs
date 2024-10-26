using System;
using BasicArchitecturalStructure;
using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _shapeSelectedClip;
        [SerializeField] private AudioClip _shapeDroppedClip;
        [SerializeField] private AudioClip _cellFilledClip;

        private EventBinding<CellFilled> _cellFilledEventBinding;
        private EventBinding<ShapeSelected> _shapeSelectedEventBinding;
        private EventBinding<ShapeDropped> _shapeDroppedEventBinding;


        private void Awake()
        {
            _cellFilledEventBinding = new EventBinding<CellFilled>(PlayFilledSound);
            _shapeSelectedEventBinding = new EventBinding<ShapeSelected>(PlaySelectedSound);
            _shapeDroppedEventBinding = new EventBinding<ShapeDropped>(PlayDroppedSound);
        }

        private void OnEnable()
        {
            EventBus<CellFilled>.Subscribe(_cellFilledEventBinding);
            EventBus<ShapeSelected>.Subscribe(_shapeSelectedEventBinding);
            EventBus<ShapeDropped>.Subscribe(_shapeDroppedEventBinding);
        }

        private void OnDisable()
        {
            EventBus<CellFilled>.Unsubscribe(_cellFilledEventBinding);
            EventBus<ShapeSelected>.Unsubscribe(_shapeSelectedEventBinding);
            EventBus<ShapeDropped>.Unsubscribe(_shapeDroppedEventBinding);
        }

        private void PlayDroppedSound()
        {
            _audioSource.PlayOneShot(_shapeDroppedClip);
        }

        private void PlaySelectedSound()
        {
            _audioSource.PlayOneShot(_shapeSelectedClip);
        }

        private void PlayFilledSound()
        {
            _audioSource.PlayOneShot(_cellFilledClip);
        }
    }
}
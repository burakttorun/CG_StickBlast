using System;
using System.Collections;
using System.Collections.Generic;
using BasicArchitecturalStructure;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace ThePrototype.Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private TextMeshProUGUI _scoreText;

        [SerializeField] private TextMeshProUGUI _cellFilledScoreText;
        [SerializeField] private CanvasFadeOutAnimation _canvasFadeOutAnimation;

        [Header("Settings")] [SerializeField] private int _shapePoint = 5;
        [SerializeField] private int _comboMultiplier = 10;

        private EventBinding<ShapePlaced> _shapePlacedEventBinding;
        private EventBinding<CellFilled> _cellFilledEventBinding;
        private int _score;
        private int _comboCount;
        private int _cellsFilledDuringShape;

        private void Awake()
        {
            _scoreText.text = _score.ToString();
            _shapePlacedEventBinding = new EventBinding<ShapePlaced>(ShapePlacedPoint);
            _cellFilledEventBinding = new EventBinding<CellFilled>(CellFilledPoint);
        }

        private void OnEnable()
        {
            EventBus<ShapePlaced>.Subscribe(_shapePlacedEventBinding);
            EventBus<CellFilled>.Subscribe(_cellFilledEventBinding);
        }

        private void OnDisable()
        {
            EventBus<ShapePlaced>.Unsubscribe(_shapePlacedEventBinding);
            EventBus<CellFilled>.Unsubscribe(_cellFilledEventBinding);
        }

        private void ShapePlacedPoint(ShapePlaced args)
        {
            _score += args.shapePieceCount * _shapePoint;
            _scoreText.text = _score.ToString();

            if (_cellsFilledDuringShape > 0)
            {
                _comboCount++;
                EventBus<HitCombo>.Publish(new HitCombo() { comboCount = _comboCount });
                int comboScore = _cellsFilledDuringShape * _comboMultiplier * _comboCount;
                _score += comboScore;

                _scoreText.text = _score.ToString();
                _cellFilledScoreText.text = $"+{comboScore}";
                _canvasFadeOutAnimation.gameObject.SetActive(true);
            }
            else
            {
                _comboCount = 0;
                _cellFilledScoreText.text = "";
            }

            _cellsFilledDuringShape = 0;
        }

        private void CellFilledPoint(CellFilled args)
        {
            _cellsFilledDuringShape++;
            _canvasFadeOutAnimation.initialPosition = args.ownDatas.gameObject.transform.position;
        }
    }
}
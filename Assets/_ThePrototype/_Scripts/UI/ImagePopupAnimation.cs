using System;
using System.Collections.Generic;
using BasicArchitecturalStructure;
using ThePrototype.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class ImagePopupAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> numberSprites;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float maxScale = 1.5f;
    [SerializeField] private Image _numberImage;
    private Vector3 _originalScale;
    private float _elapsedTime;
    private EventBinding<HitCombo> _hitComboEventBinding;

    void Awake()
    {
        _hitComboEventBinding = new EventBinding<HitCombo>(MadeCombo);
        _originalScale = Vector3.one *0.4f;
    }

    private void OnEnable()
    {
        EventBus<HitCombo>.Subscribe(_hitComboEventBinding);
    }

    private void OnDisable()
    {
        EventBus<HitCombo>.Unsubscribe(_hitComboEventBinding);
    }

    void MadeCombo(HitCombo args)
    {
        if (args.comboCount >= 2 && args.comboCount <= 9)
        {
            _numberImage.sprite = numberSprites[args.comboCount - 2];
            transform.localScale = Vector3.zero;
            _elapsedTime = 0f;
            StartCoroutine(PopupEffect());
        }
    }

    private System.Collections.IEnumerator PopupEffect()
    {
        while (_elapsedTime < duration)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / duration;

            if (t <= 0.5f)
            {
                float scaleUp = Mathf.Lerp(0, maxScale, t * 2);
                transform.localScale = _originalScale * scaleUp;
            }
            else
            {
                float scaleDown = Mathf.Lerp(maxScale, 0, (t - 0.5f) * 2);
                transform.localScale = _originalScale * scaleDown;
            }

            yield return null;
        }
    }
}
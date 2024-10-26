using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class VisualCellManager : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f;

        private Vector3 _targetScale;
        private float _elapsedTime;

        void Awake()
        {
            _targetScale = transform.localScale;
        }

        void OnEnable()
        {
            transform.localScale = Vector3.zero;
            _elapsedTime = 0f;


            StartCoroutine(ScaleUp());
        }

        private System.Collections.IEnumerator ScaleUp()
        {
            while (_elapsedTime < duration)
            {
                _elapsedTime += Time.deltaTime;


                transform.localScale = Vector3.Lerp(Vector3.zero, _targetScale, _elapsedTime / duration);


                yield return null;
            }


            transform.localScale = _targetScale;
        }
    }
}
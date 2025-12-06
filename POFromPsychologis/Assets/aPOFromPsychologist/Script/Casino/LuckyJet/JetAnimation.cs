using UnityEngine;

namespace DiplomGames
{
    public class JetAnimation : MonoBehaviour
    {
        [SerializeField] private float bobSpeed = 2f;
        [SerializeField] private float bobAmount = 0.1f;
        [SerializeField] private float rotationSpeed = 30f;
        [SerializeField] private float maxRotation = 15f;

        private Vector3 startPosition;
        private float timeOffset;

        void Start()
        {
            startPosition = transform.localPosition;
            timeOffset = Random.Range(0f, 2f * Mathf.PI);
        }

        void Update()
        {
            if (!GetComponentInParent<LuckyJetGame>())
                return;

            // Плавное покачивание вверх-вниз
            float bob = Mathf.Sin((Time.time + timeOffset) * bobSpeed) * bobAmount;
            transform.localPosition = startPosition + new Vector3(0, bob, 0);

            // Небольшой наклон при движении
            float rotation = Mathf.Sin(Time.time * rotationSpeed) * maxRotation;
            transform.localRotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}

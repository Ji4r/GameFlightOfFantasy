using System.Collections;
using UnityEngine;

namespace DiplomGames
{
    public class FirecrackerControllerParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] particleSystems;
        [SerializeField] private GameObject[] objects;

        private Coroutine[] particleCoroutine;

        private void Start()
        {
            SetEnabled(false);
            particleCoroutine = new Coroutine[particleSystems.Length];
        }

        /// <summary>
        /// Включает объекты на сцене, если isPlay == true то проиграется анимация после включения
        /// Если включен autoDisable == true то при исчезновении всех партиклов выполниться метод Disabled 
        /// </summary>
        /// <param name="isPlay"></param>
        public void Enabled(bool isPlay = false, bool autoDisable = false)
        {
            SetEnabled(true);

            if (isPlay)
                Play(autoDisable);
        }

        public void Disabled()
        {
            SetEnabled(false);
        }

        public void Play(bool autoDisable = false)
        {
            if (!CheckRanOutCoroutine())
                return;

            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Play();
                particleCoroutine[i] = StartCoroutine(CheckerAliveParticle(particleSystems[i], i, autoDisable));
            }
            SoundPlayer.instance.PlaySound(ListSound.Firecracker);
        }

        private IEnumerator CheckerAliveParticle(ParticleSystem system, int indexCoroutine, bool autoDisable = false)
        {
            yield return new WaitForSeconds(system.main.startLifetime.constantMax);

            if (autoDisable)
            {
                particleCoroutine[indexCoroutine] = null;

                if (CheckRanOutCoroutine())
                    Disabled();
            }
        }

        private bool CheckRanOutCoroutine()
        {
            foreach (var coroutine in particleCoroutine) 
            { 
                if (coroutine != null)
                {
                    return false;
                }
            }

            return true;
        }

        private void SetEnabled(bool isActive)
        {
            foreach (var obj in objects)
            {
                obj.SetActive(isActive);
            }
        }
    }
}

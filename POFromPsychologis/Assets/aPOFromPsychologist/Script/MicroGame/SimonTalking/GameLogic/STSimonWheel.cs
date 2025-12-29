using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class STSimonWheel : MonoBehaviour
    {
        [HideInInspector] public Transform parentColorSimon; // Родитель всех цветов

        [SerializeField] private STPresetColorAnimsWheel presetColorAnimsWheel;
        [SerializeField] private float darkenFactor = 0.5f;

        [Inject] private STColorValidator colorValidator;

        private STGenerateColorSubsequnce colorSubsequnce;
        private List<Image> listImage = new();
        private List<Color> originalColors = new();
        private List<Color> darkenedColor = new();
        private STAnimsColorWheel animsColorWheel;

        private void Start()
        {
            animsColorWheel = new STAnimsColorWheel(presetColorAnimsWheel);
        }

        public List<Color> GetAllColorWheel() 
        {
            if (listImage == null || listImage.Count == 0)
                GetAllImageWheel();

            List<Color> allColor = new();

            foreach (var image in listImage) 
            {
                allColor.Add(image.color);
            }

            return allColor;
        }

        private void GetAllImageWheel()
        {
            listImage.Clear();

            foreach (Transform t in parentColorSimon)
            {
                if (t.TryGetComponent<Image>(out var image))
                {
                    listImage.Add(image);
                    originalColors.Add(image.color);
                }
            }
        }

        public async Task StartSimon(Range range)
        {
            var listColor = GetAllColorWheel();
            colorSubsequnce = new();
            var sebsequnceColor = colorSubsequnce.GenerateSubsequnceColor(listColor, range);
            colorValidator.NewSubsequnce(sebsequnceColor);
            DarkenColorSimon();

            for (int i = 0; i < sebsequnceColor.Count; i++)
            {
                await animsColorWheel.WaitInterval();
                await animsColorWheel.StartFullAnims(listImage[i], originalColors[i], darkenedColor[i]);
            }
        }

        public void NextSimon(Range range)
        {
            RestoreColorSimon();
            var listColor = GetAllColorWheel();
            var sebsequnceColor = colorSubsequnce.GenerateSubsequnceColor(listColor, range);
            colorValidator.NewSubsequnce(sebsequnceColor);
            DarkenColorSimon();
        }

        public void ReplaySimon()
        {
            //colorValidator.Clea
            // Стирание истории и запуск повторно анимаций
        }

        private void DarkenColorSimon()
        {
            foreach (var image in listImage)
            {
                Color originalColor = image.color;
                Color darkenedColor = originalColor * darkenFactor;
                darkenedColor.a = originalColor.a;
                image.color = darkenedColor;
                this.darkenedColor.Add(darkenedColor);
            }
        }

        private void RestoreColorSimon()
        {
            for (int i = 0; i < listImage.Count; i++)
            {
                listImage[i].color = originalColors[i];
            }
        }

        public void Initialized(DIContainer parentContainer = null)
        {
            throw new System.NotImplementedException();
        }
    }
}

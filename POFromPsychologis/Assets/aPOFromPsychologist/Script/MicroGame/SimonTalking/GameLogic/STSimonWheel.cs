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
        private List<ImageColor> listImage = new();
        private STAnimsColorWheel animsColorWheel;

        private void Start()
        {
            animsColorWheel = new STAnimsColorWheel(presetColorAnimsWheel);
        }

        private void OnDisable()
        {
            animsColorWheel.Dispose();
        }

        public List<Color> GetAllColorWheel() 
        {
            if (listImage == null || listImage.Count == 0)
                GetAllImageWheel();

            List<Color> allColor = new();

            foreach (var image in listImage) 
            {
                allColor.Add(image.ImageSource.color);
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
                    listImage.Add(new ImageColor(image, image.color));
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


            List<ImageColor> sebsequnceImage = new List<ImageColor>();

            foreach (var color in sebsequnceColor) 
            {
                for (int i = 0; i < listImage.Count; i++)
                {
                    if (color == listImage[i].originalColors)
                        sebsequnceImage.Add(listImage[i]);
                }
            }

            for (int i = 0; i < sebsequnceImage.Count; i++)
            {
                await animsColorWheel.WaitInterval();
                await animsColorWheel.StartFullAnims(sebsequnceImage[i].ImageSource, sebsequnceImage[i].originalColors, sebsequnceImage[i].darkenedColor);
            }
        }

        public async Task NextSimon(Range range)
        {
            RestoreColorSimon();
            var listColor = GetAllColorWheel();
            var sebsequnceColor = colorSubsequnce.GenerateSubsequnceColor(listColor, range);
            colorValidator.NewSubsequnce(sebsequnceColor);
            DarkenColorSimon();

            List<ImageColor> sebsequnceImage = new List<ImageColor>();

            foreach (var color in sebsequnceColor)
            {
                for (int i = 0; i < listImage.Count; i++)
                {
                    if (color == listImage[i].originalColors)
                        sebsequnceImage.Add(listImage[i]);
                }
            }

            for (int i = 0; i < sebsequnceImage.Count; i++)
            {
                await animsColorWheel.WaitInterval();
                await animsColorWheel.StartFullAnims(sebsequnceImage[i].ImageSource, sebsequnceImage[i].originalColors, sebsequnceImage[i].darkenedColor);
            }
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
                Color originalColor = image.ImageSource.color;
                Color darkenedColor = originalColor * darkenFactor;
                darkenedColor.a = originalColor.a;
                image.ImageSource.color = darkenedColor;
                image.darkenedColor = darkenedColor;
            }
        }

        private void RestoreColorSimon()
        {
            for (int i = 0; i < listImage.Count; i++)
            {
                listImage[i].ImageSource.color = listImage[i].originalColors;
            }
        }

        public void Initialized(DIContainer parentContainer = null)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ImageColor
    {
        public Image ImageSource;
        public Color originalColors;
        public Color darkenedColor;

        public ImageColor(Image image, Color color)
        {
            ImageSource = image;
            originalColors = color;
        }

        public ImageColor(ImageColor imageColor)
        {
            ImageSource = imageColor.ImageSource;
            originalColors = imageColor.originalColors;
        }

        public ImageColor() {}
    }
}

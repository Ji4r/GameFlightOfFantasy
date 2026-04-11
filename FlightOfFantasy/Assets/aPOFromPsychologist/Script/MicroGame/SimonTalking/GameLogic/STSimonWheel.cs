using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class STSimonWheel : MonoBehaviour
    {
        [HideInInspector] public Transform parentColorSimon;

        [SerializeField] private AudioClip[] listSource;
        [SerializeField] private STPresetColorAnimsWheel presetColorAnimsWheel;
        [SerializeField] private float darkenFactor = 0.5f;
        [SerializeField] private ScriptableShake shake;

        [Inject] private STColorValidator colorValidator;

        private List<Color> currentColorSequnce = new();
        private STGenerateColorSubsequnce colorSubsequnce;
        private List<ImageColor> listImage = new();
        private STAnimsColorWheel animsColorWheel;
        private ShakeAnims shakeAnims;

        private void Start()
        {
            animsColorWheel = new STAnimsColorWheel(presetColorAnimsWheel);
            shakeAnims = new ShakeAnims(shake);
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

            for (int i = 0; i < parentColorSimon.childCount; i++)
            {
                if (parentColorSimon.GetChild(i).TryGetComponent<Image>(out var image))
                {
                    listImage.Add(new ImageColor(image, image.color, listSource[i]));
                }
            }
        }

        public async Task StartSimon(Range range)
        {
            colorSubsequnce = new();
            CreateSequnce(range);

            await AnimateSequenceColor(InitializedSequnce());
        }

        public async Task NextSimon(Range range)
        {
            RestoreColorSimon();
            CreateSequnce(range);

            await AnimateSequenceColor(InitializedSequnce());
        }

        public async Task ReplaySimon()
        {
            RestoreColorSimon();
            DarkenColorSimon();

            await AnimateSequenceColor(InitializedSequnce());
        }

        private void CreateSequnce(Range range)
        {
            var listColor = GetAllColorWheel();
            currentColorSequnce.Clear();
            currentColorSequnce = colorSubsequnce.GenerateSubsequnceColor(listColor, range);
            colorValidator.NewSubsequnce(currentColorSequnce);
            DarkenColorSimon();
        }

        private List<ImageColor> InitializedSequnce()
        {
            List<ImageColor> sebsequnceImage = new List<ImageColor>();

            foreach (var color in currentColorSequnce)
            {
                for (int i = 0; i < listImage.Count; i++)
                {
                    if (color == listImage[i].originalColors)
                        sebsequnceImage.Add(listImage[i]);
                }
            }

            return sebsequnceImage;
        }

        private async Task AnimateSequenceColor(List<ImageColor> sebsequnceImage)
        {
            for (int i = 0; i < sebsequnceImage.Count; i++)
            {
                if (sebsequnceImage[i].ImageSource == null)
                    continue;
                await animsColorWheel.WaitInterval();
                SoundPlayer.instance.PlaySound(sebsequnceImage[i].Sound);
                await animsColorWheel.StartFullAnims(sebsequnceImage[i].ImageSource, sebsequnceImage[i].originalColors, sebsequnceImage[i].darkenedColor);
            }
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

        public async Task StartShakeWheel()
        {
            await shakeAnims.StartShake(parentColorSimon.parent);
        }
    }

    public class ImageColor
    {
        public Image ImageSource;
        public Color originalColors;
        public Color darkenedColor;
        public AudioClip Sound;

        public ImageColor(Image image, Color color)
        {
            ImageSource = image;
            originalColors = color;
        }

        public ImageColor(Image image, Color color, AudioClip sound)
        {
            ImageSource = image;
            originalColors = color;
            Sound = sound;
        }

        public ImageColor(ImageColor imageColor)
        {
            ImageSource = imageColor.ImageSource;
            originalColors = imageColor.originalColors;
        }

        public ImageColor() {}
    }
}

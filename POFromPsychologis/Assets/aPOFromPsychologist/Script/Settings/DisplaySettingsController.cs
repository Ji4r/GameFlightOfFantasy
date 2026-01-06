using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace DiplomGames
{
    public class DisplaySettingsController
    {
        private Dictionary<FullScreenMode, string> keyDisplayMode;
        private List<Resolution> resolution;

        private Resolution currentResolutionScreen;
        private FullScreenMode currentScreenMode = FullScreenMode.Windowed;
        private DataSettings dataSettings;

        public DisplaySettingsController(DataSettings dataSettings)
        {
            this.dataSettings = dataSettings;
        }

        public void SetActivityVetrick(bool state)
        {
            dataSettings.DisplayVetrik = state;
        }

        public void UpdateDataSettings(DataSettings newDataSettings)
        {
            dataSettings = newDataSettings;
        }

        public FullScreenMode ChangeDisplayMode(int id)
        {
            FullScreenMode mode = id switch
            {
                0 => FullScreenMode.Windowed,
                1 => FullScreenMode.FullScreenWindow,
                2 => FullScreenMode.MaximizedWindow,
                _ => FullScreenMode.FullScreenWindow,
            };

            dataSettings.ScreenMode = mode;
            currentScreenMode = mode;
            SwitchDisplayMode(mode);
            return mode;
        }

        public void ChangeDisplayMode(FullScreenMode mode)
        {
            dataSettings.ScreenMode = mode;
            currentScreenMode = mode;
            SwitchDisplayMode(mode);
        }

        private void SwitchDisplayMode(FullScreenMode fullScreen)
        {
            Screen.fullScreenMode = fullScreen;

            if (fullScreen != FullScreenMode.Windowed)
            {
                Resolution targetResolution;
                targetResolution = GetMaxResolution();

                Screen.SetResolution(targetResolution.width, targetResolution.height, fullScreen);
                currentResolutionScreen = targetResolution;
            }
            else
            {
                Screen.fullScreenMode = fullScreen;
                SetResolution(dataSettings.ResolutionScreen.ResolutionIndex);
            }
        }

        public void SetResolution(int resolutionIndex)
        {
            if (resolution == null || resolutionIndex < 0 || resolutionIndex >= resolution.Count)
                return;

            if (dataSettings.ScreenMode != FullScreenMode.Windowed)
                return;

            Resolution resol = resolution[resolutionIndex];
            currentResolutionScreen = resol;
            Screen.SetResolution(resol.width, resol.height, currentScreenMode);
            dataSettings.ResolutionScreen = new CustomResolution(resol.width, resol.height, resolutionIndex);
        }

        public List<string> InitializedDisplayMode()
        {
            keyDisplayMode = new Dictionary<FullScreenMode, string>{
                {FullScreenMode.Windowed, "Оконный"},
                {FullScreenMode.FullScreenWindow, "Безрамочный полноэкранный"},
                {FullScreenMode.MaximizedWindow, "Полный экран"},
            };

            List<string> keys = new List<string>();
            foreach (KeyValuePair<FullScreenMode, string> kvp in keyDisplayMode)
            {
                keys.Add(kvp.Value);
            }

            return keys;
        }

        private Resolution GetMaxResolution()
        {
            Resolution maxRes = new Resolution();
            maxRes.width = 0;
            maxRes.height = 0;

            foreach (Resolution res in Screen.resolutions)
            {
                if (res.width > maxRes.width || (res.width == maxRes.width && res.height > maxRes.height))
                {
                    maxRes = res;
                }
            }

            return maxRes;
        }

        public List<Resolution> InitializedResolution(out List<string> options)
        {
            var filterResolution = Screen.resolutions;
            options = new List<string>();

            var uniqueResolutionData = filterResolution
                .Where(r => r.refreshRateRatio.value >= 60)
                .GroupBy(r => new { r.width, r.height })
                .Select(g => g.OrderBy(r => Math.Abs(r.refreshRateRatio.value - 60)).First())
                .ToList();

            int currentResolutionIndex = 0;
            for (int i = 0; i < uniqueResolutionData.Count; i++)
            {
                var res = uniqueResolutionData[i];
                string option = res.width + "x" + res.height;
                options.Add(option);

                if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolution = uniqueResolutionData;

            return resolution;
        }
    }

    [System.Serializable]
    public struct CustomResolution
    {
        public int ResolutionIndex;
        public int Width;
        public int Height;

        public CustomResolution(int Wight, int Height)
        {
            this.Height = Height;
            this.Width = Wight;
            ResolutionIndex = 0;
        }

        public CustomResolution(int Wight, int Height, int ResolutionIndex)
        {
            this.Height = Height;
            this.Width = Wight;
            this.ResolutionIndex = ResolutionIndex;
        }
    }
}

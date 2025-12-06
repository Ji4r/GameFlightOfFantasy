namespace DiplomGames
{
    public class STGameSettingsManager
    {
        private int whatCreateColor;
        private Range rangeDifficulties;

        public int WhatCreateColor { 
            get { return whatCreateColor; }
            set { if (value > 0) whatCreateColor = value; }
        }

        public Range RangeDifficulties {
            get { return rangeDifficulties; }
            set { if (value.minValue > 0 && value.maxValue > 0) rangeDifficulties = value; }
        }


        public void SetPropertyGame(int countColor, Range rangeDifficulties)
        {
            WhatCreateColor = countColor;
            RangeDifficulties = rangeDifficulties;
        }
    }
}

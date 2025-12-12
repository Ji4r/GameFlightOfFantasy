using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class M2Generator
    {
        public List<Sprite> Generate(Sprite[] listSprite, int countGenerate)
        {
            if (countGenerate > listSprite.Length)
            {
                Debug.LogError("у тебя картинок менье чем ты просиш");
                return null;
            }

            Sprite[] shuffledArray = (Sprite[])listSprite.Clone();

            for (int i = shuffledArray.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                Sprite temp = shuffledArray[i];
                shuffledArray[i] = shuffledArray[j];
                shuffledArray[j] = temp;
            }

            List<Sprite> shuffleList = new List<Sprite>();
            for (int i = 0; i < countGenerate && i < shuffledArray.Length; i++)
            {
                shuffleList.Add(shuffledArray[i]);
            }

            return shuffleList;
        }
    }
}

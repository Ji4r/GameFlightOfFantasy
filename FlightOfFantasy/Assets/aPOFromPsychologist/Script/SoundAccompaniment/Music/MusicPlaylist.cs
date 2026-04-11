using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DiplomGames
{
    [System.Serializable]
    public class MusicPlaylist
    {
        public List<AudioClip> Playlist;

        private Queue<AudioClip> shufflePlaylist;
        private AudioClip lastShuffleClip;

        public void Initialize()
        {
            shufflePlaylist = new Queue<AudioClip>();
            GenerateShufflePlaylist();
        }

        /// <summary>
        /// ЅерЄт звук из перемешенной очереди. ≈сли isAutoGeneration равен true то когда очередь
        /// будет пуста€ будет занова генерироватс€ последовательность музыки.
        /// </summary>
        /// <param name="isAutoGeneration"></param>
        /// <returns></returns>
        public AudioClip GetShuffleClip(bool isAutoGeneration = false)
        {
            if (shufflePlaylist == null)
                throw new Exception("shufflePlaylist не инициализирован и равен null");

            if (shufflePlaylist.Count == 0)
                if (isAutoGeneration)
                    GenerateShufflePlaylist();
                else
                    throw new Exception($"shufflePlaylist пуст, флаг isAutoGeneration равен - {isAutoGeneration}" +
                        $" и не возможно сгенерировать новую очередь");


            return shufflePlaylist.Dequeue();
        }

        public void GenerateShufflePlaylist()
        {
            if (shufflePlaylist != null)
                shufflePlaylist.Clear();
            else
                shufflePlaylist = new Queue<AudioClip>();

            if (Playlist == null || Playlist.Count == 0)
                throw new Exception("Playlist равен null или он пуст, поэтому музыка не запуститс€.");

            if (Playlist.Count == 1)
            {
                shufflePlaylist.Enqueue(Playlist[0]);
                return;
            }

            var tempList = new List<AudioClip>(Playlist);
            System.Random rand = new System.Random();

            int n = tempList.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                var temp = tempList[i];
                tempList[i] = tempList[j];
                tempList[j] = temp;
            }

            if (lastShuffleClip != null)
            {
                if (lastShuffleClip == tempList[0])
                {
                    int j = rand.Next(1, n);
                    var temp = tempList[0];
                    tempList[0] = tempList[j];
                    tempList[j] = temp;
                }
            }

            foreach (AudioClip clip in tempList)
            {
                shufflePlaylist.Enqueue(clip);
            }

            lastShuffleClip = tempList[n - 1];
        }
    }
}

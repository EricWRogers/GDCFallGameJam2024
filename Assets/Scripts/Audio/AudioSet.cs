using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AidensLibary.Audio
{
    [CreateAssetMenu(fileName = "AudioSet", menuName = "Data/Audio", order = 1)]
    public class AudioSet : ScriptableObject
    {
        public List<AudioInfo> sFX;
        public List<AudioInfo> music;
        public bool shuffleMusic = true;
    }
}
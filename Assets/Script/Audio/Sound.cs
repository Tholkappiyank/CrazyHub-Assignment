using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyHub.Hyderabad.Assignment
{
    [System.Serializable]
    public class Sound 
    {
        public string Name;

        public AudioClip Clip;

        public float Volume;

        public bool Loop;

        public AudioSource Source;

    }
}

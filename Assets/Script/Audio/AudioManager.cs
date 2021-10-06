using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyHub.Hyderabad.Assignment
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;

        private void Start() {
            foreach(Sound s in sounds) {
                s.Source = gameObject.AddComponent<AudioSource>();
                s.Source.clip = s.Clip;
                s.Source.loop = s.Loop;
                s.Source.playOnAwake = false;
            }           
        }

        public void PlaySound(string name ) {
            foreach (Sound s in sounds) {
               if(s.Name == name) {
                    s.Source.Play();
               }
            }
        }

        public void StopSound(string name) {
            foreach (Sound s in sounds) {
                 if (s.Name == name){  
                s.Source.Stop();
                }
            }
        }
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace PaintIn3D
{
    public class OfficeClean : MonoBehaviour
    {
        public P3dChangeCounter chanceCounter;
        public GameObject dirtyGlass;
        public bool isClean = false;
        public bool isFull = true;

        public ParticleSystem firework;

        public Worker worker;

        void Start()
        {
            StartCoroutine(StartCleaning());
        }

       

        IEnumerator StartCleaning()
        {

            yield return new WaitForSeconds(0.5f);
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                if (chanceCounter.Count < 500 && !isClean)  // && !isFull && !isClean
                {
                    PlayerControl.Instance.cleanedRoom++;
                    worker.PlayTurnAnim();                //.onComplete=()=> {

                    firework.Play();
                    yield return new WaitForSeconds(1f);
                    worker.PlayFinishAnim();
                   
                    
                    isClean = true;
                    yield return null;
                }
            }

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class PlayerTrigger : MonoBehaviour
{
    public ParticleSystem grassParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruid"))
        {
            StartCoroutine(Fruid(other)); 
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }
        else if (other.CompareTag("Finish"))
        {
            PlayerControl.Instance.GameFinish();
        } 
        else if (other.CompareTag("Grass"))
        {
            GameObject grass = other.gameObject;
            if (grass != null)
            {
                grass.tag = "GrassCute";
                GrassColorReferance referance = grass.transform.parent.GetComponent<GrassColorReferance>();
                if (referance != null)
                {
                    grassParticle.gameObject.transform.position = grass.transform.position;
                    grassParticle.startColor = referance.grassColor;
                    grassParticle.Play();
                }
                MMVibrationManager.Haptic(HapticTypes.Selection);
                grass.transform.DOScaleY(.25f, .05f);
                grass.transform.DOLocalMoveY(-2.3f, .05f);
            }
        }
    }
    public IEnumerator Fruid(Collider other)
    {
        FruidReferance fruitReferance = other.GetComponent<Fruit>().myParent.GetComponent<FruidReferance>();
        if (fruitReferance != null)
        {
            fruitReferance.fruidIndex++;
            if (fruitReferance.fruidIndex == fruitReferance.fullFruidIndex)
            {
                GameManager.Instance.Money(fruitReferance.price);
                fruitReferance.fruidOfPiece.Add(other.gameObject);
                other.transform.SetParent(GameManager.Instance.fruidCameraPos);
                other.transform.DOLocalMove(Vector3.zero + new Vector3(0, .05f * fruitReferance.fruidOfPiece.Count, 0), 1.5f).SetEase(Ease.InOutSine);
                yield return new WaitForSeconds(1.5f);
                fruitReferance.fruidAnim = false;
                for (int i = 0; i < fruitReferance.fruidOfPiece.Count; i++)
                {
                    fruitReferance.fruidOfPiece[i].transform.DORotate(Vector3.zero, .3f);
                    fruitReferance.fruidOfPiece[i].transform.DOLocalMove(Vector3.zero, .3f);
                }
                yield return new WaitForSeconds(.4f);
                fruitReferance.fruid.SetActive(true);
                fruitReferance.fruid.transform.DOScale(Vector3.zero, 1);
                fruitReferance.fruid.transform.DOLocalMove(new Vector3(1.5f, 1f, 0), 1).OnComplete(() =>
                {
                    UIManager.Instance.FruitText();
                    fruitReferance.fruid.SetActive(false);
                    fruitReferance.fruid.transform.localPosition = Vector3.zero;
                    fruitReferance.fruid.transform.localScale = Vector3.one;
                });
                for (int i = 0; i < fruitReferance.fruidOfPiece.Count; i++)
                {
                    fruitReferance.fruidOfPiece[i].SetActive(false);
                }
            }
            else
            {
                fruitReferance.fruidOfPiece.Add(other.gameObject);
                other.transform.SetParent(GameManager.Instance.fruidCameraPos);
                other.transform.DOLocalMove(Vector3.zero + new Vector3(0, .05f * fruitReferance.fruidOfPiece.Count, 0), 1.5f).SetEase(Ease.InOutSine);
                if (!fruitReferance.fruidAnim)
                {
                    fruitReferance.fruidAnim = true;
                    StartCoroutine(fruitReferance.FruidAnimation());
                }

            }
        }

    }
}

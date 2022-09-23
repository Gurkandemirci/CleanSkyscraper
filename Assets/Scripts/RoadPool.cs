using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoadPool : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Road"))
        {
            RoadChildObje(other.gameObject);
        } 
        
        else if (other.CompareTag("Fruid"))
        {
            other.enabled = false;
            FruidReferance fruitRef = other.transform.GetComponent<Fruit>().myParent.GetComponent<FruidReferance>();
            if (fruitRef != null)
            {
                for (int i = 0; i < fruitRef.fruidOfPiece.Count; i++)
                {
                    fruitRef.fruidOfPiece[i].transform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutBounce);
                }
                StartCoroutine(DestroyObj(other.transform.GetComponent<Fruit>().myParent.gameObject, .5f));
            }
        }
        else if (other.CompareTag("GrassCute") || other.CompareTag("Grass"))
        {
            GameObject cuteGrass = other.gameObject;
            if (cuteGrass != null)
                other.transform.DOScaleY(0, .05f).OnComplete(() => { other.gameObject.SetActive(false); });
        }
    }
    IEnumerator DestroyObj(GameObject destroy, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        int child = destroy.transform.childCount;
        for (int i = 0; i < child; i++)
        {
            if (destroy.transform.childCount >= i)
                if (destroy.transform.GetChild(i) != null)
                    destroy.transform.GetChild(i).gameObject.GetComponent<Collider>().enabled = false;
        }
        if (destroy != null)
        {
            destroy.SetActive(false);
        }
    }
    public void RoadChildObje(GameObject road)
    {
        for (int i = 0; i < 7; i++)
        {
            road.transform.GetChild(i).transform.DOMoveY(-30, .9f).SetDelay(Random.Range(0f, 1f)).OnComplete(() =>
            {
                road.transform.GetChild(i).gameObject.SetActive(false);
            });
        }
    }
}

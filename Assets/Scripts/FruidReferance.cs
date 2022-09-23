using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FruidReferance : MonoBehaviour
{
    public int fullFruidIndex;
    public int fruidIndex;
    public int price;
    public GameObject fruid;
    public bool fruidAnim;
    public List<GameObject> fruidOfPiece;
    public IEnumerator FruidAnimation()
    {
        while (fruidAnim)
        {
            for (int i = 0; i < fruidOfPiece.Count; i++)
            {
                fruidOfPiece[i].transform.DORotate(new Vector3(Random.Range(-15f, 15f), 0, Random.Range(-15f, 15f)), 3).SetEase(Ease.Linear);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}

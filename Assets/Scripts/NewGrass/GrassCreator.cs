using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCreator : MonoBehaviour
{
    Renderer rend;
    void Start()
    {
        int randomGrass = Random.Range(0, GameManager.Instance.grassMaterial.Length);
        Material grassMaterial = GameManager.Instance.grassMaterial[randomGrass];
        int count = transform.childCount;
        GrassColorReferance referance = GetComponent<GrassColorReferance>();
        if (referance != null)
            referance.grassColor = grassMaterial.GetColor("_BotColor");
        for (int i = 0; i < count; i++)
        {
            if (count >= i)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child != null)
                {
                    rend = child.GetComponent<MeshRenderer>();
                    if (rend != null)
                        rend.material = grassMaterial;
                    child.transform.rotation = Quaternion.Euler(0, Random.Range(-90, 30), 0);
                }
            }
        }
    }
}

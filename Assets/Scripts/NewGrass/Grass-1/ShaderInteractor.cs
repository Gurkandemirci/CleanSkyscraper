using UnityEngine;
 
public class ShaderInteractor : MonoBehaviour
{
    public GrassPainter grass;
    void Update()
    {
        //grass.dest(transform);
        Shader.SetGlobalVector("_PositionMoving", transform.position);
    }
}
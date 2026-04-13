using UnityEngine;

public class ColorChanger : MonoBehaviour
{
   [SerializeField] private Color color;
   private MaterialPropertyBlock mpb;
   private MeshRenderer meshRenderer;


   void Start()
   {
      mpb = new MaterialPropertyBlock();
      meshRenderer = GetComponent<MeshRenderer>();
      meshRenderer.SetPropertyBlock(mpb);
   }

    void Update()
    {
        mpb.SetColor("_BaseColor", color);
        meshRenderer.SetPropertyBlock(mpb);
    }
}

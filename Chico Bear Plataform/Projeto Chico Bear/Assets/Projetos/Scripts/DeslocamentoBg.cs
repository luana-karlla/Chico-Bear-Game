using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeslocamentoBg : MonoBehaviour
{
    private Renderer objetoRenderer;
    private Material objetoMaterial;
    public float offset; //deslocamento
    public float offsetIncremento;
    public float offsetVelocidade;

    //organizar as camadas do bg
    public string sortingLayer;
    public int orderinLayer;

    // Start is called before the first frame update
    void Start()
    {
        objetoRenderer = GetComponent<MeshRenderer>();

        objetoRenderer.sortingLayerName = sortingLayer;
        objetoRenderer.sortingOrder = orderinLayer;

        objetoMaterial = objetoRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += offsetIncremento;
        objetoMaterial.SetTextureOffset("_MainTex", new Vector2(offset * offsetVelocidade, 0));
    }
}

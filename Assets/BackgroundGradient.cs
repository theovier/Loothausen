using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGradient : MonoBehaviour
{
    
    public UnityEngine.UI.RawImage img;
    public Color top;
    public Color bottom;
    
    private Texture2D backgroundTexture ;

    void Awake()
    {
        backgroundTexture  = new Texture2D(1, 2);
        backgroundTexture.wrapMode = TextureWrapMode.Clamp;
        backgroundTexture.filterMode = FilterMode.Bilinear;
        SetColor( bottom, top ) ;
    }

    public void SetColor( Color color1, Color color2 )
    {
        backgroundTexture.SetPixels( new Color[] { color1, color2 } );
        backgroundTexture.Apply();
        img.texture = backgroundTexture;
    }
}

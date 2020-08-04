using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ScriptPixelPerfect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider myCollider;
    void Start()
    {
        SetPixelSize();
    }

    void SetPixelSize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float ratio_y = spriteRenderer.material.GetFloat("_RatioY");
        float ratio_x = spriteRenderer.material.GetFloat("_RatioX");

        Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
        float sprite_x = sprite_size.x / 200; 
        float sprite_y = sprite_size.y / 200; 

        ratio_y /= sprite_y;
        ratio_x /= sprite_x;

        spriteRenderer.material.SetFloat("_RatioY", ratio_y);
        spriteRenderer.material.SetFloat("_RatioX", ratio_x);

    }
}

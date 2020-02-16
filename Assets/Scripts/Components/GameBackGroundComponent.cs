using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBackGroundComponent : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Transform nextSpawnPoint;

    
    public void SetBG(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        
    }

}



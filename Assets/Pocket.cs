using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    string border_tag = "border";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != border_tag)
        {
            Destroy(other.gameObject);
            SceneReload.DestroyBall();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    string border_tag = "border";
    string main_ball = "main_ball";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != border_tag)
        {
            if (other.tag != main_ball)
            {
                Destroy(other.gameObject);
                SceneReload.DestroyBall();
            }
            else 
            {
                SceneReload.SceneChange();
            }
        } 
    }
}

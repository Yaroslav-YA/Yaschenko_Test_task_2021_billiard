using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneReload 
{
    public static int number_of_balls = 16;
    
    public static void DestroyBall()
    {
        number_of_balls--;
        //Debug.Log("ball");
        if (number_of_balls < 1)
        {
            SceneChange();
            //Debug.Log("Balls"+number_of_balls);
        }
    }
    
    public static void SceneChange()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}

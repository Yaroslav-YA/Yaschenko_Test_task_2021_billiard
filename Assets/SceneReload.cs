using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneReload 
{
    public static int number_of_balls = 15;
    
    public static void DestroyBall()
    {
        number_of_balls--;
        if (number_of_balls < 1)
        {
            SceneChange();
        }
    }
    
    public static void SceneChange()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}

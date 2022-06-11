using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneReload 
{
    const int start_number_of_balls = 15;
    public static int number_of_balls = start_number_of_balls;
    
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
        number_of_balls =start_number_of_balls;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}

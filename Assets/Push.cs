using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public Rigidbody2D ball;
    bool is_drag;
    Vector2 start;
    Vector2 end;
    public int power_multiplier=10;
    Vector2 power_multiplier_vector;
    // Start is called before the first frame update
    void Start()
    {
        power_multiplier_vector = new Vector2(power_multiplier, power_multiplier);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton (0)) {
            if (!is_drag)
            {
                MouseDown();
            }
        } else if (is_drag) {
            MouseUp();
        };
    }
    void MouseDown()
    {
        start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        is_drag = true;
    }
    void MouseUp()
    {
        end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("start" + start + "end" + end + "end-start" + (end - start));
        ball.AddForce((end - start) * power_multiplier) ;
        is_drag = false;
    }
}

    /*void OnMouseDown()
    {
        start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void OnMouseUp()
    {
        end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("start" + start + "end" + end+"end-start"+(end-start));

        ball.AddForce(end - start);
    }*/

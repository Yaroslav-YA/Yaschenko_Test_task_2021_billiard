using UnityEngine;

public class Push : MonoBehaviour
{
    #region Values
    public Rigidbody2D ball;            //value that contains Main Ball Rigidbody
    
    bool is_drag = false;               //value that used for know is the ball drag

    Vector2 start;                      //position of ball at moment when ball drag
    Vector2 end;                        //position of touch at moment when ball drag

    public int power_multiplier=100;    //strength multiplier of ball push

    LineRenderer line_renderer;         //line that shows direction of main ball
    public LineRenderer line_target;    //line that shows direction of the target ball

    RaycastHit2D raycast;               //value that saves the raycast data

    public float end_speed=0.01f;       //speed that considered like lowest speed 
    float distance;                     //distance that main ball will traveled after release
    public float second_track = 0.00001f;//length of line after collision for main ball
    public float trail_track = 2f;      //length of line after collision for target
    string border_tag = "border";       //tag for borders
    string pocket_tag = "pocket";       //tag for pockets
    #endregion
    void Start()
    {
        line_renderer= GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))    //check is start drag
        {
            is_drag = true;
        }

        if (Input.GetMouseButtonUp(0))      //check is releaze ball
        {
            start = ball.transform.position;
            MouseUp();
        }

        if (is_drag)                        //check is the drag now
        {
            start = ball.transform.position;    
            end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line_renderer.SetPosition(0, start);

            distance = CalculateDistanceToStop((Vector2.Distance((end - start) * -power_multiplier, start)), ball.angularDrag);
            
            if (raycast = Physics2D.CircleCast(start, 0.5f, start - end,distance)) //cast the ray
            {
                line_renderer.SetPosition(1, raycast.centroid);
                if (raycast.collider.tag != "pocket")   //check that collider is not a pocket
                {
                    line_renderer.SetPosition(2, VectorToPointMultiplier(raycast.point, Vector2.Reflect((start - end) , raycast.normal) + raycast.point, second_track));
                }
                else
                {
                    line_renderer.SetPosition(2, line_renderer.GetPosition(1));
                }
                line_renderer.enabled = true;
                //Debug.Log("point" + raycast.point + "reflect" + Vector2.Reflect((start - end) * power_multiplier, raycast.normal));
                if (raycast.collider.tag != border_tag&&raycast.collider.tag!=pocket_tag) //check that collider that was hit by ray not a border or pocket
                {
                    line_target.SetPosition(0, raycast.collider.transform.position);
                    line_target.SetPosition(1, VectorToPointMultiplier(raycast.collider.transform.position, (raycast.point - raycast.normal),trail_track));
                    
                    line_target.enabled = true;
                }
                else
                {
                    line_target.enabled = false;
                }
            }
            else
            {
                line_renderer.SetPosition(1,(start-end).normalized* distance);
                line_renderer.SetPosition(2, line_renderer.GetPosition(1));
                line_renderer.enabled = true;
            }
        }
        else
        {
            line_renderer.enabled = false;
            line_target.enabled = false;
        }
    }
    /// <summary>
    /// push the main ball
    /// </summary>
    void MouseUp()
    {
        end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ball.AddForce((end - start) * -power_multiplier) ;
        is_drag = false;
    }
    
    /// <summary>
    /// Calculate the time to stop the main ball
    /// </summary>
    /// <param name="strength">Force that was given to ball</param>
    /// <param name="drag">drag of ball</param>
    /// <returns></returns>
   
    float CalculateTimeToStop(float strength, float drag)
    {
        float start_speed;
        float time;
        start_speed = strength * Time.fixedDeltaTime;
        time = Mathf.Log(start_speed/end_speed,1-drag);
        return time;
    }

    /// <summary>
    /// Calculate the distance that main ball was traveled until stop
    /// </summary>
    /// <param name="strength">Force that was given to ball</param>
    /// <param name="drag">drag of ball</param>
    /// <returns></returns>
    float CalculateDistanceToStop(float strength, float drag)
    {
        float time = CalculateTimeToStop(strength, drag);
        float distance = end_speed * Mathf.Pow(1 - drag, time) / Mathf.Log(1- drag);
        return Mathf.Abs(distance)+Time.fixedDeltaTime*strength*Time.fixedDeltaTime;
    }

    /// <summary>
    /// Finds coordinates of the endpoint of the vector that multiplied by the multiplier
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="multiplier"></param>
    /// <returns></returns>
    Vector2 VectorToPointMultiplier(Vector2 start, Vector2 end, float multiplier)
    {
        return (end - start) * multiplier + start;
    }
}
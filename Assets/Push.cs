using UnityEngine;

public class Push : MonoBehaviour
{
    public Rigidbody2D ball;
    bool is_drag=false;
    Vector2 start;
    Vector2 end;
    public int power_multiplier=10;
    Vector2 power_multiplier_vector;
    LineRenderer line_renderer;
    public LineRenderer line_target;
    Ray2D ray;
    RaycastHit2D raycast;
    public float end_speed=0.01f;
    float distance;
    string border_tag = "border";
    //float drag;
    // Start is called before the first frame update
    void Start()
    {
        line_renderer= GetComponent<LineRenderer>();
        //drag=ball.GetComponent<Collider2D>().
        //power_multiplier_vector = new Vector2(power_multiplier, power_multiplier);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("start");
                is_drag = true;
            }
        }*/
        /*if (Input.touchCount > 0)
        {
            Debug.Log("Touch");
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            is_drag = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            start = ball.transform.position;
            MouseUp();
        }
        if (is_drag)
        {
            start = ball.transform.position;
            end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line_renderer.SetPosition(0, start);
            distance = CalculateDistanceToStop((Vector2.Distance((end - start) * -power_multiplier, start)), ball.angularDrag);
            //line_renderer.SetPosition(1, -end);

            /*ray.origin = start;
            ray.direction = -end - start;*/
            if (raycast = Physics2D.CircleCast(start, 0.5f, start - end,distance))
            {
                Debug.Log(raycast.point);
                line_renderer.SetPosition(1, raycast.centroid);
                line_renderer.SetPosition(2, Vector2.Reflect((start - end)* power_multiplier, raycast.normal));
                line_renderer.enabled = true;
                if (raycast.collider.tag != border_tag)
                {
                    line_target.SetPosition(0, raycast.collider.transform.position);
                    line_target.SetPosition(1, (raycast.point - raycast.normal)*2f);
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
        /*if (Input.GetMouseButton (1)) {
            
            if (!is_drag)
            {
                Debug.Log("start");                
                MouseDown();
            }
        } else if (is_drag) {
            MouseUp();
            Debug.Log("end");
        };*/
    }
    void MouseDown()
    {
        start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        is_drag = true;
    }
    void MouseUp()
    {
        end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("start" + start + "end" + end + "end-start" + (end - start));
        ball.AddForce((end - start) * -power_multiplier) ;
        is_drag = false;
    }
    float CalculateDistance(float strength,float drag)
    {
        float distance;
        float start_speed;
        start_speed = strength * Time.fixedDeltaTime;
        distance = Mathf.Pow(start_speed, 2)/(2*drag);
        return distance;
    }
    float CalculateTimeToStop(float strength, float drag)
    {
        float start_speed;
        float time;
        start_speed = strength * Time.fixedDeltaTime;
        time = Mathf.Log(start_speed/end_speed,1/*/Time.fixedDeltaTime*/-drag);
        return time;
    } 
    float CalculateDistanceToStop(float strength, float drag)
    {
        float time = CalculateTimeToStop(strength, drag);
        float distance = end_speed * Mathf.Pow(1/*/Time.fixedDeltaTime*/ - drag, time) / Mathf.Log(1/*/Time.fixedDeltaTime*/ - drag);
        return Mathf.Abs(distance)+Time.fixedDeltaTime*strength*Time.fixedDeltaTime;
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

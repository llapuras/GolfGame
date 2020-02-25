using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMove : MonoBehaviour
{
    public float thrust = 1.0f;
    public float maxthrust = 10.0f;
    public float speed = 5.0f;
    public float jumpspeed = 0.1f;

    private Rigidbody rb;
    private LineRenderer dirline;

    private Vector3 direction;
    private Vector3 pos;
    private float force;

    public float strength = 0;
    public Image powerBar;
    public float delta = 100;
    public float maxPower = 30000;

    public float totalMass;

    Vector3 dir;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dirline = GetComponent<LineRenderer>();
    }


    void Update()
    {
        BallController();

        //PointShoot();
        isOutofArea();
        //PressJumpForward();
        //PressJumpUp();
    }

    //-----------------------------------------------------------------------------
    //----------------------------------obsolete-----------------------------------
    bool isIdle()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.x<1&& gameObject.GetComponent<Rigidbody>().velocity.y < 1 && gameObject.GetComponent<Rigidbody>().velocity.z < 1 )
            return true;
        else return false;
    }

    void BallController()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(1))
            {

            }
            ShootBall();
        }


    void ShootBall()
    {
        
            if (strength <= maxPower)
            {
                strength += delta;
            }
            else
            {
                strength = maxPower;
            }
            direction = pos - rb.transform.position;
            direction.y = 0;
            pos.y = rb.transform.position.y; //avoid jumpping
            force = Mathf.Min(Vector3.Distance(pos, rb.transform.position), maxthrust) * thrust;
            powerBar.GetComponent<RectTransform>().sizeDelta = new Vector2(20, strength / maxPower * 500);
        }

        if (Input.GetMouseButtonUp(0))
        {
            totalMass = GetTotalMass(gameObject);

            rb.AddForce(direction.normalized * strength * totalMass * 0.3f);
            strength = 0;
        }
    }


    //this piece of code is used in Slime power
    float GetTotalMass(GameObject ob)
    {
        float ma = 0;
        ma += ob.GetComponent<Rigidbody>().mass;

        Rigidbody[] bodies;
        bodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody x in bodies)
        {
            ma += x.mass;
        }

        return ma;
    }


    //if die
    void isOutofArea()
    {
        if(transform.position.y < -20)
        {
            Destroy(gameObject);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}

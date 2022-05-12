using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rigi;
    public LayerMask walls;
    public TextMeshProUGUI score_txt;

    GameObject[] enemies;
    GameObject[] baits;

    int baitQuantity;
    int score = 0;

    float speed = 5.0f;

    bool right, left, up, down;

    // Start is called before the first frame update
    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        baits = GameObject.FindGameObjectsWithTag("Bait");
        baitQuantity = baits.Length;

        right = true;
        score_txt.text = "Score: " + score;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bait")
        {
            Destroy(collision.gameObject);
            score += 10;
            score_txt.text = "Score: " + score;
            baitQuantity--;

            if (baitQuantity == 0)
            {
                Debug.Log("Tebrikler");
            }
        }

        if (collision.gameObject.tag == "BigSphere")
        {
            Destroy(collision.gameObject);
            foreach(GameObject enemy in enemies)
            {
                //enemy.GetComponent<AI>().BeBlue();
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && control(Vector3.right) == false)
        {
            directionState(true, false, false, false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && control(-Vector3.right) == false)
        {
            directionState(false, true, false, false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && control(Vector3.forward) == false)
        {
            directionState(false, false, true, false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && control(-Vector3.forward) == false)
        {
            directionState(false, false, false, true);
        }
    }

    bool control(Vector3 castDirection)
    {
        RaycastHit touch;

        if (Physics.Raycast(transform.position, castDirection,out touch, 2.0f, walls))
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    void directionState(bool rightDirection, bool leftDirection, bool upDirection, bool downDirection)
    {
        right = rightDirection;
        left = leftDirection;
        up = upDirection;
        down = downDirection;
    }

    void FixedUpdate()
    {
        move();    
    }

    void move()
    {
        if (right)
        {
            rigi.velocity = Vector3.right * speed;
        }

        if (left)
        {
            rigi.velocity = Vector3.left * speed;
        }

        if (up)
        {
            rigi.velocity = Vector3.forward * speed;
        }

        if (down)
        {
            rigi.velocity = Vector3.back * speed;
        }
    }
}

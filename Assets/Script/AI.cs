using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    public NavMeshAgent fallow;
    public Animator animations;
    public Transform enemySpawner;
    public GameObject targetCube;
    public MeshRenderer visibility;
    public GameObject infoCanvas;

    string color = "Red";


    private void OnTriggerEnter(Collider collision)
    {
        switch (visibility.enabled)
        {

            case true:

                if (collision.gameObject.tag == "Player" && color == "Red")
                {
                    SceneManager.LoadScene("Scenes/scene");
                }

                if (collision.gameObject.tag == "Player" && color == "Blue")
                {
                    GameObject newInfoCanvas = Instantiate(infoCanvas, transform.position, Quaternion.identity);
                    Destroy(newInfoCanvas, 2.0f);

                    BeBlue();

                    targetCube.transform.position = enemySpawner.position;

                    CancelInvoke("BeRed");
                    CancelInvoke("StartAnimation");

                    visibility.enabled = false;
                }

                if (collision.gameObject == targetCube)
                {
                    collision.gameObject.GetComponent<TargetCube>().Move();
                }

                break;

            case false:
                if (collision.gameObject == targetCube)
                {
                    collision.gameObject.GetComponent<TargetCube>().Move();
                    visibility.enabled = true;
                }
                break;
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fallow.destination = targetCube.transform.position;
    }

    public void BeBlue()
    {
        color = "Blue";
        animations.SetBool("BeBlue", true);

        Invoke("StartAnimation", 7.0f);
        Invoke("BeRed", 10.0f);
    }

    void StartAnimation()
    {
        animations.SetBool("ChangeColor", true);
    }

    void BeRed()
    {
        color = "Red";
        animations.SetBool("BeBlue", false);
        animations.SetBool("ChangeColor", false);
    }
}

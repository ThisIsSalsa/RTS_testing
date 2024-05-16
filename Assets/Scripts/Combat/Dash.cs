using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Dash : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool isAttacking = false;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Code to handle dash movement
    }

    public void ActivateDash()
    {
        StartCoroutine(WaitForNextClick());
    }

    IEnumerator WaitForNextClick()
    {
        yield return null;

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isAttacking = true;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        agent.Warp(hit.collider.transform.position);
                        Debug.Log("Object teleported to: " + hit.collider.transform.position);

                        // Call the HandleDashHit method of the collided enemy
                        hit.collider.GetComponent<Enemy>().HandleDashHit();

                        break;
                    }
                    else
                    {
                        Debug.LogError("Clicked object is not tagged as Enemy.");
                    }
                }
                else
                {
                    Debug.LogError("No object clicked.");
                }
            }

            yield return null;
        }
    }
}

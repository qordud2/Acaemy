using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour
{
    public LayerMask ClickMask;
    public Animator myAnim;
    NavMeshPath path;

    Vector3 Pos = Vector3.zero;
    Vector3 Target = Vector3.zero;

    void Start()
    {
        path = new NavMeshPath();
        myAnim = GetComponent<Animator>();
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 999.0f, ClickMask))
            {
                
                NavMesh.CalculatePath(transform.position, hit.point, NavMesh.AllAreas, path);               
            }
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }

        for(int i = 0; i < path.corners.Length; i++)
        {
            Pos = transform.position;
            Target = path.corners[i];
            Vector3 dir = Vector3.Normalize(Pos - Target);
            float dist = Vector3.Distance(Pos, Target);
            while(dist > Mathf.Epsilon)
            {
                float delta = Time.deltaTime;
                if(dist - delta < Mathf.Epsilon)
                {
                    delta = dist;
                }
                dist -= delta;
                Vector3 pos = transform.position;
                pos += dir * delta;
                transform.position = pos;
                i++;
            }
        }
        for (int i = 0; i < path.corners.Length; i++)
        {
            Debug.Log("path.corners : " + path.corners[i]);
        }
            
    }


}

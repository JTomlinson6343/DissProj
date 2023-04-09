using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float m_MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        float deltaSpeed = m_MoveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
            transform.position = new Vector3(pos.x, pos.y + deltaSpeed, pos.z);
        if (Input.GetKey(KeyCode.A))
            transform.position = new Vector3(pos.x - deltaSpeed, pos.y, pos.z);
        if (Input.GetKey(KeyCode.S))
            transform.position = new Vector3(pos.x, pos.y - deltaSpeed, pos.z);
        if (Input.GetKey(KeyCode.D))
            transform.position = new Vector3(pos.x + deltaSpeed, pos.y, pos.z);
    }
}

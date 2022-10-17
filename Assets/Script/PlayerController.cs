using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float gravityForce;
    private CharacterController controller;
    public int coins;


    public static PlayerController Instance { get; private set; }



    void Start()
    {
        controller = GetComponent<CharacterController>();
        StreamReader sr = new(Application.persistentDataPath + "Player.txt");


        float x = float.Parse(sr.ReadLine());
        float y = float.Parse(sr.ReadLine());
        float z = float.Parse(sr.ReadLine());
        transform.position = new Vector3(x, y, z);
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * z + transform.up * -gravityForce + transform.right * x; 
        movement *= Time.deltaTime * playerSpeed;
        movement.y /= playerSpeed;
        controller.Move(movement);
    }

    private void OnDisable()
    {
        FileStream fs = File.Create(Application.persistentDataPath + "Player.txt");

        StreamWriter sw = new(fs);

        sw.WriteLine(transform.position.x);
        sw.WriteLine(transform.position.y);
        sw.WriteLine(transform.position.z);

        sw.Close();
        fs.Close();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 JoystickDirection => new Vector2(joystick.Horizontal, joystick.Vertical);
    [Header("Refs")]
    [SerializeField] Joystick joystick;
    [Header("Settings")]
    [SerializeField] float speed;

    // Propiedades //
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    void Update()
    {
        transform.Translate(JoystickDirection * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public ShootController shootController;
    public PlayerMovement movement;
    public FieldOfView FOV;
    void Awake()
    {
        instance = this;
        GetChildComponents();
    }
    void GetChildComponents()
    {
        shootController = GetComponent<ShootController>();
        movement = GetComponent<PlayerMovement>();
        FOV = GetComponent<FieldOfView>();
    }
}

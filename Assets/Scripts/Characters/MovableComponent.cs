using Characters;
using UnityEngine;

public class MovableComponent : MonoBehaviour, IMovable
{
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float rotationSpeed = 3;

    public Transform Transform => transform;
    public float MoveSpeed => moveSpeed;
    public float RotationSpeed => rotationSpeed;
}

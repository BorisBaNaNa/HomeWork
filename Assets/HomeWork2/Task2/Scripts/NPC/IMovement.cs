using UnityEngine;

public interface IMovement
{
    void Move(Vector3 dir);

    Vector3 GetPositionOnGround();
}
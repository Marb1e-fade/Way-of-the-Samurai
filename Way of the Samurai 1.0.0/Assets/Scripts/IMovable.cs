using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    void Idle();
    void Walk(sightDirection direction);
    void Jump();
}

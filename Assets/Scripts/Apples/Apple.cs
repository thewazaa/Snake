using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : ItemInstanciable
{

    private PoolInstance poolManager;
    public Vector2 position;

    public override void Action(PoolInstance poolShootsManager, Vector3 position, Quaternion rotation, Transform possibleParent = null)
    {
        gameObject.SetActive(true);

        this.poolManager = poolShootsManager;
        this.position = position;
        transform.SetPositionAndRotation(position / 3, rotation);
        transform.SetParent(possibleParent);
    }

    public void Eaten() => poolManager.Release(this);
}
using System.Collections;
using UnityEngine;

public abstract class ItemInstanciable : MonoBehaviour
{
    public abstract void Action(PoolInstance poolShootsManager, Vector3 posation, Quaternion rotation, Transform possibleParent = null);
}
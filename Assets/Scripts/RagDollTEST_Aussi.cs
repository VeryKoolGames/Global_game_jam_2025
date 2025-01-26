using UnityEngine;

public class RagDollTEST_Aussi : MonoBehaviour
{
    public Transform target;
    public Transform rig;

    void Update()
    {
        rig.position = target.position;
    }
}

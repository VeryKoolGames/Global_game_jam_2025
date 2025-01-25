using UnityEngine;

public class RagDollTEST_Aussi : MonoBehaviour
{
    public Transform target;
    public Transform rig;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rig.position = target.position;
    }
}

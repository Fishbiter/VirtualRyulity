using UnityEngine;

public class Hand : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other as BoxCollider)
        {
            if(!other.GetComponent<Rigidbody>())
            {
                other.gameObject.AddComponent<Rigidbody>();
            }
        }
    }

}
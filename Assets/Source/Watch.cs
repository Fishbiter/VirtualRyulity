using UnityEngine;

public class Watch : MonoBehaviour
{
    public Transform m_neckBone;

    private void Update()
    {
        Vector3 toCam = (Camera.main.transform.position - transform.position).normalized;
        if(Vector3.Dot(toCam, transform.forward) < -0.1f)
        {
            m_neckBone.LookAt(Camera.main.transform.position - new Vector3(0, 0.1f, 0));
            m_neckBone.Rotate(new Vector3(-90, 90, 0));
        }
    }
}
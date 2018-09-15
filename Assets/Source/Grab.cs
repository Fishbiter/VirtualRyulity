using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public GameObject m_hideWhenGrabbed;
    public GameObject m_showWhenGrabbed;

    List<Collider> m_incontact = new List<Collider>();
    List<Collider> m_held = new List<Collider>();
    SteamVR_TrackedController m_controller;
    bool m_wasGripped = false;
    Vector3 m_lastPos;

    private void Awake()
    {
        m_controller = GetComponentInParent<SteamVR_TrackedController>();
        m_showWhenGrabbed.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other as BoxCollider && !other.GetComponent<Hand>())
        {
            m_incontact.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other as BoxCollider)
        {
            m_incontact.Remove(other);
        }
    }

    private void Update()
    {
        if(m_controller.gripped != m_wasGripped)
        {
            m_showWhenGrabbed.SetActive(m_controller.gripped);
            m_hideWhenGrabbed.SetActive(!m_controller.gripped);

            if (m_controller.gripped)
            {
                m_held = m_incontact;
                foreach (var h in m_held)
                {
                    if(!h.attachedRigidbody)
                    {
                        h.gameObject.AddComponent<Rigidbody>();
                    }

                    h.attachedRigidbody.isKinematic = true;
                    h.attachedRigidbody.transform.parent = transform;
                }
            }
            else
            {
                Vector3 impulse = transform.position;
                foreach (var h in m_held)
                {
                    h.attachedRigidbody.isKinematic = false;
                    h.attachedRigidbody.transform.parent = null;
                    h.attachedRigidbody.AddForce(impulse);

                }
                m_held.Clear();
            }
        }
        m_wasGripped = m_controller.gripped;
        m_lastPos = transform.position;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyTrack : MonoBehaviour
{
    CinemachineDollyCart cart;
    public CinemachineVirtualCamera virtualCamera;
    CinemachineTrackedDolly dolly;
    public float offset = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        cart = GetComponent<CinemachineDollyCart>();
        dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    // Update is called once per frame
    void Update()
    {
        cart.m_Position = dolly.m_PathPosition + offset;
    }
}

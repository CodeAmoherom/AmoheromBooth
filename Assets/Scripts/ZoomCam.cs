using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCam : MonoBehaviour
{
    private CinemachineFreeLook freelookCam;
    private CinemachineFreeLook.Orbit[] originalOrbits;

    [Range(0.01f, 0.5f)]
    public float minZoom = 0.5f;

    [Range(1f, 5f)]
    public float maxZoom = 1f;

    [AxisStateProperty]
    public AxisState zAxis = new AxisState(0, 1, false, true, 50f, 0.1f, 0.1f, "Mouse ScrollWheel", false);

    void Start()
    {
        freelookCam = GetComponent<CinemachineFreeLook>();
        if (freelookCam != null)
        {
            originalOrbits = new CinemachineFreeLook.Orbit[freelookCam.m_Orbits.Length];
            for (int i = 0; i < originalOrbits.Length; i++)
            {
                originalOrbits[i].m_Height = freelookCam.m_Orbits[i].m_Height;
                originalOrbits[i].m_Radius = freelookCam.m_Orbits[i].m_Radius;
            }
        }
    }

    void Update()
    {
        if (originalOrbits != null)
        {
            zAxis.Update(Time.deltaTime);
            float zoomScale = Mathf.Lerp(minZoom, maxZoom, zAxis.Value);

            for(int i = 0; i < originalOrbits.Length; i++)
            {
                freelookCam.m_Orbits[i].m_Height = originalOrbits[i].m_Height * zoomScale;
                freelookCam.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * zoomScale;
            }
        }
    }
}

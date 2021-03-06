﻿using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Image Tracking manager that detects tracked images")]
    ImageTrackingObjectManager m_ImageTrackingObjectManager;
	public LineRenderer LineRenderer;
    
    /// <summary>
    /// Get the <c>ImageTrackingObjectManger</c>
    /// </summary>
    public ImageTrackingObjectManager imageTrackingObjectManager
    {
        get => m_ImageTrackingObjectManager;
        set => m_ImageTrackingObjectManager = value;
    }

    [SerializeField]
    [Tooltip("Prefab to be spawned and showed between numbers based on distance")]
    GameObject m_SumPrefab;

    /// <summary>
    /// Get the sum prefab
    /// </summary>
    public GameObject sumPrefab
    {
        get => m_SumPrefab;
        set => m_SumPrefab = value;
    }

    GameObject m_SpawnedSumPrefab;
    GameObject m_OneObject;
    GameObject m_TwoObject;
    float m_Distance;
    bool m_SumActive;

    const float k_SumDistance = 0.3f;

    void Start()
    {
        m_SpawnedSumPrefab = Instantiate(m_SumPrefab, Vector3.zero, Quaternion.identity);
        m_SpawnedSumPrefab.SetActive(false);
    }
	
	 void MakeLine()
    {
        // set the color of the line
        LineRenderer.startColor = Color.red;
        LineRenderer.endColor = Color.red;
 
        // set width of the renderer
        LineRenderer.startWidth = 0.3f;
        LineRenderer.endWidth = 0.3f;
 
        // set the position
        LineRenderer.SetPosition(0, m_OneObject.transform.position);
        LineRenderer.SetPosition(1,  m_TwoObject.transform.position);
    }

    void Update()
    {
        m_OneObject = m_ImageTrackingObjectManager.spawnedOnePrefab;
        m_TwoObject = m_ImageTrackingObjectManager.spawnedTwoPrefab;

        if (m_ImageTrackingObjectManager.NumberOfTrackedImages() > 1)
        {
            m_Distance = Vector3.Distance(m_OneObject.transform.position, m_TwoObject.transform.position);

            if (true)
            {
                if (!m_SumActive)
                {
                    m_SpawnedSumPrefab.SetActive(true);
                    m_SumActive = true;
                }
                
                m_SpawnedSumPrefab.transform.position = (m_OneObject.transform.position + m_TwoObject.transform.position) / 2;
				Invoke("MakeLine", 0.1f);

            }
            else
            {
                m_SpawnedSumPrefab.SetActive(false);
                m_SumActive = false;
            }
        }
        else
        {
            m_SpawnedSumPrefab.SetActive(false);
            m_SumActive = false;
        }
    }
}



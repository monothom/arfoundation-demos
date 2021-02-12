using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Image Tracking manager that detects tracked images")]
    ImageTrackingObjectManager m_ImageTrackingObjectManager;
	public LineRenderer LineRendererOne;
    public LineRenderer LineRendererTwo;

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
    GameObject m_ThreeObject;
    float m_Distance;
    bool m_SumActive;

    const float k_SumDistance = 0.3f;

    void Start()
    {
        m_SpawnedSumPrefab = Instantiate(m_SumPrefab, Vector3.zero, Quaternion.identity);
        m_SpawnedSumPrefab.SetActive(false);
    }
	
	 void MakeLineOne()
    {
        // set the color of the line
        LineRendererOne.startColor = Color.red;
        LineRendererOne.endColor = Color.red;
        LineRendererOne.positionCount = 3;

        // set width of the renderer
        LineRendererOne.startWidth = 0.002f;
        LineRendererOne.endWidth = 0.002f;

        // set the position
        LineRendererOne.SetPosition(0, m_OneObject.transform.position);
        LineRendererOne.SetPosition(1, m_TwoObject.transform.position);
        LineRendererOne.SetPosition(2, m_ThreeObject.transform.position);
    }

    void MakeLineTwo()
    {
        // set the color of the line
        LineRendererTwo.startColor = Color.blue;
        LineRendererTwo.endColor = Color.blue;

        // set width of the renderer
        LineRendererTwo.startWidth = 0.002f;
        LineRendererTwo.endWidth = 0.002f;

        // set the position
        LineRendererTwo.SetPosition(0, m_OneObject.transform.position);
        LineRendererTwo.SetPosition(1, m_ThreeObject.transform.position);
    }

    void Update()
    {
        m_OneObject = m_ImageTrackingObjectManager.spawnedOnePrefab;
        m_TwoObject = m_ImageTrackingObjectManager.spawnedTwoPrefab;
        m_ThreeObject = m_ImageTrackingObjectManager.spawnedTwoPrefab;

        if (m_ImageTrackingObjectManager.NumberOfTrackedImages() > 2)
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
				Invoke("MakeLineOne", 0.0f);
                Invoke("MakeLineTwo", 0.1f);
            }
            else
            {
                m_SpawnedSumPrefab.SetActive(false);
                m_SumActive = false;
            }
        }
        //else if (m_ImageTrackingObjectManager.NumberOfTrackedImages() < 2)
        //{
        //    if (true)
        //    {
        //        if (!m_SumActive)
        //        {
        //            m_SpawnedSumPrefab.SetActive(true);
        //            m_SumActive = true;
        //        }

        //        m_SpawnedSumPrefab.transform.position = (m_OneObject.transform.position + m_TwoObject.transform.position) / 2;
        //        Invoke("MakeLineOne", 0.0f);
        //        Invoke("MakeLineTwo", 0.0f);

        //    }
        //    else
        //    {
        //        m_SpawnedSumPrefab.SetActive(false);
        //        m_SumActive = false;
        //    }
        //}
        else
        {
            m_SpawnedSumPrefab.SetActive(false);
            m_SumActive = false;
        }
    }
}



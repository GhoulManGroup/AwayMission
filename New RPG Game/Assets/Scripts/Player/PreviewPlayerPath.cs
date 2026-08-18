using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Attaches to a GameObject with a NavMeshAgent and LineRenderer.
/// Draws the agent's current path in real time.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]
public class NavMeshPathVisualizer : MonoBehaviour
{
    private NavMeshAgent agent;
    private LineRenderer lineRenderer;

    [Header("Line Settings")]
    public Color lineColor = Color.green;
    public float lineWidth = 0.2f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();

        // Configure LineRenderer
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        DrawPath();
    }

    /// <summary>
    /// Draws the NavMeshAgent's current path using the LineRenderer.
    /// </summary>
    private void DrawPath()
    {
        if (agent.path == null || agent.path.corners.Length == 0)
        {
            lineRenderer.positionCount = 0;
            return;
        }

        // Set positions to match path corners
        lineRenderer.positionCount = agent.path.corners.Length;
        for (int i = 0; i < agent.path.corners.Length; i++)
        {
            lineRenderer.SetPosition(i, agent.path.corners[i]);
        }
    }
}
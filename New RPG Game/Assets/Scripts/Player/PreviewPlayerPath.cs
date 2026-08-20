using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Attaches to a GameObject with a NavMeshAgent and LineRenderer.
/// Draws the agent's current path in real time.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]
public class PreviewPlayerPath : MonoBehaviour
{
    private NavMeshAgent agent;
    private LineRenderer lineRenderer;

    [Header("Line Settings")]
    public Color previewBadMoveLineColor = Color.green;
    public Color previewGoodMoveLineColor = Color.red;
    public Color movementLineColor = Color.blue;
    public float lineWidth = 0.2f;

    public bool moveOrderSent = false;

    public bool canCoverDistance = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();

        // Configure LineRenderer
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = previewBadMoveLineColor;
        lineRenderer.endColor = previewBadMoveLineColor;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 0;
    }

    public void SetActiveColor()
    {
        Gradient tempGradient = new Gradient();
        GradientColorKey[] tempColorKeys = new GradientColorKey[2];

        tempColorKeys[0] = new GradientColorKey(movementLineColor, 0);

        tempColorKeys[1] = new GradientColorKey(movementLineColor, 1);

        tempGradient.colorKeys = tempColorKeys;

        lineRenderer.colorGradient = tempGradient;

        if (canCoverDistance == false)
        {
           // lineRenderer.startColor = previewBadMoveLineColor;
        }
        else if (canCoverDistance == true)
        {
            //lineRenderer.startColor = previewGoodMoveLineColor;
        }
        else if (canCoverDistance == true && moveOrderSent)
        {


        }
    }

    /// <summary>
    /// Draws the NavMeshAgent's current path using the LineRenderer.
    /// </summary>
    public void DrawPath()
    {
        SetActiveColor();
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

    public IEnumerator UpdatePath()
    {
        SetActiveColor();
        while(agent.remainingDistance != 0)
        {
            lineRenderer.positionCount = agent.path.corners.Length;
            for (int i = 0; i < agent.path.corners.Length; i++)
            {
                lineRenderer.SetPosition(i, agent.path.corners[i]);
            }
            yield return null;
        }
        yield return ClearLine();
    }

    public IEnumerator ClearLine()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            lineRenderer.positionCount = 0;
        }
        yield return null;
    }
}
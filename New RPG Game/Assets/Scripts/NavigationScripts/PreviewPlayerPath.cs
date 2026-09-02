using PartyManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Attaches to a GameObject with a NavMeshAgent and LineRenderer.
/// Draws the agent's current path in real time.
/// </summary>

[RequireComponent(typeof(LineRenderer))]
public class PreviewPlayerPath : MonoBehaviour
{
    private NavMeshAgent agent;
    private LineRenderer lineRenderer;

    [Header("Line Settings")]
    Color previewBadMoveLineColor = Color.red;
    Color previewGoodMoveLineColor = Color.green;
    Color movementLineColor = Color.blue;

    public float lineWidth = 0.2f;

    public bool moveOrderSent = false;

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
        if (Manager.instance.partyController.partyMovementController.canCoverDistance == false)
        {
            //Somthing to note encase we come back to it having the line gradiant show using the two colors the point where the player could reach along the current potential path.
            tempColorKeys[0] = new GradientColorKey(previewBadMoveLineColor, 0);
            tempColorKeys[1] = new GradientColorKey(previewBadMoveLineColor, 1);
        }
        else if (Manager.instance.partyController.partyMovementController.canCoverDistance == true && !moveOrderSent)
        {
            tempColorKeys[0] = new GradientColorKey(previewGoodMoveLineColor, 0);
            tempColorKeys[1] = new GradientColorKey(previewGoodMoveLineColor, 1);
        }
        else if (Manager.instance.partyController.partyMovementController.canCoverDistance == true && moveOrderSent)
        {
            tempColorKeys[0] = new GradientColorKey(movementLineColor, 0);
            tempColorKeys[1] = new GradientColorKey(movementLineColor, 1);
        }

        tempGradient.colorKeys = tempColorKeys;

        lineRenderer.colorGradient = tempGradient;
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

        while (agent.hasPath && agent.remainingDistance > agent.stoppingDistance)
        {
            lineRenderer.positionCount = agent.path.corners.Length;
            for (int i = 0; i < agent.path.corners.Length; i++)
            {
                lineRenderer.SetPosition(i, agent.path.corners[i]);
            }

            yield return null;
        }

        // Resume agent state when the loop finishes
        agent.isStopped = false;

        if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            moveOrderSent = false;
            ClearLine();
        }
    }

    public void ClearLine()
    {
        lineRenderer.positionCount = 0;
    }
}
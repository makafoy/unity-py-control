using UnityEngine;

namespace Unity.MLAgentsExamples
{
    /// <summary>
    /// A helper class for the ML-Agents example scenes to override various
    /// global settings, and restore them afterwards.
    /// This can modify some Physics and time-stepping properties, so you
    /// shouldn't copy it into your project unless you know what you're doing.
    /// </summary>
    public class ProjectSettingsOverrides : MonoBehaviour
    {
        // Original values
        Vector3 m_OriginalGravity;
        float m_OriginalMaximumDeltaTime;
        int m_OriginalSolverIterations;
        int m_OriginalSolverVelocityIterations;
        bool m_OriginalReuseCollisionCallbacks;

        [Tooltip("Increase or decrease the scene gravity. Use ~3x to make things less floaty")]
        public float gravityMultiplier = 1.0f;

        [Header("Advanced physics settings")]
        [Tooltip("The maximum time a frame can take. Physics and other fixed frame rate updates (like MonoBehaviour's FixedUpdate) will be perform
using UnityEngine;
using UnityEngine.Events;

namespace Unity.MLAgentsExamples
{
    /// <summary>
    /// Utility class to allow target placement and collision detection with an agent
    /// Add this script to the target you want the agent to touch.
    /// Callbacks will be triggered any time the target is touched with a collider tagged as 'tagToDetect'
    /// </summary>
    public class CollisionCallbacks : MonoBehaviour
    {
        //        [System.Serializable] public class BoolEvent : UnityEvent<bool> { }
        //        [SerializeField] BoolEvent boolEvent = new BoolEvent();
        //        public void OnBoolEvent(bool value)
        //        {
        //            Debug.Log($"OnBoolEvent {value}");
        //        }


        [Header("Collider Tag To Detect")]
        public string tagToDetect = "agent"; //collider tag to detect

        //        [Header("Target Placement")]
        //        public float spawnRadius; //The radius in which a target can be randomly spawned.
        //        public bool respawnIfTouched; //Should the target respawn to a different position when touched
        //
        //        [Header("Target Fell Protection")]
        //        public bool respawnIfFallsOffPlatform = true; //If the target falls off the platform, reset the position.
        //        public float fallDistance = 5; //distance below the starting height that will trigger a respawn
        //
        //
        //        private Vector3 m_startingPos; //the starting position of the target
        //        private Agent m_agentTouching; //the agent currently touching the target

        [System.Serializable]
        //        public class TriggerEvent : UnityEvent<string>
        public class TriggerEvent : UnityEvent<Collider>
        {
        }

        [Header("Trigger Callbacks")]
        public TriggerEvent onTriggerEnterEvent = new 
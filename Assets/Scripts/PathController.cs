using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PathController : MonoBehaviour
    {
        [FormerlySerializedAs("_Waypoints")] [SerializeField]
        public List<Waypoint> waypoints = new List<Waypoint>();

    }
}
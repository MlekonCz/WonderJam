using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PathController : MonoBehaviour
    {
        [SerializeField]
        public List<Waypoint> _Waypoints = new List<Waypoint>();

    }
}
using System;
using CodeMonkey.Utils;
using UnityEngine;

namespace DefaultNamespace
{
    public class Testing : MonoBehaviour
    {

        private Grid _grid;
        private Camera _camera;

        private void Awake()
        {
            _camera = FindObjectOfType<Camera>();
        }

        private void Start()
        {
            _grid = new Grid(3, 5, 10f, new Vector3(-20,-40));

        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _grid.SetValue(_camera.ScreenToWorldPoint(Input.mousePosition), 22);
            }
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log(_grid.GetValue(_camera.ScreenToWorldPoint(Input.mousePosition)));
            }
        }
    }
}
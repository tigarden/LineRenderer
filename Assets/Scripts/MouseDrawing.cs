using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MouseDrawing : MonoBehaviour
{
    [SerializeField] private float lineWidth;

    private LineRenderer _lineRenderer;
    private bool _isMousePressed;
    private Vector2 _mousePosition;
    private Camera _mainCamera;

    // Start is called before the first frame update
    private void Start()
    {
        if (TryGetComponent(out _lineRenderer))
        {
            _lineRenderer.startWidth = lineWidth;
            _lineRenderer.endWidth = lineWidth;
        }

        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        _isMousePressed = Input.GetMouseButton(0);
        if (Input.GetMouseButtonDown(0))
        {
            ResetLine();
        }

        if (_isMousePressed && (Vector2)Input.mousePosition != _mousePosition)
        {
            DrawLine();
        }
    }

    private void ResetLine()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.material.color = Random.ColorHSV();
    }

    private void DrawLine()
    {
        _mousePosition = Input.mousePosition;
        var ray = _mainCamera.ScreenToWorldPoint(_mousePosition);
        ray.z = 0;
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray);
    }
}
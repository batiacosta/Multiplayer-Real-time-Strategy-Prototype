using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = new LayerMask();
    [SerializeField] private RectTransform unitSelectionArea = null;
     
    private Camera _mainCamera;
    private List<Unit> _selectedUnits = new List<Unit>();
    private RTSPlayer _player;
    private Vector2 _startSelectionAreaPosition;
    
    public List<Unit> GetSelectedUnits() => _selectedUnits;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        Unit.AuthorityOnUnitDeSpawned += Unit_AuthorityOnUnitDeSpawned;
    }

    private void OnDestroy()
    {
        Unit.AuthorityOnUnitDeSpawned -= Unit_AuthorityOnUnitDeSpawned;
    }

    private void Update()
    {
        if (_player == null)
        {
            _player = NetworkClient.connection.identity.GetComponent<RTSPlayer>(); // this will be removed when Lobby
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartSelectionArea();
        }
        else if (Mouse.current.leftButton.isPressed)
        {
            UpdateSelectionArea();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ClearSelectionArea();
        }
    }

    private void StartSelectionArea()
    {
        if (!Keyboard.current.leftShiftKey.isPressed)
        {
            foreach (Unit selectedUnit in _selectedUnits)
            {
                selectedUnit.Deselect();
            }
            _selectedUnits.Clear();
        }
        
        unitSelectionArea.gameObject.SetActive(true);
        _startSelectionAreaPosition = Mouse.current.position.ReadValue();
        UpdateSelectionArea();
    }
    private void UpdateSelectionArea()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        float areaWidth = mousePosition.x - _startSelectionAreaPosition.x;
        float areaHeight = mousePosition.y - _startSelectionAreaPosition.y;

        var absoluteX = Mathf.Abs(areaWidth);
        float absoluteY = Mathf.Abs(areaHeight);
        unitSelectionArea.sizeDelta = new Vector2(absoluteX, absoluteY);

        unitSelectionArea.anchoredPosition = _startSelectionAreaPosition + new Vector2(areaWidth / 2, areaHeight / 2);
    }

    private void ClearSelectionArea()
    {
        unitSelectionArea.gameObject.SetActive(false);

        if (unitSelectionArea.sizeDelta.magnitude == 0)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) return;
            if (!hit.collider.TryGetComponent<Unit>(out Unit unit)) return;
            if (!unit.isOwned) return;
        
            _selectedUnits.Add(unit);

            foreach (Unit selectedUnit in _selectedUnits)
            {
                selectedUnit.Select();
            }
        }

        Vector2 min = unitSelectionArea.anchoredPosition - (unitSelectionArea.sizeDelta / 2);
        Vector2 max = unitSelectionArea.anchoredPosition + (unitSelectionArea.sizeDelta / 2);

        foreach (Unit unit in _player.GetPlayerUnits())
        {
            if (_selectedUnits.Contains(unit))
            {
                continue;
            }
            Vector3 unitScreenPosition = _mainCamera.WorldToScreenPoint(unit.transform.position);
            if (unitScreenPosition.x > min.x && 
                unitScreenPosition.x < max.x && 
                unitScreenPosition.y > min.y && 
                unitScreenPosition.y < max.y)
            {
                _selectedUnits.Add(unit);
                unit.Select();
            }
        }
    }
    
    private void Unit_AuthorityOnUnitDeSpawned(Unit unit)
    {
        _selectedUnits.Remove(unit);
    }

    
}

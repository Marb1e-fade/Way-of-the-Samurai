using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private GameObject _inventoryCoin;
    private GameObject _background;
    private Collider2D _coinCollider;
    private PlayerControls _playerControls;

    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Text _coinTextField;

    void Start()
    {
        _background = GameObject.Find("Sakura_background");
        _playerControls = GameObject.Find("Player").GetComponent<PlayerControls>();
        _inventoryCoin = Instantiate(_coinPrefab, _inventoryPanel.transform.position, Quaternion.identity);
        _coinCollider = _inventoryCoin.GetComponent<Collider2D>();
    }

    void Update()
    {
        _coinCollider.transform.position = _background.transform.position + Vector3.left * 2.2f + Vector3.up * 1.25f;
        _coinTextField.text = _playerControls.CoinsCount.ToString();
    }
}

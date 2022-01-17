using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class BallShopItemView : MonoBehaviour
    {
        public event Action<BallShopItemDescriptor> OnItemClicked;

        [SerializeField] private Image _ballImage;
        [SerializeField] private TextMeshProUGUI _ballNameMesh;
        [SerializeField] private TextMeshProUGUI _ballPriceMesh;
        [SerializeField] private Button _byeButton;

        private string _ballPriceText;
        private BallShopItemDescriptor _descriptor;

        public void Init(BallShopItemDescriptor descriptor)
        {
            _ballImage.color = descriptor.Color;
            _ballNameMesh.text = descriptor.Name;
            _ballPriceMesh.text = string.Format(_ballPriceText, descriptor.Price);
            _descriptor = descriptor;
        }

        private void Awake()
        {
            _ballPriceText = _ballNameMesh.text;
            _byeButton.onClick.AddListener(OnByeButtonClickHandler);
        }

        private void OnByeButtonClickHandler()
        {
            OnItemClicked?.Invoke(_descriptor);
        }
    }
}
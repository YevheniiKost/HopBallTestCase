using Core.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace Core.Backend
{
    public class ShopItemsAdapter
    {
        public string ColorKey => "Color";
        public string PriceKey => "Price";
        public string RadiusKey => "Radius";
        public string WeightKey => "Weight";

        public BallItemDescriptor GetBallDescriptor(string name, string color, string price, string weight, string radius)
        {
            Color ballColor = GetColorFromString(color);
            int ballPrice = int.Parse(price);
            float ballWeight = float.Parse(weight, CultureInfo.InvariantCulture);
            float ballRadius = float.Parse(radius, CultureInfo.InvariantCulture);

            return new BallItemDescriptor
            {
                Name = name,
                Color = ballColor,
                Price = ballPrice,
                Radius = ballRadius,
                Weight = ballWeight
            };
        }

        private enum BallColor
        {
            Blue,
            Green,
            Red,
            Yellow
        }

        private Color GetColorFromString(string colorValue)
        {
            foreach (var color in Enum.GetValues(typeof(BallColor)).Cast<BallColor>().ToList())
            {
                if(String.Equals(color.ToString(), colorValue))
                {
                    return GetColorFromEnum(color);
                }
            }
            return Color.white;
        }

        private Color GetColorFromEnum(BallColor color)
        {
            switch (color)
            {
                case BallColor.Blue:
                    return Color.blue;
                case BallColor.Green:
                    return Color.green;
                case BallColor.Red:
                    return Color.red;
                case BallColor.Yellow:
                    return Color.yellow;
            }
            return Color.white;
        }
    }
}

using System;
using Lumia.Imaging;

namespace DialImaging.Effects
{
    public class SaturationEffect : IEffect
    {
        private double _saturationValue = 0;
        private double _stepSize = 0.05;

        public string Value => _saturationValue.ToString();

        public void IncreaseEffect()
        {
            _saturationValue += _stepSize;

            _saturationValue = Math.Min(_saturationValue, 1);
        }

        public void DecreaseEffect()
        {
            _saturationValue -= _stepSize;

            _saturationValue = Math.Max(_saturationValue, -1);
        }

        public IImageProvider AddEffect(IImageProvider provider)
        {
            return new Lumia.Imaging.Adjustments.HueSaturationEffect(provider,0, _saturationValue);
        }
    }
}

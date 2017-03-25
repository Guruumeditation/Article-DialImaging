using System;
using Lumia.Imaging;

namespace DialImaging.Effects
{
    public class HueEffect : IEffect
    {
        private double _hueValue = 0;
        private double _stepSize = 0.05;

        public string Value => _hueValue.ToString();

        public void IncreaseEffect()
        {
            _hueValue += _stepSize;

            _hueValue = Math.Min(_hueValue, 1);
        }

        public void DecreaseEffect()
        {
            _hueValue -= _stepSize;

            _hueValue = Math.Max(_hueValue, -1);
        }

        public IImageProvider AddEffect(IImageProvider provider)
        {
            return new Lumia.Imaging.Adjustments.HueSaturationEffect(provider, _hueValue, 0);
        }
    }
}

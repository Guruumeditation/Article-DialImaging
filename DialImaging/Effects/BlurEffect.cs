using Lumia.Imaging;

namespace DialImaging.Effects
{
    public class BlurEffect : IEffect
    {
        private int _blurValue = 1;
        private int _stepSize = 5;

        public string Value => _blurValue.ToString();

        public void IncreaseEffect()
        {
            _blurValue += _stepSize;

            _blurValue = _blurValue > 256 ? 256 : _blurValue;
        }

        public void DecreaseEffect()
        {
            _blurValue -= _stepSize;

            _blurValue = _blurValue < 1 ? 1 : _blurValue;
        }

        public IImageProvider AddEffect(IImageProvider provider)
        {
            return new Lumia.Imaging.Adjustments.BlurEffect(provider,_blurValue);
        }
    }
}

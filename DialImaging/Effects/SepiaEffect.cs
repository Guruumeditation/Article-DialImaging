using Lumia.Imaging;

namespace DialImaging.Effects
{
    public class SepiaEffect : IEffect
    {
        private bool _isOn;

        public string Value => _isOn.ToString();
        public void IncreaseEffect()
        {
            _isOn = !_isOn;
        }
        public void DecreaseEffect()
        {
            _isOn = !_isOn;
        }

        public IImageProvider AddEffect(IImageProvider provider)
        {
            return _isOn ? new Lumia.Imaging.Artistic.SepiaEffect(provider) : provider;
        }
    }
}

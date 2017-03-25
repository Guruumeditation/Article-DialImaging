using Lumia.Imaging;

namespace DialImaging.Effects
{
    public class FogEffect : IEffect
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
            return _isOn ? new Lumia.Imaging.Artistic.FogEffect(provider) : provider;
        }
    }
}

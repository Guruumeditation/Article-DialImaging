using Lumia.Imaging;

namespace DialImaging.Effects
{
    public interface IEffect
    {
        string Value { get; }
        void IncreaseEffect();
        void DecreaseEffect();
        IImageProvider AddEffect(IImageProvider provider);
    }
}

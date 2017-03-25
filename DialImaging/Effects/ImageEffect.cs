using Windows.Storage;
using Lumia.Imaging;

namespace DialImaging.Effects
{
    public class ImageEffect : IEffect
    {
        private readonly IImageProvider _effect;

        public string Value => string.Empty;

        public ImageEffect(StorageFile source)
        {
            _effect = new StorageFileImageSource(source);
        }

        public void IncreaseEffect()
        {

        }

        public void DecreaseEffect()
        {

        }

        public IImageProvider AddEffect(IImageProvider provider)
        {
            return _effect;
        }
    }
}

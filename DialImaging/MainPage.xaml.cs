using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using DialImaging.Effects;
using Lumia.Imaging;
using BlurEffect = DialImaging.Effects.BlurEffect;


namespace DialImaging
{
    public sealed partial class MainPage
    {
        private class TAGS
        {
            internal const string Image = "1-Image";
            internal const string Blur = "6-Blur";
            internal const string Fog = "5-Fog";
            internal const string Hue = "2-Hue";
            internal const string Saturation = "3-Saturation";
            internal const string Sepia = "4-Sepia";
        }

        private StorageFile _storagefile;

        private RadialController _radialController;

        private readonly SortedList<string, IEffect> _effectChain = new SortedList<string, IEffect>();

        private IEffect _currentEffect;


        public MainPage()
        {
            this.InitializeComponent();

            InitializeSurfaceDial();
        }

        private void InitializeSurfaceDial()
        {
            _radialController = RadialController.CreateForCurrentView();

            _radialController.ButtonClicked += _radialController_ButtonClicked;
            _radialController.RotationChanged += _radialController_RotationChanged;

            var bluricon = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Blur.png"));
            var fogicon = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Fog.png"));
            var hueicon = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Hue.png"));
            var saturationicon = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Saturation.png"));
            var sepiaicon = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Sepia.png"));

            var blurItem = RadialControllerMenuItem.CreateFromIcon(TAGS.Blur, bluricon);
            blurItem.Tag = blurItem.DisplayText;
            var fogItem = RadialControllerMenuItem.CreateFromIcon(TAGS.Fog, fogicon);
            fogItem.Tag = fogItem.DisplayText;
            var hueItem = RadialControllerMenuItem.CreateFromIcon(TAGS.Hue, hueicon);
            hueItem.Tag = hueItem.DisplayText;
            var saturationItem = RadialControllerMenuItem.CreateFromIcon(TAGS.Saturation, saturationicon);
            saturationItem.Tag = saturationItem.DisplayText;
            var sepiaItem = RadialControllerMenuItem.CreateFromIcon(TAGS.Sepia, sepiaicon);
            sepiaItem.Tag = sepiaItem.DisplayText;

            _radialController.Menu.Items.Add(blurItem);
            _radialController.Menu.Items.Add(fogItem);
            _radialController.Menu.Items.Add(hueItem);
            _radialController.Menu.Items.Add(saturationItem);
            _radialController.Menu.Items.Add(sepiaItem);


        }

        private async void _radialController_RotationChanged(RadialController sender,
            RadialControllerRotationChangedEventArgs args)
        {
            if (args.RotationDeltaInDegrees > 0)
                _currentEffect.IncreaseEffect();
            else
                _currentEffect.DecreaseEffect();

            EffectValue.Text = _currentEffect.Value;

            await RenderAsync();
        }

        private void _radialController_ButtonClicked(RadialController sender,
            RadialControllerButtonClickedEventArgs args)
        {
            var selecteditem = sender.Menu.GetSelectedMenuItem();

            _currentEffect = _effectChain[selecteditem.Tag.ToString()];

            EffectName.Text = selecteditem.Tag.ToString();
            EffectValue.Text = _currentEffect.Value;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var assetsfolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");

            _storagefile = await assetsfolder.GetFileAsync("Night City Glow Wallpapers 1.jpg");

            _effectChain.Add(TAGS.Image, new ImageEffect(_storagefile));
            _effectChain.Add(TAGS.Blur, new BlurEffect());
            _effectChain.Add(TAGS.Fog, new FogEffect());
            _effectChain.Add(TAGS.Hue, new HueEffect());
            _effectChain.Add(TAGS.Saturation, new SaturationEffect());
            _effectChain.Add(TAGS.Sepia, new SepiaEffect());

            _currentEffect = _effectChain[TAGS.Image];
        }

        private async Task RenderAsync()
        {
            IImageProvider effect = null;
            WriteableBitmap writeableBitmap = new WriteableBitmap(800, 800);

            for (var i = 0; i < _effectChain.Count; i++)
            {
                effect = _effectChain.Values[i].AddEffect(effect);
            }

            Image.Source = await new WriteableBitmapRenderer(effect, writeableBitmap).RenderAsync();
        }
    }
}

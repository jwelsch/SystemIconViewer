namespace SystemIconViewer
{
    public class ImageListDrawOptions
    {
        public bool Transparent { get; }

        public bool Blend { get; }

        public bool Selected { get; }

        public ImageListDrawOptions(bool transparent, bool blend, bool selected)
        {
            Transparent = transparent;
            Blend = blend;
            Selected = selected;
        }
    }
}

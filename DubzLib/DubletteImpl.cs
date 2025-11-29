namespace DubzLib
{
    internal class DubletteImpl : IDublette
    {
        private readonly IReadOnlyCollection<string> _dateipfade;

        public DubletteImpl(IReadOnlyCollection<string> dateipfade)
        {
            _dateipfade = dateipfade;
        }

        public IReadOnlyCollection<string> Dateipfade => _dateipfade;
    }
}
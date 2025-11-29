namespace DubzLib
{
    public interface IDublettenpruefung
    {
        IReadOnlyCollection<IDublette> SammleKandidaten(string pfad);
        IReadOnlyCollection<IDublette> SammleKandidaten(string pfad, Vergleichsmodi modus);
        IReadOnlyCollection<IDublette> PruefeKandidaten(IEnumerable<IDublette> kandidaten);
    }
}
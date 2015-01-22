namespace FlockingBirds.Configurator.Dependencies
{
    public interface ICompressingService
    {
        string Compress(string text);

        string Decompress(string text);
    }
}

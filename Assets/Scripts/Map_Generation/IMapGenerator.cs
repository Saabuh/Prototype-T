namespace Prototype_S
{
    /// <summary>
    /// Map Generator interface for map generator implementation
    /// </summary>
    public interface IMapGenerator
    {
        void Generate();
        void RaiseMapGeneratedEvent(MapData mapData);
    }
}
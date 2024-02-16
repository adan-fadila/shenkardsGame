namespace Location_package
{
    public interface ILocation
    {
        string Name { get; }
        string Desc {get;}
        ILocationBattleStrategy battleStrategy { get; }
    }
}
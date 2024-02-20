namespace Location_package
{
    public interface ILocation
    {
        string Name { get; }
        string Desc {get;}

        string Image { get; set; }
        ILocationBattleStrategy battleStrategy { get; }
    }
}
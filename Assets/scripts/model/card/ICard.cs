using UnityEngine;

namespace Card_package
{
    public interface ICard
    {
        int id { get; }
        string Name
        {
            get;
        }
        string Desc
        {
            get;
        }
        int Cost
        {
            get;
            set;
        }
        int Power
        {
            get;
            set;
        }

        string Image
        {
            get;
            set;
        }
    }
}
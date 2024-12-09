using UnityEngine;

namespace URIMP
{
    public interface IModule
    {
        string Name { get; }
        void Load();
    }
}

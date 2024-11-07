using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URIMP
{
    public abstract class ContentBase : IContent
    {
        public string Id { get; private set; }
        public string Name { get; set; }

        protected ContentBase(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract void Load();
        public abstract void Save();
        public abstract void Delete();
        public ContentData ToContentData()
        {
            return new ContentData
            {
                Id = Id,
                Name = Name,
                Type = GetType().Name // Assuming type name matches the JSON type
            };
        }
    }
}

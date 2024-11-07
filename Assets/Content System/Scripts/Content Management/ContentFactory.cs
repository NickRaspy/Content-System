using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URIMP
{
    public static class ContentFactory
    {
        private static readonly Dictionary<string, Func<string, string, IContent>> contentCreators = new Dictionary<string, Func<string, string, IContent>>();

        public static void RegisterContentType<T>(string typeName) where T : IContent
        {
            contentCreators[typeName] = (id, name) => (IContent)Activator.CreateInstance(typeof(T), id, name);
        }

        public static IContent CreateContent(ContentData data)
        {
            if (contentCreators.TryGetValue(data.Type, out var creator))
            {
                return creator(data.Id, data.Name);
            }
            throw new InvalidOperationException($"Unknown content type: {data.Type}");
        }
    }
}

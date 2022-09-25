using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace WebApplication1
{
    internal sealed class CustomReferenceResolver : ReferenceResolver
    {
        private uint _referenceCount;
        private readonly Dictionary<string, object>? _referenceIdToObjectMap = new Dictionary<string, object>();
        private readonly Dictionary<object, string>? _objectToReferenceIdMap = new Dictionary<object, string>(ReferenceEqualityComparer.Instance);

        public override void AddReference(string referenceId, object value)
        {
            if (!TryAdd(_referenceIdToObjectMap, referenceId, value))
            {
            }
        }

        public override string GetReference(object value, out bool alreadyExists)
        {
                  if (_objectToReferenceIdMap.TryGetValue(value, out string? referenceId))
            {
                alreadyExists = true;
            }
            else
            {
                _referenceCount++;
                referenceId = _referenceCount.ToString();
                _objectToReferenceIdMap.Add(value, referenceId);
                alreadyExists = false;
            }

            return referenceId;
        }

        public override object ResolveReference(string referenceId)
        {
            if (!_referenceIdToObjectMap.TryGetValue(referenceId, out object? value))
            {
            }

            return value;
        }

        public static bool TryAdd<TKey, TValue>(Dictionary<TKey, TValue> dictionary, in TKey key, in TValue value) where TKey : notnull
        {
#if NETSTANDARD2_0 || NETFRAMEWORK
            if (!dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
                return true;
            }

            return false;
#else
            return dictionary.TryAdd(key, value);
#endif
        }
    }
}

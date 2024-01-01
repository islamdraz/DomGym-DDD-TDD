using System.Text.Json;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DomeGym.Infrastructure.Persistence.Converters;

// NOTE: SQLite doesn't support JSON columns yet. Otherwise, we'd prefer calling .ToJson() on the owned entity instead.
public class ValueJsonConverter<T> : ValueConverter<T, string>
{
    public ValueJsonConverter(ConverterMappingHints? mappingHints = null)
        : base(
            v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
            v => JsonSerializer.Deserialize<T>(v, JsonSerializerOptions.Default)!,
            mappingHints)
    {
    }
}

// NOTE: SQLite doesn't support JSON columns yet. Otherwise, we'd prefer calling .ToJson() on the owned entity instead.
public class ValueJsonComparer<T> : ValueComparer<T>
{
    public ValueJsonComparer() : base(
      (l, r) => JsonSerializer.Serialize(l, JsonSerializerOptions.Default) == JsonSerializer.Serialize(r, JsonSerializerOptions.Default),
      v => v == null ? 0 : JsonSerializer.Serialize(v, JsonSerializerOptions.Default).GetHashCode(),
      v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, JsonSerializerOptions.Default), JsonSerializerOptions.Default)!)
    {
    }
}
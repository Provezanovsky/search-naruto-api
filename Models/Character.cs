using System.Text.Json;

namespace SearchNarutoAPI.Models;

public sealed record CharacterPage
{
    public Character[] Characters { get; init; } = [];
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }
    public int Total { get; init; }
}

public sealed record Character
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public string[]? Images { get; init; }
    public string[]? Jutsu { get; init; }
    public PersonalInfo? Personal { get; init; }
    public CharacterRank? Rank { get; init; }

    public IReadOnlyList<string> Clans => ReadLabels(Personal?.Clan ?? default);
    public IReadOnlyList<string> Affiliations => ReadLabels(Personal?.Affiliation ?? default);
    public IReadOnlyList<string> NinjaRanks => ReadLabels(Rank?.NinjaRank ?? default);

    // A API pode devolver texto, lista ou um objeto com os valores por fase.
    private static IReadOnlyList<string> ReadLabels(JsonElement value)
    {
        var labels = new List<string>();
        switch (value.ValueKind)
        {
            case JsonValueKind.String:
                if (value.GetString() is { } label && !string.IsNullOrWhiteSpace(label))
                {
                    labels.Add(label);
                }
                break;
            case JsonValueKind.Array:
                foreach (var item in value.EnumerateArray())
                {
                    labels.AddRange(ReadLabels(item));
                }
                break;
            case JsonValueKind.Object:
                foreach (var item in value.EnumerateObject())
                {
                    var itemLabels = ReadLabels(item.Value);
                    if (itemLabels.Count > 0)
                    {
                        labels.Add($"{item.Name}: {string.Join(", ", itemLabels)}");
                    }
                }
                break;
        }
        return labels;
    }
}

public sealed record PersonalInfo
{
    public JsonElement Clan { get; init; }
    public JsonElement Affiliation { get; init; }
}

public sealed record CharacterRank
{
    public JsonElement NinjaRank { get; init; }
}

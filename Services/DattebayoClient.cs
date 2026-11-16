using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using SearchNarutoAPI.Models;

namespace SearchNarutoAPI.Services;

public sealed class DattebayoClient(HttpClient http)
{
    public async Task<CharacterPage> ListAsync(int page, int pageSize, string name, CancellationToken token)
    {
        var endpoint = $"characters?page={page}&limit={pageSize}";
        if (!string.IsNullOrWhiteSpace(name))
        {
            endpoint += $"&name={Uri.EscapeDataString(name)}";
        }

        return await http.GetFromJsonAsync(endpoint, DattebayoJson.Default.CharacterPage, token)
            ?? throw new InvalidDataException("A API devolveu uma lista vazia.");
    }

    public async Task<Character> GetAsync(int id, CancellationToken token)
    {
        return await http.GetFromJsonAsync($"characters/{id}", DattebayoJson.Default.Character, token)
            ?? throw new InvalidDataException("A API não devolveu o personagem solicitado.");
    }
}

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(CharacterPage))]
[JsonSerializable(typeof(Character))]
internal partial class DattebayoJson : JsonSerializerContext;

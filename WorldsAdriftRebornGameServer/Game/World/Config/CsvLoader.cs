using System.Collections;

namespace WorldsAdriftRebornGameServer.Game.World.Config;

public abstract class CsvLoader<T> : IReadOnlyList<T>
{
    private readonly List<T> _items = new List<T>();

    protected abstract T ParseRow(string[] headers, string[] values);

    public static TLoader Load<TLoader>(string path) where TLoader : CsvLoader<T>, new()
    {
        if (!File.Exists(path))
            throw new FileNotFoundException(path);

        var lines = File.ReadAllLines(path);
        if (lines.Length == 0)
            throw new InvalidDataException("CSV file is empty");

        var headers = Split(lines[0]);
        var loader = new TLoader();

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            var values = Split(lines[i]);
            loader._items.Add(loader.ParseRow(headers, values));
        }

        return loader;
    }

    protected static string[] Split(string line)
    {
        var fields = new List<string>();
        var sb = new System.Text.StringBuilder();

        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            switch (c)
            {
                case '"' when inQuotes && i + 1 < line.Length && line[i + 1] == '"':
                    sb.Append('"');
                    i++;
                    break;
                case '"':
                    inQuotes = !inQuotes;
                    break;
                case ',' when !inQuotes:
                    fields.Add(sb.ToString());
                    sb.Clear();
                    break;
                default:
                    sb.Append(c);
                    break;
            }
        }

        fields.Add(sb.ToString());
        return fields.ToArray();
    }

    public int Count => _items.Count;
    public T this[int index] => _items[index];

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

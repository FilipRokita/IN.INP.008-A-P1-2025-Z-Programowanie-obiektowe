using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class JsonContactRepository
{
    private readonly string _path;

    public JsonContactRepository(string path)
    {
        _path = path;
    }

    public List<Contact> Load()
    {
        if (!File.Exists(_path))
            return new List<Contact>();

        var json = File.ReadAllText(_path);

        var data = JsonSerializer.Deserialize<List<Contact>>(json);
        return data ?? new List<Contact>();
    }

    public void Save(List<Contact> contacts)
    {
        var json = JsonSerializer.Serialize(contacts, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(_path, json);
    }
}

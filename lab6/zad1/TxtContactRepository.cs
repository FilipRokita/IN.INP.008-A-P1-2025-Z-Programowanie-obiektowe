using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TxtContactRepository
{
    private readonly string _path;

    public TxtContactRepository(string path)
    {
        _path = path;
    }

    public List<Contact> Load()
    {
        var result = new List<Contact>();

        if (!File.Exists(_path))
            return result;

        var lines = File.ReadAllLines(_path);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(';');
            if (parts.Length != 3) continue;

            if (!int.TryParse(parts[0], out int id)) continue;

            result.Add(new Contact
            {
                Id = id,
                Name = parts[1],
                Email = parts[2]
            });
        }

        return result;
    }

    public void Save(List<Contact> contacts)
    {
        var lines = contacts
            .OrderBy(c => c.Id)
            .Select(c => $"{c.Id};{c.Name};{c.Email}")
            .ToArray();

        File.WriteAllLines(_path, lines);
    }
}

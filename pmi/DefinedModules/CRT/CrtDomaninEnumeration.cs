using System.Text.Json;

namespace pmi.DefinedModules.CRT;

public class CrtDomainEnumeration
{
    private string _target { get; set; }
    private readonly string URL = "https://crt.sh/json?q=";

    public CrtDomainEnumeration(string target)
    {
        _target = target;
    }
    public async Task<string> Execute()
    {
        using var client = new HttpClient();

        var response = await client.GetAsync($"{URL}{_target}");
        response.EnsureSuccessStatusCode();
        WriteLine("EXECUTING CRT");

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<List<CertificateEntry>>(json);
        result.ForEach(e => Console.WriteLine(e));
        return string.Join(Environment.NewLine, result);
    }

}
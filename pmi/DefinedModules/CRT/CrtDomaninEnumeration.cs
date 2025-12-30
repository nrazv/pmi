using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using pmi.DefinedModules.BackgroundJob;
using pmi.DefinedModules.Models;
using pmi.Project.Models;
using pmi.Project.Service;
using pmi.Subdomain.Models;
using pmi.Subdomain.Service;
using pmi.Tool.Managers;

namespace pmi.DefinedModules.CRT;

public class CrtDomainEnumeration : IModuleBackgroundJob
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ModuleExecutionRequest _request;
    private ProcessManager ProcessM = new ProcessManager();

    public Guid ExecutionId { get; }

    private readonly string URL = "https://crt.sh/json?q=";

    private Regex EmailMatching;
    public CrtDomainEnumeration(Guid executionId, ModuleExecutionRequest request, IServiceScopeFactory scopeFactory)
    {
        ExecutionId = executionId;
        _request = request;
        _scopeFactory = scopeFactory;
        EmailMatching = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }


    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 Gecko Firefox/140.0");

        var response = await client.GetAsync($"{URL}{_request.Target}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<List<CertificateEntry>>(json);
        if (result?.Count > 0)
        {
            await UpdateProjectSubdomains(result);
        }
    }

    private async Task UpdateProjectSubdomains(List<CertificateEntry> certificateEntries)
    {
        using var eventScope = _scopeFactory.CreateScope();
        var projectService = eventScope.ServiceProvider.GetRequiredService<IProjectService>();
        var project = await projectService.GetByName(_request.ProjectName);

        if (project is null) { return; }
        List<string> extractedSubdomains = new List<string>();

        foreach (var entry in certificateEntries)
        {
            foreach (var domain in ExtractDomains(entry))
            {
                if (!extractedSubdomains.Contains(domain))
                {
                    extractedSubdomains.Add(domain);
                }
            }
        }

        foreach (var domain in extractedSubdomains)
        {
            var validDomain = await ValidateDomain(domain);
            if (validDomain is not null)
            {
                if (EmailMatching.IsMatch(validDomain))
                {
                    continue;
                }

                var subdomainService = eventScope.ServiceProvider.GetRequiredService<ISubdomainService>();
                var newDomain = NewSubdomain(project, domain, "whois", validDomain);
                WriteLine($"\nSaving to the database: {domain} \n");
                await subdomainService.AddNew(newDomain);
            }
        }
    }

    private SubdomainEntity NewSubdomain(ProjectEntity project, string domain, string validationTool, string validationResponse)
    {
        return new SubdomainEntity
        {
            Id = Guid.NewGuid(),
            Domain = domain,
            ProjectId = project.Id,
            Validated = true,
            ValidatedBy = validationTool,
            ValidationResult = validationResponse,
        };
    }

    private static IEnumerable<string> ExtractDomains(CertificateEntry entry)
    {
        foreach (var value in new[] { entry.NameValue, entry.CommonName })
        {
            if (string.IsNullOrWhiteSpace(value))
                continue;

            var split = value
                .Split(new[] { '\n', ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var s in split)
                yield return s;
        }
    }

    private async Task<string?> ValidateDomain(string domain)
    {
        bool valid = false;
        var process = ProcessM.CreateNewProcess(tool: "whois", "", domain);
        var stdout = "";

        process.OutputDataReceived += async (s, e) =>
        {
            if (e.Data is null)
            {
                return;
            }
            stdout += $"\n{e.Data}\n";
        };

        process.ErrorDataReceived += async (s, e) => { };
        process.Exited += async (s, e) =>
        {

            string errorMessage = "No match";
            if (!stdout.ToString().Contains(errorMessage))
            {
                valid = true;
            }
        };

        try
        {
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            await process.WaitForExitAsync();

        }
        catch (Exception e)
        {

        }
        finally
        {

            process.Close();
            process.Dispose();
        }

        if (valid)
        {
            return stdout;
        }
        else
        {
            return null;
        }
    }
}


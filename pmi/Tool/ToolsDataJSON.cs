using System.Text.Json;
using pmi.Tool;

namespace pmi.DataContext;

public class ToolsDataJSON
{

    public List<InstalledTool> InstalledTools { get; } = new List<InstalledTool>();

    public ToolsDataJSON()
    {
        using (StreamReader r = new StreamReader("installedTools.json"))
        {
            string json = r.ReadToEnd();
            var data = JsonSerializer.Deserialize<List<InstalledTool>>(json);
            if (data != null)
            {
                InstalledTools = data;
            }
        }
    }


}
using Microsoft.AspNetCore.Mvc;
using pmi.Models;

namespace pmi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private static readonly List<TestModel> _models = new()
    {
        new TestModel(){Id  = "45398eyd", Name = "Test Model"},
        new TestModel(){Id= "423ee5398eyd", Name = "Random String"},
    };

    [HttpGet(Name = "Get All")]
    public IEnumerable<TestModel> GetAll()
    {
        return _models;
    }
}

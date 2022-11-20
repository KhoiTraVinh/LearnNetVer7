using MessageBrokerRBMQ.Models;
using MessageBrokerRBMQ.Services;
using Microsoft.AspNetCore.Mvc;

namespace MessageBrokerRBMQ.Controllers;

[ApiController]
[Route("[controller]")]
public class MesController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<MesController> _logger;

    private readonly IMesProducer _mesProducer;

    private readonly IMesConsumer _mesConsumer;


    private static readonly List<Mes> _mes = new();

    public MesController(ILogger<MesController> logger, IMesProducer mesProducer, IMesConsumer mesConsumer)
    {
        _logger = logger;
        _mesProducer = mesProducer;
        _mesConsumer = mesConsumer;
    }

    [HttpPost]
    public IActionResult CreateMes(Mes message){
        if(!ModelState.IsValid) return BadRequest();

        _mes.Add(message);

        _mesProducer.SendingMes<Mes>(message);

        _mesConsumer.Recive<Mes>();

        return Ok();
    }

}

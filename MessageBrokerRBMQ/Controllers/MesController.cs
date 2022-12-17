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


    private readonly IPub _pub;

    private readonly ISub _sub;


    private static readonly List<Mes> _mes = new();

    public MesController(ILogger<MesController> logger, IMesProducer mesProducer, IMesConsumer mesConsumer, IPub pub, ISub sub)
    {
        _logger = logger;
        _mesProducer = mesProducer;
        _mesConsumer = mesConsumer;
        _pub = pub;
        _sub = sub;
    }

    [HttpPost]
    public IActionResult CreateMes(Mes message){
        if(!ModelState.IsValid) return BadRequest();

        _mes.Add(message);


        _pub.Send<Mes>(message);
        _sub.Recive<Mes>();
        // _mesProducer.SendingMes<Mes>(message);

        // _mesConsumer.Recive<Mes>();

        return Ok();
    }

}

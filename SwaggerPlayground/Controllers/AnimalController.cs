using System;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NJsonSchema.Converters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SwaggerPlayground.Controllers
{
    public class Container
    {
        public Animal Animal { get; set; }
    }

    [JsonConverter(typeof(JsonInheritanceConverter), "discriminator")]
    [KnownType(typeof(Dog))]
    public class Animal
    {
        public int Age { get; set; }
    }

    public class Dog : Animal
    {
        public string Bark { get; set; } = "Bark";
    }

    public class Cat : Animal
    {
        public string Mieow { get; set; } = "Mieow";
    }

    // see https://github.com/RicoSuter/NJsonSchema/wiki/Inheritance

    [Route("api/[controller]")]
    public class AnimalController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public Animal Get(string type) => (type?.ToLowerInvariant()) switch
        {
            "cat" => new Cat(),
            "dog" => new Dog(),
            _ => throw new NotSupportedException(),
        };
    }
}

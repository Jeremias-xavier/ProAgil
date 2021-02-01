using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.WebAPI.Data;
using ProAgil.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public DataContext _contexto { get; }

        public ValuesController(DataContext contexto)
        {
            _contexto = contexto;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _contexto.Eventos.ToListAsync();

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            
            }
            
        
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id)
        {
            return await _contexto.Eventos.FirstOrDefaultAsync(x=> x.EventoID == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

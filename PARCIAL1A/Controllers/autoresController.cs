using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class autoresController : ControllerBase
    {

        private readonly autoresContext _autoresContexto;

        public autoresController(autoresContext autoresContexto)
        {
            _autoresContexto = autoresContexto;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<autores> ListadoAutores = (from e in _autoresContexto.autores
                                           select e).ToList();
            if (ListadoAutores.Count == 0)
            { return NotFound(); }

            return Ok(ListadoAutores);

        }


        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            autores? autor = (from e in _autoresContexto?.autores where e.id == id select e).FirstOrDefault();

            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        //BUSCAR POR DESCRIPCION 
        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescription(string filtro)
        {
            autores? autor = (from e in _autoresContexto.autores where e.nombre.Contains(filtro) select e).FirstOrDefault();

            if (autor == null)
            { return NotFound(); }

            return Ok(autor);
        }




        //crear

        [HttpPost]
        [Route("add")]
        public IActionResult GuardarEquipo([FromBody] autores autores)
        {
            try

            {

                _autoresContexto.autores.Add(autores);
                _autoresContexto.SaveChanges();

                return Ok(autores);

            }

            catch (Exception ex)

            {
                return BadRequest(ex.Message);
            }


        }


        //modificar

        [HttpPut]
        [Route("actualizar/{id}")]
        public ActionResult ActualizarEquipo(int id, [FromBody] autores autorModificar)
        {
            autores? autorActual = (from e in _autoresContexto.autores where e.id == id select e).FirstOrDefault();

            if (autorActual == null)
            {
                return NotFound();
            }

            autorActual.nombre = autorModificar.nombre;
           
           
           

            _autoresContexto.Entry(autorActual).State = EntityState.Modified;

            _autoresContexto.SaveChanges();

            return Ok(autorActual);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public ActionResult EliminarEquipo(int id)
        {
            autores? autor = (from e in _autoresContexto.autores where e.id == id select e).FirstOrDefault();

            // Verificamos que exista el registro según su ID
            if (autor == null)
            {
                return NotFound();
            }

            _autoresContexto.autores.Attach(autor);
            _autoresContexto.autores.Remove(autor);
            _autoresContexto.SaveChanges();

            return Ok(autor);
        }



    }
}

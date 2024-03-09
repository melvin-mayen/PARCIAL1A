using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorLibroController : ControllerBase
    {

        private readonly autoresContext _autoresContexto;

        public AutorLibroController(autoresContext autoresContexto)
        {
            _autoresContexto = autoresContexto;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<AutorLibro> ListadoAutores = (from e in _autoresContexto.AutorLibro
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


        //crear

        [HttpPost]
        [Route("add")]
        public IActionResult GuardarAutor([FromBody] AutorLibro autorLibro)
        {
            try

            {

                _autoresContexto.AutorLibro.Add(autorLibro);
                _autoresContexto.SaveChanges();

                return Ok(autorLibro);

            }

            catch (Exception ex)

            {
                return BadRequest(ex.Message);
            }


        }


        //modificar

        [HttpPut]
        [Route("actualizar/{id}")]
        public ActionResult ActualizarAutores(int id, [FromBody] AutorLibro autorLibroModificar)
        {
            AutorLibro? autorActual = (from e in _autoresContexto.AutorLibro where e.AutorId == id select e).FirstOrDefault();

            if (autorActual == null)
            {
                return NotFound();
            }

            autorActual.AutorId = autorLibroModificar.Orden;




            _autoresContexto.Entry(autorActual).State = EntityState.Modified;

            _autoresContexto.SaveChanges();

            return Ok(autorActual);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public ActionResult EliminarAutor(int id)
        {
            AutorLibro? autorLibro = (from e in _autoresContexto.AutorLibro where e.LibroId == id select e).FirstOrDefault();

            // Verificamos que exista el registro según su ID
            if (autorLibro == null)
            {
                return NotFound();
            }

            _autoresContexto.AutorLibro.Attach(autorLibro);
            _autoresContexto.AutorLibro.Remove(autorLibro);
            _autoresContexto.SaveChanges();

            return Ok(autorLibro);
        }

    }
}

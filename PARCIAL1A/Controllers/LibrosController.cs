using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {

        private readonly autoresContext _autoresContexto;

        public LibrosController(autoresContext autoresContexto)
        {
            _autoresContexto = autoresContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Libros> ListadoLibros = (from e in _autoresContexto.Libros
                                            select e).ToList();
            if (ListadoLibros.Count == 0)
            { return NotFound(); }

            return Ok(ListadoLibros);

        }



        [HttpGet]
        [Route("FiltrarPorAutor/{filter}")]
        public IActionResult findByName(String filter)
        {
            var listadoEquipo = (from au in _autoresContexto.autores
                                 join auLi in _autoresContexto.AutorLibro
                                    on au.id equals auLi.AutorId
                                 join li in _autoresContexto.Libros
                                    on auLi.LibroId equals li.Id
                                 where au.nombre.Contains(filter)
                                 select new
                                 {
                                     idLibro = li.Id,
                                     tituloLibro = li.Titulo,
                                     autorLibro = au.nombre

                                 }).ToList();
            if (listadoEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);

             
            
            
            }


            //crear

            [HttpPost]
        [Route("add")]
        public IActionResult GuardarAutor([FromBody] Libros autorLibro)
        {
            try

            {

                _autoresContexto.Libros.Add(autorLibro);
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
        public ActionResult ActualizarLibros(int id, [FromBody] Libros LibrosModificar)
        {
            Libros? LibroActual = (from e in _autoresContexto.Libros where e.Id == id select e).FirstOrDefault();

            if (LibroActual == null)
            {
                return NotFound();
            }

            LibroActual.Titulo = LibrosModificar.Titulo;




            _autoresContexto.Entry(LibroActual).State = EntityState.Modified;

            _autoresContexto.SaveChanges();

            return Ok(LibroActual);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public ActionResult EliminarLibro(int id)
        {
            Libros? Libro = (from e in _autoresContexto.Libros where e.Id == id select e).FirstOrDefault();

            // Verificamos que exista el registro según su ID
            if (Libro == null)
            {
                return NotFound();
            }

            _autoresContexto.Libros.Attach(Libro);
            _autoresContexto.Libros.Remove(Libro);
            _autoresContexto.SaveChanges();

            return Ok(Libro);
        }


    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly autoresContext _autoresContexto;

        public PostsController(autoresContext autoresContexto)
        {
            _autoresContexto = autoresContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Posts> ListadoPosts = (from e in _autoresContexto.Posts
                                          select e).ToList();
            if (ListadoPosts.Count == 0)
            { return NotFound(); }

            return Ok(ListadoPosts);

        }


        [HttpGet]
        [Route("FiltrarPorLibroo/{filter}")]
        public IActionResult FiltrarPorLibro(String filter)
        {
            var listadoPost = (from li in _autoresContexto.Libros
                               join auli in _autoresContexto.AutorLibro
                                  on li.Id equals auli.LibroId
                               join au in _autoresContexto.autores
                                  on auli.AutorId equals au.id
                               join ps in _autoresContexto.Posts
                                  on au.id equals ps.id
                               where li.Titulo.Contains(filter)
                               select new
                               {
                                   idPosts = ps.id,
                                   TituloPosts = ps.Titulo,
                                   contenidoPosts = ps.Contenido,
                                   fechaPublicacion = ps.FechaPublicacion,
                                   nombreAutor = au.nombre,
                                   libroTitulo = li.Titulo

                               }).ToList();
            if (listadoPost.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPost);
        }


        //crear

        [HttpPost]
        [Route("add")]
        public IActionResult GuardarPosts([FromBody] Posts posts)
        {
            try

            {

                _autoresContexto.Posts.Add(posts);
                _autoresContexto.SaveChanges();

                return Ok(posts);

            }

            catch (Exception ex)

            {
                return BadRequest(ex.Message);
            }


        }


        //modificar

        [HttpPut]
        [Route("actualizar/{id}")]
        public ActionResult ActualizarPosts(int id, [FromBody] Libros PostsModificar)
        {
            Posts? PostActual = (from e in _autoresContexto.Posts where e.id == id select e).FirstOrDefault();

            if (PostActual == null)
            {
                return NotFound();
            }

            PostActual.Titulo = PostsModificar.Titulo;




            _autoresContexto.Entry(PostActual).State = EntityState.Modified;

            _autoresContexto.SaveChanges();

            return Ok(PostActual);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public ActionResult EliminarPost(int id)
        {
            Posts? Post = (from e in _autoresContexto.Posts where e.id == id select e).FirstOrDefault();

            // Verificamos que exista el registro según su ID
            if (Post == null)
            {
                return NotFound();
            }

            _autoresContexto.Posts.Attach(Post);
            _autoresContexto.Posts.Remove(Post);
            _autoresContexto.SaveChanges();

            return Ok(Post);
        }

    }
}

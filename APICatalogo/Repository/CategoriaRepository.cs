using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly APICatalogoContext _context;

        public CategoriaRepository(APICatalogoContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            return GetCategorias = _context.Categorias.ToList();
        }

        public Categoria GetCategoria(int id)
        {
            return GetCategoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
        }

        public Categoria Create(Categoria categoria)
        {

        }

        public Categoria Update(Categoria categoria)
        {

        }

        public Categoria Delete(Categoria categoria)
        {
            
        }
    }
}

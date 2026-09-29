using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>,ICategoriaRepository
    {
        public CategoriaRepository(APICatalogoContext context) : base(context) 
        { 
        }
    }
}

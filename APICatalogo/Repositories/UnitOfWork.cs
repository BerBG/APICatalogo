using APICatalogo.Context;
using APICatalogo.Repository;

namespace APICatalogo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository _produtoRepo;
        private ICategoriaRepository _categoriaRepo;

        public APICatalogoContext _context;
        public UnitOfWork(APICatalogoContext context)
        {
            _context = context;
        }

        // Lazy Loading: adiar obtenção de dados até que sejam realmente necessários
        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
            }
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

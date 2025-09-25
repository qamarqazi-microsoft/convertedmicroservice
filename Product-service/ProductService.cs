using System.Threading.Tasks;

namespace Product
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        // Placeholder for Product business logic methods
    }
}


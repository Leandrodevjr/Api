using Exo.WebApi.Contexts;
using Exo.WebApi.Models;

namespace Exo.WebApi.Repositories
{
    public class ProjetoRepository
    {
        private readonly ExoContext _context;

        // Construtor: Injeta a conexão com o banco de dados (ExoContext)
        public ProjetoRepository(ExoContext context)
        {
            _context = context;
        }

        // Método Read: Lista todos os projetos no banco de dados.
        public List<Projeto> Listar()
        {
            // Retorna todos os registros da tabela Projetos
            return _context.Projetos.ToList(); 
        }

        // Método Create: Cadastra um novo projeto no banco de dados.
        public void Cadastrar(Projeto projeto)
        {
            // Adiciona o novo projeto ao conjunto (DbSet) de Projetos no contexto
            _context.Projetos.Add(projeto);

            // Salva as mudanças pendentes no banco de dados
            _context.SaveChanges();
        }

        // Método Read (Unitário): Busca um projeto pelo ID.
        public Projeto BuscarPorId(int id)
        {
            // Usa o método Find() que é otimizado para buscar pela chave primária (Id)
            // Se não encontrar, retorna null (o '!' suprime o aviso de nullability)
            return _context.Projetos.Find(id)!;
        }
        
        // Os métodos Atualizar e Deletar podem ser adicionados em seguida.
    }
}

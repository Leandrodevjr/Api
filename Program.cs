using Exo.WebApi.Contexts;
using Exo.WebApi.Repositories; // Importação adicionada, pois a classe foi movida para um arquivo separado
using Microsoft.OpenApi.Models; // Necessário para a classe OpenApiInfo

var builder = WebApplication.CreateBuilder(args);

// ====================================================================
// PASSO 1: Adicionar Serviços do Swagger/OpenAPI
// ====================================================================
// Adiciona o serviço que explora os endpoints da API
builder.Services.AddEndpointsApiExplorer();

// Adiciona o gerador do Swagger E CONFIGURA A VERSÃO
builder.Services.AddSwaggerGen(c =>
{
    // Configuração da informação de metadados da API para o Swagger UI
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Exo Web API", Version = "v1" });
});

// Configurações de serviços existentes
// Se a conexão estiver dentro da classe ExoContext (método OnConfiguring), esta linha é suficiente:
builder.Services.AddScoped<ExoContext, ExoContext>();

/* Injeção de dependência */
builder.Services.AddControllers();
// O repositório agora é importado via 'using' no topo.
builder.Services.AddTransient<ProjetoRepository, ProjetoRepository>();

var app = builder.Build();

// ====================================================================
// PASSO 2 & 3: Adicionar Middlewares do Swagger
// ====================================================================
// Habilita o Swagger apenas no ambiente de desenvolvimento.
if (app.Environment.IsDevelopment())
{
    // Middleware que gera o documento JSON do Swagger
    app.UseSwagger(); 
    
    // Middleware que serve a interface do usuário (UI) do Swagger
    app.UseSwaggerUI(c =>
    {
        // Define o endpoint do documento Swagger e o título na UI
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exo Web API V1");
        
        // Se quiser que o Swagger UI abra na raiz da aplicação (ex: http://localhost:<porta>/), descomente a linha abaixo:
        // c.RoutePrefix = string.Empty; 
    });
}

// Configurações de Middlewares existentes
app.UseRouting();

// É crucial que a autorização venha antes do MapControllers, se estiver usando
// app.UseAuthentication();
// app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

// A CLASSE DUPLICADA 'internal class ProjetoRepository { ... }' FOI REMOVIDA DAQUI.

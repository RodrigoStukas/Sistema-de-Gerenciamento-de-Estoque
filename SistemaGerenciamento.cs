using System;
using System.Collections.Generic;

class Program
{
    static List<Produto> estoque = new List<Produto>();

    static void Main(string[] args)
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Sistema de Gerenciamento de Estoque ===");
            Console.WriteLine("1. Adicionar Produto");
            Console.WriteLine("2. Listar Produtos");
            Console.WriteLine("3. Atualizar Produto");
            Console.WriteLine("4. Remover Produto");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1: AdicionarProduto(); break;
                case 2: ListarProdutos(); break;
                case 3: AtualizarProduto(); break;
                case 4: RemoverProduto(); break;
                case 0: Console.WriteLine("Saindo..."); break;
                default: Console.WriteLine("Opção inválida!"); break;
            }
        } while (opcao != 0);
    }

    static void AdicionarProduto()
    {
        Console.Clear();
        Console.Write("Nome do Produto: ");
        string nome = Console.ReadLine();
        Console.Write("Quantidade: ");
        int quantidade = int.Parse(Console.ReadLine());
        Console.Write("Preço: ");
        decimal preco = decimal.Parse(Console.ReadLine());

        estoque.Add(new Produto { Nome = nome, Quantidade = quantidade, Preco = preco });
        Console.WriteLine("Produto adicionado com sucesso!");
        Console.ReadKey();
    }

    static void ListarProdutos()
    {
        Console.Clear();
        if (estoque.Count == 0)
        {
            Console.WriteLine("Nenhum produto no estoque.");
        }
        else
        {
            Console.WriteLine("=== Lista de Produtos ===");
            foreach (var produto in estoque)
            {
                Console.WriteLine(produto);
            }
        }
        Console.ReadKey();
    }

    static void AtualizarProduto()
    {
        Console.Clear();
        Console.Write("Digite o nome do produto a ser atualizado: ");
        string nome = Console.ReadLine();

        var produto = estoque.Find(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        if (produto != null)
        {
            Console.Write("Nova Quantidade: ");
            produto.Quantidade = int.Parse(Console.ReadLine());
            Console.Write("Novo Preço: ");
            produto.Preco = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Produto atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
        Console.ReadKey();
    }

    static void RemoverProduto()
    {
        Console.Clear();
        Console.Write("Digite o nome do produto a ser removido: ");
        string nome = Console.ReadLine();

        var produto = estoque.Find(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        if (produto != null)
        {
            estoque.Remove(produto);
            Console.WriteLine("Produto removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
        Console.ReadKey();
    }
}

class Produto
{
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }

    public override string ToString()
    {
        return $"Nome: {Nome}, Quantidade: {Quantidade}, Preço: R$ {Preco:F2}";
    }
}

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços de Razor Pages
builder.Services.AddRazorPages();

// Adicionar o serviço para o Sistema de Estoque (API)
builder.Services.AddScoped<EstoqueService>();

var app = builder.Build();

// Configuração do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();  // Para mapear APIs (controladores)

app.Run();

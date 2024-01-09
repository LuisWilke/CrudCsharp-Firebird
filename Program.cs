using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

class Program
{
    static string connectionString = "User=sysdba;Password=masterkey;Database=c:/ecosis/dados/ecodados.eco;DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;Pooling=true;Connection Lifetime=0;Packet Size=8192;ServerType=Default;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("1. Listar Clientes");
            Console.WriteLine("2. Adicionar Cliente");
            Console.WriteLine("3. Atualizar Cliente");
            Console.WriteLine("4. Excluir Cliente");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();


            switch (opcao)
            {
                case "1":
                    ListarClientes();
                    break;
                case "2":
                    AdicionarCliente();
                    break;
                case "3":
                    AtualizarCliente();
                    break;
                case "4":
                    ExcluirCliente();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void ListarClientes()
    {
        using (var connection = new FbConnection(connectionString))
        {
            connection.Open();

            using (var command = new FbCommand("SELECT * FROM trecclientegeral", connection))
            {
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Codigo"]}\t{reader["Nome"]}\t{reader["cpfcnpj"]}");
                    }
                }
            }
        }
    }

    static void AdicionarCliente()
    {
        Console.Write("Digite o nome do usuário: ");
        string nome = Console.ReadLine();

        Console.Write("Digite o email do usuário: ");
        string email = Console.ReadLine();

        using (var connection = new FbConnection(connectionString))
        {
            connection.Open();

            using (var command = new FbCommand("INSERT INTO Trecclientegeral (Nome, Email) VALUES (@Nome, @email)", connection))
            {
                command.Parameters.AddWithValue("Nome", nome);
                command.Parameters.AddWithValue("cpfcnpj", email);

                command.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Usuário adicionado com sucesso.");
    }

    static void AtualizarCliente()
    {
        Console.Write("Digite o ID do Cliente que deseja atualizar: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Digite o novo nome do Cliente: ");
        string novoNome = Console.ReadLine();

        Console.Write("Digite o novo email do Cliente: ");
        string novoEmail = Console.ReadLine();

        using (var connection = new FbConnection(connectionString))
        {
            connection.Open();

            using (var command = new FbCommand("UPDATE treccliente SET Nome = @Nome, Email = @Email WHERE codigo = @codigo", connection))
            {
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.AddWithValue("Nome", novoNome);
                command.Parameters.AddWithValue("Email", novoEmail);

                command.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Cliente atualizado com sucesso.");
    }

    static void ExcluirCliente()
    {
        Console.Write("Digite o Codigo do cliente que deseja excluir: ");
        int id = int.Parse(Console.ReadLine());

        using (var connection = new FbConnection(connectionString))
        {
            connection.Open();

            using (var command = new FbCommand("DELETE FROM Usuarios WHERE codigo = @Codigo", connection))
            {
                command.Parameters.AddWithValue("Codigo", id);

                command.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Usuário excluído com sucesso.");
    }

}
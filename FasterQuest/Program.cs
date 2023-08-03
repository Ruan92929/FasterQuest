using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

class Program
{
    static int selectedOption = 1;
    static void Main(string[] args)
    {
        Jogo jogo = new Jogo();
        jogo.TelaCadastro();

    }
    class Jogo
    {
        private string nomeClasseEscolhida;
        private string ferramentaBatalhaEscolhida;
        private CustomizacaoAvatar customizacaoAvatarEscolhida;
        private Montaria montariaEscolhida;
        private List<Montaria> montariasDisponiveis;
        string nomeCompleto;
        DateTime dataNascimento;
        string email = "";
        string senha = "";

        public Jogo()
        {
                montariasDisponiveis = new List<Montaria>
            {
                new Montaria("Panda", 55, 65, 60, "Mana +5"),
                new Montaria("Cavalo", 75, 60, 55, "Stamina +3"),
                new Montaria("Tigre", 80, 40, 75, "Velocidade de Ataque +4"),
                new Montaria("Lobo", 70, 50, 65, "Ataque +6"),
                new Montaria("Rinoceronte", 55, 65, 65, "Vida +5")
            };
        }
        public void TelaCadastro()
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Bem-vindos ao FasterQuest!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Cadastre-se agora mesmo e torne-se uma lenda em nosso mundo de aventuras!");
            Console.ResetColor();
            Console.WriteLine();

            string nomeCompleto;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Nome completo: ");
                Console.ResetColor();
                nomeCompleto = Console.ReadLine();

                if (HasNumbers(nomeCompleto))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O nome não pode conter números. Tente novamente.");
                    Console.ResetColor();
                }
                else if (HasSpecialCharacters(nomeCompleto))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O nome não pode conter caracteres especiais. Tente novamente.");
                    Console.ResetColor();
                }
                else if (string.IsNullOrWhiteSpace(nomeCompleto))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O nome não pode estar vazio. Tente novamente.");
                    Console.ResetColor();
                }
            } while (HasNumbers(nomeCompleto) || HasSpecialCharacters(nomeCompleto) || string.IsNullOrWhiteSpace(nomeCompleto));

            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Data de nascimento (dd/mm/aaaa): ");
            Console.ResetColor();
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataNascimento))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Data inválida. Tente novamente.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Data de nascimento (dd/mm/aaaa): ");
                Console.ResetColor();
            }

            int idadeMinima = 18;
            if (DateTime.Today.AddYears(-idadeMinima) < dataNascimento)
            {
                Console.WriteLine($"Você deve ter no mínimo {idadeMinima} anos para se cadastrar.");
                return;
            }

            bool emailCorreto = false;

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Email: ");
                Console.ResetColor();
                email = Console.ReadLine();
                if (!IsValidEmail(email))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O formato do e-mail é inválido. Tente novamente.");
                    Console.ResetColor();
                }
                else
                {
                    emailCorreto = true;
                }
            } while (!emailCorreto);

            bool senhaCorreta = false;

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Senha: ");
                Console.ResetColor();
                senha = LerSenha();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Confirmar senha: ");
                Console.ResetColor();
                string confirmarSenha = LerSenha();

                if (senha != confirmarSenha)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("As senhas não são iguais. Tente novamente.");
                    Console.ResetColor();
                }
                else
                {
                    senhaCorreta = true;
                }
            } while (!senhaCorreta);


            Usuario novoUsuario = new Usuario
            {
                NomeCompleto = nomeCompleto,
                DataNascimento = dataNascimento,
                Email = email,
                Senha = senha
            };

            novoUsuario.RealizarCadastro();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cadastro realizado com sucesso!");
            Console.WriteLine("Redirecionando para a tela de Login");
            Console.ResetColor();

            for (int i = 5; i > 0; i--)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(i + "...");
                Console.ResetColor();
                Thread.Sleep(250);
            }

            Console.WriteLine();
            Console.ResetColor();

            Console.Clear();
            TelaLogin();

          

            }
        private bool HasNumbers(string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                    return true;
            }
            return false;
        }
        private bool HasSpecialCharacters(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    return true;
            }
            return false;
        }
        private static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
        private string LerSenha()
        {
            string senha = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (senha.Length > 0)
                    {
                        senha = senha.Substring(0, senha.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    senha += key.KeyChar;
                    Console.Write("*");
                }
            } while (true);

            Console.WriteLine();
            return senha;
        }
        public void TelaLogin()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Tela de login - FasterQuest");
            Console.ResetColor();
            Console.WriteLine();

            int numeroTentativas = 3;
            bool loginSucesso = false;

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Email: ");
                Console.ResetColor();
                string email = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Senha: ");
                Console.ResetColor();
                string senha = LerSenha();

                Usuario usuarioEncontrado = Usuario.BuscarUsuarioPorEmail(email);

                if (usuarioEncontrado != null && usuarioEncontrado.Senha == senha)
                {
                    loginSucesso = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Login realizado com sucesso!");

                    for (int i = 5; i > 0; i--)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(i + "... ");
                        Console.ResetColor();
                        Thread.Sleep(250);                    
                    }

                    Console.Clear();
                    EscolherClasse();
                }
                else
                {
                    numeroTentativas--;
                    if (numeroTentativas == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Número máximo de tentativas excedido. Encerrando o programa.");
                        Console.ResetColor();
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Credenciais incorretas. Você tem mais {numeroTentativas} tentativas.");
                        Console.ResetColor();
                    }
                }
            } while (!loginSucesso);

        }
        public void EscolherClasse()
        {
            ferramentaBatalhaEscolhida = null;

            List<string> classes = new List<string>
              {
                "Paladino",
                "Atirador",
                "Guerreiro",
                "Bárbaro",
                "Arqueiro"
            };

            int chosenClass = 0;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Para iniciar sua aventura escolha uma classe:");
                Console.WriteLine();
                for (int i = 0; i < classes.Count; i++)
                {
                    string additionalInfo = ExibirInformacoesClasse(classes[i]);

                    if (chosenClass == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{i + 1}) {classes[i],-15}");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(additionalInfo);
                        Console.ResetColor();


                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine($"  {i + 1}) {classes[i]}");
                    }
                }
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine();
                }
                Console.ForegroundColor= ConsoleColor.Red;
                Console.Write("DICA: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Utilize as setas do teclado cima \u2191 e baixo \u2193 para navegar pelas opções disponíveis. ");


                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    chosenClass--;
                    if (chosenClass < 0) chosenClass = classes.Count - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    chosenClass++;
                    if (chosenClass >= classes.Count) chosenClass = 0;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            switch (chosenClass)
            {
                case 0:

                    Paladino classPaladino = new Paladino();
                    nomeClasseEscolhida = "Paladino";
                    ClassePersonagem paladino = new ClassePersonagem("Paladino");
                    paladino.AdicionarFerramentaBatalha("Lança");
                    paladino.AdicionarFerramentaBatalha("Escudo");

                    ferramentaBatalhaEscolhida = EscolherArma(paladino.FerramentasBatalha, "Paladino");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Arma de batalha escolhida: {ferramentaBatalhaEscolhida} ");
                    Console.WriteLine();

                    Console.ResetColor();
                    classPaladino.MostrarAtributos();

                    CustomizarAvatar();
                    break;


                case 1:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Classe escolhida: Atirador, esta classe utiliza a arma Rifle de Precisão. ");
                    Console.ResetColor();
                    Atirador classAtirador = new Atirador();

                    nomeClasseEscolhida = "Atirador";
                    ClassePersonagem atirador = new ClassePersonagem("Atirador");
                    atirador.AdicionarFerramentaBatalha("Rifle de precisão");
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Arma de batalha escolhida: {atirador.FerramentasBatalha[0]}");
                    ferramentaBatalhaEscolhida = atirador.FerramentasBatalha[0];
                    Console.WriteLine();
                    classAtirador.MostrarAtributos();

                    CustomizarAvatar();
                    break;


                case 2:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Classe escolhida: Guerreiro, esta classe utiliza Espada e Escudo.");
                    Console.ResetColor();
                    Guerreiro classGuerreiro = new Guerreiro();

                    nomeClasseEscolhida = "Guerreiro";
                    ClassePersonagem guerreiro = new ClassePersonagem("Guerreiro");
                    guerreiro.AdicionarFerramentaBatalha("Espada");
                    guerreiro.AdicionarFerramentaBatalha("Escudo");
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Arma de batalha escolhida: {guerreiro.FerramentasBatalha[0]} e {guerreiro.FerramentasBatalha[1]}");
                    ferramentaBatalhaEscolhida = guerreiro.FerramentasBatalha[0];
                    Console.WriteLine();
                    classGuerreiro.MostrarAtributos();

                    CustomizarAvatar();
                    break;

                case 3:
                    Console.Clear();
                    Barbaro classBarbaro = new Barbaro();
                    nomeClasseEscolhida = "Bárbaro";
                    ClassePersonagem barbaro = new ClassePersonagem("Bárbaro");
                    barbaro.AdicionarFerramentaBatalha("Machado");
                    barbaro.AdicionarFerramentaBatalha("Marreta");


                    ferramentaBatalhaEscolhida = EscolherArma(barbaro.FerramentasBatalha, "Bárbaro");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Arma de batalha escolhida: {ferramentaBatalhaEscolhida}");
                    Console.WriteLine();

                    Console.ResetColor();
                    classBarbaro.MostrarAtributos();

                    CustomizarAvatar();

                    break;

                case 4:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Classe escolhida: Arqueiro, esta classe utiliza Arco.");
                    Console.ResetColor();
                    Arqueiro classArqueiro = new Arqueiro();

                    nomeClasseEscolhida = "Arqueiro";
                    ClassePersonagem arqueiro = new ClassePersonagem("Arqueiro");
                    arqueiro.AdicionarFerramentaBatalha("Arco");
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"A sua ferramenta de batalha será: {arqueiro.FerramentasBatalha[0]}");
                    ferramentaBatalhaEscolhida = arqueiro.FerramentasBatalha[0];
                    Console.WriteLine();
                    classArqueiro.MostrarAtributos();

                    CustomizarAvatar();
                    break;

                default:
                    Console.WriteLine("Escolha inválida.");
                    break;

                   
            }
        }
        private string EscolherArma(List<string> armasDisponiveis, string nomeClasseEscolhida)
        {
            int escolhaArma = 0;
            ConsoleColor originalColor = Console.ForegroundColor; 

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Classe escolhida: {nomeClasseEscolhida}, esta classe permite que você escolha entre duas armas.");
                Console.WriteLine();
                Console.WriteLine("Por favor, selecione uma das seguintes opções:");


                for (int i = 0; i < armasDisponiveis.Count; i++)
                {
                    if (escolhaArma == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"> {i + 1}) {armasDisponiveis[i]} <");
                    }
                    else
                    {
                        Console.ForegroundColor = originalColor;
                        Console.WriteLine($"  {i + 1}) {armasDisponiveis[i]}");
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    escolhaArma--;
                    if (escolhaArma < 0) escolhaArma = armasDisponiveis.Count - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    escolhaArma++;
                    if (escolhaArma >= armasDisponiveis.Count) escolhaArma = 0;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break; 
                }
            }

            string armaEscolhida = armasDisponiveis[escolhaArma];
            Console.ForegroundColor = originalColor;
            return armaEscolhida;
        }
        private string ExibirInformacoesClasse(string classe)
        {
            switch (classe)
            {
                case "Paladino":
                    Paladino paladino = new Paladino();
                    return $"[Lança ou Escudo] - Vida = {paladino.Vida}, Mana = {paladino.Mana}, Ataque = {paladino.DanoAtaque}, Velocidade de Ataque = {paladino.VelocidadeAtaque.ToString(CultureInfo.InvariantCulture)}, Defesa = {paladino.Defesa}";

                case "Atirador":
                    Atirador atirador = new Atirador();
                    return $" [Arma] - Vida = {atirador.Vida}, Stamina = {atirador.Stamina}, Ataque = {atirador.DanoAtaque}, Velocidade de Ataque = {atirador.VelocidadeAtaque.ToString(CultureInfo.InvariantCulture)}, Defesa = {atirador.Defesa}";

                case "Guerreiro":
                    Guerreiro guerreiro = new Guerreiro();
                    return $"[Espada e Escudo] - Vida = {guerreiro.Vida}, Stamina = {guerreiro.Stamina}, Ataque = {guerreiro.DanoAtaque}, Velocidade de Ataque = {guerreiro.VelocidadeAtaque.ToString(CultureInfo.InvariantCulture)}, Defesa = {guerreiro.Defesa}";

                case "Bárbaro":
                    Barbaro barbaro = new Barbaro();
                    return $"[Machado ou Marreta] - Vida = {barbaro.Vida}, Stamina = {barbaro.Stamina}, Ataque = {barbaro.DanoAtaque}, Velocidade de Ataque = {barbaro.VelocidadeAtaque.ToString(CultureInfo.InvariantCulture)}, Defesa = {barbaro.Defesa}";

                case "Arqueiro":
                    Arqueiro arqueiro = new Arqueiro();
                    return $" [Arco] - Vida = {arqueiro.Vida}, Stamina = {arqueiro.Stamina}, Ataque = {arqueiro.DanoAtaque}, Velocidade de Ataque = {arqueiro.VelocidadeAtaque.ToString(CultureInfo.InvariantCulture)}, Defesa = {arqueiro.Defesa}";

                default:
                    return "Informações adicionais não disponíveis.";
            }
        }
        public bool IsNumericInput(string input)
        {
            return int.TryParse(input, out _);
        }
        public void EscolherMontaria()
        {
            int escolhaMontaria = 0;

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Escolha sua montaria:");
                Console.ResetColor();
                Console.WriteLine();

                for (int i = 0; i < montariasDisponiveis.Count; i++)
                {
                    string additionalInfo = ExibirInformacoesDasMontarias(montariasDisponiveis[i]);

                    if (escolhaMontaria == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{i + 1}) {montariasDisponiveis[i].Nome,-20}");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(additionalInfo);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine($"  {i + 1}) {montariasDisponiveis[i].Nome}");

                    }
                }
                for (int i = 0; i < 15; i++)
                {
                    Console.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("DICA: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Utilize as setas do teclado cima \u2191 e baixo \u2193 para navegar pelas opções disponíveis. ");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    escolhaMontaria--;
                    if (escolhaMontaria < 0) escolhaMontaria = montariasDisponiveis.Count - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    escolhaMontaria++;
                    if (escolhaMontaria >= montariasDisponiveis.Count) escolhaMontaria = 0;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break; 
                }
            }
            Montaria montariaEscolhida = montariasDisponiveis[escolhaMontaria];
            this.montariaEscolhida = montariaEscolhida;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Montaria {montariaEscolhida.Nome} escolhida com sucesso!");
            Console.WriteLine();
            Console.WriteLine("Finalizando cadastro FasterQuest");
            for (int i = 5; i > 0; i--)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("... " + i);
                Console.ResetColor();
                Thread.Sleep(500);                  
            }

            Console.Clear();
            MostrarInformacoesEscolhidas();
        }
        private string ExibirInformacoesDasMontarias(Montaria montaria)
        {
            switch (montaria.Nome)
            {

                case "Panda":
                    return $"Vida: {montaria.Vida}, Velocidade: {montaria.Velocidade}, Stamina: {montaria.Stamina}, Bonus: {montaria.Bonus}";

                case "Cavalo":
                    return $"Vida: {montaria.Vida}, Velocidade: {montaria.Velocidade}, Stamina: {montaria.Stamina}, Bonus: {montaria.Bonus}";

                case "Tigre":
                    return $"Vida: {montaria.Vida}, Velocidade: {montaria.Velocidade}, Stamina: {montaria.Stamina}, Bonus: {montaria.Bonus}";

                case "Lobo":
                    return $"Vida: {montaria.Vida}, Velocidade: {montaria.Velocidade}, Stamina: {montaria.Stamina}, Bonus: {montaria.Bonus}";

                case "Rinoceronte":
                    return $"Vida: {montaria.Vida}, Velocidade: {montaria.Velocidade}, Stamina: {montaria.Stamina}, Bonus: {montaria.Bonus}";

                default:
                    return "Informações adicionais não disponíveis.";
            }
        }
        public void CustomizarAvatar()
        {
            CustomizacaoAvatar customizacao = new CustomizacaoAvatar();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Cadastre as características físicas do seu personagem:");
            Console.ResetColor();
            Console.WriteLine();

            do
            {
                Console.Write("Nome do Personagem: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                customizacao.NomePersonagem = Console.ReadLine();
                Console.ResetColor();

                if (string.IsNullOrWhiteSpace(customizacao.NomePersonagem) || customizacao.NomePersonagem.Length < 4 || !customizacao.NomePersonagem.All(char.IsLetter))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nome do personagem: obrigatório, mínimo 4 caracteres compostos exclusivamente por letras");
                    Console.ResetColor();
                }
            } while (string.IsNullOrWhiteSpace(customizacao.NomePersonagem) || customizacao.NomePersonagem.Length < 4 || !customizacao.NomePersonagem.All(char.IsLetter));

            Console.Write("Gênero do personagem (Opcional): ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            customizacao.Genero = Console.ReadLine();
            Console.ResetColor();

            if (!string.IsNullOrWhiteSpace(customizacao.Genero) && !customizacao.Genero.All(char.IsLetter))
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Gênero do personagem deve conter letras e sem uso de espaço.");
                    Console.ResetColor();

                    Console.Write("Gênero do personagem (opcional, apenas letras): ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    customizacao.Genero = Console.ReadLine();
                    Console.ResetColor();
                } while (!customizacao.Genero.All(char.IsLetter));
            }

            if (string.IsNullOrWhiteSpace(customizacao.CorCabelo) || customizacao.CorCabelo.Length < 3 || !customizacao.CorCabelo.All(char.IsLetter))
            {
                do
                {
                    Console.Write("Cor do cabelo: ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    customizacao.CorCabelo = Console.ReadLine();
                    Console.ResetColor();

                    if (string.IsNullOrWhiteSpace(customizacao.CorCabelo) || customizacao.CorCabelo.Length < 3 || !customizacao.CorCabelo.All(char.IsLetter))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Cor do cabelo: obrigatório, mínimo de 3 caracteres compostos exclusivamente por letras");
                        Console.ResetColor();
                    }
                } while (string.IsNullOrWhiteSpace(customizacao.CorCabelo) || customizacao.CorCabelo.Length < 3 || !customizacao.CorCabelo.All(char.IsLetter));
            }

            if (string.IsNullOrWhiteSpace(customizacao.CorPele) || customizacao.CorPele.Length < 3 || !customizacao.CorPele.All(char.IsLetter))
            {
                do
                {
                    Console.Write("Cor da pele: ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    customizacao.CorPele = Console.ReadLine();
                    Console.ResetColor();

                    if (string.IsNullOrWhiteSpace(customizacao.CorPele) || customizacao.CorPele.Length < 3 || !customizacao.CorPele.All(char.IsLetter))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Cor da pele: obrigatório, mínimo de 3 caracteres compostos exclusivamente por letras.");
                        Console.ResetColor();
                    }
                } while (string.IsNullOrWhiteSpace(customizacao.CorPele) || customizacao.CorPele.Length < 3 || !customizacao.CorPele.All(char.IsLetter));
            }

            customizacaoAvatarEscolhida = customizacao;

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Classe customizada com sucesso!");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Resumo das características físicas do personagem:");
            Console.ResetColor();
            Console.WriteLine($"Nome do personagem: {customizacao.NomePersonagem}");
            if (!string.IsNullOrWhiteSpace(customizacao.Genero))
            {
                Console.WriteLine($"Gênero do personagem: {customizacao.Genero}");
            }
            Console.WriteLine($"Cor do cabelo: {customizacao.CorCabelo}");
            Console.WriteLine($"Cor da pele: {customizacao.CorPele}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Direcionando para a tela de seleção de montaria");
            Console.ResetColor();
            for (int i = 5; i > 0; i--)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("... " + i);
                Console.ResetColor();
                Thread.Sleep(200);                 
            }

            EscolherMontaria();
        }
        public void MostrarInformacoesEscolhidas()

        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Parabéns, você finalizou seu cadastro no FasterQuest!");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Abaixo seguem as informações de customização da sua conta:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Classe Escolhida: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(nomeClasseEscolhida);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Status do personagem: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ExibirInformacoesClasse(nomeClasseEscolhida));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Arma de Batalha Selecionada: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{ferramentaBatalhaEscolhida}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Nome do Personagem: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{customizacaoAvatarEscolhida.NomePersonagem}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Gênero do Personagem: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{customizacaoAvatarEscolhida.Genero}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Cor do Cabelo: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{customizacaoAvatarEscolhida.CorCabelo}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Cor da Pele: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{customizacaoAvatarEscolhida.CorPele}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Montaria Escolhida: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{montariaEscolhida.Nome}");
            Console.ResetColor();
            montariaEscolhida.MostrarAtributos();
            Console.ResetColor();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine();
            }
        }

    }
    class CustomizacaoAvatar
    {
        public string NomePersonagem { get; set; }
        public string Genero { get; set; }
        public string CorCabelo { get; set; }
        public string CorPele { get; set; }

    }
    class ClassePersonagem
    {
        public string Nome { get; set; }
        public List<string> Armas { get; set; }
        public List<string> FerramentasBatalha { get; set; }

        public ClassePersonagem(string nome)
        {
            Nome = nome;
            Armas = new List<string>();
            FerramentasBatalha = new List<string>();
        }

        public void AdicionarFerramentaBatalha(string ferramenta)
        {
            FerramentasBatalha.Add(ferramenta);
        }
    }
    class Montaria
    {
        public string Nome { get; set; }
        public int Velocidade { get; set; }
        public int Stamina { get; set; }
        public int Vida { get; set; }
        public string Bonus { get; set; }

        public Montaria(string nome, int velocidade, int stamina, int vida, string bonus = "")
        {
            Nome = nome;
            Velocidade = velocidade;
            Stamina = stamina;
            Vida = vida;
            Bonus = bonus;
        }

        public void MostrarAtributos()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Atributos da montaria {Nome}: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Vida = {Vida}, Stamina = {Stamina}, Velocidade = {Velocidade}, Bonus = {Bonus}");
        }
    }
    class Paladino
    {
        public int Vida { get; set; }
        public int Mana { get; set; }
        public int DanoAtaque { get; set; }
        public double VelocidadeAtaque { get; set; }
        public int Defesa { get; set; }

        public Paladino()
        {

            Vida = 85;
            Mana = 50;
            DanoAtaque = 25;
            VelocidadeAtaque = 1.10;
            Defesa = 55;
        }

        public void MostrarAtributos()
        {

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Atributos do Paladino: ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Vida: {Vida}, Mana: {Mana}, Ataque: {DanoAtaque}, Velocidade de ataque: {VelocidadeAtaque.ToString(CultureInfo.InvariantCulture)}, Defesa: {Defesa}");
            Console.WriteLine();
        }

    }
    class Atirador
    {
        public int Vida { get; set; }
        public int Stamina { get; set; }
        public int DanoAtaque { get; set; }
        public double VelocidadeAtaque { get; set; }
        public int Defesa { get; set; }

        public Atirador()
        {
            Vida = 60;
            Stamina = 30;
            DanoAtaque = 35;
            VelocidadeAtaque = 1.25;
            Defesa = 15;
        }

        public void MostrarAtributos()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Atributos do Atirador:");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Vida: {Vida}, Stamina: {Stamina}, Ataque: {DanoAtaque}, Velocidade de ataque: {VelocidadeAtaque}, Defesa: {Defesa} ");
            Console.WriteLine();
        }
    }
    class Guerreiro
    {
        public int Vida { get; set; }
        public int Stamina { get; set; }
        public int DanoAtaque { get; set; }
        public double VelocidadeAtaque { get; set; }
        public int Defesa { get; set; }

        public Guerreiro()
        {
            Vida = 90;
            Stamina = 45;
            DanoAtaque = 30;
            VelocidadeAtaque = 1.20;
            Defesa = 50;
        }

        public void MostrarAtributos()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Atributos do Guerreiro: ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Vida: {Vida}, Stamina: {Stamina}, Ataque:{DanoAtaque},Velocidade de ataque: {VelocidadeAtaque}, Defesa: {Defesa}");
            Console.WriteLine();
        }
    }
    class Barbaro
    {
        public int Vida { get; set; }
        public int Stamina { get; set; }
        public int DanoAtaque { get; set; }
        public double VelocidadeAtaque { get; set; }
        public int Defesa { get; set; }

        public Barbaro()
        {
            Vida = 75;
            Stamina = 40;
            DanoAtaque = 40;
            VelocidadeAtaque = 1.15;
            Defesa = 20;
        }

        public void MostrarAtributos()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Atributos do Bárbaro: ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Vida: {Vida}, Stamina: {Stamina}, Ataque: {DanoAtaque}, Velocidade de ataque: {VelocidadeAtaque}, Defesa: {Defesa} ");
            Console.WriteLine();
        }
    }
    class Arqueiro
    {
        public int Vida { get; set; }
        public int Stamina { get; set; }
        public int DanoAtaque { get; set; }
        public double VelocidadeAtaque { get; set; }
        public int Defesa { get; set; }

        public Arqueiro()
        {
            Vida = 65;
            Stamina = 30;
            DanoAtaque = 45;
            VelocidadeAtaque = 1.35;
            Defesa = 10;
        }

        public void MostrarAtributos()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Atributos do Arqueiro: ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Vida: {Vida}, Stamina: {Stamina}, Ataque: {DanoAtaque}, Velocidade de ataque: {VelocidadeAtaque},Defesa: {Defesa}  ");
            Console.WriteLine();
        }
    }
}

class Usuario
{
    public string NomeCompleto { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }


    private static List<Usuario> usuariosCadastrados = new List<Usuario>();

    public static Usuario BuscarUsuarioPorEmail(string email)
    {
        return usuariosCadastrados.Find(u => u.Email == email);
    }

    public void RealizarCadastro()
    {
        usuariosCadastrados.Add(this);
    }
}


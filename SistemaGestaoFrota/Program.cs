using System;
using System.Collections.Generic;

namespace SistemaGestaoFrota
{
    class Program
    {
        static List<Veiculo> frota = new List<Veiculo>();

        static void Main(string[] args)
        {
            int opcao = -1;

            while (opcao != 0)
            {
                Console.Clear();

                Console.WriteLine("==================================");
                Console.WriteLine("     SISTEMA DE GESTÃO DE FROTA");
                Console.WriteLine("==================================");

                Console.WriteLine("1 - Adicionar veículo");
                Console.WriteLine("2 - Consultar veículos");
                Console.WriteLine("3 - Abastecer veículo");
                Console.WriteLine("4 - Realizar viagem");
                Console.WriteLine("0 - Sair");

                Console.Write("\nEscolhe uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            AdicionarVeiculo();
                            break;

                        case 2:
                            ConsultarVeiculos();
                            break;

                        case 3:
                            AbastecerVeiculo();
                            break;

                        case 4:
                            RealizarViagem();
                            break;

                        case 0:
                            Console.WriteLine("A terminar o programa...");
                            break;

                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }
                else
                {
                    opcao = -1;
                    Console.WriteLine("Introduz um número válido!");
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressiona uma tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        // Adicionar veículo
        static void AdicionarVeiculo()
        {
            string matricula;
            int capacidade;

            Console.Write("\nIntroduz a matrícula: ");
            matricula = Console.ReadLine();

            Console.Write("Capacidade do depósito: ");

            if (int.TryParse(Console.ReadLine(), out capacidade))
            {
                if (matricula != "" && capacidade > 0)
                {
                    Veiculo novoVeiculo = new Veiculo(matricula, capacidade);

                    novoVeiculo.ReservaAtingida += AvisarReserva;

                    frota.Add(novoVeiculo);

                    Console.WriteLine("Veículo adicionado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Dados inválidos!");
                }
            }
            else
            {
                Console.WriteLine("Capacidade inválida!");
            }
        }

        // Consultar veículos
        static void ConsultarVeiculos()
        {
            Console.WriteLine("\n--- VEÍCULOS REGISTADOS ---");

            if (frota.Count == 0)
            {
                Console.WriteLine("Não existem veículos registados.");
            }
            else
            {
                foreach (Veiculo veiculo in frota)
                {
                    Console.WriteLine("\nMatrícula: " + veiculo.Matricula);
                    Console.WriteLine("Quilometragem: " + veiculo.Quilometragem + " km");
                    Console.WriteLine("Combustível: " + veiculo.NivelCombustivel.ToString("F2") + " litros");
                    Console.WriteLine("Capacidade: " + veiculo.CapacidadeDeposito + " litros");
                }
            }
        }

        // Abastecer veículo
        static void AbastecerVeiculo()
        {
            Veiculo veiculo = SelecionarVeiculo();

            if (veiculo != null)
            {
                Console.Write("Quantos litros pretendes abastecer? ");

                if (double.TryParse(Console.ReadLine(), out double litros))
                {
                    veiculo.Abastecer(litros);
                }
                else
                {
                    Console.WriteLine("Quantidade inválida!");
                }
            }
        }

        // Realizar viagem
        static void RealizarViagem()
        {
            Veiculo veiculo = SelecionarVeiculo();

            if (veiculo != null)
            {
                Console.Write("Qual a distância da viagem em km? ");

                if (int.TryParse(Console.ReadLine(), out int distancia))
                {
                    veiculo.RealizarViagem(distancia);
                }
                else
                {
                    Console.WriteLine("Distância inválida!");
                }
            }
        }

        // Selecionar veículo
        static Veiculo SelecionarVeiculo()
        {
            string matricula;

            if (frota.Count == 0)
            {
                Console.WriteLine("Não existem veículos registados.");
                return null;
            }

            Console.Write("\nIntroduz a matrícula do veículo: ");
            matricula = Console.ReadLine();

            foreach (Veiculo veiculo in frota)
            {
                if (veiculo.Matricula == matricula)
                {
                    return veiculo;
                }
            }

            Console.WriteLine("Veículo não encontrado!");
            return null;
        }

        // Evento de alerta
        static void AvisarReserva(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("\n==================================");
            Console.WriteLine("      ALERTA DE COMBUSTÍVEL!");
            Console.WriteLine("   O veículo está na reserva!");
            Console.WriteLine("==================================");

            Console.ResetColor();
        }
    }
}

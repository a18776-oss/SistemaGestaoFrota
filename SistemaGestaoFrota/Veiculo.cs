using System;

namespace SistemaGestaoFrota
{
    public class Veiculo
    {
        // Atributos privados
        private int _quilometragem;
        private double _nivelCombustivel;

        // Propriedades públicas
        public string Matricula { get; set; }
        public int CapacidadeDeposito { get; set; }

        // Evento
        public event EventHandler ReservaAtingida;

        // Propriedade Quilometragem
        public int Quilometragem
        {
            get
            {
                return _quilometragem;
            }
            set
            {
                if (value >= _quilometragem)
                {
                    _quilometragem = value;
                }
                else
                {
                    Console.WriteLine("Erro: A quilometragem não pode diminuir!");
                }
            }
        }

        // Propriedade NivelCombustivel
        public double NivelCombustivel
        {
            get
            {
                return _nivelCombustivel;
            }
            set
            {
                if (value >= 0 && value <= CapacidadeDeposito)
                {
                    _nivelCombustivel = value;

                    if (_nivelCombustivel < CapacidadeDeposito * 0.10)
                    {
                        ReservaAtingida?.Invoke(this, EventArgs.Empty);
                    }
                }
                else
                {
                    Console.WriteLine("Erro: Nível de combustível inválido!");
                }
            }
        }

        // Construtor
        public Veiculo(string matricula, int capacidadeDeposito)
        {
            Matricula = matricula;
            CapacidadeDeposito = capacidadeDeposito;

            _quilometragem = 0;
            _nivelCombustivel = capacidadeDeposito;
        }

        // Método Abastecer
        public void Abastecer(double litros)
        {
            if (litros <= 0)
            {
                Console.WriteLine("Erro: Introduz uma quantidade válida!");
            }
            else
            {
                if (NivelCombustivel + litros > CapacidadeDeposito)
                {
                    Console.WriteLine("Aviso: O depósito ficou cheio!");
                    NivelCombustivel = CapacidadeDeposito;
                }
                else
                {
                    NivelCombustivel = NivelCombustivel + litros;
                    Console.WriteLine("Abastecimento realizado com sucesso!");
                }
            }
        }

        // Método RealizarViagem
        public bool RealizarViagem(int kmDistancia)
        {
            double combustivelNecessario;

            if (kmDistancia <= 0)
            {
                Console.WriteLine("Erro: Distância inválida!");
                return false;
            }

            combustivelNecessario = kmDistancia * 0.07;

            if (NivelCombustivel >= combustivelNecessario)
            {
                NivelCombustivel = NivelCombustivel - combustivelNecessario;
                Quilometragem = Quilometragem + kmDistancia;

                Console.WriteLine("Viagem realizada com sucesso!");
                Console.WriteLine("Combustível consumido: " + combustivelNecessario + " litros");

                return true;
            }
            else
            {
                Console.WriteLine("Combustível insuficiente!");
                return false;
            }
        }
    }
}
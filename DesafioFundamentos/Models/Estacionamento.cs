using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private readonly decimal precoInicial;
        private readonly decimal precoPorHora;
        private readonly List<string> veiculos;

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
            veiculos = new List<string>();
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Informe a placa do veículo para estacionar (formatos aceitos: ABC1234 ou ABC1B23):");
            string placa = Console.ReadLine()?.Trim().ToUpper();

            if (string.IsNullOrEmpty(placa) || !ValidarPlaca(placa))
            {
                Console.WriteLine("Placa inválida! Certifique-se de usar um formato correto (ex.: ABC1234 ou ABC1B23).");
                return;
            }

            if (veiculos.Contains(placa))
            {
                Console.WriteLine("Esse veículo já está estacionado.");
            }
            else
            {
                veiculos.Add(placa);
                string placaFormatada = FormatarPlaca(placa);
                Console.WriteLine($"Veículo com placa {placaFormatada} foi estacionado com sucesso.");
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Informe a placa do veículo que deseja remover (formatos aceitos: ABC1234 ou ABC1B23):");
            string placa = Console.ReadLine()?.Trim().ToUpper();

            if (string.IsNullOrEmpty(placa) || !ValidarPlaca(placa))
            {
                Console.WriteLine("Placa inválida! Certifique-se de usar um formato correto (ex.: ABC1234 ou ABC1B23).");
                return;
            }

            if (veiculos.Remove(placa))
            {
                Console.WriteLine("Informe a quantidade de horas que o veículo permaneceu estacionado:");
                if (int.TryParse(Console.ReadLine(), out int horas) && horas > 0)
                {
                    decimal valorTotal = CalcularValor(horas);
                    string placaFormatada = FormatarPlaca(placa);
                    Console.WriteLine($"O veículo {placaFormatada} foi removido. Total a pagar: R$ {valorTotal:F2}");
                }
                else
                {
                    Console.WriteLine("Quantidade de horas inválida!");
                }
            }
            else
            {
                Console.WriteLine("Veículo não encontrado. Verifique se a placa foi digitada corretamente.");
            }
        }

        public void ListarVeiculos()
        {
            if (veiculos.Count == 0)
            {
                Console.WriteLine("Nenhum veículo estacionado no momento.");
                return;
            }

            Console.WriteLine("Veículos estacionados:");
            foreach (var placa in veiculos)
            {
                Console.WriteLine($"- {FormatarPlaca(placa)}");
            }
        }

        private static bool ValidarPlaca(string placa)
        {
            // Valida placas nos formatos ABC1234 e ABC1B23
            return Regex.IsMatch(placa, @"^[A-Z]{3}\d{4}$") || Regex.IsMatch(placa, @"^[A-Z]{3}\d[A-Z]\d{2}$");
        }

        public static string FormatarPlaca(string placa)
        {
            if (Regex.IsMatch(placa, @"^[A-Z]{3}\d{4}$"))
            {
                // Formato ABC1234 -> ABC-1234
                return placa.Insert(3, "-");
            }
            else if (Regex.IsMatch(placa, @"^[A-Z]{3}\d[A-Z]\d{2}$"))
            {
                // Formato ABC1B23 -> ABC-1B23
                return placa.Insert(3, "-");
            }

            throw new ArgumentException("Placa em formato inválido.");
        }

        private decimal CalcularValor(int horas)
        {
            return precoInicial + (precoPorHora * horas);
        }
    }
}

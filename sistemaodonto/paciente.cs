using System;

namespace sistemaodonto
{
    public class Paciente
    {
        public string CPF { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }

        public Paciente(string cpf, string nome, DateTime dataNascimento)
        {
            CPF = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
        }

        public int CalcularIdade()
        {
            int idade = DateTime.Now.Year - DataNascimento.Year;
            if (DateTime.Now.DayOfYear < DataNascimento.DayOfYear)
                idade--;
            return idade;
        }

        public bool ValidarCPF()
        {
            // Lógica de validação de CPF (exemplo simplificado)
            return CPF.Length == 11;
        }
    }
}

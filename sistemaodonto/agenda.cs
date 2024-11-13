using System;
using System.Collections.Generic;
using System.Linq;

namespace sistemaodonto
{
    public class Agenda
    {
        private List<Paciente> pacientes = new List<Paciente>();
        private List<Consulta> consultas = new List<Consulta>();

        public void CadastrarPaciente(Paciente paciente)
        {
            if (!pacientes.Any(p => p.CPF == paciente.CPF))
            {
                pacientes.Add(paciente);
                Console.WriteLine("Paciente cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro: CPF já cadastrado");
            }
        }

        public void ExcluirPaciente(string cpf)
        {
            var paciente = pacientes.FirstOrDefault(p => p.CPF == cpf);
            if (paciente != null)
            {
                if (!consultas.Any(c => c.CPFDoPaciente == cpf && c.Data > DateTime.Now))
                {
                    pacientes.Remove(paciente);
                    consultas.RemoveAll(c => c.CPFDoPaciente == cpf);
                    Console.WriteLine("Paciente excluído com sucesso!");
                }
                else
                {
                    Console.WriteLine("Erro: paciente está agendado.");
                }
            }
            else
            {
                Console.WriteLine("Erro: paciente não cadastrado.");
            }
        }

        public void AgendarConsulta(Consulta consulta)
        {
            var paciente = pacientes.FirstOrDefault(p => p.CPF == consulta.CPFDoPaciente);
            if (paciente == null)
            {
                Console.WriteLine("Erro: paciente não cadastrado");
                return;
            }

            if (consulta.Data < DateTime.Now.Date ||
               (consulta.Data == DateTime.Now.Date && consulta.HoraInicio < DateTime.Now.TimeOfDay))
            {
                Console.WriteLine("Erro: a consulta deve ser para uma data futura");
                return;
            }

            if (consulta.HoraFim <= consulta.HoraInicio)
            {
                Console.WriteLine("Erro: Hora final deve ser maior que a hora inicial");
                return;
            }

            if (consultas.Any(c => c.CPFDoPaciente == consulta.CPFDoPaciente && c.Data > DateTime.Now))
            {
                Console.WriteLine("Erro: paciente já tem uma consulta futura agendada.");
                return;
            }

            if (consultas.Any(c => c.VerificarConflitoDeHorario(consulta)))
            {
                Console.WriteLine("Erro: já existe uma consulta agendada nesse horário");
                return;
            }

            consultas.Add(consulta);
            Console.WriteLine("Agendamento realizado com sucesso!");
        }

        public void CancelarAgendamento(string cpf, DateTime data, TimeSpan horaInicio)
        {
            var consulta = consultas.FirstOrDefault(c => c.CPFDoPaciente == cpf && c.Data == data && c.HoraInicio == horaInicio);

            if (consulta != null && (consulta.Data > DateTime.Now.Date || 
                (consulta.Data == DateTime.Now.Date && consulta.HoraInicio > DateTime.Now.TimeOfDay)))
            {
                consultas.Remove(consulta);
                Console.WriteLine("Agendamento cancelado com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro: agendamento não encontrado ou já passado.");
            }
        }

        public void ListarPacientes(string ordem)
        {
            var lista = ordem == "CPF" ? pacientes.OrderBy(p => p.CPF) : pacientes.OrderBy(p => p.Nome);
            Console.WriteLine("CPF      Nome                     Dt.Nasc  Idade");

            foreach (var p in lista)
            {
                Console.WriteLine($"{p.CPF} {p.Nome,-25} {p.DataNascimento:dd/MM/yyyy} {p.CalcularIdade()}");
                var consultaFutura = consultas.FirstOrDefault(c => c.CPFDoPaciente == p.CPF && c.Data > DateTime.Now);
                if (consultaFutura != null)
                {
                    Console.WriteLine($"Agendado para: {consultaFutura.Data:dd/MM/yyyy}");
                    Console.WriteLine($"{consultaFutura.HoraInicio:hh\\:mm} às {consultaFutura.HoraFim:hh\\:mm}");
                }
            }
        }

        public void ListarAgenda()
        {
            var lista = consultas.OrderBy(c => c.Data).ThenBy(c => c.HoraInicio);
            Console.WriteLine("Data       H.Ini  H.Fim  Tempo  Nome                     Dt.Nasc");

            foreach (var c in lista)
            {
                var paciente = pacientes.FirstOrDefault(p => p.CPF == c.CPFDoPaciente);
                Console.WriteLine($"{c.Data:dd/MM/yyyy} {c.HoraInicio:hh\\:mm} {c.HoraFim:hh\\:mm} {(c.HoraFim - c.HoraInicio):hh\\:mm} {paciente.Nome,-25} {paciente.DataNascimento:dd/MM/yyyy}");
            }
        }

        public void ListarAgenda(DateTime dataInicial, DateTime dataFinal)
        {
            var lista = consultas
                .Where(c => c.Data >= dataInicial && c.Data <= dataFinal)
                .OrderBy(c => c.Data)
                .ThenBy(c => c.HoraInicio);

            foreach (var c in lista)
            {
                var paciente = pacientes.FirstOrDefault(p => p.CPF == c.CPFDoPaciente);
                Console.WriteLine($"{c.Data:dd/MM/yyyy} {c.HoraInicio:hh\\:mm} {c.HoraFim:hh\\:mm} {(c.HoraFim - c.HoraInicio):hh\\:mm} {paciente.Nome,-25} {paciente.DataNascimento:dd/MM/yyyy}");
            }
        }
    }
}

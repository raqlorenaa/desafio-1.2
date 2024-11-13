using System;

namespace sistemaodonto
{
    class ConsultorioApp
    {
        static Agenda agenda = new Agenda();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Menu Principal");
                Console.WriteLine("1-Cadastro de pacientes");
                Console.WriteLine("2-Agenda");
                Console.WriteLine("3-Fim");
                Console.Write("Escolha uma opção: ");
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        MenuCadastroPacientes();
                        break;
                    case 2:
                        MenuAgenda();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }

        static void MenuCadastroPacientes()
        {
            Console.WriteLine("1-Cadastrar novo paciente");
            Console.WriteLine("2-Excluir paciente");
            Console.WriteLine("3-Listar pacientes (ordenado por CPF)");
            Console.WriteLine("4-Listar pacientes (ordenado por nome)");
            Console.WriteLine("5-Voltar p/ menu principal");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    CadastrarNovoPaciente();
                    break;
                case 2:
                    ExcluirPaciente();
                    break;
                case 3:
                    agenda.ListarPacientes("CPF");
                    break;
                case 4:
                    agenda.ListarPacientes("Nome");
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }

        static void CadastrarNovoPaciente()
        {
            string cpf;
            while (true)
            {
                Console.Write("CPF: ");
                cpf = Console.ReadLine();
                if (CPFValidator.ValidarCPF(cpf))
                {
                    break;
                }
                Console.WriteLine("Erro: CPF inválido. Tente novamente.");
            }

            string nome;
            while (true)
            {
                Console.Write("Nome: ");
                nome = Console.ReadLine();
                if (nome.Length >= 5)
                {
                    break;
                }
                Console.WriteLine("Erro: O nome deve ter pelo menos 5 caracteres. Tente novamente.");
            }

            DateTime dataNascimento;
            while (true)
            {
                Console.Write("Data de nascimento (DDMMAAAA): ");
                string dataInput = Console.ReadLine();
                if (DateTime.TryParseExact(dataInput, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out dataNascimento))
                {
                    Paciente novoPaciente = new Paciente(cpf, nome, dataNascimento);
                    if (novoPaciente.CalcularIdade() >= 13)
                    {
                        agenda.CadastrarPaciente(novoPaciente);
                        Console.WriteLine("Paciente cadastrado com sucesso.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Erro: O paciente precisa ter pelo menos 13 anos. Tente novamente.");
                    }
                }
                else
                {
                    Console.WriteLine("Erro: Data de nascimento inválida. Tente novamente.");
                }
            }
        }

        static void ExcluirPaciente()
        {
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();
            agenda.ExcluirPaciente(cpf);
        }

        static void MenuAgenda()
        {
            Console.WriteLine("1-Agendar consulta");
            Console.WriteLine("2-Cancelar agendamento");
            Console.WriteLine("3-Listar agenda");
            Console.WriteLine("4-Voltar p/ menu principal");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    AgendarConsulta();
                    break;
                case 2:
                    CancelarAgendamento();
                    break;
                case 3:
                    ListarAgenda();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }

                static void AgendarConsulta()
        {
            Console.Write("CPF do paciente: ");
            string cpf = Console.ReadLine();

            Console.Write("Data da consulta (DDMMAAAA): ");
            DateTime dataConsulta = DateTime.ParseExact(Console.ReadLine(), "ddMMyyyy", null);

            TimeSpan horaInicio;
            while (true)
            {
                Console.Write("Hora inicial (HHMM): ");
                string horaInicioInput = Console.ReadLine();
                horaInicio = TimeSpan.ParseExact(horaInicioInput, "hhmm", null);

                if (HoraValida(horaInicio) && DentroHorarioFuncionamento(horaInicio))
                    break;

                Console.WriteLine("Erro: Hora inicial inválida. Deve estar no intervalo de 15 minutos e entre 08:00 e 19:00. Tente novamente.");
            }

            TimeSpan horaFim;
            while (true)
            {
                Console.Write("Hora final (HHMM): ");
                string horaFimInput = Console.ReadLine();
                horaFim = TimeSpan.ParseExact(horaFimInput, "hhmm", null);

                if (HoraValida(horaFim) && DentroHorarioFuncionamento(horaFim) && horaFim > horaInicio)
                    break;

                Console.WriteLine("Erro: Hora final inválida. Deve estar no intervalo de 15 minutos, entre 08:00 e 19:00 e ser maior que a hora inicial. Tente novamente.");
            }

            Consulta consulta = new Consulta(cpf, dataConsulta, horaInicio, horaFim);
            agenda.AgendarConsulta(consulta);
        }

        // Método para verificar se a hora está nos intervalos de 15 minutos
        static bool HoraValida(TimeSpan hora)
        {
            return hora.Minutes % 15 == 0;
        }

        // Método para verificar se a hora está dentro do horário de funcionamento (8:00 - 19:00)
        static bool DentroHorarioFuncionamento(TimeSpan hora)
        {
            TimeSpan horarioAbertura = new TimeSpan(8, 0, 0); 
            TimeSpan horarioFechamento = new TimeSpan(19, 0, 0); 
            return hora >= horarioAbertura && hora <= horarioFechamento;
        }


        static void CancelarAgendamento()
        {
            Console.Write("CPF do paciente: ");
            string cpf = Console.ReadLine();
            Console.Write("Data da consulta (DDMMAAAA): ");
            DateTime dataConsulta = DateTime.ParseExact(Console.ReadLine(), "ddMMyyyy", null);
            Console.Write("Hora inicial (HHMM): ");
            TimeSpan horaInicio = TimeSpan.ParseExact(Console.ReadLine(), "hhmm", null);

            agenda.CancelarAgendamento(cpf, dataConsulta, horaInicio);
        }

        static void ListarAgenda()
        {
            Console.Write("Apresentar a agenda T-Toda ou P-Periodo: ");
            char tipo = Console.ReadLine().ToUpper()[0];

            if (tipo == 'T')
            {
                agenda.ListarAgenda();
            }
            else if (tipo == 'P')
            {
                Console.Write("Data inicial (DDMMAAAA): ");
                DateTime dataInicial = DateTime.ParseExact(Console.ReadLine(), "ddMMyyyy", null);
                Console.Write("Data final (DDMMAAAA): ");
                DateTime dataFinal = DateTime.ParseExact(Console.ReadLine(), "ddMMyyyy", null);
                agenda.ListarAgenda(dataInicial, dataFinal);
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }
        }
    }
}

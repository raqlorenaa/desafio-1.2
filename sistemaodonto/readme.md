
Estrutura do Sistema

Classes Principais
-ConsultorioApp: Classe principal responsável por gerenciar a interação com o usuário e navegar entre as funcionalidades do sistema.
-Agenda: Responsável por armazenar e gerenciar a lista de pacientes e a agenda de consultas.
-Paciente: Representa os dados de um paciente, como CPF, nome e data de nascimento.
-Consulta: Representa os dados de uma consulta, incluindo CPF do paciente, data, hora de início e hora de fim.
-CPFValidator: Utilitário para validação de CPF, assegurando que os números inseridos estejam em conformidade com o formato válido de CPF no Brasil.

Separação de Responsabilidades
1. ConsultorioApp: Ponto de Entrada e Interface do Usuário
Responsabilidade: Gerencia a navegação entre os menus e interage diretamente com o usuário. Ela chama métodos das classes Agenda, Paciente e Consulta, mas não manipula diretamente os dados.
Métodos:
Main(): Exibe o menu principal e direciona o usuário para as opções de cadastro de pacientes, agenda ou para finalizar o programa.
MenuCadastroPacientes(): Exibe o menu de opções de gerenciamento de pacientes e chama métodos correspondentes da classe Agenda.
MenuAgenda(): Exibe o menu de gerenciamento de agenda, permitindo ao usuário agendar consultas, cancelar e listar a agenda.
2. Agenda: Gerenciamento de Pacientes e Consultas
Responsabilidade: Mantém uma lista de pacientes e consultas e realiza operações como cadastrar, excluir pacientes, e gerenciar agendamentos.
Métodos:
CadastrarPaciente(Paciente paciente): Adiciona um novo paciente à lista, após validações de CPF e idade mínima de 13 anos.
ExcluirPaciente(string cpf): Remove um paciente da lista com base no CPF.
ListarPacientes(string ordenacao): Lista pacientes ordenados por CPF ou nome.
AgendarConsulta(Consulta consulta): Insere uma consulta na lista de agendamentos.
CancelarAgendamento(string cpf, DateTime data, TimeSpan horaInicio): Cancela uma consulta específica de acordo com o CPF, data e hora de início.
ListarAgenda(): Lista todas as consultas agendadas.
ListarAgenda(DateTime dataInicial, DateTime dataFinal): Lista consultas dentro de um período específico.
3. Paciente: Dados do Paciente
Responsabilidade: Armazena e valida os dados individuais do paciente.
Propriedades:
CPF: Armazena o CPF do paciente.
Nome: Armazena o nome do paciente.
DataNascimento: Armazena a data de nascimento do paciente.
Métodos:
CalcularIdade(): Calcula a idade do paciente com base na data de nascimento.
4. Consulta: Dados da Consulta
Responsabilidade: Armazena e valida os dados de uma consulta.
Propriedades:
CPF: CPF do paciente associado à consulta.
DataConsulta: Data da consulta.
HoraInicio: Hora de início da consulta.
HoraFim: Hora de término da consulta.
5. CPFValidator: Validação do CPF
Responsabilidade: Realiza validação do CPF para garantir que o valor informado atende aos critérios de CPF brasileiro válido.
Métodos:
ValidarCPF(string cpf): Verifica a validade do CPF, retornando true para CPF válido e false para CPF inválido.


using System;

namespace sistemaodonto
{
    public class Consulta
    {
        public string CPFDoPaciente { get; private set; }
        public DateTime Data { get; private set; }
        public TimeSpan HoraInicio { get; private set; }
        public TimeSpan HoraFim { get; private set; }

        public Consulta(string cpfPaciente, DateTime data, TimeSpan horaInicio, TimeSpan horaFim)
        {
            CPFDoPaciente = cpfPaciente;
            Data = data;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }

        public bool VerificarConflitoDeHorario(Consulta outraConsulta)
        {
            return Data == outraConsulta.Data &&
                   HoraInicio < outraConsulta.HoraFim &&
                   HoraFim > outraConsulta.HoraInicio;
        }
    }
}

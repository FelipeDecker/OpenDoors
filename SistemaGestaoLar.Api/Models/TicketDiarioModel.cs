using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SistemaGestaoLar.Api.Entities;
using SistemaGestaoLar.Api.Models;

namespace SistemaGestaoLar.Api.Models
{
    public class TicketDiarioModel
    {
        public TicketDiarioModel() { }

        public TicketDiarioModel(TicketDiario entidade)
        {
            Id = entidade.Id;
            MoradorId = entidade.MoradorId;
            DataServico = entidade.DataServico;
            if (entidade.Servicos != null)
            {
                Servicos = new List<ServicoStatusModel>();
                foreach (var s in entidade.Servicos)
                {
                    Servicos.Add(new ServicoStatusModel(s));
                }
            }
        }

        public int Id { get; set; }

        public int MoradorId { get; set; }

        public DateOnly DataServico { get; set; }

        [Required]
        public List<ServicoStatusModel> Servicos { get; set; }
    }
}

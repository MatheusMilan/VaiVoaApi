using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaiVoaApi.Model
{
    public class Cartao
    {
        public Cartao(long numeroCartao, DateTime criacao)
        {
            NumeroCartao = numeroCartao;
            Criacao = criacao;
        }

        public int Id { get; set; }
        public long NumeroCartao { get; set; }
        public DateTime Criacao { get; set; }
    }
}

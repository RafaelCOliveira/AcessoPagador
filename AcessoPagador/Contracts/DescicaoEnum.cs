using System;
using System.ComponentModel;
using System.Reflection;
using AcessoPagador.Contracts.Enumerat;

namespace AcessoPagador.Contracts
{
    public static class DescicaoEnum
    {
        public static string GetDescription(string tipo, int valor)
        {
            string descricao = "";
            if (tipo == "StatusTransacaoEnum")
            {
                System.Enum.GetName(typeof(StatusTransacaoEnum), valor);
                StatusTransacaoEnum en = (StatusTransacaoEnum)valor;

                FieldInfo fi = en.GetType().GetField(en.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                    //pega pelo atributo description
                    descricao = attributes[0].Description;
                else
                    //pega pelo tipo do enum
                    descricao = System.Enum.GetName(typeof(StatusTransacaoEnum), valor);
            }
            return descricao;


        }
    }
}

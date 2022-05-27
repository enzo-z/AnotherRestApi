using Newtonsoft.Json;
using System.Text;
using WebApiTest.Controllers.DTOs;

namespace WebApiTest.Controllers.Helpers
{
    public static class CustomJsonSerializer
    {
        public static string ToJson(this IEnumerable<ReadFilmeDto> readFilmeDtos)
        {
            var sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                writer.WriteStartArray();

                foreach (var filme in readFilmeDtos)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("id");
                    writer.WriteValue(filme.Id);
                    writer.WritePropertyName("titulo");
                    writer.WriteValue(filme.Titulo);
                    writer.WritePropertyName("diretor");
                    writer.WriteValue(filme.Diretor);
                    writer.WritePropertyName("genero");
                    writer.WriteValue(filme.Genero);
                    writer.WritePropertyName("duracao");
                    writer.WriteValue(filme.Duracao);
                    writer.WritePropertyName("horarioConsulta");
                    writer.WriteValue(filme.HorarioConsulta);
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();

                return sw.ToString();
            }
        }
    }
}

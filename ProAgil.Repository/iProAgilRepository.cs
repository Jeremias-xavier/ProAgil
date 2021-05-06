using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
         void Add<T>(T entity) where T : class;

         void Update<T>(T entity) where T : class;

         void Delete<T>(T entity) where T : class;

         Task<bool> SaveChangesAsync();

         //EVENTOS
         Task<Evento[]> GetAllEventoAsyncByTema(string Tema, bool includePalestrante);

         Task<Evento[]> GetAllEventoAsync(bool includePalestrante);

         Task<Evento> GetEventoAsyncByID(int EventoId, bool includePalestrante);
         
          Task<Palestrante[]> GetAllPalestranteAsyncByName(string Nome, bool includeEvento);

         Task<Palestrante> GetPalestranteAsync(int PalestranteId, bool includeEvento);

    }
}
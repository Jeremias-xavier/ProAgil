using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : iProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrante)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);

            }
            query = query.OrderByDescending(c => c.DataEvento);
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string Tema, bool includePalestrante)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrante)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);

            }
            query = query.OrderByDescending(c => c.DataEvento)
            .Where(c => c.Tema.Contains(Tema));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string Nome, bool includeEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c => c.RedesSociais);

            if(includeEvento)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Evento);

            }
            query = query.OrderBy(c => c.Nome)
            .Where(c => c.Nome.Contains(Nome));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncByID(int EventoId, bool includePalestrante)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrante)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);

            }
            query = query.OrderByDescending(c => c.DataEvento)
            .Where(c => c.ID == EventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante> GetPalestranteAsync(int PalestranteId, bool includeEvento)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c => c.RedesSociais);

            if(includeEvento)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Evento);

            }
            query = query.OrderBy(c => c.Nome)
            .Where(c => c.ID == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(_context);
        }
    }
}
using MyComicList.Application.Commands.Genres;
using MyComicList.Application.DataTransfer.Genres;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.EFCommands.Genres
{
    public class EFUpdateGenre : EFBaseCommand, IUpdateGenre
    {
        public EFUpdateGenre(MyComicListContext context) : base(context)
        {
        }

        public void Execute(GenreDTO request)
        {
            var genre = Context.Genres
                .Where(g => g.Id == request.Id && g.DeletedAt == null)
                .FirstOrDefault();

            if (genre == null) throw new EntityNotFoundException("Genre", request.Id);

            if(genre.Name != request.Name)
            {
                if (Context.Genres.Where(g => g.DeletedAt == null).Any(g => g.Name == request.Name))
                {
                    throw new EntityAlreadyExistsException("Genre", request.Name);
                }

                genre.Name = request.Name;
                genre.UpdatedAt = DateTime.Now;

                Context.SaveChanges();
            }
            
        }
    }
}

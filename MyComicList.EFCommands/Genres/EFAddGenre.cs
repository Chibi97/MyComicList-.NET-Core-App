using MyComicList.Application.Commands.Genres;
using MyComicList.Application.DataTransfer.Genres;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;
using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.EFCommands.Genres
{
    public class EFAddGenre : EFBaseCommand, IAddGenre
    {
        public EFAddGenre(MyComicListContext context) : base(context)
        {
        }

        public void Execute(GenreAddDTO request)
        {
            if(Context.Genres.Where(g => g.DeletedAt == null).Any(c => c.Name == request.Name))
            {
                throw new EntityAlreadyExistsException("Name", request.Name);
            }

            Genre newGenre = new Genre
            {
                Name = request.Name.Trim()
            };

            Context.Genres.Add(newGenre);

            Context.SaveChanges();
            
        }
    }
}

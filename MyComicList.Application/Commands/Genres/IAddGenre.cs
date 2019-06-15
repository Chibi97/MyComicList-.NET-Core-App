using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Commands.Genres
{
    public interface IAddGenre : ICommand<GenreAddDTO>
    {
        
    }
}

using MyComicList.Application.Commands.Comics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Commands.Genres
{
    public interface IDeleteGenre : ICommand<int>
    {
        
    }
}

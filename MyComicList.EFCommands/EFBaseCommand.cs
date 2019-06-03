using MyComicList.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.EFCommands
{
    public abstract class EFBaseCommand
    {
        protected MyComicListContext Context { get; }

        protected EFBaseCommand(MyComicListContext context) => Context = context;
    }
}

using MyComicList.Domain;

namespace MyComicList.Shared.Services
{
    public interface ILoginService
    {
        User PossibleUser();
        void Login(int id);
    }
}

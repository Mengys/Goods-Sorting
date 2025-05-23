using R3;

namespace _Project.Code.Services.ApplicationLifecycle
{
    public class AppLifeCycleEvents
    {
        public Observable<Unit> Paused { get; }
        public Observable<Unit> Focused { get; }
        public Observable<Unit> Quit { get; }

        public AppLifeCycleEvents(
            Observable<Unit> paused,
            Observable<Unit> focused,
            Observable<Unit> quit)
        {
            Paused = paused;
            Focused = focused;
            Quit = quit;
        }
    }
}
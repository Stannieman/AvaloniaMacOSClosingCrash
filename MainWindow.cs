using Avalonia.Controls;
using System.Threading.Tasks;

namespace CrashRepro
{
    internal class MainWindow : Window
    {
        private bool canClose = false;

        public MainWindow()
        {
            Closing += async (_, e) =>
            {
                if (canClose)
                {
                    return;
                }

                e.Cancel = true;

                // A particular MVVM framework calls an async CanClose function on the view model here.
                // This await causes an crash on macOS.
                await Task.Yield();

                canClose = true;
                Close();
            };
        }
    }
}

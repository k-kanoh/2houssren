using MahApps.Metro.Controls;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace _2houssren
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.AddRange(MyCommands.BindingMyCommands());

            DragOver += (_, e) =>
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effects = DragDropEffects.Move;
            };

            Drop += (_, e) =>
            {
                if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                    return;

                var drop = (string[])e.Data.GetData(DataFormats.FileDrop);
                var output = Path.Combine(Path.GetDirectoryName(drop.First()), "Ren.bat");

                int i = 0;
                var ni = new StringBuilder();
                foreach (var file in drop.OrderBy(x => x))
                {
                    if (Path.GetExtension(file) != ".bmp")
                        continue;

                    var src = Path.GetFileName(file);
                    var dest = Path.GetFileNameWithoutExtension(file);
                    dest = Regex.Match(dest, @"^(th[\d_]*)\d{3}").Groups[1].Value;

                    ni.AppendLine($"ren {src} {dest}{i++:000}{Path.GetExtension(file)}");
                }

                if (ni.Length > 0)
                    File.WriteAllText(output, ni.ToString());
            };
        }
    }
}

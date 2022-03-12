using GongSolutions.Wpf.DragDrop;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace _2houssren
{
    internal class ViewModel : INotifyPropertyChanged, IDropTarget
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void DragEnter(IDropInfo dropInfo)
        { }

        public void DragLeave(IDropInfo dropInfo)
        { }

        public void DragOver(IDropInfo dropInfo)
        {
            var drop = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();

            if (drop.Any())
                dropInfo.Effects = DragDropEffects.Move;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var drop = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();
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
        }
    }
}

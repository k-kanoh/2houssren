using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace _2houssren
{
    internal class MyCommands
    {
        public static RoutedCommand ApplicationExecute { get; } = new RoutedCommand(nameof(ApplicationExecute), typeof(Window));

        public static IList BindingMyCommands()
        {
            return new List<CommandBinding>()
            {
                new CommandBinding(ApplicationExecute, (_, e) =>
                {
                    var path = e.Parameter.ToString();
                    if (File.Exists(path))
                        Process.Start(new ProcessStartInfo() { FileName = path });
                })
            };
        }
    }
}

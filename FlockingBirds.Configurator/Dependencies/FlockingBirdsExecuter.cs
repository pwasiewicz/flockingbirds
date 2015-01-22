namespace FlockingBirds.Configurator.Dependencies
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    public class FlockingBirdsExecuter : IFlockingBirdsExecuter
    {
        private const string ProgramPath = "FlockingBirds.exe";

        private delegate string GetFlockingHelpDelegate();

        public void Execute(params FlockingBirdsArgument[] arguments)
        {
            var args = this.BuildArguments(arguments);

            this.ExecuteFlockingBirds(args);
        }

        public string GenerateExecutableArguments(params FlockingBirdsArgument[] arguments)
        {
            return this.BuildArguments(arguments);
        }

        public void GetFlockingBirdHelp(Action<IAsyncResult> callback)
        {
            Func<string> func = new Func<string>(this.GetFlockingBirdHelp);
            func.BeginInvoke(new AsyncCallback(callback), func);

        }

        public string GetFlockingBirdHelp()
        {
            var output = string.Empty;

            var procStartInfo = new ProcessStartInfo
            {
                Arguments = "-help",
                CreateNoWindow = true,
                FileName = this.BuildGamePath(),
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var proc = Process.Start(procStartInfo);

            output = proc.StandardOutput.ReadToEnd();

            proc.WaitForExit();
            proc.Close();

            return output;
        }

        private string BuildArguments(IEnumerable<FlockingBirdsArgument> arguments)
        {
            var argsBuilder = new StringBuilder();

            foreach (var flockingBirdsArgument in arguments)
            {
                argsBuilder.Append(flockingBirdsArgument.ArgumentValue);
                argsBuilder.Append(" ");
            }

            return argsBuilder.ToString();
        }

        private void ExecuteFlockingBirds(string arguments)
        {
            var fileName = this.BuildGamePath();

            var procStartInfo = new ProcessStartInfo
            {
                Arguments = arguments,
                CreateNoWindow = false,
                FileName = fileName,
                UseShellExecute = true
            };

            var proc = Process.Start(procStartInfo);

            proc.WaitForExit();
            proc.Close();
        }

        private string BuildGamePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ProgramPath);
        }
    }
}

using System;
using System.IO;
using System.Runtime.InteropServices;
using FlockingBirds.Configurator.Properties;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace FlockingBirds.Configurator.Dependencies
{
    using System.Collections.Generic;
    using System.Reflection;
    using ViewModels;

    public class FlockingArgumentsSettingsStore : IFlockingArgumentsSettingsStore
    {
        private const string SettingsFileFilter = "json file|*.json";

        private readonly Settings settings;

        private readonly string defaultUserSettingsPath;

        private readonly ICompressingService compressingService;

        public FlockingArgumentsSettingsStore(ICompressingService compressingService)
        {
            this.compressingService = compressingService;
            this.settings = Settings.Default;
            this.defaultUserSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public void StoreDefault(IFlockingArgumentsViewModel flockingArguments)
        {
            if (flockingArguments == null)
            {
                throw new ArgumentNullException("flockingArguments");
            }

            this.settings.DefaultFlockingArguments = this.SerializeFlockingArguments(flockingArguments);
            this.settings.Save();
        }

        public void LoadDefault(IFlockingArgumentsViewModel flockingArguments)
        {
            if (flockingArguments == null)
            {
                throw new ArgumentNullException("flockingArguments");
            }

            var serializedSettings = this.settings.DefaultFlockingArguments;

            this.DeserializeAndAssign(flockingArguments, serializedSettings);
        }

        public void StoreUser(IFlockingArgumentsViewModel flockingArguments)
        {
            if (flockingArguments == null)
            {
                throw new ArgumentNullException("flockingArguments");
            }

            var arguments = this.SerializeFlockingArguments(flockingArguments);

            arguments = this.compressingService.Compress(arguments);

            this.StoreUserArgumentsInFile(arguments);
        }

        public void LoadUser(IFlockingArgumentsViewModel flockingArguments)
        {
            if (flockingArguments == null)
            {
                throw new ArgumentNullException("flockingArguments");
            }

            var content = this.LoadFile();

            content = this.compressingService.Decompress(content);

            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            this.DeserializeAndAssign(flockingArguments, content);
        }

        public void ResetAll()
        {
            this.settings.DefaultFlockingArguments = null;
        }

        private string LoadFile()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = "json",
                Filter = SettingsFileFilter,
                CheckFileExists = true
            };

            var result = dialog.ShowDialog();

            if (result == null || !result.Value)
            {
                return null;
            }

            return File.ReadAllText(dialog.FileName);
        }

        private void DeserializeAndAssign(IFlockingArgumentsViewModel flockingArguments, string serializedSettings)
        {
            if (string.IsNullOrEmpty(serializedSettings))
            {
                return;
            }

            var wrapper = JsonConvert.DeserializeObject<FlockingArguemntsSettingsWrapper>(serializedSettings);
            wrapper.AssignTo(flockingArguments);
        }

        private void StoreUserArgumentsInFile(string fileContent)
        {
            var saveDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "json",
                Filter = SettingsFileFilter,
                InitialDirectory = this.defaultUserSettingsPath
            };

            var dgResult = saveDialog.ShowDialog();

            if (dgResult.HasValue && dgResult.Value)
            {
                this.SaveStringToFile(saveDialog.FileName, fileContent);
            }
        }

        private void SaveStringToFile(string fileName, string fileContent)
        {
            File.WriteAllText(fileName, fileContent);
        }

        private string SerializeFlockingArguments(IFlockingArgumentsViewModel flockingArguments)
        {
            var wrapper = new FlockingArguemntsSettingsWrapper();
            wrapper.AssignFrom(flockingArguments);

            return JsonConvert.SerializeObject(wrapper);
        }

        internal class FlockingArguemntsSettingsWrapper : IFlockingArgumentsViewModel
        {
            internal void AssignFrom(IFlockingArgumentsViewModel model)
            {
                foreach (var flockingArgumentsProperty in this.GetFlockingArgumentsProperties())
                {
                    var value = flockingArgumentsProperty.GetValue(model);

                    flockingArgumentsProperty.SetValue(this, value);
                }
            }

            internal void AssignTo(IFlockingArgumentsViewModel model)
            {
                foreach (var flockingArgumentsProperty in this.GetFlockingArgumentsProperties())
                {
                    var value = flockingArgumentsProperty.GetValue(this);

                    flockingArgumentsProperty.SetValue(model, value);
                }
            }

            private IEnumerable<PropertyInfo> GetFlockingArgumentsProperties()
            {
                var type = typeof (IFlockingArgumentsViewModel);

                return type.GetProperties();
            }

            public bool FullScreenMode { get; set; }
            public bool RunAtStart { get; set; }
            public int Groups { get; set; }
            public int BirdsCount { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int VisibilityDistance { get; set; }
            public float MaxBirdSpeed { get; set; }
            public float BirdsSeparation { get; set; }
            public float BirdsAlignment { get; set; }
            public float BirdsCohesion { get; set; }
            public bool MouseInteraction { get; set; }
            public float BirdMaxSteer { get; set; }


            public float WindDirection
            {
                get;
                set;
            }

            public float WindPower
            {
                get;
                set;
            }
        }
    }
}

// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace MrBoom
{
    public class NameGenerator
    {
        private readonly Random random;


        public NameGenerator(Random random)
        {
            this.random = random;
            names = new List<string>(); // temporary empty list
        }

        public string GenerateName(int index)
        {
            
            var ret = names[index];
            
            return ret;
        }

        public async Task InitializeAsync()
        {
            names = await LoadNamesAsync();
        }

        public async Task<List<string>> LoadNamesAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            List<string> names = new List<string>();

            try
            {
                await GetNamesFromFile(localFolder, names);
            }
            catch (Exception)
            {
                await SaveNamesAsync();
                await GetNamesFromFile(localFolder, names);
            }

            return names;
        }

        private static async Task GetNamesFromFile(StorageFolder localFolder, List<string> names)
        {
            StorageFile file = await localFolder.GetFileAsync("names.txt");
            var lines = await FileIO.ReadLinesAsync(file);
            names.AddRange(lines);
        }

        public async Task SaveNamesAsync()
        {
            // Get the app's local folder
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            // Create or replace the file
            StorageFile file = await localFolder.CreateFileAsync("names.txt", CreationCollisionOption.ReplaceExisting);

            List<string> names = new List<string>
            {
                "ola", "ala", "gre", "srv", "zuk", "jer", "ami", "mar"
            };

            // Write all names, each on a new line
            await FileIO.WriteLinesAsync(file, names);
        }
    }
}
